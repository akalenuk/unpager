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

        static public void test(){
            double[,] A = new double[2, 3] { {1.0, 2.0, 3.0}, {4.0, 5.0, 6.0}};
            double[,] B = new double[3, 2] { {7.0, 8.0}, {9.0, 10.0}, {11.0, 12.0}};
            double[,] test = new double[2, 2] { {58.0, 64.0}, {139.0, 154.0} };
            double[,] res = mul(A, B);
            Debug.Assert(equals(res, test));
            double[] c = new double[] { 3.0, 4.0, 2.0 };
            double[,] D = new double[3, 4] { { 13.0, 9.0, 7.0, 15.0 }, { 8.0, 7.0, 4.0, 6.0 }, { 6.0, 4.0, 0.0, 3.0 } };
            double[] test2 = new double[] { 83.0, 63.0, 37.0, 75.0 };
            double[] res2 = mul(c, D);
            Debug.Assert(Vector.equal(res2, test2));
            double[,] E = new double[4, 3] { {1.0, 2.0, 3.0},  {4.0, 5.0, 6.0}, {7.0, 8.0, 9.0}, {10.0, 11.0, 12.0}};
            double[] f = new double[3] { -2.0, 1.0, 0.0 };
            double[] test3 = new double[4] { 0.0, -3.0, -6.0, -9.0 };
            double[] res3 = mul(E, f);
            Debug.Assert(Vector.equal(res3, test3));
        }
    }
}
