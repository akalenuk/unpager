#include <array>
#include <cmath>
#include <vector>
#include <array>
#include <algorithm>
#include <functional>

namespace polynoms
{
    constexpr double INITIAL_MULTIPLIER = 1.0;
    constexpr double MULTIPLIER_MULTIPLIER = 10.0;

    template <int N>
    using Vector = std::array<double, N>;

    template <int N>
    using Matrix = std::array<std::array<double, N>, N>;

    template <int N>
    using Solver = std::function<bool(const Matrix<N>&, const Vector<N>&, Vector<N>&)>;

    using Data = std::vector<std::pair<double, double> >;

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

    static bool good_enough(const Data& input, std::function<double(double)> pol, double u){
        for(auto xy : input){
            if(std::abs(pol(xy.first) - xy.second) > u) return false;
        }
        return true;
    }

    template <int N>
    bool approximate_and_interpolate(const Data& ap_input, const Data& ip_input, Solver<N> solver, double u, Vector<N>& output){
        double multiplier = INITIAL_MULTIPLIER;
        std::function<double(double)> pol;
        while(true){
            std::array<std::array<double, N>, N> a{0};
            std::array<double, N> b{0};
            for (int i = 0; i < N; i++){
                for (int j = 0; j < N; j++){
                    for(auto xy : ap_input){
                        a[i][j] += std::pow(xy.first, i + j);
                    }
                    for(auto xy : ip_input){
                        a[i][j] += std::pow(xy.first, i + j) * multiplier;
                    }
                }
                for(auto xy : ap_input){
                    b[i] += xy.second * std::pow(xy.first, i);
                }
                for(auto xy : ip_input){
                    b[i] += xy.second * std::pow(xy.first, i) * multiplier;
                }
            }
            if(!solver(a, b, output)) return false;
            pol = make_into_function<N>(output);
            if(good_enough(ip_input, pol, u)){
                return true;
            }else{
                multiplier *= MULTIPLIER_MULTIPLIER;
            }
        }
    }

    template <int N>
    bool approximate(const Data& input, Solver<N> solver, Vector<N>& output){
        Data no_data;
        return approximate_and_interpolate<N>(input, no_data, solver, 0.0, output);
    }
}
