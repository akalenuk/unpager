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
using System.Diagnostics;

namespace WindowsFormsApplication1 {
    class Triangulation {
        static double sq(double x1, double y1, double x2, double y2, double x3, double y3) {
            return Vector.cross(new double[2][] { new double[] { x2 - x1, y2 - y1, 0.0 }, new double[] { x3 - x1, y3 - y1, 0.0 } })[2];
        }

        static bool in_tri(double x, double y, int i, int[][] tris, double[][] xs) {
            double x1 = xs[tris[i][0]][0];
            double y1 = xs[tris[i][0]][1];
            double x2 = xs[tris[i][1]][0];
            double y2 = xs[tris[i][1]][1];
            double x3 = xs[tris[i][2]][0];
            double y3 = xs[tris[i][2]][1];
            if (x < Math.Min(Math.Min(x1, x2), x3)) return false;
            if (x > Math.Max(Math.Max(x1, x2), x3)) return false;
            if (y < Math.Min(Math.Min(y1, y2), y3)) return false;
            if (y > Math.Max(Math.Max(y1, y2), y3)) return false;
            double S =sq(x1,y1, x2,y2, x3,y3);
            double s1=sq(x,y, x2,y2, x3,y3);
            double s2=sq(x1,y1, x,y, x3,y3);
            double s3=sq(x1,y1, x2,y2, x,y);
            if(Scalar.semi_equal((S - s1 - s2 - s3), 0.0)) return true;
            return false;
        }

        static bool crosses(int i1, int i2, int j1, int j2, double[][] xs) { 
            double ta = (xs[i2][0]-xs[i1][0])*(xs[j1][1]-xs[j2][1]) - (xs[j1][0]-xs[j2][0])*(xs[i2][1]-xs[i1][1]);
            double tb = (xs[j1][0]-xs[j2][0])*(xs[i2][1]-xs[i1][1]) - (xs[i2][0]-xs[i1][0])*(xs[j1][1]-xs[j2][1]);
            if (Scalar.semi_equal(ta, 0.0) || Scalar.semi_equal(tb, 0.0)) return false;
            double a = ( (xs[j1][1]-xs[j2][1])*(xs[j1][0]-xs[i1][0]) - (xs[j1][0]-xs[j2][0])*(xs[j1][1]-xs[i1][1]) ) / ta;
            double b = ( (xs[i2][1]-xs[i1][1])*(xs[j1][0]-xs[i1][0]) - (xs[i2][0]-xs[i1][0])*(xs[j1][1]-xs[i1][1]) ) / tb;
            if (a < 0 || a > 1.0 || b < 0 || b > 1.0) return false;
            return true;
        }

        static double d(int a, int b, double[][] xs){
            return Math.Sqrt(Math.Pow(xs[a][0] - xs[b][0], 2) + Math.Pow(xs[a][1] - xs[b][1], 2));
        }
    

        static double forma(int i1, int i2, int i3, double[][] xs) {
            double the_forma = 0.0;
            the_forma = Math.Abs(Math.Min(d(i1, i2, xs), d(i2, i3, xs)) / Math.Max(d(i1, i2, xs), d(i2, i3, xs)));
            the_forma += Math.Abs(Math.Min(d(i2, i3, xs), d(i3, i1, xs)) / Math.Max(d(i2, i3, xs), d(i3, i1, xs)));
            the_forma += Math.Abs(Math.Min(d(i3, i1, xs), d(i1, i2, xs)) / Math.Max(d(i3, i1, xs), d(i1, i2, xs)));
            return the_forma;
        }
        /*
def triangulate(xs):
    tris=[[0,0,0]]
    icnt=0
    found=1
    xcnt=len(xs)

    while found>0:
        found=0
        for m in range(30,-1,-1):
            for i in range(xcnt-2):
                for j in range(i+1, xcnt-1) :
                    for k in range(j+1, xcnt):
                        tris[icnt][0]=i
                        tris[icnt][1]=j
                        tris[icnt][2]=k

                        is_simple=True
                        for l in range(0, xcnt):
                            if l!=i and l!=j and l!=k:
                                if in_tri(xs[l][0], xs[l][1], icnt,  tris, xs):
                                    is_simple=False

                        in_list=False
                        for l in range(icnt):
                            if tris[l][0]==i and tris[l][1]==j and tris[l][2]==k:
                                in_list=True

                        it_crosses=False
                        for l in range(icnt):
                            if(crosses(tris[l][0], tris[l][1],   tris[icnt][0], tris[icnt][1],  xs)): it_crosses=True
                            if(crosses(tris[l][1], tris[l][2],   tris[icnt][0], tris[icnt][1],  xs)): it_crosses=True
                            if(crosses(tris[l][2], tris[l][0],   tris[icnt][0], tris[icnt][1],  xs)): it_crosses=True

                            if(crosses(tris[l][0], tris[l][1],   tris[icnt][1], tris[icnt][2],  xs)): it_crosses=True
                            if(crosses(tris[l][1], tris[l][2],   tris[icnt][1], tris[icnt][2],  xs)): it_crosses=True
                            if(crosses(tris[l][2], tris[l][0],   tris[icnt][1], tris[icnt][2],  xs)): it_crosses=True

                            if(crosses(tris[l][0], tris[l][1],   tris[icnt][2], tris[icnt][0],  xs)): it_crosses=True
                            if(crosses(tris[l][1], tris[l][2],   tris[icnt][2], tris[icnt][0],  xs)): it_crosses=True
                            if(crosses(tris[l][2], tris[l][0],   tris[icnt][2], tris[icnt][0],  xs)): it_crosses=True

                        if not in_list and is_simple and not it_crosses:
                            if forma(tris[icnt][0], tris[icnt][1], tris[icnt][2],  xs)>=0.1*m:
                                icnt+=1
                                tris+=[[0,0,0]]
                                found+=1
    
    return tris[:-1]
         */
    }
}
