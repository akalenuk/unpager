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

        public static Bitmap Sharpen(Bitmap source, int magnitude) {
            Bitmap sharp = new Bitmap(source.Width, source.Height);
            int max_r = 0;
            int max_g = 0;
            int max_b = 0;

            for (int i = 0; i < source.Height; i++)
            {
                for (int j = 0; j < source.Width; j++)
                {
                    Color col = source.GetPixel(j, i);
                    if(col.R > max_r){
                        max_r = col.R;
                    }
                    if(col.G > max_g){
                        max_g = col.G;
                    }
                    if(col.B > max_b){
                        max_b = col.B;
                    }
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
                    double tr = Math.Pow(1.0 / ((double)nR / max_r), magnitude);
                    double fr = Math.Pow(1.0 / (1.0 - tr), magnitude);
                    double tg = Math.Pow(1.0 / ((double)nG / max_g), magnitude);
                    double fg = Math.Pow(1.0 / (1.0 - tg), magnitude);
                    double tb = Math.Pow(1.0 / ((double)nB / max_b), magnitude);
                    double fb = Math.Pow(1.0 / (1.0 - tb), magnitude);

                    int nnR = (int)(255 * fr / (tr + fr));
                    int nnG = (int)(255 * fg / (tg + fg));
                    int nnB = (int)(255 * fb / (tb + fb));

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
