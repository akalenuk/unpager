#include <cmath>
#include <limits>
#include <functional>
#include <array>

namespace linear_equations{

	constexpr double SMALL_ENOUGH = std::sqrt(std::numeric_limits<double>::epsilon());

	bool solve(const std::array<std::array<double, 2>, 2>& a,
		const std::array<double, 2>& b,
		std::array<double, 2>& x){

		double d = a[0][1] * a[1][0] - a[0][0] * a[1][1];
		if (std::abs(d) < SMALL_ENOUGH) return false;

		x[0] = a[0][1] * b[1] - a[1][1] * b[0] / d;
		x[1] = a[1][0] * b[0] - a[0][0] * b[1] / d;
		return true;
	}

	bool solve(const std::array<std::array<double, 3>, 3>& a,
		const std::array<double, 3>& b,
		std::array<double, 3>& x){

		double d =
			  a[0][0] * (a[1][2] * a[2][1] - a[1][1] * a[2][2]) +
			+ a[0][1] * (a[1][0] * a[2][2] - a[1][2] * a[2][0])
			+ a[0][2] * (a[1][1] * a[2][0] - a[1][0] * a[2][1]);
		if (std::abs(d) < SMALL_ENOUGH) return false;

		x[0] =
			 (a[0][1] * (a[2][2] * b[1] - a[1][2] * b[2])
			+ a[0][2] * (a[1][1] * b[2] - a[2][1] * b[1])
			+ (a[1][2] * a[2][1] - a[1][1] * a[2][2]) * b[0]) / d;

		x[1] =
			-(a[0][0] * (a[2][2] * b[1] - a[1][2] * b[2])
			+ a[0][2] * (a[1][0] * b[2] - a[2][0] * b[1])
			+ (a[1][2] * a[2][0] - a[1][0] * a[2][2]) * b[0]) / d;

		x[2] =
			 (a[0][0] * (a[2][1] * b[1] - a[1][1] * b[2])
			+ a[0][1] * (a[1][0] * b[2] - a[2][0] * b[1])
			+ (a[1][1] * a[2][0] - a[1][0] * a[2][1]) * b[0]) / d;
		return true;
	}

	template <int N>
	bool solve(const std::array<std::array<double, N>, N>& a,
		const std::array<double, N>& b,
		std::array<double, N>& x){

		// you have to explicilty set lambda type to use it recursively
		std::function<double(int, int, int)> fa = [&](int i, int j, int n) -> double {
			if(n == N) return a[i][j];
			return fa(i, j, n+1) * fa(n, n, n+1)
				- fa(i, n, n+1) * fa(n, j, n+1);
		};

		std::function<double(int, int)> fb = [&](int i, int n) -> double{
			if(n == N) return b[i];
			return fa(n, n, n+1) * fb(i, n+1)
				- fa(i, n, n+1) * fb(n, n+1);
		};

		std::function<double(int)> fd = [&](int i) -> double{
			double d = fb(i, i+1);
			for(int j = 0; j < i; j++){
				d -= fa(i, j, i+1) * x[j];
			}
			return d;
		};

		auto fx = [&](int i, double d) -> double{
			return d / fa(i, i, i+1);
		};

		for(int i = 0; i < N; i++){
			double d = fd(i);
			if(std::abs(d) < SMALL_ENOUGH) return false;
			x[i] = fx(i, d);
		}
		return true;
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
