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
        /*
        def in_tri(x, y, i,  tris, xs):
    x1=xs[tris[i][0]][0]
    y1=xs[tris[i][0]][1]
    x2=xs[tris[i][1]][0]
    y2=xs[tris[i][1]][1]
    x3=xs[tris[i][2]][0]
    y3=xs[tris[i][2]][1]
    if x<min(x1,x2,x3):
        return False
    if x>max(x1,x2,x3):
        return False
    if y<min(y1,y2,y3):
        return False
    if y>max(y1,y2,y3):
        return False
    S=sq(x1,y1, x2,y2, x3,y3);
    s1=sq(x,y, x2,y2, x3,y3);
    s2=sq(x1,y1, x,y, x3,y3);
    s3=sq(x1,y1, x2,y2, x,y);
    if abs(S-s1-s2-s3)<EPS:
        return True
    return False


def crosses(i1,i2, j1,j2,  xs):
    ta=(xs[i2][0]-xs[i1][0])*(xs[j1][1]-xs[j2][1]) - (xs[j1][0]-xs[j2][0])*(xs[i2][1]-xs[i1][1])
    tb=(xs[j1][0]-xs[j2][0])*(xs[i2][1]-xs[i1][1]) - (xs[i2][0]-xs[i1][0])*(xs[j1][1]-xs[j2][1])
    if ta==0 or tb==0:
        return False
    a=( (xs[j1][1]-xs[j2][1])*(xs[j1][0]-xs[i1][0]) - (xs[j1][0]-xs[j2][0])*(xs[j1][1]-xs[i1][1]) ) / ta
    b=( (xs[i2][1]-xs[i1][1])*(xs[j1][0]-xs[i1][0]) - (xs[i2][0]-xs[i1][0])*(xs[j1][1]-xs[i1][1]) ) / tb
    if 1>a>0 and 1>b>0:
        return True;
    return False;


def forma(i1, i2, i3,  xs):
    the_forma = abs( min(d(i1,i2, xs), d(i2,i3, xs)) / max(d(i1,i2, xs), d(i2,i3, xs)) )
    the_forma+= abs( min(d(i2,i3, xs), d(i3,i1, xs)) / max(d(i2,i3, xs), d(i3,i1, xs)) )
    the_forma+= abs( min(d(i3,i1, xs), d(i1,i2, xs)) / max(d(i3,i1, xs), d(i1,i2, xs)) )
    return the_forma


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
