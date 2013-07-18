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

namespace WindowsFormsApplication1 {
    class Scalar {
        const double Epsilon = 1.0e-5;  // differ small enough to consider equality of scalars

        static Random rand_gen = new Random();
        static public double jitter() {
            return rand_gen.NextDouble() / 1000.0;
        }        

        static public bool semi_equal(double a, double b) {
            if (Math.Abs(a - b) < Epsilon) {
                return true;
            }
            return false;
        }

        static public int pow(int A, int n) {
            if (n == 0) return 1;
            return A * pow(A, n - 1);
        }

        static public double pow(double A, int n) {
            if (n == 0) return 1;
            return A * pow(A, n - 1);
        }        
    }
}
