#include "continuous_colormap.hpp"
#include "linear_equations.hpp"
#include "polynoms.hpp"
#include "projection.hpp"
#include "colormap_darning.hpp"

#include <iostream>
#include <assert.h>

int zero(int j, int i){
    return i!=j ? 0 : 0;
}

int main()
{
    auto a2 = std::array<std::array<double, 2>, 2>{{{5.0, 2.5}, {7.0, 3.3}}};
    auto b2 = std::array<double, 2>{6.0, 1.5};
    auto x2 = std::array<double, 2>{0.0, 0.0};
    auto le2 = linear_equations::solve<2>(a2, b2, x2);
    auto le2_ok = linear_equations::verify<2>(a2, b2, x2);
    assert(le2 && le2_ok);

    auto colormap = continuous_colormap::make_from_discrete<int>(0,0,zero);
    return colormap(0.0, 0.0);

    auto v = std::vector< std::pair<double, double> > {{2.0, 4.0}, {6.0, 4.0}, {4.0, 2.0}};
    auto x3 = std::array<double, 3>{0};
    polynoms::approximate<3>(v, linear_equations::solve<3>, x3);
    auto pol = polynoms::make_into_function<3>(x3);
    assert(std::abs(pol(0.0) - 8.0) < 0.000001);

    std::cout << "Ok" << std::endl;
    return 0;
}
