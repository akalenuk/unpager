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
using System.Drawing;

// TODO: fix first solver and put them all to LinearAlgebra only

namespace WindowsFormsApplication1
{
    class Polynomial
    {
        const int FIRM_MULTIPLIER = 1000;

        protected double[] ais;

        public Polynomial() { 
            ais = new double[1];
            ais[0] = 0.0;
        }

        public Polynomial(int pol_n, List<Point> carcas, List<Point> firm_carcas) {
            Rebuild(pol_n, carcas, firm_carcas);
        }

        public double In(double x)
        {
            double P = 0.0;
            for (int i = ais.Length-1; i >= 0; i--)
            {
                P += ais[i] + x*P;
            }
            return P;
        }

        public void Rebuild(int pol_n, List<Point> carcas, List<Point> firm_carcas)
        {
            double[,] A = new double[pol_n, pol_n];
            double[] B = new double[pol_n];
            for (int i = 0; i < pol_n; i++)
            {
                for (int j = 0; j < pol_n; j++)
                {
                    A[i, j] = 0.0;
                    foreach (Point p in carcas)
                    {
                        A[i, j] += Math.Pow(p.X, i + j);
                    }
                    foreach (Point p in firm_carcas)
                    {
                        A[i, j] += Math.Pow(p.X, i + j) * FIRM_MULTIPLIER;
                    }
                }
                B[i] = 0.0;
                foreach (Point p in carcas)
                {
                    B[i] += p.Y * Math.Pow(p.X, i);
                }
                foreach (Point p in firm_carcas)
                {
                    B[i] += p.Y * Math.Pow(p.X, i) * FIRM_MULTIPLIER;
                }
            }
            ais = SolveSLAE_Gauss(A, B);
        }
        
        // this solver woudn't work!
        private double Sa(double[,] A, double[] B, int i, int j, int n) 
        {
            int N = B.Length;
            if (n == N)
            {
                return A[i, j];
            }
            return Sa(A, B, i, j, n + 1) * Sa(A, B, n, n, n + 1) - Sa(A, B, i, n, n + 1) * Sa(A, B, n, j, n + 1);
        }

        private double Sb(double[,] A, double[] B, int i, int n) 
        { 
            int N = B.Length;
            if (n == N)
            {
                return B[i];
            }
            return Sa(A, B, n, n, n + 1) * Sb(A, B, i, n + 1) - Sa(A, B, i, n, n + 1) * Sb(A, B, n, n + 1);
        }

        double Sx(double[,] A, double[] B, double[] X, int i)
        {
            double d = Sb(A, B, i, i + 1);
            for (int j = 0; j < i; j++)
            {
                d -= Sa(A, B, i, j, i + 1) * X[j];
            }
            return d / Sa(A, B, i, i, i + 1);
        }

        private double[] SolveSLAE(double[,] A, double[] B) 
        {
            int n = B.Length;
            double[] X = new double[n];
            for (int i = 0; i < n; i++)
            {
                X[i] = Sx(A, B , X, i);
            }
            return X;
        }
        // should probably fix it

        

        private double[] SolveSLAE_Gauss(double[,] A, double[] B)
        {
            int N = B.Length;
            double[] X = new double[N];
            for (int k = 0; k < N-1; k++){
                for (int j = 0; j < k + 1; j++) {
                    if (A[j, j] == 0.0) A[j, j] = 0.000000001;  // hack!
                    double r = A[k + 1, j] / A[j, j];
                    A[k + 1, j] = 0.0;
                    for (int bj = j + 1; bj < N; bj++) {
                        A[k + 1, bj] = A[k + 1, bj] - A[j, bj] * r;
                    }
                    B[k + 1] = B[k+1] - B[j] * r;
                }
            }

            if (A[N - 1, N - 1] == 0.0) A[N-1, N-1] = 0.000000001;  // hack!
            X[N - 1] = B[N - 1] / A[N - 1, N - 1];
            for (int i = N - 2; i >= 0; i--) {
                double s = 0.0;
                for (int j = i; j < N; j++){
                    s = s + A[i, j] * X[j];
                }
                if (A[i, i] == 0) A[i, i] = 0.0000001;
                X[i] = (B[i] - s) / A[i, i];
            }

            return X;
        }        
    }
}
