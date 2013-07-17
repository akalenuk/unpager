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

namespace WindowsFormsApplication1 {
    class Matrix {
        static double[,] mul(double[,] A, double[,] B) {
            int AH = A.GetLength(0);
            int AW = A.GetLength(1);
            int BH = B.GetLength(0);
            int BW = B.GetLength(1);
            Debug.Assert(AW == BH);
            double[,] C = new double[AH, BW];
            for (int i = 0; i < AH; i++) {
                for (int j = 0; j < BW; j++) {
                    for (int k = 0; k < AW; k++) {
                        C[i, j] += A[i, k] * B[k, j];
                    }
                }
            }
            return C;
        }

        static double[] mul(double[] a, double[,] B) {
            double[,] A = new double[1, a.Length];
            for (int i = 0; i < a.Length; i++) {
                A[0, i] = a[i];
            }
            double[,] mres = mul(A, B);
            int vres_dimm = mres.GetLength(1);
            double[] vres = new double[vres_dimm];
            for (int i = 0; i < vres_dimm; i++) {
                vres[i] = mres[0, i];
            }
            return vres;
        }

        static double[] mul(double[,] A, double[] b) {
            return mul(b, trans(A));
        }

        static double[,] trans(double[,] A) {
            int AH = A.GetLength(0);
            int AW = A.GetLength(1);
            double[,] C = new double[AW, AH];
            for (int i = 0; i < AW; i++) {
                for (int j = 0; j < AH; j++) {
                    C[i, j] = A[j, i];
                }
            }
            return C;
        }

        static bool equals(double[,] A, double[,] B) {
            int AH = A.GetLength(0);
            int AW = A.GetLength(1);
            int BH = B.GetLength(0);
            int BW = B.GetLength(1);
            if (AH != BH || AW != BW) {
                return false;
            }
            for (int i = 0; i < AH; i++) {
                for (int j = 0; j < AW; j++) {
                    if (A[i, j] != B[i, j]) {
                        return false;
                    }
                }
            }
            return true;
        }

        // Warning! This may generate NaNs in special position. Use some kind of jitter to generalize it.
        static public double[,] make_projection(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4){            
            double s1 = y1 * x2 - x1 * y2;
            double s2 = y1 * x4 - x1 * y4;
            double s3 = x3 * x2 * x1 + x3 * x4 * x1 - x2 * x4 * x1 - x3 * x2 * x4;
            double s4 = y3 * y2 * y1 + y3 * y4 * y1 - y2 * y4 * y1 - y3 * y2 * y4;
            double a3 = x2 * x4 - x3 * x4;
            double b3 = x2 * x4 - x3 * x2;
            double d4 = y2 * y4 - y3 * y4;
            double e4 = y2 * y4 - y3 * y2;

            double dB = ((y2 * b3) * (x4 * d4) - (x2 * a3) * (y4 * e4));
            double B = (((x4 * d4) * (y2 * s3 - s1 * a3) - (x2 * a3) * (s2 * e4 + s4 * x4))) / dB;

            double dD = ((x2 * a3) * (y4 * e4) - (y2 * b3) * (x4 * d4));
            double D = (((y4 * e4) * (y2 * s3 - s1 * a3) - (y2 * b3) * (s2 * e4 + s4 * x4))) / dD;

            double A = (s3 - b3 * B) / a3;
            double E = (s4 - d4 * D) / e4;

            double a = (A + x1 - x2) / x2;
            double b = (B + x1 - x4) / x4;

            double C = x1;
            double F = y1;
            double c = 1.0;

            double[,] M = new double[3, 3] {{A, D, a}, {B, E, b}, {C, F, c}};

            return M;
        }

        static public double[,] make_inverse_to_projection(double [,] M) { // this is not the same as inverse! This one is faster, but works only with projections.
            Debug.Assert(M.GetLength(0) == 3);
            Debug.Assert(M.GetLength(1) == 3);
            double A = M[0, 0];
            double B = M[1, 0];
            double C = M[2, 0];
            double D = M[0, 1];
            double E = M[1, 1];
            double F = M[2, 1];
            double a = M[0, 2];
            double b = M[1, 2];
            double c = M[2, 2];

            double A_ = E*c - b*F;
            double B_ = b*C - B*c;
            double C_ = B*F - E*C;

            double D_ = a*F - D*c;
            double E_ = A*c - a*C;
            double F_ = C*D - A*F;

            double a_ = b*D - a*E;
            double b_ = B*a - A*b;
            double c_ = A*E - B*D;

            double[,] M_ = new double[3, 3] { { A_, D_, a_ }, { B_, E_, b_ }, { C_, F_, c_ } };

            return M_;
        }

        static public void test(){
            double[,] A = new double[2, 3] { {1.0, 2.0, 3.0}, {4.0, 5.0, 6.0}};
            double[,] B = new double[3, 2] { {7.0, 8.0}, {9.0, 10.0}, {11.0, 12.0}};
            double[,] test = new double[2, 2] { {58.0, 64.0}, {139.0, 154.0} };
            double[,] res = mul(A, B);
            Debug.Assert(equals(res, test));    // matrix-matrix multiplication test
            double[] c = new double[] { 3.0, 4.0, 2.0 };
            double[,] D = new double[3, 4] { { 13.0, 9.0, 7.0, 15.0 }, { 8.0, 7.0, 4.0, 6.0 }, { 6.0, 4.0, 0.0, 3.0 } };
            double[] test2 = new double[] { 83.0, 63.0, 37.0, 75.0 };
            double[] res2 = mul(c, D);
            Debug.Assert(Vector.equal(res2, test2));    // vector-matrix multiplication
            double[,] E = new double[4, 3] { {1.0, 2.0, 3.0},  {4.0, 5.0, 6.0}, {7.0, 8.0, 9.0}, {10.0, 11.0, 12.0}};
            double[] f = new double[3] { -2.0, 1.0, 0.0 };
            double[] test3 = new double[4] { 0.0, -3.0, -6.0, -9.0 };
            double[] res3 = mul(E, f);
            Debug.Assert(Vector.equal(res3, test3));    // matrix-vector multiplication
            double[] point_test = new double[] {4.0, 5.0, 1.0};
            double[,] M_proj_test = make_projection(0.0, 0.0,  100.0, 10.0,  200.0, 200.0,  300.0, 10.0);
            double[] proj_point_test = mul(point_test, M_proj_test);
            double[,] M_inv_proj_test = make_inverse_to_projection(M_proj_test);
            double[] inv_proj_point_test = mul(proj_point_test, M_inv_proj_test);
            double[] point_res = new double[3] { inv_proj_point_test[0] / inv_proj_point_test[2], inv_proj_point_test[1] / inv_proj_point_test[2], 1.0 };
            Debug.Assert(Vector.semi_equal(point_res, point_test)); // projection and inverse to projection test
        }
    }
}
