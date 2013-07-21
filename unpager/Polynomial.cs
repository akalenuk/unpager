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
                        A[i, j] += Scalar.pow((double)p.X, i + j);
                    }
                    foreach (Point p in firm_carcas)
                    {
                        A[i, j] += Scalar.pow((double)p.X, i + j) * FIRM_MULTIPLIER;
                    }
                }
                B[i] = 0.0;
                foreach (Point p in carcas)
                {
                    B[i] += p.Y * Scalar.pow((double)p.X, i);
                }
                foreach (Point p in firm_carcas)
                {
                    B[i] += p.Y * Scalar.pow(p.X, i) * FIRM_MULTIPLIER;
                }
            }
            ais = Vector.Gauss(A, B);
        }
    }
}
