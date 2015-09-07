#include <cmath>
#include <limits>
#include <functional>
#include <array>

namespace linear_equations  // not waiting for static if :-)
{
    constexpr double SMALL_ENOUGH = std::sqrt(std::numeric_limits<double>::epsilon());

    template <int I, int J, int K, int N>
    inline static double aij(const std::array<std::array<double, N>, N>& a){
        //std::array<int, N> unused = {0};
        if(K == N) return a[I][J];
        return aij<I, J, K+(K<N), N>(a) * aij<K, K, K+(K<N), N>(a) - aij<I, K, K+(K<N), N>(a) * aij<K, J, K+(K<N), N>(a);
    }

    template <int I, int K, int N>
    inline static double bi(const std::array<std::array<double, N>, N>& a, const std::array<double, N>& b){
        //std::array<int, N> unused = {0};
        if(K == N) return b[I];
        return aij<K, K, K+(K<N), N>(a) * bi<I, K+(K<N), N>(a, b) - aij<I, K, K+(K<N), N>(a) * bi<K, K+(K<N), N>(a, b);
    }

    template <int J, int I, int N>
    inline static void d_for(double& d, const std::array<std::array<double, N>, N>& a, std::array<double, N>& x){
        //std::array<int, N> unused = {0};
        if(J < I){
            d -= aij<I, J, I+(J<I), N>(a) * x[J];
            d_for<J+(J<I), I, N>(d, a, x);
        }
    }

    template <int I, int N>
    inline static double di(const std::array<std::array<double, N>, N>& a, const std::array<double, N>& b, std::array<double, N>& x){
        //std::array<int, N> unused = {0};
        double d = bi<I, I+1, N>(a, b);
        d_for<0, I, N>(d, a, x);
        return d;
    }

    template <int I, int N>
    inline static bool x_for(const std::array<std::array<double, N>, N>& a, const std::array<double, N>& b,    std::array<double, N>& x){
        //std::array<int, N> unused = {0};
        if(I < N){
            double d = di<I, N>(a, b, x);
            if(std::abs(d) < SMALL_ENOUGH) return false;
            x[I] = d / aij<I, I, I+1, N>(a);
            return x_for<I+(I<N), N>(a, b, x);
        }
        return true;
    }

    template <int N>
    bool solve_s(const std::array<std::array<double, N>, N>& a, const std::array<double, N>& b,    std::array<double, N>& x){
        return x_for<0, N>(a, b, x);
    }
}


#include <iostream>

using namespace std;

int main()
{
    auto a2 = std::array<std::array<double, 2>, 2>{{{5.0, 2.5}, {7.0, 3.3}}};
    auto b2 = std::array<double, 2>{6.0, 1.5};
    auto x2 = std::array<double, 2>{0.0, 0.0};
    cin >> a2[0][0] >> a2[0][1] >> a2[1][0] >> a2[1][1] >> b2[0] >> b2[1];
    auto le2 = linear_equations::solve_s<2>(a2, b2, x2);
//    auto le2_ok = linear_equations::verify<2>(a2, b2, x2);
//    assert(le2 && le2_ok);
    cout << "x0 " << x2[0] << "  x1 " << x2[1] << endl;
    return 0;
}
// gcc -std=c++11 -O2 -S main.cpp
