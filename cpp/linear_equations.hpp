#include <cmath>
#include <limits>
#include <functional>
#include <array>

namespace linear_equations{

    constexpr double SMALL_ENOUGH = std::sqrt(std::numeric_limits<double>::epsilon());

    namespace semi_static{
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
            //std::array<int, N> unused = {0}; // tracing statically
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
    }

    namespace iterative_projections{
        template <size_t N>
        double dot(const std::array<double, N>& a, const std::array<double, N>& b){
            double c = 0.0;
            for(size_t i = 0; i < N; i++){
                c += a[i]*b[i];
            }
            return c;
        }

        template <size_t N>
        const std::array<double, N> scale(const std::array<double, N>& a, double x){
            std::array<double, N> scaled {};
            for(size_t i = 0; i < N; i++){
                scaled[i] = a[i]*x;
            }
            return scaled;
        }

        template <size_t N>
        const std::array<double, N> add(const std::array<double, N>& a, const std::array<double, N>& b){
            std::array<double, N> added {};
            for(size_t i = 0; i < N; i++){
                added[i] = a[i] + b[i];
            }
            return added;
        }

        template <size_t N>
        std::array<double, N> project(const std::array<double, N>& point, const std::array<double, N>& n, const double d){
            double x = (d - dot(point, n)) / (dot(n, n));
            return add(point, scale(n, x));
        }

        template <size_t N>
        bool verify(const std::array<std::array<double, N>, N>& a,
            const std::array<double, N>& b,
            const std::array<double, N>& x,
            double e){

            std::array<double, N> b_for_x{0};
            for(int i = 0; i < N; i++){
                for(int j = 0; j < N; j++){
                    b_for_x[i] += a[i][j] * x[j];
                }
                if(std::abs(b[i] - b_for_x[i]) > e) return false;
            }
            return true;
        }

        template <size_t N>
        bool solve(const std::array<std::array<double, N>, N>& a,
            const std::array<double, N>& b,
            std::array<double, N>& x,
            double e = SMALL_ENOUGH,
            unsigned int max_i = std::numeric_limits<unsigned int>::max()
            ){
            for(unsigned int i = 0; i < max_i; i++){
                for(size_t j = 0; j < N; j++){
                    x = project(x, a[j], b[j]);
                }
                if(verify(a, b, x, e)) return true;
            }
            return false;
        }
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
