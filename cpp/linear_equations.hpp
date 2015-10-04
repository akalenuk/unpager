#include <cmath>
#include <limits>
#include <functional>
#include <array>

namespace linear_equations{

	constexpr double SMALL_ENOUGH = std::sqrt(std::numeric_limits<double>::epsilon());

    template <int I, int J, int K, int N>
    inline static double aij(const std::array<std::array<double, N>, N>& a){
        if(K == N) return a[I][J];
        return aij<I, J, K+(K<N), N>(a) * aij<K, K, K+(K<N), N>(a) - aij<I, K, K+(K<N), N>(a) * aij<K, J, K+(K<N), N>(a);
    }

    template <int I, int K, int N>
    inline static double bi(const std::array<std::array<double, N>, N>& a, const std::array<double, N>& b){
        if(K == N) return b[I];
        return aij<K, K, K+(K<N), N>(a) * bi<I, K+(K<N), N>(a, b) - aij<I, K, K+(K<N), N>(a) * bi<K, K+(K<N), N>(a, b);
    }

    template <int J, int I, int N>
    inline static void d_for(double& d, const std::array<std::array<double, N>, N>& a, std::array<double, N>& x){
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
    inline static bool x_for(const std::array<std::array<double, N>, N>& a, const std::array<double, N>& b, std::array<double, N>& x){
        if(I < N){
            double d = di<I, N>(a, b, x);
            double aiji = aij<I, I, I+1, N>(a);
            if(std::abs(d) < SMALL_ENOUGH)
                x[I] = 0.0;
            else if(std::abs(aiji) < SMALL_ENOUGH)
                return false;
            else
                x[I] = d / aiji;
            return x_for<I+(I<N), N>(a, b, x);
        }
        return true;
    }

    template <int N>
    bool solve(const std::array<std::array<double, N>, N>& a, const std::array<double, N>& b, std::array<double, N>& x){
        return x_for<0, N>(a, b, x);
    }

	template <int N>
	bool verify(const std::array<std::array<double, N>, N>& a,
		const std::array<double, N>& b,
		const std::array<double, N>& x){

		std::array<double, N> b_for_x{0};
		for(int i = 0; i < N; i++){
			for(int j = 0; j < N; j++){
				b_for_x[i] += a[i][j] * x[j];
			}
			if(std::abs(b[i] - b_for_x[i]) > SMALL_ENOUGH) return false;
		}
		return true;
	}
}
