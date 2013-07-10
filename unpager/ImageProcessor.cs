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
    class ImageProcessor
    {
        public static Bitmap FlattenLight(Bitmap source, List<Point> light_points) {
            List<Point> light_hr = new List<Point>();
            List<Point> light_hg = new List<Point>();
            List<Point> light_hb = new List<Point>();
            List<Point> light_vr = new List<Point>();
            List<Point> light_vg = new List<Point>();
            List<Point> light_vb = new List<Point>();
            int max_r = 0;
            int max_g = 0;
            int max_b = 0;

            foreach (Point p in light_points)
            {
                Color col = source.GetPixel(p.X, p.Y);
                if (col.R > max_r) max_r = col.R;
                if (col.G > max_g) max_g = col.G;
                if (col.B > max_b) max_b = col.B;

                light_hr.Add(new Point(p.X, col.R));
                light_hg.Add(new Point(p.X, col.G));
                light_hb.Add(new Point(p.X, col.B));

                light_vr.Add(new Point(p.Y, col.R));
                light_vg.Add(new Point(p.Y, col.G));
                light_vb.Add(new Point(p.Y, col.B));
            }

            Polynomial pol_hr = new Polynomial(3, light_hr, new List<Point>());
            Polynomial pol_hg = new Polynomial(3, light_hg, new List<Point>());
            Polynomial pol_hb = new Polynomial(3, light_hb, new List<Point>());

            Polynomial pol_vr = new Polynomial(3, light_vr, new List<Point>());
            Polynomial pol_vg = new Polynomial(3, light_vg, new List<Point>());
            Polynomial pol_vb = new Polynomial(3, light_vb, new List<Point>());

            Bitmap light = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < source.Height; i++)
            {
                for (int j = 0; j < source.Width; j++)
                {
                    double dhr = pol_hr.In(j);
                    double dvr = pol_vr.In(i);
                    int dr = (int)((dhr + dvr) / 2);
                    double dhg = pol_hg.In(j);
                    double dvg = pol_vg.In(i);
                    int dg = (int)((dhg + dvg) / 2);
                    double dhb = pol_hb.In(j);
                    double dvb = pol_vb.In(i);
                    int db = (int)((dhb + dvb) / 2);
                    Color scol = source.GetPixel(j, i);
                    int nR = scol.R + max_r - dr;
                    int nG = scol.G + max_g - dg;
                    int nB = scol.B + max_b - db;

                    if (nR < 0) nR = 0;
                    if (nR > max_r) nR = max_r;
                    if (nG < 0) nG = 0;
                    if (nG > max_g) nG = max_g;
                    if (nB < 0) nB = 0;
                    if (nB > max_b) nB = max_b;
                   
                    Color ncol = Color.FromArgb(nR, nG, nB);
                    light.SetPixel(j, i, ncol);
                }
            }
            return light;
        }

        public static Bitmap Reinterpolate(Bitmap source, double magnitude1, double magnitude2) {
            Bitmap sharp = new Bitmap(source.Width, source.Height);
            int max_r = 0;
            int max_g = 0;
            int max_b = 0;
            int min_r = 255;
            int min_g = 255;
            int min_b = 255;

            for (int i = 0; i < source.Height; i++)
            {
                for (int j = 0; j < source.Width; j++)
                {
                    Color col = source.GetPixel(j, i);
                    if(col.R > max_r) max_r = col.R;
                    if(col.G > max_g) max_g = col.G;
                    if(col.B > max_b) max_b = col.B;

                    if (col.R < min_r) min_r = col.R;
                    if (col.G < min_g) min_g = col.G;
                    if (col.B < min_b) min_b = col.B;
                }
            }
            for (int i = 0; i < source.Height; i++)
            {
                for (int j = 0; j < source.Width; j++)
                {
                    Color col = source.GetPixel(j, i);
                    int nR = col.R;
                    int nG = col.G;
                    int nB = col.B;
                    if (nR <= 0) nR = 1;
                    if (nR >= max_r) nR = max_r - 1;
                    if (nG <= 0) nG = 1;
                    if (nG >= max_g) nG = max_g - 1;
                    if (nB <= 0) nB = 1;
                    if (nB >= max_b) nB = max_b - 1;

                    // sharp
                    double tr = ((double)(nR - min_r) / (max_r - min_r));
                    double fr = (1.0 - tr);
                    double tg = ((double)(nG - min_g) / (max_g - min_g));
                    double fg = (1.0 - tg);
                    double tb = ((double)(nB - min_b) / (max_b - min_b));
                    double fb = (1.0 - tb);

                    double itr = Math.Pow(1.0 / tr, magnitude1);
                    double itg = Math.Pow(1.0 / tg, magnitude2);
                    double itb = Math.Pow(1.0 / tb, magnitude1);
                    double ifr = Math.Pow(1.0 / fr, magnitude2);
                    double ifg = Math.Pow(1.0 / fg, magnitude1);
                    double ifb = Math.Pow(1.0 / fb, magnitude2);

                    int nnR = min_r + (int)((max_r-min_r) * ifr / (itr + ifr));
                    int nnG = min_g + (int)((max_g-min_g) * ifg / (itg + ifg));
                    int nnB = min_b + (int)((max_b-min_b) * ifb / (itb + ifb));

                    Color ncol = Color.FromArgb(nnR, nnG, nnB);
                    sharp.SetPixel(j, i, ncol);
                }
            }
            return sharp;
        }

        public static Bitmap Grayscale(Bitmap source)
        {
            Bitmap gray = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < source.Height; i++)
            {
                for (int j = 0; j < source.Width; j++)
                {
                    Color col = source.GetPixel(j, i);
                    int nR = col.R;
                    int nG = col.G;
                    int nB = col.B;
                    int nC = (nR + nG + nB) / 3;

                    Color ncol = Color.FromArgb(nC, nC, nC);
                    gray.SetPixel(j, i, ncol);
                }
            }
            return gray;
        }

        public static Bitmap Normalize(Bitmap source)
        {
            int max_r = 0;
            int max_g = 0;
            int max_b = 0;
            int min_r = 255;
            int min_g = 255;
            int min_b = 255;

            for (int i = 0; i < source.Height; i++)
            {
                for (int j = 0; j < source.Width; j++)
                {
                    Color col = source.GetPixel(j, i);
                    if (col.R > max_r) max_r = col.R;
                    if (col.G > max_g) max_g = col.G;
                    if (col.B > max_b) max_b = col.B;

                    if (col.R < min_r) min_r = col.R;
                    if (col.G < min_g) min_g = col.G;
                    if (col.B < min_b) min_b = col.B;
                }
            }
            Bitmap norm = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < source.Height; i++)
            {
                for (int j = 0; j < source.Width; j++)
                {
                    Color col = source.GetPixel(j, i);
                    int nR = 255 * (col.R - min_r) / (max_r - min_r);
                    int nG = 255 * (col.G - min_g) / (max_g - min_g);
                    int nB = 255 * (col.B - min_b) / (max_b - min_b);

                    Color ncol = Color.FromArgb(nR, nG, nB);
                    norm.SetPixel(j, i, ncol);
                }
            }
            return norm;
        }
    }
}
