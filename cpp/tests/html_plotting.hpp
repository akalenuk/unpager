#include <array>
#include <functional>
#include <memory>

namespace html_plotting
{
    template <size_t W, size_t H>
    using HtmlCanvas = std::array<std::array<std::string, W>, H>;

    template <size_t W, size_t H>   // c++ would not deduct W and H from array size if their type is not size_t
    std::string table_from_canvas(const HtmlCanvas<W, H>& canvas){
        auto opt_canvas_in_heap = std::make_shared< std::array<std::array<std::tuple<std::string, int, int>, W>, H> >();
        auto& opt_canvas = *opt_canvas_in_heap;
        for(int i = 0; i < H; i++){
            for(int j = 0; j < W; j++){
                opt_canvas[i][j] = std::make_tuple(canvas[i][j], 1, 1);
            }
        }
        for(int i = 0; i < H; i++){
            for(int j = 0; j < W; j++){
                std::string c = std::get<0>(opt_canvas[i][j]);
                auto w = std::get<1>(opt_canvas[i][j]);
                auto h = std::get<2>(opt_canvas[i][j]);
                if( w<0 || h <0 )
                    continue;
                auto dj = W - j;
                for(int jj = j + 1; jj < W; jj++){
                    std::string cj = std::get<0>(opt_canvas[i][jj]);
                    auto wj = std::get<1>(opt_canvas[i][jj]);
                    auto hj = std::get<2>(opt_canvas[i][jj]);
                    if(wj < 0 || hj < 0 || cj != c){
                        dj = jj - j;
                        break;
                    }
                }
                auto di = H - i;
                for(int ii = i+1; ii < H; ii++){
                    for(int jj = j; jj < j + dj; jj++){
                        std::string cij = std::get<0>(opt_canvas[ii][jj]);
                        auto wij = std::get<1>(opt_canvas[ii][jj]);
                        auto hij = std::get<2>(opt_canvas[ii][jj]);
                        if(wij < 0 || hij < 0 || cij != c){
                            di = ii - i;
                            break;
                        }
                    }
                    if(di != H - i){
                        break;
                    }
                }
                if(di != 1 || dj != 1){
                    for(int ii = i; ii < i + di; ii++){
                        for(int jj = j; jj < j + dj; jj++){
                            opt_canvas[ii][jj] = std::make_tuple(c, -1, -1);
                        }
                    }
                }
                opt_canvas[i][j] = std::make_tuple(c, dj, di);
            }
        }

        std::string table = "<table border=0 cellspacing=0 cellpadding=0 width=" + std::to_string(W) + ">\n";
        table += "<tr height=0>";
        for(auto c : canvas[0]){
            table += "<td width=1></td>";
        }
        table += "</tr>\n";
        for(auto cline : opt_canvas){
            table += "<tr height=1>";
            for(auto c_w_h : cline){
                std::string c = std::get<0>(c_w_h);
                auto w = std::get<1>(c_w_h);
                auto h = std::get<2>(c_w_h);
                std::string bg = (c == "") ? "" : " bgcolor=" + c;
                if( w<0 || h<0 ){
                    continue;
                }else if( w>1 && h>1 ){
                    table += "<td" + bg + " colspan=" + std::to_string(w) + " rowspan=" + std::to_string(h) + "></td>";
                }else if( w>1 ){
                    table += "<td" + bg + " colspan=" + std::to_string(w) + "></td>";
                }else if( h>1 ){
                    table += "<td" + bg + " rowspan=" + std::to_string(h) + "></td>";
                }else{
                    table += "<td" + bg + "></td>";
                }
            }
            table += "</tr>\n";
        }
        table += "</table>";

        return table;
    }

    template <size_t W, size_t H>
    void plot_line_on_canvas(HtmlCanvas<W, H>& canvas, std::array<int, 2> xy1, std::array<int, 2> xy2, const std::string& c){
        bool yx;
        int x1, x2, y1, y2;
        if( std::abs(xy2[1] - xy1[1]) < std::abs(xy2[0] - xy1[0]) ){
            x1 = xy1[0];    y1 = xy1[1];
            x2 = xy2[0];    y2 = xy2[1];
            yx = false;
        }else {
            x1 = xy1[1];    y1 = xy1[0];
            x2 = xy2[1];    y2 = xy2[0];
            yx = true;
        }
        if(x1 > x2){
            std::swap(x1, x2);
            std::swap(y1, y2);
        }
        int dx = x2-x1;
        int dy = std::abs(y2-y1);
        int error = dx / 2;
        int i = y1;
        int ystep = y1 < y2 ? 1 : -1;
        for(int j = x1; j <= x2; j++){
            if(! yx){
                canvas[i][j] = c;
            }else{
                canvas[j][i] = c;
            }
            error -= dy;
            if(error < 0){
                error += dx;
                i += ystep;
            }
        }
    }

    template <size_t W, size_t H>
    void plot_f_on_canvas(HtmlCanvas<W, H>& canvas, const std::function<double(double)>& f,
        double x_min, double x_max, double y_min, double y_max, const std::string& c){
        int last_i;
        for(int j = 0; j < W; j++){
            int i = H - ( (f(x_min + (x_max-x_min) * j / W) - y_min) * H / (y_max-y_min) );
            if( j>0 && i>=0 && i<H ){
                plot_line_on_canvas(canvas, {j-1, last_i}, {j, i}, c);
            }
            last_i = i;
        }
    }

    template <size_t W, size_t H>
    void plot_xy_on_canvas(HtmlCanvas<W, H>& canvas, const std::vector<std::array<double, 2> >& xys,
        double x_min, double x_max, double y_min, double y_max, const std::string& c){
        for(auto xy : xys){
            double x = xy[0];
            double y = xy[1];
            int j = (x - x_min) * W / (x_max - x_min);
            int i = H - (y - y_min) * H / (y_max - y_min);
            if( i>=0 && i<H && j>=0 && j<W ){
                canvas[i][j] = c;
            }
        }
    }

    std::string wrap_in_limits(std::string table, int x_min, int x_max, int y_min, int y_max){
        return "<table><tr><td valign=top>" + std::to_string(y_max) + "</td>"
            + "<td colspan=2 rowspan=2>" + table + "</td></tr>"
            + "<tr><td valign=bottom>" + std::to_string(y_min) + "</td></tr>"
            + "<tr><td></td><td align=left>" + std::to_string(x_min) + "</td>"
            + "<td align=right>" + std::to_string(x_max) + "</td></tr></table>";
    }
}
