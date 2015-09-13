namespace colormap_darning
{
    template <class Color>
    using ContinousColormap = std::function<Color(std::array<double, 2>)>;

    using Projection = std::function<std::array<double, 2>(std::array<double, 2>)>;

    template <class Color>
    ContinousColormap<Color> darn_by_projection(ContinousColormap<Color> src, Projection to_sc, Projection to_cc){
        return [=](double x, double y) -> Color {
            std::array<double, 2> in_singular_cube = Projection({x, y});
            if(in_singular_cube[0] > 0.0 && in_singular_cube[0] < 1.0 && in_singular_cube[1] > 0.0 && in_singular_cube[1] < 1.0){
                auto col_top    = src( to_cc({in_singular_cube[0], 0.0}) );
                auto col_bottom = src( to_cc({in_singular_cube[0], 1.0}) );
                auto col_left   = src( to_cc({0.0, in_singular_cube[1]}) );
                auto col_right  = src( to_cc({1.0, in_singular_cube[1]}) );

                double tx_ = 1.0 / in_singular_cube[0];
                double fx_ = 1.0 / (1.0 - in_singular_cube[0]);
                double ty_ = 1.0 / in_singular_cube[1];
                double fy_ = 1.0 / (1.0 - in_singular_cube[1]);
                return (col_left * tx_ + col_right * fx_ + col_top * ty_ + col_bottom * fy_) * (1.0 / (tx_ + fx_ + ty_ + fy_));
            }else{
                return src({x, y});
            }
        };
    }
}
