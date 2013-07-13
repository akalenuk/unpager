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

// TODO assertions for all algebra invariants
// TODO descent unit testing
// TODO refactor variable names

namespace WindowsFormsApplication1 {
    class LinearAlgebra {
        static double[] v_add(double[] a, double[] b) {
            double[] res = new double[a.Length];
            for (int i = 0; i < a.Length; i++) {
                res[i] = a[i] + b[i];
            }
            return res;
        }

        static double[] v_sub(double[] a, double[] b) {
            double[] res = new double[a.Length];
            for (int i = 0; i < a.Length; i++) {
                res[i] = a[i] - b[i];
            }
            return res;
        }

        static double v_dot(double[] a, double[] b) {
            double res = 0.0;
            for (int i = 0; i < a.Length; i++) {
                res += a[i] * b[i];
            }
            return res;
        }

        static double v_len(double[] a) {
            double res = 0.0;
            for (int i = 0; i < a.Length; i++) {
                res += a[i] * a[i];
            }
            return Math.Sqrt(res);
        }

        static double[] s_mul(double s, double[] a) {
            double[] res = new double[a.Length];
            for (int i = 0; i < a.Length; i++) {
                res[i] = s*a[i];
            }
            return res;
        }

        static double[,] m_mul(double[,] A, double[,] B){
            int AH = A.GetLength(0);
            int AW = A.GetLength(1);
            int BH = B.GetLength(0);
            int BW = B.GetLength(1);            
            Debug.Assert(AH == BW && AW == BH);
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

        static double[,] m_trans(double[,] A) {
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

        // Levi-Civita symbol. a is a list of indices
        static int _LC(List<int> a) {  
            int la = a.Count;
            int n = 0;
            List<int> t = new List<int>(a);
           
            for (int i = 0; i < la; i++) {
                for (int j = 0; j < la - i - 1; j++ ) {
                    if (t[j] == t[j + 1]) {
                        return 0;
                    } else if (t[j] > t[j + 1]) {
                        n += 1;
                        int r = t[j];
                        t.RemoveAt(j);
                        t.Insert(j + 1, r);
                    }
                }
            }
            if ( n % 2 == 0){
                return 1;
            }else{
                return -1;
            }
        }

        static double s_pow(double A, int n) {
            if (n == 0) return 1;
            return A * s_pow(A, n-1);
        }

        static int s_pow(int A, int n) {
            if (n == 0) return 1;
            return A * s_pow(A, n-1);
        }

        // this is multidimentional cross product. A is an array of vectors, not a matrix
        static double[] v_cross(double[][] A) {
            int DIMM = 0;
            int N = A.Length;
            Debug.Assert( N>=2 );
            foreach (double[] a in A) {
                DIMM = a.Length;
                Debug.Assert(DIMM == N + 1);
            }
            double[] v_res = new double[DIMM];
            for (int i = 0; i < DIMM; i++) {
                for(int jk = 0; jk < s_pow(DIMM, N); jk++){
                    List<int> v_ijk = new List<int>();
                    v_ijk.Add(i);
                    for (int j = 0; j < N; j++) {
                        v_ijk.Add( (jk / ( s_pow(DIMM, (N - j - 1)) )) % DIMM);
                    }
                    int t_res = _LC(v_ijk);
                    if (t_res != 0) {
                        double s_res = t_res;
                        for (int k = 0; k < N; k++) {
                            s_res *= A[k][v_ijk[k + 1]];
                        }
                        v_res[i] += s_res;
                    }
                }
            }
            return v_res;
        }

        // multidimensional triple product. A is an array of vectors
        static double v_mixed(double[][] A) {
            int DIMM = 0;
            int N = A.Length;
            Debug.Assert(N >= 3);
            foreach (double[] a in A) {
                DIMM = a.Length;
                Debug.Assert(DIMM == N);
            }

            double v_res = 0.0;
            for (int jk = 0; jk < s_pow(DIMM, N); jk++) {
                List<int> v_ijk = new List<int>();
                for (int j = 0; j < N; j++) {
                    v_ijk.Add( (jk / s_pow(DIMM, N - j - 1)) % DIMM );
                }
                int t_res = _LC(v_ijk);
                if( t_res != 0 ){
                    double s_res = t_res;
                    for(int k = 0; k < N; k++){
                        s_res *= A[k][v_ijk[k]];
                    }
                    v_res += s_res;
                }
            }
            return v_res;
        }

        private double[] SolveSLAE_Gauss(double[,] A, double[] B) { // it's temporary, I don't like this code
            int N = B.Length;
            double[] X = new double[N];
            for (int k = 0; k < N - 1; k++) {
                for (int j = 0; j < k + 1; j++) {
                    if (A[j, j] == 0.0) A[j, j] = 0.000000001;  // hack!
                    double r = A[k + 1, j] / A[j, j];
                    A[k + 1, j] = 0.0;
                    for (int bj = j + 1; bj < N; bj++) {
                        A[k + 1, bj] = A[k + 1, bj] - A[j, bj] * r;
                    }
                    B[k + 1] = B[k + 1] - B[j] * r;
                }
            }

            if (A[N - 1, N - 1] == 0.0) A[N - 1, N - 1] = 0.000000001;  // hack!
            X[N - 1] = B[N - 1] / A[N - 1, N - 1];
            for (int i = N - 2; i >= 0; i--) {
                double s = 0.0;
                for (int j = i; j < N; j++) {
                    s = s + A[i, j] * X[j];
                }
                if (A[i, i] == 0) A[i, i] = 0.0000001;
                X[i] = (B[i] - s) / A[i, i];
            }

            return X;
        }

        static bool v_cmp(double[] A, double[] B) {
            int la = A.Length;
            int lb = B.Length;
            if (la != lb) return false;
            for (int i = 0; i < la; i++) {
                if (A[i] != B[i]) return false;
            }
            return true;
        }

        static public void test() { 
            double[][] A = new double[3][] {new double[] {-2, 3, 1}, new double[] {0, 4, 0}, new double[] {-1, 3, 3}};
            Debug.Assert(v_mixed(A) == -20);
            double[][] B = new double[2][] { new double[] { 3, -3, 1 }, new double[] { 4, 9, 2 } };
            double[] cross_calc = v_cross(B);
            double[] cross_test = new double[] { -15, -2, 39 };
            Debug.Assert(v_cmp(cross_calc, cross_test));
        }
    }
}
