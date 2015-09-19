#include "continuous_colormap.hpp"
#include "linear_equations.hpp"
#include "polynoms.hpp"
#include "projection.hpp"
#include "colormap_darning.hpp"
#include "html_plotting.hpp"

#include <iostream>
#include <assert.h>
#include <fstream>

int zero(std::array<int, 2> ji){
    return ji[0] != ji[1] ? 0 : 0;
}


int main()
{
    auto a2 = std::array<std::array<double, 3>, 3>{{{1.0, 0.0, 0.0}, {2.0, 1.0, 0.0}, {3.0, 2.0, 1.0}}};
    auto b2 = std::array<double, 3>{1.0, 4.0, 9.3};
    auto x2 = std::array<double, 3>{0.0, 0.0, 0.0};
    auto le2 = linear_equations::solve<3>(a2, b2, x2);
    auto le2_ok = linear_equations::verify<3>(a2, b2, x2);
    assert(le2 && le2_ok);

    auto colormap = continuous_colormap::make_from_discrete<int>(0,0,zero);
    std::cout << colormap({0.0, 0.0}) << std::endl;

    auto v = std::vector< std::array<double, 2> > {{2.0, 4.0}, {6.0, 4.0}, {4.0, 2.0}};
    auto x3 = std::array<double, 3>{0};
    polynoms::approximate<3>(v, linear_equations::solve<3>, x3);
    auto pol = polynoms::make_into_function<3>(x3);
    std::cout << pol(0.0) << std::endl;

    html_plotting::HtmlCanvas<100, 100> canvas;
    canvas[99][99] = "#ff00ff";
    html_plotting::plot_f_on_canvas<100, 100>(canvas, pol, 0.0, 10.0, 0.0, 10.0, "#00ff00");
    html_plotting::plot_xy_on_canvas<100, 100>(canvas, v, 0.0, 10.0, 0.0, 10.0, "#ff0000");
    std::fstream fs;
    fs.open("test.html", std::fstream::out);
    fs << html_plotting::wrap_in_limits(html_plotting::table_from_canvas<100, 100>(canvas), 0, 10, 0, 10);
    fs.close();

    std::cout << "Ok" << std::endl;
    return 0;
}
