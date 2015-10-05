#include "continuous_colormap.hpp"
#include "linear_equations.hpp"
#include "polynoms.hpp"
#include "projection.hpp"
#include "colormap_darning.hpp"
#include "html_plotting.hpp"

#include "pm5544.hpp"

#include <iostream>
#include <assert.h>
#include <fstream>

int zero(std::array<int, 2> ji){
    return ji[0] != ji[1] ? 0 : 0;
}

using Double_3 = std::array<double, 3>;
Double_3 operator+ (Double_3 a, Double_3 b){return {a[0] + b[0], a[1] + b[1], a[2] + b[2]};}
Double_3 operator* (Double_3 a, double b){return {a[0] * b, a[1] * b, a[2] * b};}

Double_3 pm5544(std::array<int, 2> xy){
    return {static_cast<double>(PM5544.pixel_data[3 * (xy[1] * PM5544.width + xy[0])]),
        static_cast<double>(PM5544.pixel_data[3 * (xy[1] * PM5544.width + xy[0]) + 1]),
        static_cast<double>(PM5544.pixel_data[3 * (xy[1] * PM5544.width + xy[0]) + 2])};
}

std::string html(Double_3 a){
    std::string ret = "#";
    constexpr std::array<char, 16> HEX {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
    for(double c : a){
        uint8_t ic = static_cast<uint8_t>(c);
        ret += HEX[ic / 16 % 16];
        ret += HEX[ic % 16];
    }
    return ret;
}

int main()
{
    // equations
    auto a2 = std::array<std::array<double, 3>, 3>{{{1.0, 0.0, 0.0}, {2.0, 1.0, 0.0}, {3.0, 2.0, 1.0}}};
    auto b2 = std::array<double, 3>{1.0, 4.0, 9.3};
    auto x2 = std::array<double, 3>{0.0, 0.0, 0.0};
    auto le2 = linear_equations::solve<3>(a2, b2, x2);
    auto le2_ok = linear_equations::verify<3>(a2, b2, x2);
    assert(le2 && le2_ok);

    // colormap simple
    auto colormap = continuous_colormap::make_from_discrete<int>(0,0,zero);
    std::cout << colormap({0.0, 0.0}) << std::endl;

    // polynomial approximation
    auto v = std::vector< std::array<double, 2> > {{2.0, 4.0}, {6.0, 4.0}, {4.0, 2.0}};
    auto x3 = std::array<double, 3>{0};
    polynoms::approximate<3>(v, linear_equations::solve<3>, x3);
    auto pol = polynoms::make_into_function<3>(x3);
    std::cout << pol(0.0) << std::endl;

    // polynomial construction
    auto pa = std::array<std::array<double, 3>, 3> {{0}};
    auto pb = std::array<double, 3> {0};
    auto px = std::array<double, 3> {0};

    pa[0] = polynoms::construction::p<3>(-1.0);            pb[0] = 1.0;
    pa[1] = polynoms::construction::dp<3>(0.0, 1);         pb[1] = 0.0;
    pa[2] = polynoms::construction::ip<3>(-1.0, 1.0, 1);   pb[2] = 1.0;

    bool le3_ok = linear_equations::solve<3>(pa, pb, px);
    auto pol2 = polynoms::make_into_function<3>(px);

    // plot
    html_plotting::HtmlCanvas<256, 256> canvas;
    html_plotting::plot_f_on_canvas(canvas, pol, 0.0, 10.0, 0.0, 10.0, "#00ff00");
    html_plotting::plot_xy_on_canvas(canvas, v, 0.0, 10.0, 0.0, 10.0, "#ff0000");
    canvas[0][0] = "#000000";

    html_plotting::HtmlCanvas<256, 256> canvas2;
    html_plotting::plot_f_on_canvas(canvas2, pol2, -1.0, 1.0, -1.0, 1.0, "#00ff00");

    std::fstream fs;
    fs.open("test.html", std::fstream::out);
    fs << html_plotting::wrap_in_limits(html_plotting::table_from_canvas(canvas), 0, 10, 0, 10);
    fs << "<br />\n<br /n>\n";
    fs << html_plotting::wrap_in_limits(html_plotting::table_from_canvas(canvas2), -1.0, 1.0, -1.0, 1.0);
    fs.close();

    // pm5544
    constexpr double SCALE = 0.3;
    constexpr size_t new_w = static_cast<size_t>(PM5544.width*SCALE);
    constexpr size_t new_h = static_cast<size_t>(PM5544.height*SCALE);
    html_plotting::HtmlCanvas<new_w, new_h> canvas3;
    auto testcard_colormap = continuous_colormap::make_from_discrete<Double_3>(PM5544.width, PM5544.height, pm5544);
    for(size_t y = 0; y < new_h; y++){
        for(size_t x = 0; x < new_w; x++){
            canvas3[y][x] = html(testcard_colormap({x / SCALE, y / SCALE}));
        }
    }
    fs.open("pm5544.html", std::fstream::out);
    fs << html_plotting::table_from_canvas(canvas3);
    fs.close();

    std::cout << "Ok" << std::endl;
    return 0;
}
