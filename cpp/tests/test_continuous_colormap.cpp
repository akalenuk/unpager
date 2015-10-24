#include "../continuous_colormap.hpp"

#include "html_plotting.hpp"
#include "pm5544.hpp"

#include <iostream>
#include <fstream>

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

int main(){
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
    std::fstream fs;
    fs.open("tests/test_continuous_colormap.html", std::fstream::out);
    fs << html_plotting::table_from_canvas(canvas3);
    fs.close();

    std::cout << " + Coninuous colormap is fine." << std::endl;
    return 0;
}
