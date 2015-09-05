#include <cmath>
#include <limits>
#include <functional>
#include <array>

namespace linear_equations  // waiting for static if
{
	constexpr double SMALL_ENOUGH = std::sqrt(std::numeric_limits<double>::epsilon());

    template <int I, int J, int K, int N>
    inline static double aij(const std::array<std::array<double, N>, N>& a){
        std::array<int, N> unused = {0};
        return a[I][J];
    }

    template <int I, int J, int K, int N>
    inline static double aij(const std::array<std::array<double, N>, N>& a){
        std::array<int, N> unused = {0};
        static_if(K == N) return a[I][J];
        return aij<I, J, K+1, N>(a) * aij<K, K, K+1, N>(a) - aij<I, K, K+1, N>(a) * aij<K, J, K+1, N>(a);
    }

    template <int I, int K, int N>
    inline static double bi(const std::array<std::array<double, N>, N>& a, const std::array<double, N>& b){
        std::array<int, N> unused = {0};
        static_if(K == N) return b[I];
        return aij<K, K, K+1, N>(a) * bi<I, K+1, N>(a, b) - aij<I, K, K+1, N>(a) * bi<K, K+1, N>(a, b);
    }

    template <int J, int I, int N>
    inline static void d_for(double& d, const std::array<std::array<double, N>, N>& a, std::array<double, N>& x){
        std::array<int, N> unused = {0};
        if(J < I){
            d -= aij<I, J, I+1, N>(a) * x[J];
            d_for<J+1, I, N>(d, a, x);
        }
    }

    template <int I, int N>
    inline static double di(const std::array<std::array<double, N>, N>& a, const std::array<double, N>& b, std::array<double, N>& x){
        std::array<int, N> unused = {0};
		double d = bi<I, I+1, N>(a, b);
		d_for<0, I, N>(d, a, x);
		return d;
	}

    template <int I, int N>
    inline static bool x_for(const std::array<std::array<double, N>, N>& a, const std::array<double, N>& b,	std::array<double, N>& x){
        std::array<int, N> unused = {0};
        if(I < N){
            double d = di<I, N>(a, b, x);
            if(std::abs(d) < SMALL_ENOUGH) return false;
            x[I] = d / aij<I, I, I+1, N>(a);
            return x_for<I+1, N>(a, b, x);
        }
        return true;
    }

	template <int N>
	bool solve_s(const std::array<std::array<double, N>, N>& a, const std::array<double, N>& b,	std::array<double, N>& x){
		return x_for<0, N>(a, b, x);
    }
}
