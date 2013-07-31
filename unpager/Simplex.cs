/*
 Copyright 2013 Alexandr Kalenuk.

 Licensed under the Apache License, Version 2.0 (the "License");
 you may not use this file except in compliance with the License.
 You may obtain a copy of the License at
 
   http://www.apache.org/licenses/LICENSE-2.0
 
 Unless required by applicable law or agreed to in writing, software
 distributed under the License is distributed on an "AS IS" BASIS,
 WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 See the License for the specific language governing permissions and
 limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WindowsFormsApplication1 {    // Bits of simplicial geometry
    class Simplex {
        /*
        Projection on a simplex plane.

        Args:
            a: Projecting point.
            S: Simplex given by a list of points.
            
        Returns:
            Point, which is an 'a' projection on 'S' simplex plane.
        */
        public static double[] proj(double[] a, double[][] S, bool check_if_in = false, double[] ret_if_not = null) {
            int DIMM = a.Length;
            foreach (double[] b in S) {
                Debug.Assert(b.Length == DIMM);
            }
            if (S.Length == 1) {        // projection on a point considered to be that point
                return S[0];
            } else if (S.Length == 2) { // projection on an edge
                double[] a0 = Vector.sub(a, S[0]);
                double[] v01 = Vector.sub(S[1], S[0]);
                double Ei = 0.0;
                double E = 0.0;
                for (int i = 0; i < DIMM; i++) {
                    Ei += v01[i] * v01[i];
                    E += a0[i] * v01[i];
                }
                double k = E / Ei;

                if (check_if_in) { 
                    if ( k < 0.0 || k > 1.0){
                        return ret_if_not;
                    }
                }

                return Vector.add(S[0], Vector.mul(k, v01));
            } else { // projection on a plane/hyperplane
                int N = S.Length - 1;
                double[] a0 = Vector.sub(a, S[0]);
                double[][] v0i = new double[N][];
                for (int i = 0; i < N; i++) {
                    v0i[i] = Vector.sub(S[i + 1], S[0]);
                }
                double[,] A = new double[N,N];
                double[] B = new double[N];
                for (int k = 0; k < DIMM; k++) {
                    for (int i = 0; i < N; i++) {
                        for (int j = 0; j < N; j++) {
                            A[i, j] += v0i[j][k] * v0i[i][k];
                        }
                        B[i] += a0[k] * v0i[i][k];
                    }
                }
                double[] I = Vector.Gauss(A, B);

                if (check_if_in) {  // I are the coordinates in simplicial basis. If any one goes out of [0, 1], the point is not on the simplexs parallelotop
                    double sum_I = 0.0;
                    foreach (double i in I) {
                        if (i < 0.0 || i > 1.0) {
                            return ret_if_not;
                        }
                        sum_I += i; 
                    }
                    if (sum_I > 1.0) {  // if sum of basis coordinates > 1.0 then point is not on the simplex
                        return ret_if_not;
                    }
                }
                double[] to_ret = new double[DIMM];
                to_ret = Vector.add( to_ret, S[0] );
                for(int i = 0; i < N; i++){
                    to_ret = Vector.add(to_ret, Vector.mul(I[i], v0i[i]));
                }
                return to_ret;
            }
        }


        /* Determines if a point is in simplex

            Args:
                dot: point coordinates in an a basis set by simplex 1-edges.
                sx: index for basic simplex in an array of simplexes 'Sx'.
                pnt: index of an origin point in simplex.
                xyz: list of points,
                Sx: list of point indexes, representing simplicial complex.
                crd: the return value for calculated coordinates.

            Returns:
                'True' if point is in a simplex, 'False' otherwise.
        */
        public static bool point_in_simplex(int sx, double[] dot, int pnt, double[][] xyz, int[][] Sx, out double[] crd) {
            int DIMM = dot.Length;
            double[,] A = new double[DIMM, DIMM];
            double[] B = new double[DIMM];

            crd = new double[DIMM];

            int cnt = 0;
            int p_pnt = Sx[sx][pnt] - 1;
            for (int i = 0; i < DIMM + 1; i++) {
                int p_i = Sx[sx][i] - 1;
                if (p_i != p_pnt) {
                    for (int j = 0; j < DIMM; j++) {
                        A[j,cnt] = xyz[p_i][j] - xyz[p_pnt][j];
                    }
                    cnt += 1;
                }
            }
            Debug.Assert(cnt == DIMM);
            for (int j = 0; j < DIMM; j++) {
                B[j] = dot[j] - xyz[p_pnt][j];
            }
            crd = Vector.Gauss(A, B);

            double sum = 0.0;
            for (int j = 0; j < DIMM; j++) {
                if (crd[j] < 0.0 || crd[j] > 1.0) {
                    return false;
                }
                sum += crd[j];
            }
            if (sum > 1) {
                return false;
            }
            return true;
        }
       

        /*
        Finds a simplex which is the nearest to a 'dot' point.
        Args:
            sx: A candiate simplex.
            xyz: List of all points forming simplicial complex.
            Sx: List of point indexes, representing simplicial complex.
            best_pack: Structure for passing found data recoursively.

        Returns:
            List, first element of which represents nearest simplex index. 
         */
        public class BestPack {
            public double len;
            public double[] vec1;
            public int[] vec2;
        };

        public static BestPack get_nearest_simplex(double[] dot, double[][] xyz, int[][] Sx, int[] sx, BestPack best_pack) {
            BestPack new_pack = new BestPack();
            new_pack.len = best_pack.len;
            new_pack.vec1 = (double[])best_pack.vec1.Clone();
            new_pack.vec2 = (int[])best_pack.vec2.Clone();

            double[][] new_S = new double[sx.Length][];
            for (int i = 0; i < sx.Length; i++) {
                new_S[i] = xyz[sx[i] - 1];
            }

            double[] bad_proj = new double[dot.Length];
            for (int i = 0; i < dot.Length; i++) {
                bad_proj[i] = 1.0e10;
            }
            
            double[] new_prj = proj(dot, new_S, true, bad_proj);
            double new_l = Vector.len(Vector.sub(new_prj, dot));
            if (new_l < best_pack.len) {
                best_pack.len = new_l;
                best_pack.vec1 = (double[])new_prj.Clone();
                best_pack.vec1 = (double[])sx.Clone();
            }

            if ( sx.Length > 1.0) {
                for (int i = 0; i < sx.Length; i++) {
                    int[] c_sx = new int[sx.Length-1];
                    int n = 0;
                    for(int j = 0; j<sx.Length;j++){
                        if( j!= i){
                            c_sx[n] = sx[j];
                        }
                        n++;
                    }
                    best_pack = get_nearest_simplex(dot, xyz, Sx, c_sx, best_pack);
                }
            }
            return best_pack;
        }
    }
}
