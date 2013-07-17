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


namespace WindowsFormsApplication1
{
    class ImageTransformer
    {
        static Random rand_gen = new Random();
        private static double jitter()
        {
            return rand_gen.NextDouble() / 1000.0;
        }        

        public static Bitmap Projection(ContinuousBitmap source, Point P1, Point P2, Point P3, Point P4){
            int new_w = (int)(Dr(P1.X, P1.Y, P2.X, P2.Y) + Dr(P3.X, P3.Y, P4.X, P4.Y)) / 2;
            int new_h = (int)(Dr(P1.X, P1.Y, P4.X, P4.Y) + Dr(P2.X, P2.Y, P3.X, P3.Y)) / 2;

            double x1 = P1.X;
            double x2 = P2.X;
            double x3 = P3.X;
            double x4 = P4.X;

            double y1 = P1.Y;
            double y2 = P2.Y;
            double y3 = P3.Y;
            double y4 = P4.Y;

            x1 += jitter();
            x2 -= jitter();
            x3 -= jitter();
            x4 += jitter();

            y1 += jitter();
            y2 += jitter();
            y3 -= jitter();
            y4 -= jitter();

            double[,] M = Matrix.make_projection(x1, y1, x2, y2, x3, y3, x4, y4);
            double A = M[0, 0];
            double B = M[1, 0];
            double C = M[2, 0];
            double D = M[0, 1];
            double E = M[1, 1];
            double F = M[2, 1];
            double a = M[0, 2];
            double b = M[1, 2];
            double c = M[2, 2];

            Bitmap proj = new Bitmap(new_w, new_h);

            for (int i = 0; i < new_h; i++)
            {
                double y = i / (double)(new_h);
                for (int j = 0; j < new_w; j++)
                {
                    double x = j / (double)(new_w);
                    double d = 1.0 / (a * x + b * y + c);
                    double xn = (A * x + B * y + C) * d;
                    double yn = (D * x + E * y + F) * d;

                    Color col = source.GetPixel(xn, yn);
                    proj.SetPixel(j, i, col);
                }
            }
            return proj;
        }

        private static double Dp(int x1, int y1, int x2, int y2)
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        }

        private static double Dr(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Dp(x1, y1, x2, y2));
        }

        public static Bitmap ByPolynomialProfiles(ContinuousBitmap source, Polynomial pol1, Polynomial pol2, int line1, int line2){
            int new_w = source.Width;
            int new_h = line2 - line1;
            Bitmap flat = new Bitmap(new_w, new_h);
            for (int i = 0; i < new_h; i++)
            {
                for (int j = 0; j < new_w; j++)
                {
                    double t = (double)i / new_h;
                    double f = 1.0 - t;
                    double di = pol1.In(j) * f + pol2.In(j) * t;
                    Color col = source.GetPixel(j, i + di + line1);
                    flat.SetPixel(j, i, col);
                }
            }
            return flat;
        }
    }
}
