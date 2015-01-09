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
    // TODO: cosider droping this out of code entirely
    class Automatic
    {
        public static List<Point> FindLightingPoints(Bitmap source, int w, int h) {
            List<Point> points = new List<Point>();
            int dw = source.Width / w;
            int dh = source.Height / h;
            for (int i = 0; i < h; i++) {
                for (int j = 0; j < w; j++) {
                    int min_c = 0;
                    int min_ci = 0;
                    int min_cj = 0;
                    for (int ii = 0; ii < dh; ii++) {
                        for (int jj = 0; jj < dw; jj++) {
                            int iii = i * dh + ii;
                            int jjj = j * dw + jj;
                            Color col = source.GetPixel(jjj, iii);
                            int c = col.R + col.G + col.B;
                            if (c > min_c) {
                                min_c = c;
                                min_ci = iii;
                                min_cj = jjj;
                            }
                        }
                    }
                    points.Add(new Point(min_cj, min_ci));
                }
            }
            return points;
        }
    }
}
