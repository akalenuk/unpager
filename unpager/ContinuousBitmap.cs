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
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    struct RGB {
        public byte R;
        public byte G;
        public byte B;

        public RGB(byte r, byte g, byte b) {
            R = r;
            B = b;
            G = g;
        }
    };

    // TRY: stochastic interpolation
    class ContinuousBitmap
    {
        private const double EPSILON = 1.0e-5;
  
        private Bitmap bitmap;
        private byte[] bitmap_data;
        private int bytes_per_color;
        private int bytes_per_stryde;
        public int Width;
        public int Height;
        
        
        public ContinuousBitmap(Bitmap source) {
            bitmap = new Bitmap( source );

            BitmapData bd = source.LockBits(
                new Rectangle(0, 0, source.Width, source.Height),
                ImageLockMode.ReadWrite, source.PixelFormat);

            Width = source.Width;
            Height = source.Height;
            bytes_per_color = bd.Stride / Width;    // this is wrong. TODO - replace it with proper measure
            bytes_per_stryde = bd.Stride;

            bitmap_data = new byte[bd.Stride * bd.Height];

            Marshal.Copy(bd.Scan0, bitmap_data, 0, bitmap_data.Length);

            source.UnlockBits(bd);
        }

        private RGB GetRGB(int j, int i) {
            return new RGB(bitmap_data[i * bytes_per_stryde + j * bytes_per_color + 2],
                bitmap_data[i*bytes_per_stryde + j*bytes_per_color + 1],
                bitmap_data[i*bytes_per_stryde + j*bytes_per_color + 0]);            
        }

        private double Fract(double x)
        {
            return x - Math.Floor(x);
        }

        private bool CloseToInt(double x) 
        {
            return (Math.Min(x - Math.Floor(x), Math.Ceiling(x) - x) < EPSILON);
        }

        private RGB GetPixelPWH(double x, int y)
        {
            double t = Fract(x);
            double f = 1 - t;
            RGB left = GetRGB((int)x, (int)y);
            if (t == 0.0) return left;
            RGB right = GetRGB((int)x + 1, (int)y);
            if (f == 0.0) return right;
            double d = 1.0 / (1.0 / t + 1.0 / f);
            double r = ((double)left.R / t + (double)right.R / f) * d;
            double g = ((double)left.G / t + (double)right.G / f) * d;
            double b = ((double)left.B / t + (double)right.B / f) * d;

            return new RGB((byte)r, (byte)g, (byte)b);
        }

        private RGB GetPixelPWV(int x, double y)
        {
            double t = Fract(y);
            double f = 1 - t;
            RGB top = GetRGB((int)x, (int)y);
            if (t == 0) return top;
            RGB bottom = GetRGB((int)x, (int)y + 1);
            if (f == 0) return bottom;
            double d = 1.0 / (1.0 / t + 1.0 / f);
            double r = ((double)top.R / t + (double)bottom.R / f) * d;
            double g = ((double)top.G / t + (double)bottom.G / f) * d;
            double b = ((double)top.B / t + (double)bottom.B / f) * d;

            return new RGB((byte)r, (byte)g, (byte)b);
        }

        private RGB GetPixelPWHV(double x, double y)
        {
            double tx = Fract(x);
            double fx = 1 - tx;
            double ty = Fract(y);
            double fy = 1 - ty;
            RGB left_top = GetRGB((int)x, (int)y);
            RGB right_top = GetRGB((int)x + 1, (int)y);
            RGB right_bottom = GetRGB((int)x + 1, (int)y + 1);
            RGB left_bottom = GetRGB((int)x, (int)y + 1);
            double d = 1.0 / (1.0 / (tx * ty) + 1.0 / (fx * ty) + 1.0 / (fx * fy) + 1.0 / (tx * fy));
            double r = (left_top.R / (tx * ty) + right_top.R / (fx * ty) + right_bottom.R / (fx * fy) + left_bottom.R / (tx * fy)) * d;
            double g = (left_top.G / (tx * ty) + right_top.G / (fx * ty) + right_bottom.G / (fx * fy) + left_bottom.G / (tx * fy)) * d;
            double b = (left_top.B / (tx * ty) + right_top.B / (fx * ty) + right_bottom.B / (fx * fy) + left_bottom.B / (tx * fy)) * d;

            return new RGB((byte)r, (byte)g, (byte)b);
        }

        private RGB GetPixelPW(double x, double y)
        {
            // corners
            if (x < 0 && y < 0)
            {
                return GetRGB(0, 0);
            }
            int w = Width - 1;
            if (x > w && y < 0)
            {
                return GetRGB(w, 0);
            }
            int h = Height - 1;
            if (x < 0 && y > h)
            {
                return GetRGB(0, h);
            }
            if (x > w && y > h)
            {
                return GetRGB(w, h);
            }
            // left from bitmap
            if (x < 0)
            {
                return GetPixelPWV(0, y);
            }

            // right from bitmap
            if (x > w)
            {
                return GetPixelPWV(w, y);
            }

            // atop of bitmap
            if (y < 0)
            {
                return GetPixelPWH(x, 0);
            }

            // below bitmap
            if (y > h)
            {
                return GetPixelPWH(x, h);
            }

            // exactly on the point
            bool x_close_to_int = CloseToInt(x);
            bool y_close_to_int = CloseToInt(y);

            if (x_close_to_int && y_close_to_int)
            {
                return GetRGB((int)x, (int)y);
            }

            // exactly on the edge
            if (x_close_to_int)
            {
                return GetPixelPWV((int)x, y);
            }
            if (y_close_to_int)
            {
                return GetPixelPWH(x, (int)y);
            }

            // in bitmap
            return GetPixelPWHV(x, y);
        }

        private Color GetPixelSharp(Bitmap from, double x, double y)
        {
            int ixn = (int)x;
            int iyn = (int)y;
            if (ixn < 0) ixn = 0;
            if (iyn < 0) iyn = 0;
            if (ixn > from.Width - 1) ixn = from.Width - 1;
            if (iyn > from.Height - 1) iyn = from.Height - 1;
            return from.GetPixel(ixn, iyn);
        }

        public Color GetPixel(double x, double y) {
            RGB rgb = GetPixelPW(x, y);
            return Color.FromArgb(rgb.R, rgb.G, rgb.B);
        }
    }
}
