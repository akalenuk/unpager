#include <array>

namespace html_plotting
{
    template <int W, int H>
    using HtmlCanvas = std::array<std::array<std::string, W>, H>;

    template <int W, int H>
    std::string table_from_canvas(HtmlCanvas<W, H>& canvas){
        std::array<std::array<std::tuple<std::string, int, int>, W>, H> opt_canvas;
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
}
