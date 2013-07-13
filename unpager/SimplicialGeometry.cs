using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace WindowsFormsApplication1 {
    class SimplicialGeometry {
        /*
        Projection on a simplex plane.

        Args:
            a: Projecting point.
            S: Simplex given by a list of points.
            
        Returns:
            Point, which is an 'a' projection on 'S' simplex plane.
        */
        double[] v_proj(double[] a, double[][] S, bool check_if_in = false, double[] ret_if_not = null) {
            int DIMM = a.Length;
            foreach (double[] b in S) {
                Debug.Assert(b.Length == DIMM);
            }
            if (S.Length == 1) {        // projection on a point considered to be that point
                return S[0];
            } else if (S.Length == 2) { // projection on an edge
                double[] a0 = LinearAlgebra.v_sub(a, S[0]);
                double[] v01 = LinearAlgebra.v_sub(S[1], S[0]);
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

                return LinearAlgebra.v_add(S[0], LinearAlgebra.s_mul(k, v01));
            } else { // projection on a plane/hyperplane
                int N = S.Length - 1;
                double[] a0 = LinearAlgebra.v_sub(a, S[0]);
                double[][] v0i = new double[N][];
                for (int i = 0; i < N; i++) {
                    v0i[i] = LinearAlgebra.v_sub(S[i + 1], S[0]);
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
                double[] I = LinearAlgebra.SolveSLAE_Gauss(A, B);

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
                to_ret = LinearAlgebra.v_add( to_ret, S[0] );
                for(int i = 0; i < N; i++){
                    to_ret = LinearAlgebra.v_add(to_ret, LinearAlgebra.s_mul(I[i], v0i[i]));
                }
                return to_ret;
            }
        }



    }
}
