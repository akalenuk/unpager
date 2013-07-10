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
    // TRY: stochastic interpolation
    class ContinuousBitmap  // Inherite from Bitmap?
    {
        public enum Interpolation { 
            None,
            PiecewiseWeight
        }
        
        private Interpolation cur_interpolation = Interpolation.None;
        
        private Bitmap bitmap;
        public int Width    // really calls for inheritance!
        {
            get { return bitmap.Width; }
        }
        public int Height
        {
            get { return bitmap.Height; }
        }

        public ContinuousBitmap(Bitmap source) {
            bitmap = new Bitmap( source );
        }

        public void ChooseInterpolation(Interpolation method){
            cur_interpolation = method;
        }

        private double Fract(double x)
        {
            return x - Math.Floor(x);
        }

        private Color GetPixelPWH(Bitmap from, double x, int y)
        {
            double t = Fract(x);
            double f = 1 - t;
            Color left = from.GetPixel((int)x, (int)y);
            if (t == 0.0) return left;
            Color right = from.GetPixel((int)x + 1, (int)y);
            if (f == 0.0) return right;
            double d = 1.0 / (1.0 / t + 1.0 / f);
            double r = ((double)left.R / t + (double)right.R / f) * d;
            double g = ((double)left.G / t + (double)right.G / f) * d;
            double b = ((double)left.B / t + (double)right.B / f) * d;

            return Color.FromArgb((int)r, (int)g, (int)b);
        }

        private Color GetPixelPWV(Bitmap from, int x, double y)
        {
            double t = Fract(y);
            double f = 1 - t;
            Color top = from.GetPixel((int)x, (int)y);
            if (t == 0) return top;
            Color bottom = from.GetPixel((int)x, (int)y + 1);
            if (f == 0) return bottom;
            double d = 1.0 / (1.0 / t + 1.0 / f);
            double r = ((double)top.R / t + (double)bottom.R / f) * d;
            double g = ((double)top.G / t + (double)bottom.G / f) * d;
            double b = ((double)top.B / t + (double)bottom.B / f) * d;

            return Color.FromArgb((int)r, (int)g, (int)b);
        }

        private Color GetPixelPWHV(Bitmap from, double x, double y)
        {
            double tx = Fract(x);
            double fx = 1 - tx;
            double ty = Fract(y);
            double fy = 1 - ty;
            Color left_top = from.GetPixel((int)x, (int)y);
            Color right_top = from.GetPixel((int)x + 1, (int)y);
            Color right_bottom = from.GetPixel((int)x + 1, (int)y + 1);
            Color left_bottom = from.GetPixel((int)x, (int)y + 1);
            double d = 1.0 / (1.0 / (tx * ty) + 1.0 / (fx * ty) + 1.0 / (fx * fy) + 1.0 / (tx * fy));
            double r = (left_top.R / (tx * ty) + right_top.R / (fx * ty) + right_bottom.R / (fx * fy) + left_bottom.R / (tx * fy)) * d;
            double g = (left_top.G / (tx * ty) + right_top.G / (fx * ty) + right_bottom.G / (fx * fy) + left_bottom.G / (tx * fy)) * d;
            double b = (left_top.B / (tx * ty) + right_top.B / (fx * ty) + right_bottom.B / (fx * fy) + left_bottom.B / (tx * fy)) * d;

            return Color.FromArgb((int)r, (int)g, (int)b);
        }

        private Color GetPixelPW(Bitmap from, double x, double y)
        {
            // corners
            if (x < 0 && y < 0)
            {
                return from.GetPixel(0, 0);
            }
            int w = from.Width - 1;
            if (x > w && y < 0)
            {
                return from.GetPixel(w, 0);
            }
            int h = from.Height - 1;
            if (x < 0 && y > h)
            {
                return from.GetPixel(0, h);
            }
            if (x > w && y > h)
            {
                return from.GetPixel(w, h);
            }
            if (x < 0)
            {
                return GetPixelPWV(from, 0, y);
            }
            if (x > w)
            {
                return GetPixelPWV(from, w, y);
            }
            if (y < 0)
            {
                return GetPixelPWH(from, x, 0);
            }
            if (y > h)
            {
                return GetPixelPWH(from, x, h);
            }
            if (Fract(x) == 0.0 && Fract(y) == 0.0)
            {
                return from.GetPixel((int)x, (int)y);
            }
            if (Fract(x) == 0.0)
            {
                return GetPixelPWV(from, (int)x, y);
            }
            if (Fract(y) == 0.0)
            {
                return GetPixelPWH(from, x, (int)y);
            }
            return GetPixelPWHV(from, x, y);
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
            switch(cur_interpolation) {
                case Interpolation.PiecewiseWeight:
                    return GetPixelPW(bitmap, x, y);
                default:
                    return GetPixelSharp(bitmap, x, y);
            }
        }
    }
}
