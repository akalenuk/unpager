#include "../linear_equations.hpp"

#include <iostream>
#include <assert.h>

int main(){
    // semi-static Cramers tule based solution
    auto a = std::array<std::array<double, 3>, 3>{{{1.0, 0.0, 0.0}, {2.0, 1.0, 0.0}, {3.0, 2.0, 1.0}}};
    auto b = std::array<double, 3>{1.0, 4.0, 9.3};
    auto x = std::array<double, 3>{0.0, 0.0, 0.0};
    auto ss_solved = linear_equations::semi_static::solve<3>(a, b, x);
    auto ss_verified = linear_equations::verify<3>(a, b, x);
    assert(ss_solved);
    assert(ss_verified);

    // iterative projections solution
    x = std::array<double, 3>{0.0, 0.0, 0.0};
    auto ip_solved = linear_equations::iterative_projections::solve<3>(a, b, x);
    auto ip_verified = linear_equations::verify<3>(a, b, x);
    assert(ip_solved);
    assert(ip_verified);

    std::cout << " + Linear equations solvers are fine." << std::endl;
    return 0;
}
