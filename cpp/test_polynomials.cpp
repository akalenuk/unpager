#include "linear_equations.hpp"
#include "polynomials.hpp"
#include "html_plotting.hpp"

#include <iostream>
#include <assert.h>
#include <fstream>

int main(){
    // polynomial approximation
    auto v = std::vector< std::array<double, 2> > {{2.0, 4.0}, {6.0, 4.0}, {4.0, 2.0}};
    auto x3 = std::array<double, 3>{0};
    polynoms::approximate<3>(v, linear_equations::semi_static::solve<3>, x3);
    auto pol = polynoms::make_into_function<3>(x3);
    assert(pol(0.0) == 10.0);

    // polynomial construction
    auto pa = std::array<std::array<double, 3>, 3> {{0}};
    auto pb = std::array<double, 3> {0};
    auto px = std::array<double, 3> {0};

    pa[0] = polynoms::construction::p<3>(-1.0);            pb[0] = 1.0;
    pa[1] = polynoms::construction::dp<3>(0.0, 1);         pb[1] = 0.0;
    pa[2] = polynoms::construction::ip<3>(-1.0, 1.0, 1);   pb[2] = 1.0;

    bool le3_ok = linear_equations::iterative_projections::solve<3>(pa, pb, px);
    auto pol2 = polynoms::make_into_function<3>(px);

    // plot
    html_plotting::HtmlCanvas<256, 256> canvas;
    html_plotting::plot_f_on_canvas(canvas, pol, 0.0, 10.0, 0.0, 10.0, "#00ff00");
    html_plotting::plot_xy_on_canvas(canvas, v, 0.0, 10.0, 0.0, 10.0, "#ff0000");
    canvas[0][0] = "#000000";

    html_plotting::HtmlCanvas<256, 256> canvas2;
    html_plotting::plot_f_on_canvas(canvas2, pol2, -1.0, 1.0, -1.0, 1.0, "#00ff00");

    std::fstream fs;
    fs.open("test_polynomials.html", std::fstream::out);
    fs << html_plotting::wrap_in_limits(html_plotting::table_from_canvas(canvas), 0, 10, 0, 10);
    fs << "<br />\n<br /n>\n";
    fs << html_plotting::wrap_in_limits(html_plotting::table_from_canvas(canvas2), -1.0, 1.0, -1.0, 1.0);
    fs.close();

    std::cout << " + Polynomial approximation and construction is fine." << std::endl;
}
