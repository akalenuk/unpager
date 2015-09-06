#include <cmath>
#include <algorithm>
#include <functional>

namespace continuous_colormap
{
    constexpr int COLOR_DEPTH = 8;
    constexpr double SMALL_ENOUGH_DXY = 1.0 / std::pow(2, COLOR_DEPTH);

    template<class Color>
    using DiscreteColormap = std::function<Color(int, int)>;  // lambda decays to function pointer only when not enclosing

    template<class Color>
    using ContinousColormap = std::function<Color(double, double)>; // better use array 2 here

    static double fractional(double x){
        return std::fmod(x, 1.0);
    }

    static bool close_to_grid(double x){
        return std::min(x - std::floor(x), std::ceil(x) - x) < SMALL_ENOUGH_DXY;
    }

    // this doesn't have to be static. It doesn't matter unless this links separately.
    template <class Color>
    static Color color_on_horizontal_edge(double x, int y, DiscreteColormap<Color> get_color){
        double t = fractional(x);
        double f = 1.0 - t;
        int int_x = static_cast<int>(x);
        Color left = get_color(int_x, y);
        if (t <= 0.0) return left;
        Color right = get_color(int_x + 1, y);
        if (f <= 0.0) return right;
        double t_ = 1.0 / t;
        double f_ = 1.0 / f;
        return (left * t_ + right * f_) / (t_ + f_);
    }

    template <class Color>
    static Color color_on_vertical_edge(double x, int y, DiscreteColormap<Color> get_color){
        double t = fractional(y);
        double f = 1.0 - t;
        int int_y = static_cast<int>(y);
        Color top = get_color(x, int_y);
        if (t <= 0.0) return top;
        Color bottom = get_color(x, int_y + 1);
        if (f <= 0.0) return bottom;
        double t_ = 1.0 / t;
        double f_ = 1.0 / f;
        return (top * t_ + bottom * f_) / (t_ + f_);
    }

    template <class Color>
    static Color color_in_grid_cell(double x, double y, DiscreteColormap<Color> get_color){
        double tx = fractional(x);
        double fx = 1.0 - tx;
        double ty = fractional(y);
        double fy = 1.0 - ty;
        int int_x = static_cast<int>(x);
        int int_y = static_cast<int>(y);
        Color left_top = get_color(int_x, int_y);
        Color right_top = get_color(int_x + 1, int_y);
        Color left_bottom = get_color(int_x, int_y + 1);
        Color right_bottom = get_color(int_x + 1, int_y + 1);
        double tt_ = 1.0 / (tx * ty);
        double ft_ = 1.0 / (fx * ty);
        double tf_ = 1.0 / (tx * fy);
        double ff_ = 1.0 / (fx * fy);
        return (left_top * ff_ + right_top * ft_ + left_bottom * tf_ + right_bottom * ff_) /
            (tt_ + tf_ + ft_ + ff_);
    }

    template<class Color>
    ContinousColormap<Color> make_from_discrete(int w, int h, DiscreteColormap<Color> get_color){
        return [w, h, get_color](double x, double y) -> Color {
            if (x <= 0 && y <= 0) return get_color(0, 0);
            if (x >= w - 1 && y <= 0) return get_color(w - 1, 0);
            if (x <= 0 && y >= h - 1) return get_color(0, h - 1);
            if (x >= w - 1 && y >= h - 1) return get_color(w - 1, h - 1);

            if (x <= 0) return color_on_vertical_edge(0, y, get_color);
            if (x >= w - 1) return color_on_vertical_edge(w - 1, y, get_color);

            if (y <= 0) return color_on_horizontal_edge(x, 0, get_color);
            if (y >= h - 1) return color_on_horizontal_edge(x, h - 1, get_color);

            bool x_close_to_edge = close_to_grid(x);
            bool y_close_to_edge = close_to_grid(y);
            int int_x = static_cast<int>(x);
            int int_y = static_cast<int>(y);

            if (x_close_to_edge && y_close_to_edge) return get_color(int_x, int_y);
            if (x_close_to_edge) return color_on_vertical_edge(int_x, y, get_color);
            if (y_close_to_edge) return color_on_horizontal_edge(x, int_y, get_color);

            return color_in_grid_cell(x, y, get_color);
        };
    }
}
