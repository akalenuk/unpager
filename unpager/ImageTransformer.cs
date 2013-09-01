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

            x1 += Scalar.jitter();
            x2 -= Scalar.jitter();
            x3 -= Scalar.jitter();
            x4 += Scalar.jitter();

            y1 += Scalar.jitter();
            y2 += Scalar.jitter();
            y3 -= Scalar.jitter();
            y4 -= Scalar.jitter();

            double[,] M = Matrix.make_projection(x1, y1, x2, y2, x3, y3, x4, y4);

            Bitmap proj = new Bitmap(new_w, new_h);

            for (int i = 0; i < new_h; i++)
            {
                double y = i / (double)(new_h);
                for (int j = 0; j < new_w; j++)
                {
                    double x = j / (double)(new_w);
                    double[] xyn = Matrix.project_point(M, new double[] { x, y });

                    Color col = source.GetPixel(xyn[0], xyn[1]);
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

        public static Bitmap BySwineProfiles(ContinuousBitmap source, 
                                                double[][] carcas1, int[][] complex1, Mswine.BasisFunction[] basis1,
                                                double[][] carcas2, int[][] complex2, Mswine.BasisFunction[] basis2, 
                                                int line1, int line2) {
            int new_w = source.Width;
            int new_h = line2 - line1;
            Bitmap flat = new Bitmap(new_w, new_h);
            double jitter = Scalar.jitter();
            for (int i = 0; i < new_h; i++) {
                for (int j = 0; j < new_w; j++) {
                    double t = (double)i / new_h;
                    double f = 1.0 - t;
                    double[] dot = new double[1] { j + jitter };
                    double y1 = Mswine.F_s(dot, carcas1, complex1, basis1, Mswine.s_l);
                    double y2 = Mswine.F_s(dot, carcas2, complex2, basis2, Mswine.s_l);
                    double di = y1 * f + y2 * t;
                    Color col = source.GetPixel(j, i + di + line1);
                    flat.SetPixel(j, i, col);
                }
            }
            return flat;
        }

        public static Bitmap ByPolynomialModel(Bitmap source, List<Point> points){
            // this thing doesn't work at all
            int N = points.Count / 2;
            double[,] A = new double[N, 4];
            double[] Bx = new double[N];
            double[] By = new double[N];
            for (int i = 0; i < N; i++) {
                double x = (double)points[i * 2 + 1].X / source.Width;
                double y = (double)points[i * 2 + 1].Y / source.Height;
                double dx = (double)(points[i * 2].X - points[i * 2 + 1].X) / source.Width;
                double dy = (double)(points[i * 2].Y - points[i * 2 + 1].Y) / source.Width;
                /*A[i, 0] = x * x * x;
                A[i, 1] = x * x;
                A[i, 2] = x;
                A[i, 3] = 1;
                A[i, 4] = x * y;
                A[i, 5] = y;
                A[i, 6] = x * y * y;
                A[i, 7] = y * y;*/

                A[i, 0] = x * y;
                A[i, 1] = x;
                A[i, 2] = y;
                A[i, 3] = 1.0;

                Bx[i] = dx;
                By[i] = dy;                 
            }
            double[,] Ax = Matrix.mul(A, Matrix.trans(A));
            double[,] Ay = Matrix.mul(A, Matrix.trans(A));
            double[] Px = Vector.Gauss(Ax, Bx);
            double[] Py = Vector.Gauss(Ay, By);
            ContinuousBitmap csource = new ContinuousBitmap(source);
            Bitmap ret = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < ret.Height; i++) {
                for (int j = 0; j < ret.Width; j++) {
                    double x = (double)j / ret.Width;
                    double y = (double)i / ret.Height;
                    /*double dx = Px[0]*x*x*x + Px[1]*x*x + Px[2]*x + Px[3] + Px[4]*x*y + Px[5]*y + Px[6]*x*y*y + Px[7]*x*y;
                    double dy = Py[0]*x*x*x + Py[1]*x*x + Py[2]*x + Py[3] + Py[4]*x*y + Py[5]*y + Py[6]*x*y*y + Py[7]*x*y;*/
                    double dx = Px[0] * x * y + Px[1] * x + Px[2] * y + Px[3];
                    double dy = Py[0] * x * y + Py[1] * x + Py[2] * y + Py[3];

                    double dj = dx * source.Width;
                    double di = dy * source.Height;
                    ret.SetPixel(j, i, csource.GetPixel(j + dj, i + di));
                }
            }
            return ret;
        }

        public static Bitmap ByFFD(Bitmap source, List<Point> points) {
            int N = points.Count / 2;
            double[][] carcas = new double[N][];
            double[] dx = new double[N];
            double[] dy = new double[N];
            for (int i = 0; i < N; i++) {
                carcas[i] = new double[2] { points[2 * i + 1].X, points[2 * i + 1].Y};
                dx[i] = points[2 * i].X - points[2 * i + 1].X;
                dy[i] = points[2 * i].Y - points[2 * i + 1].Y;
            }
            int[][] SC = Triangulation.triangulate(carcas);
            // this is a little retarded, but triangulation counts points from 0, and swine - from 1
            foreach (int[] p in SC) {
                p[0] += 1;
                p[1] += 1;
                p[2] += 1;
            }
            Mswine.BasisFunction[] swine_basis_x = Mswine.get_constant_functions(carcas, dx, SC);
            Mswine.BasisFunction[] swine_basis_y = Mswine.get_constant_functions(carcas, dy, SC);

            ContinuousBitmap csource = new ContinuousBitmap(source);
            Bitmap ret = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < ret.Height; i++) {
                for (int j = 0; j < ret.Width; j++) {
                    double[] xy = new double[2]{j, i};
                    double dj = Mswine.F_s(xy, carcas, SC, swine_basis_x, Mswine.s_l);
                    double di = Mswine.F_s(xy, carcas, SC, swine_basis_y, Mswine.s_l);
                    ret.SetPixel(j, i, csource.GetPixel(j + dj, i + di));
                }
            }
            return ret;
        }
    }
}
