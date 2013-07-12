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

// TODO consider introdusing assertions for all algebra invariants

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
    }
}
