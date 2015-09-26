#include <cmath>
#include <algorithm>
#include <functional>

namespace continuous_colormap
{
    constexpr int COLOR_DEPTH = 8;
    constexpr double SMALL_ENOUGH_DXY = 1.0 / std::pow(2, COLOR_DEPTH);

    template<class Color>
    using DiscreteColormap = std::function<Color(std::array<int, 2>)>;  // lambda decays to function pointer only when not enclosing

    template<class Color>
    using ContinousColormap = std::function<Color(std::array<double, 2>)>;

    template<class Color>
    ContinousColormap<Color> make_from_discrete(int w, int h, DiscreteColormap<Color> get_color){
        auto fractional = [](double x) -> double{
            return std::fmod(x, 1.0);
        };

        auto close_to_int = [](double x) -> bool{
            return std::min(x - std::floor(x), std::ceil(x) - x) < SMALL_ENOUGH_DXY;
        };

        auto color_on_horizontal_edge = [=](double x, int y) -> Color {
            double t = fractional(x);
            double f = 1.0 - t;
            int int_x = static_cast<int>(x);
            Color left = get_color({int_x, y});
            if (t <= 0.0) return left;
            Color right = get_color({int_x + 1, y});
            if (f <= 0.0) return right;
            double t_ = 1.0 / t;
            double f_ = 1.0 / f;
            return (left * t_ + right * f_) * (1.0 / (t_ + f_));
        };

        auto color_on_vertical_edge = [=](int x, double y) -> Color {
            double t = fractional(y);
            double f = 1.0 - t;
            int int_y = static_cast<int>(y);
            Color top = get_color({x, int_y});
            if (t <= 0.0) return top;
            Color bottom = get_color({x, int_y + 1});
            if (f <= 0.0) return bottom;
            double t_ = 1.0 / t;
            double f_ = 1.0 / f;
            return (top * t_ + bottom * f_) * (1.0 / (t_ + f_));
        };

        auto color_in_grid_cell = [=](double x, double y) -> Color {
            double tx = fractional(x);
            double fx = 1.0 - tx;
            double ty = fractional(y);
            double fy = 1.0 - ty;
            int int_x = static_cast<int>(x);
            int int_y = static_cast<int>(y);
            Color left_top = get_color({int_x, int_y});
            Color right_top = get_color({int_x + 1, int_y});
            Color left_bottom = get_color({int_x, int_y + 1});
            Color right_bottom = get_color({int_x + 1, int_y + 1});
            double tt_ = 1.0 / (tx * ty);
            double ft_ = 1.0 / (fx * ty);
            double tf_ = 1.0 / (tx * fy);
            double ff_ = 1.0 / (fx * fy);
            return (left_top * tt_ + right_top * ft_ + left_bottom * tf_ + right_bottom * ff_) *
                (1.0 / (tt_ + tf_ + ft_ + ff_));
        };

        return [=](std::array<double, 2> xy) -> Color {
            auto x = xy[0];
            auto y = xy[1];
            if (x <= 0 && y <= 0) return get_color({0, 0});
            if (x >= w - 1 && y <= 0) return get_color({w - 1, 0});
            if (x <= 0 && y >= h - 1) return get_color({0, h - 1});
            if (x >= w - 1 && y >= h - 1) return get_color({w - 1, h - 1});

            if (x <= 0) return color_on_vertical_edge(0, y);
            if (x >= w - 1) return color_on_vertical_edge(w - 1, y);

            if (y <= 0) return color_on_vertical_edge(x, 0);
            if (y >= h - 1) return color_on_horizontal_edge(x, h - 1);

            bool x_close_to_edge = close_to_int(x);
            bool y_close_to_edge = close_to_int(y);
            int int_x = static_cast<int>(x);
            int int_y = static_cast<int>(y);

            if (x_close_to_edge && y_close_to_edge) return get_color({int_x, int_y});
            if (x_close_to_edge) return color_on_vertical_edge(int_x, y);
            if (y_close_to_edge) return color_on_horizontal_edge(x, int_y);

            return color_in_grid_cell(x, y);
        };
    }
}
