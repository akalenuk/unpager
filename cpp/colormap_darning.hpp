namespace colormap_darning
{
    template <class Color>
    using ContinousColormap = std::function<Color(double, double)>;

    using Projection = std::function<std::array<double, 2>(std::array<double, 2>)>;

    template <class Color>
    ContinousColormap<Color> darn_by_projection(ContinousColormap<Color> before, Projection to_sc, Projection to_cc){
        return [=](double x, double y) -> Color {
            std::array<double, 2> in_singular_cube = Projection({x, y});
            if(in_singular_cube[0] > 0.0 && in_singular_cube[0] < 1.0 && in_singular_cube[1] > 0.0 && in_singular_cube[1] < 1.0){
                auto top = to_cc({in_singular_cube[0], 0.0});
                auto bottom = to_cc({in_singular_cube[0], 1.0});
                auto left = to_cc({0.0, in_singular_cube[1]});
                auto right = to_cc({1.0, in_singular_cube[1]});

                auto col_top = before(top[0], top[1]);
                auto col_bottom = before(bottom[0], bottom[1]);
                auto col_left = before(left[0], left[1]);
                auto col_right = before(right[0], right[1]);

                double tx_ = 1.0 / in_singular_cube[0];
                double fx_ = 1.0 / (1.0 - in_singular_cube[0]);
                double ty_ = 1.0 / in_singular_cube[1];
                double fy_ = 1.0 / (1.0 - in_singular_cube[1]);
                return (col_left * tx_ + col_right * fx_ + col_top * ty_ + col_bottom * fy_) * (1.0 / (tx_ + fx_ + ty_ + fy_));
            }else{
                return before(x, y);
            }
        };
    }
}
