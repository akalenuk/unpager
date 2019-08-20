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
