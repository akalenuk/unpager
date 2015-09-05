#include <array>
#include <cmath>
#include <vector>
#include <array>
#include <algorithm>
#include <functional>

namespace polynoms
{
    template <int N>
    using Vector = std::array<double, N>;

    template <int N>
    using Matrix = std::array<std::array<double, N>, N>;

    template <int N>
    using Solver = std::function<bool(const Matrix<N>&, const Vector<N>&, Vector<N>&)>;

    template <int N>
    bool approximate(const std::vector<std::pair<double, double> >& input, Solver<N> solver, Vector<N>& output){
        std::array<std::array<double, N>, N> a{0};
        std::array<double, N> b{0};
        for (int i = 0; i < N; i++){
            for (int j = 0; j < N; j++){
                for(auto xy : input){
                    a[i][j] += std::pow(xy.first, i + j);
                }
            }
            for(auto xy : input){
                b[i] += xy.second * std::pow(xy.first, i);
            }
        }
        return solver(a, b, output);
    }

    template <int N>
    std::function<double(double)> make_into_function(std::array<double, N> coefficients){
        return [=](double x) -> double{
            double ret = 0.0;
            for(int i = 0; i < N; i++){
                ret += coefficients[i] * std::pow(x, i);
            }
            return ret;
        };
    }
}
