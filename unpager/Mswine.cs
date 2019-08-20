using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WindowsFormsApplication1 {
    class Mswine {

        public delegate double WeightFunction(double x);
        public delegate double BasisFunction(double[] x);

        /* Inverse linear weight function */
        public static double s_l(double x) {
            return 1.0 / x;
        }

        /* Inverse quadratic weight function */
        public static double s_q(double x) {
            return 1.0 / (x*x);
        }

        /* Weight function for 'vx' vector
            Args:
                vx: vector to weight.
                s_k: scalar weight function.
            Returns:
                Weight of the vector
        */
        public static double v_k(double[] vx, WeightFunction s_x) {
            int DIMM = vx.Length;
            Debug.Assert(DIMM > 0);
            double r = s_x(vx[0]);
            for(int i = 1; i < DIMM; i++){
                r *= s_x(vx[i]);
            }
            return r;
        }

        /*
        Determines a list of constant basis functions
        Args:
            xyz: Point set.
            f: Corresponding array of function values.
            Sx: List of simplexes 
        Returns:
            List of basis functions
        */
        public static BasisFunction[] get_constant_functions(double[][] xyz, double[] f, int[][] Sx) {
            int f_len = f.Length;
            BasisFunction[] ret = new BasisFunction[f_len];
            for (int i = 0; i < f_len; i++) {
                double yi = f[i];
                ret[i] = x => yi;
            }
            return ret;
        }

        /*
        Determines a list of linear basis functions
        Args:
            xyz: Point set.
            f: Corresponding array of function values.
            Sx: List of simplexes 
        Returns:
            List of basis functions
        */
        public static BasisFunction[] get_linear_functions(double[][] xyz, double[] f, int[][] Sx) {
            int f_len = f.Length;
            Debug.Assert(xyz.Length == f_len && f_len > 0);
            BasisFunction[] ret = new BasisFunction[f_len];
            int dimm = xyz[0].Length;
            double[][] simplex_linears = new double[Sx.Length][];            
            for (int i = 0; i < Sx.Length; i++) {
                simplex_linears[i] = new double[dimm + 1];
            }
            double[][] point_linears = new double[xyz.Length][];
            for (int i = 0; i < xyz.Length; i++) {
                point_linears[i] = new double[dimm + 1];
            }
            for (int i = 0; i < Sx.Length; i++) {
                double[,] A = new double[dimm + 1, dimm + 1];
                double[] B = new double[dimm + 1];
                for (int j = 0; j < dimm + 1; j++) {
                    int pnt = Sx[i][j] - 1;
                    for (int k = 0; k < dimm; k++) {
                        A[j,k] = xyz[pnt][k];
                    }
                    A[j,dimm] = 1.0;
                    B[j] = f[pnt];
                }
                simplex_linears[i] = Vector.Gauss(A, B);
            }

            for (int i = 0; i < xyz.Length; i++) {
                int sx_N = 0;
                for (int j = 0; j < Sx.Length; j++) {
                    for (int k = 0; k < dimm + 1; k++) {
                        if (Sx[j][k] == i + 1) {
                            sx_N += 1;
                            for (int l = 0; l < dimm + 1; l++) {
                                point_linears[i][l] += simplex_linears[j][l];
                            }
                            break;
                        }
                    }
                }
                Debug.Assert(sx_N != 0);
                for (int j = 0; j < point_linears[i].Length; j++) {
                    point_linears[i][j] /= sx_N;
                }
            }
            for (int i = 0; i < f_len; i++) {
                double[] this_linears = (double[])point_linears[i].Clone();
                ret[i] = dot => {                    
                    double t = this_linears[dimm];
                    for (int j = 0; j < dimm; j++) {
                        t += this_linears[j] * dot[j];
                    }
                    return t;
                };                    
            }
            return ret;
        }

        /*
        Simplicial weighted interpolation.

        Args:
        dot: Argument for interpolation function provided by a list of variables
        xyz: Data points.
        Sx: List of simplexes, represeting simplicial complex
        base_f: Corresponding to 'xyz' list of basic functions.
        s_k: Scalar weight function.

        Returns:
        Value of interpolation function.
        */
        public static double F_s(double[] dot, double[][] xyz, int[][] Sx, BasisFunction[] base_f, WeightFunction s_k) {
            int DIMM = dot.Length;
            for (int sx = 0; sx < Sx.Length; sx++) {
                double[] crd = new double[DIMM];
                if (Simplex.point_in_simplex(sx, dot, 0, xyz, Sx, out crd)) {
                    int[] pnt_set = new int[Sx[0].Length];
                    for (int j = 0; j < Sx[0].Length; j++) {
                        pnt_set[j] = Sx[sx][j] - 1;
                    }
                    return get_inS(dot, dot, pnt_set, xyz, Sx, base_f, s_k);
                }
            }
            return F_sex(dot, xyz, Sx, base_f, s_k);
        }

        /*
        Simplex weighted extrapolation

        Args:
        dot: Argument for interpolation function provided by a list of variables
        xyz: Data points.
        Sx: List of simplexes, represeting simplicial complex
        base_f: Corresponding to 'xyz' list of basic functions.
        s_k: Scalar weight function.

        Returns:
        Value of extrapolation function.
        */
        public static double F_sex(double[] dot, double[][] xyz, int[][] Sx, BasisFunction[] base_f, WeightFunction s_k) {
            Simplex.BestPack best_pack = new Simplex.BestPack();
            best_pack.point = (double[])xyz[0].Clone();
            best_pack.simplex = (int[])Sx[0].Clone();
            best_pack.len = 1.0e10;
            foreach (int[] sx in Sx) {
                for (int i = 0; i < sx.Length; i++) {
                    int[] c_sx = new int[sx.Length-1];
                    for(int j = 0; j < sx.Length-1; j++){
                        if(j<i){
                            c_sx[j] = sx[j];
                        }else{
                            c_sx[j] = sx[j+1];
                        }
                    }
                    best_pack = Simplex.get_nearest_simplex(dot, xyz, Sx, c_sx, best_pack);
                }
            }
            int[] pnt_set = new int[best_pack.simplex.Length];
            for(int i = 0; i < best_pack.simplex.Length; i++){
                pnt_set[i] = best_pack.simplex[i] - 1;
            }
            return get_inS(dot,best_pack.point,pnt_set, xyz,Sx,base_f,s_k);
        }

        /*
        Gets a simplex interpolated value in a subsimplex
         
        Args:
        dot: Argument of interpolation function.
        prj: Current level projection of a 'dot'.
        pnt_set: Point set representing current level simplex.
        xyz: Data point set.
        Sx: Simplicial complex.
        base_f: Corresponding to 'xyz' list of basic functions.
        s_k: Scalar weight function.

        Returns:
        Iterpolation value for 'dot' projection on a subsimplex
        */
        static double get_inS(double[] dot, double[] prj, int[] pnt_set, double[][] xyz, int[][] Sx, BasisFunction[] base_f, WeightFunction s_k) {
            int PSL = pnt_set.Length;
            Debug.Assert(PSL > 0);
            if (PSL == 1) {
                return base_f[pnt_set[0]](dot);
            } else {
                double Up = 0.0;
                double Down = 0.0;
                for (int i = 0; i < PSL; i++) { 
                    int[] new_pnt_set = new int[PSL-1];
                    double[][] new_S = new double[PSL-1][];
                    for(int j = 0; j < PSL-1; j++){
                        if(j<i){
                            new_pnt_set[j] = pnt_set[j];
                            new_S[j] = xyz[pnt_set[j]];
                        }else{
                            new_pnt_set[j] = pnt_set[j+1];
                            new_S[j] = xyz[pnt_set[j+1]];
                        }
                    }
                    double[] new_prj = Simplex.proj(prj, new_S);
                    double cur_k = s_k(Vector.len(Vector.sub(new_prj,prj)));
                    double up = get_inS(dot,new_prj,new_pnt_set, xyz,Sx,base_f,s_k);
                    Up += up * cur_k;
                    Down += cur_k;
                }
                return Up/Down;
            }
        }        
    }
}
