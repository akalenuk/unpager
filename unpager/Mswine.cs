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
using System.Diagnostics;

namespace WindowsFormsApplication1 {
    class Mswine {

        public delegate double WeightFunction(double x);
        public delegate double BasisFunction(double[] x);

        /* Inverse linear weight function */
        double s_l(double x) {
            return 1.0 / x;
        }

        /* Inverse quadratic weight function */
        double s_q(double x) {
            return 1.0 / (x*x);
        }

        /* Weight function for 'vx' vector
            Args:
                vx: vector to weight.
                s_k: scalar weight function.
            Returns:
                Weight of the vector        */        double v_k(double[] vx, WeightFunction s_x){            int DIMM = vx.Length;            Debug.Assert(DIMM > 0);            double r = s_x(vx[0]);            for(int i = 1; i < DIMM; i++){                r *= s_x(vx[i]);            }            return r;        }

        /*        Determines a list of constant basis functions        Args:            xyz: Point set.            f: Corresponding array of function values.            Sx: List of simplexes         Returns:
            List of basis functions
        */
        BasisFunction[] get_constant_functions(double[][] xyz, double[] f, double[][] Sx) {
            int f_len = f.Length;
            BasisFunction[] ret = new BasisFunction[f_len];
            for (int i = 0; i < f_len; i++) {
                ret[i] = x => f[i];
            }
            return ret;
        }
    }
}
