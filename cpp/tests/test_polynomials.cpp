#include "../linear_equations.hpp"
#include "../polynomials.hpp"

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
    auto pa = std::array<std::array<double, 7>, 7> {{0}};
    auto pb = std::array<double, 7> {0};
    auto px = std::array<double, 7> {0};

    pa[0] = polynoms::construction::p<7>(-1.0);            pb[0] = 0.0;
    pa[1] = polynoms::construction::p<7>(0.0);             pb[1] = 1.0;
    pa[2] = polynoms::construction::p<7>(1.0);             pb[2] = 0.0;
    pa[3] = polynoms::construction::dp<7>(-1.0, 1);        pb[3] = 0.0;
    pa[4] = polynoms::construction::dp<7>(0.0, 1);         pb[4] = 0.0;
    pa[5] = polynoms::construction::dp<7>(1.0, 1);         pb[5] = 0.0;
    pa[6] = polynoms::construction::ip<7>(-1.0, 1.0, 1);   pb[6] = 1.0;

    bool le_ok = linear_equations::iterative_projections::solve<7>(pa, pb, px);
    auto pol2 = polynoms::make_into_function<7>(px);

    // plot
    html_plotting::HtmlCanvas<256, 256> canvas;
    html_plotting::plot_f_on_canvas(canvas, pol, 0.0, 10.0, 0.0, 10.0, "#00ff00");
    html_plotting::plot_xy_on_canvas(canvas, v, 0.0, 10.0, 0.0, 10.0, "#ff0000");
    canvas[0][0] = "#000000";

    html_plotting::HtmlCanvas<256, 256> canvas2;
    html_plotting::plot_f_on_canvas(canvas2, pol2, -1.0, 1.0, -1.0, 1.0, "#00ff00");

    std::fstream fs;
    fs.open("tests/test_polynomials.html", std::fstream::out);
    fs << html_plotting::wrap_in_limits(html_plotting::table_from_canvas(canvas), 0, 10, 0, 10);
    fs << "<br />\n<br /n>\n";
    fs << html_plotting::wrap_in_limits(html_plotting::table_from_canvas(canvas2), -1.0, 1.0, -1.0, 1.0);
    fs.close();

    std::cout << " + Polynomial approximation and construction is fine." << std::endl;
}
