#include <array>
#include <functional>

namespace projection
{
    using Projection = std::function<std::array<double, 2>(std::array<double, 2>)>;

    using Matrix = std::array<std::array<double, 3>, 3>;

    Matrix make_projection_matrix(
    double x1, double y1,   double x2, double y2,
    double x3, double y3,   double x4, double y4){

        double s1 = y1 * x2 - x1 * y2;
        double s2 = y1 * x4 - x1 * y4;
        double s3 = x3 * x2 * x1 + x3 * x4 * x1 - x2 * x4 * x1 - x3 * x2 * x4;
        double s4 = y3 * y2 * y1 + y3 * y4 * y1 - y2 * y4 * y1 - y3 * y2 * y4;
        double a3 = x2 * x4 - x3 * x4;
        double b3 = x2 * x4 - x3 * x2;
        double d4 = y2 * y4 - y3 * y4;
        double e4 = y2 * y4 - y3 * y2;

        double dB = ((y2 * b3) * (x4 * d4) - (x2 * a3) * (y4 * e4));
        double B = (((x4 * d4) * (y2 * s3 - s1 * a3) - (x2 * a3) * (s2 * e4 + s4 * x4))) / dB;

        double dD = ((x2 * a3) * (y4 * e4) - (y2 * b3) * (x4 * d4));
        double D = (((y4 * e4) * (y2 * s3 - s1 * a3) - (y2 * b3) * (s2 * e4 + s4 * x4))) / dD;

        double A = (s3 - b3 * B) / a3;
        double E = (s4 - d4 * D) / e4;

        double a = (A + x1 - x2) / x2;
        double b = (B + x1 - x4) / x4;

        double C = x1;
        double F = y1;
        double c = 1.0;

        return Matrix{{{A, D, a}, {B, E, b}, {C, F, c}}};
    }

    Matrix make_inverse_to_projection(Matrix m) {
        double A = m[0][0];     double D = m[0][1];     double a = m[0][2];
        double B = m[1][0];     double E = m[1][1];     double b = m[1][2];
        double C = m[2][0];     double F = m[2][1];     double c = m[2][2];

        double A_ = E*c - b*F;  double D_ = a*F - D*c;  double a_ = b*D - a*E;
        double B_ = b*C - B*c;  double E_ = A*c - a*C;  double b_ = B*a - A*b;
        double C_ = B*F - E*C;  double F_ = C*D - A*F;  double c_ = A*E - B*D;

        return Matrix{{{ A_, D_, a_ }, { B_, E_, b_ }, { C_, F_, c_ }}};
    }

    Projection make_into_function(Matrix m){
        return [=](std::array<double, 2> xy){
            double A = m[0][0];     double D = m[0][1];     double a = m[0][2];
            double B = m[1][0];     double E = m[1][1];     double b = m[1][2];
            double C = m[2][0];     double F = m[2][1];     double c = m[2][2];

            double d_ = 1.0 / (a * xy[0] + b * xy[1] + c);
            double x_ = (A * xy[0] + B * xy[1] + C) * d_;
            double y_ = (D * xy[0] + E * xy[1] + F) * d_;
            return std::array<double, 2>{x_, y_};
        };
    }
}
