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
    class Vector {
        static public double[] add(double[] a, double[] b) {
            int DIMM = a.Length;
            Debug.Assert(b.Length == DIMM);
            double[] res = new double[DIMM];
            for (int i = 0; i < DIMM; i++) {
                res[i] = a[i] + b[i];
            }
            return res;
        }

        static public double[] sub(double[] a, double[] b) {
            int DIMM = a.Length;
            Debug.Assert(b.Length == DIMM);
            double[] res = new double[DIMM];
            for (int i = 0; i < DIMM; i++) {
                res[i] = a[i] - b[i];
            }
            return res;
        }

        static public double dot(double[] a, double[] b) {
            int DIMM = a.Length;
            Debug.Assert(b.Length == DIMM);
            double res = 0.0;
            for (int i = 0; i < DIMM; i++) {
                res += a[i] * b[i];
            }
            return res;
        }

        static public double len(double[] a) {
            double res = 0.0;
            for (int i = 0; i < a.Length; i++) {
                res += a[i] * a[i];
            }
            return Math.Sqrt(res);
        }

        static public double[] mul(double s, double[] a) {
            double[] res = new double[a.Length];
            for (int i = 0; i < a.Length; i++) {
                res[i] = s*a[i];
            }
            return res;
        }

        // Levi-Civita symbol. a is a list of indices
        static public int LC(List<int> a) {  
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

        // this is multidimentional cross product. A is an array of vectors, not a matrix
        static public double[] cross(double[][] A) {
            int DIMM = 0;
            int N = A.Length;
            Debug.Assert( N>=2 );
            foreach (double[] a in A) {
                DIMM = a.Length;
                Debug.Assert(DIMM == N + 1);
            }
            double[] res = new double[DIMM];
            for (int i = 0; i < DIMM; i++) {
                for (int jk = 0; jk < Scalar.pow(DIMM, N); jk++) {
                    List<int> v_ijk = new List<int>();
                    v_ijk.Add(i);
                    for (int j = 0; j < N; j++) {
                        v_ijk.Add((jk / (Scalar.pow(DIMM, (N - j - 1)))) % DIMM);
                    }
                    int sign = LC(v_ijk);
                    if (sign != 0) {
                        double res_add = sign;
                        for (int k = 0; k < N; k++) {
                            res_add *= A[k][v_ijk[k + 1]];
                        }
                        res[i] += res_add;
                    }
                }
            }
            return res;
        }

        // multidimensional triple product. A is an array of vectors
        static public double nple(double[][] A) {
            int DIMM = 0;
            int N = A.Length;
            Debug.Assert(N >= 3);
            foreach (double[] a in A) {
                DIMM = a.Length;
                Debug.Assert(DIMM == N);
            }

            double res = 0.0;
            for (int jk = 0; jk < Scalar.pow(DIMM, N); jk++) {
                List<int> v_ijk = new List<int>();
                for (int j = 0; j < N; j++) {
                    v_ijk.Add( (jk / Scalar.pow(DIMM, N - j - 1)) % DIMM );
                }
                int sign = LC(v_ijk);
                if( sign != 0 ){
                    double res_add = sign;
                    for(int k = 0; k < N; k++){
                        res_add *= A[k][v_ijk[k]];
                    }
                    res += res_add;
                }
            }
            return res;
        }

        const double FUCKING_MAGIC = 0.00000001; // it's temporary, I don't like this code
        static public double[] Gauss(double[,] A, double[] B) {
            int N = B.Length;
            double[] X = new double[N];
            for (int k = 0; k < N - 1; k++) {
                for (int j = 0; j < k + 1; j++) {
                    if (A[j, j] == 0.0) A[j, j] = FUCKING_MAGIC;  // hack!
                    double r = A[k + 1, j] / A[j, j];
                    A[k + 1, j] = 0.0;
                    for (int bj = j + 1; bj < N; bj++) {
                        A[k + 1, bj] = A[k + 1, bj] - A[j, bj] * r;
                    }
                    B[k + 1] = B[k + 1] - B[j] * r;
                }
            }

            if (A[N - 1, N - 1] == 0.0) A[N - 1, N - 1] = FUCKING_MAGIC;  // hack!
            X[N - 1] = B[N - 1] / A[N - 1, N - 1];
            for (int i = N - 2; i >= 0; i--) {
                double s = 0.0;
                for (int j = i; j < N; j++) {
                    s = s + A[i, j] * X[j];
                }
                if (A[i, i] == 0) A[i, i] = FUCKING_MAGIC;  // hack!
                X[i] = (B[i] - s) / A[i, i];
            }

            return X;
        }

        static public bool equal(double[] A, double[] B) {
            int la = A.Length;
            int lb = B.Length;
            if (la != lb) return false;
            for (int i = 0; i < la; i++) {
                if (A[i] != B[i]) return false;
            }
            return true;
        }

        static public bool semi_equal(double[] A, double[] B) {
            int la = A.Length;
            int lb = B.Length;
            if (la != lb) return false;
            for (int i = 0; i < la; i++) {
                if (! Scalar.semi_equal(A[i], B[i]) ) return false;
            }
            return true;
        }

        static public void test() { 
            double[][] A = new double[3][] {new double[] {-2, 3, 1}, new double[] {0, 4, 0}, new double[] {-1, 3, 3}};
            Debug.Assert(nple(A) == -20);
            double[][] B = new double[2][] { new double[] { 3, -3, 1 }, new double[] { 4, 9, 2 } };
            double[] cross_calc = cross(B);
            double[] cross_test = new double[] { -15, -2, 39 };
            Debug.Assert(equal(cross_calc, cross_test));
        }
    }
}
