#include "continuous_colormap.hpp"
#include "linear_equations.hpp"
#include <assert.h>

int zero(int w, int h){
  return 0;
}

int main(){
  auto colormap = continuous_colormap::make_from_discrete<int>(0,0,zero);
  return colormap(0.0, 0.0);
  
  auto a2 = std::array<std::array<double, 2>, 2>{{{5.0, 2.5}, {7.0, 3.3}}};
  auto b2 = std::array<double, 2>{6.0, 1.5};
  auto x2 = std::array<double, 2>{};
  auto le2 = linear_equations::solve<2>(a2, b2, x2);
  auto le2_ok = linear_equations::verify<2>(a2, b2, x2);
  assert(le2 && le2_ok);   
}
