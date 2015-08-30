#include "continuous_colormap.hpp"
#include "linear_equations.hpp"


int zero(int w, int h){
  return 0;
}

int main(){
  auto colormap = continuous_colormap::make_from_discrete<int>(0,0,zero);
  return colormap(0.0, 0.0);
}
