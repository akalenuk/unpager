using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;

// TODO: FFD on 2-SWINE
// TODO: approximate 3x2 polynomial model 
// TODO: 1-SWINE profiles with selectable basis functions

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

       enum Tool {
            None,
            ProjectionFrame,
            PolynomialProfiles,
            SWINEProfiles,
            LightingPoints,
            FreeForm
        };

        const double SCALE_DIVISOR = 1000.0;
        const int FRAME_DEFAULT = 25;

        Tool cur_tool = Tool.None;

        Bitmap source = new Bitmap(100, 100);
        Point source_point = new Point(0, 0);
        double source_scale = 1;

        Point P1 = new Point(0, 0);
        Point P2 = new Point(100, 0);
        Point P3 = new Point(100, 100);
        Point P4 = new Point(0, 100);

        int line1 = 0;
        int line2 = 100;
        const int max_points = 128;
        List<Point> carcas1 = new List<Point>();
        List<Point> carcas2 = new List<Point>();
        
        List<Point> firm_carcas1 = new List<Point>();
        List<Point> firm_carcas2 = new List<Point>();
        const int pol_n = 4;
        Polynomial pol1 = new Polynomial();
        Polynomial pol2 = new Polynomial();

        double[][] swine_carcas1 = new double[0][];
        int[][] swine_complex1 = new int[0][];
        Mswine.BasisFunction[] swine_basis1 = new Mswine.BasisFunction[0];
        Mswine.BasisFunction[] swine_linear_basis1 = new Mswine.BasisFunction[0];
        Mswine.BasisFunction[] swine_constant_basis1 = new Mswine.BasisFunction[0];

        double[][] swine_carcas2 = new double[0][];
        int[][] swine_complex2 = new int[0][];
        Mswine.BasisFunction[] swine_basis2 = new Mswine.BasisFunction[0];
        Mswine.BasisFunction[] swine_linear_basis2 = new Mswine.BasisFunction[0];
        Mswine.BasisFunction[] swine_constant_basis2 = new Mswine.BasisFunction[0];

        Point last_mouse_pos = new Point(0, 0);

        List<Point> light_points = new List<Point>();

        List<Point> ffd_points = new List<Point>();

        public Form1()
        {
            InitializeComponent();
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseWheel);

            firm_carcas1 = new List<Point>();
            firm_carcas1.Add(new Point(0, 0));
            firm_carcas2 = new List<Point>();
            firm_carcas2.Add(new Point(0, 0));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            openFileDialog1.Filter = "Pictures (*.png, *.jpg, ...)|*.jpg;*.jpeg;*.png;*.bmp|All Files (*.*)|*.*";
            saveFileDialog1.Filter = "PNG (*.png)|*.png|Everything (*.*)|*.*";

            // ad hoc localization
            // I just have to make presentation in russian really quick
            if (System.AppDomain.CurrentDomain.FriendlyName == "unpager_ru.exe") {

                fileToolStripMenuItem.Text = "Файлы";
                openToolStripMenuItem.Text = "Открыть";
                saveToolStripMenuItem.Text = "Сохранить";
                exitToolStripMenuItem.Text = "Выход";

                trivialitiesToolStripMenuItem.Text = "Повороты/зеркало";
                turnClockwiseToolStripMenuItem.Text = "Поворот по часовой";
                turnContrclockwiseToolStripMenuItem.Text = "Поворот против часовой";
                mirrorToolStripMenuItem.Text = "Зеркальное отображение";

                toolToolStripMenuItem.Text = "Инструменты";
                noneToolStripMenuItem.Text = "Просмотрщик";
                projectionFrameToolStripMenuItem.Text = "Рамка";
                polynomialProfilesToolStripMenuItem.Text = "Профили";
                lightingPointsToolStripMenuItem.Text = "Точки";

                processToolStripMenuItem.Text = "Обработка";
                undoToolStripMenuItem.Text = "Отменить последнее";
                projectToolStripMenuItem.Text = "Проецировать по рамке";
                flattenToolStripMenuItem.Text = "Спрямить по профилям";
                lightToolStripMenuItem.Text = "Пересчитать свет по точкам";
                sharpenToolStripMenuItem.Text = "Отделить текст от фона";
                grayscaleToolStripMenuItem.Text = "Обесцветить в Ч/Б";
                normalizeToolStripMenuItem.Text = "Нормализировать яркость";
                darnToolStripMenuItem.Text = "Залатать по рамке";

                automaticToolStripMenuItem.Text = "Автоматизация";
                findLightingPointsToolStripMenuItem.Text = "Найти точки освещения";

                settingsToolStripMenuItem.Text = "Выбираемые настройки";
                smoothTransformToolStripMenuItem.Text = "Картинка при трансформации сглаживается";
                rectangularFrameToolStripMenuItem.Text = "Рамка становится прямоугольной";
            }
            InvalidateWhole();
        }

        private void InvalidateWhole() {
            Invalidate();
            SetMenuChecks();
            Cursor = Cursors.Arrow;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                source = new Bitmap(openFileDialog1.FileName);
                P1 = new Point(0, 0);
                P2 = new Point(source.Width, 0);
                P3 = new Point(source.Width, source.Height);
                P4 = new Point(0, source.Height);
                line1 = (int)(source.Height * source_scale) / 10;
                line2 = source.Height - line1;
                cur_tool = Tool.None;
                source_scale = 0.8F * (double)ClientSize.Height / source.Height;
                source_point.X = ClientRectangle.Left + ClientSize.Width / 2 - (int)(source.Width * source_scale) / 2;
                source_point.Y = ClientRectangle.Top + ClientSize.Height / 2 - (int)(source.Height * source_scale) / 2;
                InvalidateWhole();
            }
        }

        private int x_to_s(int X) 
        {
            return (int)(X * source_scale) + source_point.X;            
        }

        private int y_to_s(int Y) 
        {
            return (int)(Y * source_scale) + source_point.Y;
        }

        private int s_to_x(int X)
        {
            return (int)((X - source_point.X) / source_scale);
        }

        private int s_to_y(int Y)
        {
            return (int)((Y - source_point.Y) / source_scale);
        }

        private double Dp(int x1, int y1, int x2, int y2) 
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
        }

        private double Dr(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Dp(x1, y1, x2, y2));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int scaled_w;
            int scaled_h;
            Pen red_pen = new Pen(Color.Red);
            Pen blue_pen = new Pen(Color.Blue);
            switch (cur_tool) 
            { 
                case Tool.ProjectionFrame:
                    scaled_w = (int)(source.Width * source_scale);
                    scaled_h = (int)(source.Height * source_scale);
                    e.Graphics.DrawImage(source, source_point.X, source_point.Y, scaled_w, scaled_h);

                    e.Graphics.DrawLine(red_pen, x_to_s(P1.X), y_to_s(P1.Y), x_to_s(P2.X), y_to_s(P2.Y));
                    e.Graphics.DrawLine(red_pen, x_to_s(P2.X), y_to_s(P2.Y), x_to_s(P3.X), y_to_s(P3.Y));
                    e.Graphics.DrawLine(red_pen, x_to_s(P3.X), y_to_s(P3.Y), x_to_s(P4.X), y_to_s(P4.Y));
                    e.Graphics.DrawLine(red_pen, x_to_s(P4.X), y_to_s(P4.Y), x_to_s(P1.X), y_to_s(P1.Y));
                    break;
                case Tool.PolynomialProfiles:
                    scaled_w = (int)(source.Width * source_scale);
                    scaled_h = (int)(source.Height * source_scale);
                    e.Graphics.DrawImage(source, source_point.X, source_point.Y, scaled_w, scaled_h);

                    e.Graphics.DrawLine(blue_pen, x_to_s(0), y_to_s(line1), x_to_s(source.Width), y_to_s(line1));
                    e.Graphics.DrawLine(blue_pen, x_to_s(0), y_to_s(line2), x_to_s(source.Width), y_to_s(line2));
                    e.Graphics.DrawEllipse(blue_pen, x_to_s(0) - 2, y_to_s(line1) - 2, 4, 4);
                    e.Graphics.DrawEllipse(blue_pen, x_to_s(source.Width) - 2, y_to_s(line1) - 2, 4, 4);
                    e.Graphics.DrawEllipse(blue_pen, x_to_s(0) - 2, y_to_s(line2) - 2, 4, 4);
                    e.Graphics.DrawEllipse(blue_pen, x_to_s(source.Width) - 2, y_to_s(line2) - 2, 4, 4);

                    foreach (Point p in carcas1) 
                    {
                        e.Graphics.DrawEllipse(red_pen, x_to_s(p.X) - 2, y_to_s(p.Y + line1) - 2, 4, 4);
                    }
                    foreach (Point p in carcas2)
                    {
                        e.Graphics.DrawEllipse(red_pen, x_to_s(p.X) - 2, y_to_s(p.Y + line2) - 2, 4, 4);
                    }

                    for (int j = x_to_s(0); j < x_to_s(source.Width); j++)
                    {
                        double x = s_to_x(j);
                        {
                            double y = pol1.In( x );
                            int i = y_to_s((int)y) + y_to_s(line1) - source_point.Y;
                            e.Graphics.DrawLine(red_pen, j, i, j + 1, i);
                        }
                        {
                            double y = pol2.In( x );
                            int i = y_to_s((int)y) + y_to_s(line2) - source_point.Y;
                            e.Graphics.DrawLine(red_pen, j, i, j + 1, i);
                        }
                    }
                    break;
                case Tool.SWINEProfiles:
                    scaled_w = (int)(source.Width * source_scale);
                    scaled_h = (int)(source.Height * source_scale);
                    e.Graphics.DrawImage(source, source_point.X, source_point.Y, scaled_w, scaled_h);

                    e.Graphics.DrawLine(blue_pen, x_to_s(0), y_to_s(line1), x_to_s(source.Width), y_to_s(line1));
                    e.Graphics.DrawLine(blue_pen, x_to_s(0), y_to_s(line2), x_to_s(source.Width), y_to_s(line2));
                    e.Graphics.DrawEllipse(blue_pen, x_to_s(0) - 2, y_to_s(line1) - 2, 4, 4);
                    e.Graphics.DrawEllipse(blue_pen, x_to_s(source.Width) - 2, y_to_s(line1) - 2, 4, 4);
                    e.Graphics.DrawEllipse(blue_pen, x_to_s(0) - 2, y_to_s(line2) - 2, 4, 4);
                    e.Graphics.DrawEllipse(blue_pen, x_to_s(source.Width) - 2, y_to_s(line2) - 2, 4, 4);

                    foreach (Point p in carcas1) {
                        e.Graphics.DrawEllipse(red_pen, x_to_s(p.X) - 2, y_to_s(p.Y + line1) - 2, 4, 4);
                    }
                    foreach (Point p in carcas2) {
                        e.Graphics.DrawEllipse(red_pen, x_to_s(p.X) - 2, y_to_s(p.Y + line2) - 2, 4, 4);
                    }

                    for (int j = x_to_s(0); j < x_to_s(source.Width); j++) {                        
                        if(swine_basis1.Length > 0)
                        {
                            double x = s_to_x(j) + Scalar.jitter();
                            double[] dot = new double[1];
                            dot[0] = x;
                            double y = Mswine.F_s(dot, swine_carcas1, swine_complex1, swine_basis1, Mswine.s_l);
                            int i = y_to_s((int)y) + y_to_s(line1) - source_point.Y;
                            e.Graphics.DrawLine(red_pen, j, i, j + 1, i);
                        }
                        if (swine_basis2.Length > 0) {
                            double x = s_to_x(j) + Scalar.jitter();
                            double[] dot = new double[1];
                            dot[0] = x;
                            double y = Mswine.F_s(dot, swine_carcas2, swine_complex2, swine_basis2, Mswine.s_l);
                            int i = y_to_s((int)y) + y_to_s(line2) - source_point.Y;
                            e.Graphics.DrawLine(red_pen, j, i, j + 1, i);
                        }
                    }

                    break;
                case Tool.LightingPoints:
                    scaled_w = (int)(source.Width * source_scale);
                    scaled_h = (int)(source.Height * source_scale);
                    e.Graphics.DrawImage(source, source_point.X, source_point.Y, scaled_w, scaled_h);
                    foreach (Point p in light_points) 
                    {
                        e.Graphics.DrawEllipse(red_pen, x_to_s(p.X) - 2, y_to_s(p.Y) - 2, 4, 4);
                    }
                    break;
                case Tool.FreeForm:
                    scaled_w = (int)(source.Width * source_scale);
                    scaled_h = (int)(source.Height * source_scale);
                    e.Graphics.DrawImage(source, source_point.X, source_point.Y, scaled_w, scaled_h);
                    Pen red_arrow = new Pen(red_pen.Brush);
                    red_arrow.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    red_arrow.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                    for (int i = 0; i < ffd_points.Count/2; i++ ) {
                        int x1 = x_to_s(ffd_points[2 * i].X);
                        int y1 = y_to_s(ffd_points[2 * i].Y);
                        int x2 = x_to_s(ffd_points[2 * i + 1].X);
                        int y2 = y_to_s(ffd_points[2 * i + 1].Y);
                        e.Graphics.DrawLine(red_arrow, x1, y1, x2, y2);
                    }
                    break;
                default:
                    if (source != null)
                    {
                        scaled_w = (int)(source.Width * source_scale);
                        scaled_h = (int)(source.Height * source_scale);
                        e.Graphics.DrawImage(source, source_point.X, source_point.Y, scaled_w, scaled_h);
                    }
                    break;
            }
        }

        Point capture_point;
        bool rpoint_captured = false;
        bool lpoint_captured = false;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            switch(cur_tool)
            {
                case Tool.None:
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        capture_point = new Point(e.X, e.Y);
                        rpoint_captured = true;
                    }
                    break;
                case Tool.ProjectionFrame:
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        capture_point = new Point(e.X, e.Y);
                        rpoint_captured = true;
                    }
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        lpoint_captured = true;
                    }
                    break;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            rpoint_captured = false;
            lpoint_captured = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            last_mouse_pos = new Point(e.X, e.Y);
            switch(cur_tool)
            {
                case Tool.None:
                    if (rpoint_captured) {
                        source_point.X += e.X - capture_point.X;
                        source_point.Y += e.Y - capture_point.Y;
                        capture_point = new Point(e.X, e.Y);
                        InvalidateWhole();
                    }
                    break;
                case Tool.ProjectionFrame:
                    if (rpoint_captured) {
                        source_point.X += e.X - capture_point.X;
                        source_point.Y += e.Y - capture_point.Y;
                        capture_point = new Point(e.X, e.Y);
                        InvalidateWhole();
                    }
                    if (lpoint_captured) {
                        CheckProjPoint(new Point(e.X, e.Y));
                    }
                    break;
                case Tool.PolynomialProfiles:
                case Tool.SWINEProfiles:
                    if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                        CheckLine(e.Y);
                    }
                    break;
            }
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e) {
            if (cur_tool == Tool.ProjectionFrame || cur_tool == Tool.None)
            {
                int x = s_to_x(e.X);
                int y = s_to_y(e.Y);
                source_scale *= (1.0 + e.Delta / SCALE_DIVISOR );
                InvalidateWhole();
                int dx = x_to_s(x) - e.X;
                int dy = y_to_s(y) - e.Y;
                source_point.X -= dx;
                source_point.Y -= dy;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e) {
            switch(cur_tool)
            {
                case Tool.ProjectionFrame:
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        CheckProjPoint(new Point(e.X, e.Y));
                    }
                    break;
                case Tool.PolynomialProfiles:
                case Tool.SWINEProfiles:
                    if (e.Button == System.Windows.Forms.MouseButtons.Right) 
                    {
                        CheckLine(e.Y);
                    }
                    if (e.Button == System.Windows.Forms.MouseButtons.Left) 
                    {
                        CheckCarcas(new Point(e.X, e.Y));
                    }
                    break;
                case Tool.LightingPoints:
                    if (e.Button == System.Windows.Forms.MouseButtons.Left) {
                        light_points.Add(new Point(s_to_x(e.X), s_to_y(e.Y)));
                        InvalidateWhole();
                    }
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        if(light_points.Count > 0){
                            double min_D = Dp(s_to_x(e.X), s_to_y(e.Y), light_points[0].X, light_points[0].Y);
                            int min_Di = 0;
                            for (int i = 0; i < light_points.Count; i++) {
                                double cur_D = Dp(s_to_x(e.X), s_to_y(e.Y), light_points[i].X, light_points[i].Y);
                                if (cur_D < min_D) {
                                    min_D = cur_D;
                                    min_Di = i;
                                }
                            }
                            light_points.RemoveAt(min_Di);
                            InvalidateWhole();
                        }
                    }
                    break;
                case Tool.FreeForm:
                    if (e.Button == System.Windows.Forms.MouseButtons.Left) {
                        ffd_points.Add(new Point(s_to_x(e.X), s_to_y(e.Y)));
                        InvalidateWhole();
                    }
                    if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                        ffd_points.RemoveAt(ffd_points.Count-1);
                        InvalidateWhole();
                    }
                    break;
            }
        }

        private void CheckProjPoint(Point e) {
            int sx = s_to_x(e.X);
            int sy = s_to_y(e.Y);
            double D1 = Dp(sx, sy, P1.X, P1.Y);
            double D2 = Dp(sx, sy, P2.X, P2.Y);
            double D3 = Dp(sx, sy, P3.X, P3.Y);
            double D4 = Dp(sx, sy, P4.X, P4.Y);
            if (D1 <= D2 && D1 <= D3 && D1 <= D4) {
                P1.X = sx;
                P1.Y = sy;
                if (rectangularFrameToolStripMenuItem.Checked) {
                    P2.Y = sy;
                    P4.X = sx;
                }
            } else if (D2 <= D3 && D2 <= D4) {
                P2.X = sx;
                P2.Y = sy;
                if (rectangularFrameToolStripMenuItem.Checked) {
                    P1.Y = sy;
                    P3.X = sx;
                }
            } else if (D3 <= D4) {
                P3.X = sx;
                P3.Y = sy;
                if (rectangularFrameToolStripMenuItem.Checked) {
                    P2.X = sx;
                    P4.Y = sy;
                }
            } else {
                P4.X = sx;
                P4.Y = sy;
                if (rectangularFrameToolStripMenuItem.Checked) {
                    P1.X = sx;
                    P3.Y = sy;
                }
            }
            InvalidateWhole();
        }

        private void CheckLine(int sy){
            int y = s_to_y(sy);
            if (Math.Abs(y - line1) < Math.Abs(y - line2)) {
                line1 = y;
            } else {
                line2 = y;
            }
            InvalidateWhole();
        }


        class ByX : IComparer<Point> {
            int IComparer<Point>.Compare(Point a, Point b) {
                return a.X - b.X;
            }
        }
        ByX by_x = new ByX();

        private void CheckCarcas(Point p) {
            int y = s_to_y(p.Y);
            int x = s_to_x(p.X);
            if (Math.Abs(y - line1) < Math.Abs(y - line2)) {
                ChangeCarcas(carcas1, new Point(x, y - line1));
                pol1 = new Polynomial(pol_n, carcas1, firm_carcas1);
                
                List<Point> sorted_carcas = new List<Point>();
                sorted_carcas = carcas1.GetRange(0, carcas1.Count);
                sorted_carcas.Add(new Point(0, 0));
                sorted_carcas.Sort(by_x);
                int n = sorted_carcas.Count;
                double[] ys = new double[n];
                swine_carcas1 = new double[n][];
                swine_complex1 = new int[n-1][];
                for (int i = 0; i < sorted_carcas.Count; i++) {
                    ys[i] = (double)sorted_carcas[i].Y;
                    swine_carcas1[i] = new double[1];
                    swine_carcas1[i][0] = (double)sorted_carcas[i].X;
                    if (i != 0) {
                        swine_complex1[i-1] = new int[2];
                        swine_complex1[i-1][0] = i;
                        swine_complex1[i-1][1] = i+1;
                    }
                }
                swine_linear_basis1 = Mswine.get_linear_functions(swine_carcas1, ys, swine_complex1);
                swine_constant_basis1 = Mswine.get_constant_functions(swine_carcas1, ys, swine_complex1);
                swine_basis1 = new Mswine.BasisFunction[swine_linear_basis1.Length];
                for (int i = 0; i < swine_basis1.Length; i++) {
                    swine_basis1[i] = (Mswine.BasisFunction)swine_linear_basis1[i].Clone();
                }

            } else {
                ChangeCarcas(carcas2, new Point(x, y - line2));
                pol2 = new Polynomial(pol_n, carcas2, firm_carcas2);

                List<Point> sorted_carcas = new List<Point>();
                sorted_carcas = carcas2.GetRange(0, carcas2.Count);
                sorted_carcas.Add(new Point(0, 0));
                sorted_carcas.Sort(by_x);
                int n = sorted_carcas.Count;
                double[] ys = new double[n];
                swine_carcas2 = new double[n][];
                swine_complex2 = new int[n - 1][];
                for (int i = 0; i < sorted_carcas.Count; i++) {
                    ys[i] = (double)sorted_carcas[i].Y;
                    swine_carcas2[i] = new double[1];
                    swine_carcas2[i][0] = (double)sorted_carcas[i].X;
                    if (i != 0) {
                        swine_complex2[i - 1] = new int[2];
                        swine_complex2[i - 1][0] = i;
                        swine_complex2[i - 1][1] = i + 1;
                    }
                }

                swine_linear_basis2 = Mswine.get_linear_functions(swine_carcas2, ys, swine_complex2);
                swine_constant_basis2 = Mswine.get_constant_functions(swine_carcas2, ys, swine_complex2);
                swine_basis2 = new Mswine.BasisFunction[swine_linear_basis2.Length];
                for (int i = 0; i < swine_basis2.Length; i++) {
                    swine_basis2[i] = (Mswine.BasisFunction)swine_linear_basis2[i].Clone();
                }
            }
            InvalidateWhole();
        }

        private void ChangeCarcas(List<Point> carcas, Point p){
            int x1 = x_to_s(p.X);
            int y1 = x_to_s(p.Y);
            if (carcas.Count < max_points) {
                int i = -1;
                for(int j = 0; j < carcas.Count; j++){
                    int x2 = x_to_s(carcas[j].X);
                    int y2 = x_to_s(carcas[j].Y);
                    double d2 = (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
                    if (d2 < 25) {
                        i = j;
                    }
                }
                if (i >= 0) {
                    carcas.RemoveAt(i);
                } else {
                    carcas.Add(p);
                }
            }
        }        
        

        private void projectToolStripMenuItem_Click(object sender, EventArgs e) // projection
        {
            Cursor = Cursors.WaitCursor;
            Undo.push(source);
            ContinuousBitmap csource = new ContinuousBitmap(source);
            if (smoothTransformToolStripMenuItem.Checked)
            {
                csource.ChooseInterpolation(ContinuousBitmap.Interpolation.PiecewiseWeight);
            }
            source = ImageTransformer.Projection(csource, P1, P2, P3, P4);

            source_scale = 0.8F * (double)ClientSize.Height / source.Height;
            source_point.X = ClientRectangle.Left + ClientSize.Width / 2 - (int)(source.Width * source_scale) / 2;
            source_point.Y = ClientRectangle.Top + ClientSize.Height / 2 - (int)(source.Height * source_scale) / 2;
            line1 = (int)(source.Height * source_scale) / 10;
            line2 = source.Height - line1;
            resetFrameToolStripMenuItem1_Click(sender, e);
            InvalidateWhole();
        }

        private void flattenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Undo.push(source);
            ContinuousBitmap csource = new ContinuousBitmap(source);
            if (smoothTransformToolStripMenuItem.Checked)
            {
                csource.ChooseInterpolation(ContinuousBitmap.Interpolation.PiecewiseWeight);
            }
            if (cur_tool == Tool.PolynomialProfiles) {
                source = ImageTransformer.ByPolynomialProfiles(csource, pol1, pol2, line1, line2);
            } else if (cur_tool == Tool.SWINEProfiles) {
                source = ImageTransformer.BySwineProfiles(csource,
                                                            swine_carcas1, swine_complex1, swine_basis1,
                                                            swine_carcas2, swine_complex2, swine_basis2,
                                                            line1, line2);
            } 
            
            source_scale = 0.8F * (double)ClientSize.Height / source.Height;
            source_point.X = ClientRectangle.Left + ClientSize.Width / 2 - (int)(source.Width * source_scale) / 2;
            source_point.Y = ClientRectangle.Top + ClientSize.Height / 2 - (int)(source.Height * source_scale) / 2;
            InvalidateWhole();
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Undo.push(source);
            source = ImageProcessor.FlattenLight(source, light_points);

            InvalidateWhole();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "png";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                source.Save(saveFileDialog1.FileName);
            }
        }

        private void projectionFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cur_tool = Tool.ProjectionFrame;
            InvalidateWhole();
        }

        private void polynomialProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cur_tool = Tool.PolynomialProfiles;
            InvalidateWhole();
        }

        private void lightingPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cur_tool = Tool.LightingPoints;
            InvalidateWhole();
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cur_tool = Tool.None;
            InvalidateWhole();
        }

        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Undo.push(source);
            source = ImageProcessor.Reinterpolate(source, 4, 4);
            InvalidateWhole();
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Undo.push(source);
            source = ImageProcessor.Grayscale(source);
            InvalidateWhole();
        }

        private void normalizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Undo.push(source);
            source = ImageProcessor.Normalize(source);
            InvalidateWhole();
        }

        private void findLightingPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            cur_tool = Tool.LightingPoints;
            light_points.Clear();
            light_points = Automatic.FindLightingPoints(source, 10, 10);
            InvalidateWhole();
        }

        private void turnClockwiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo.push(source);
            source.RotateFlip(RotateFlipType.Rotate90FlipNone);
            InvalidateWhole();
        }

        private void turnContrclockwiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo.push(source);
            source.RotateFlip(RotateFlipType.Rotate270FlipNone);
            InvalidateWhole();
        }

        private void mirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo.push(source);
            source.RotateFlip(RotateFlipType.RotateNoneFlipX);
            InvalidateWhole();
        }

        private void SetMenuChecks() {
            noneToolStripMenuItem.Checked = false;
            projectionFrameToolStripMenuItem.Checked = false;
            polynomialProfilesToolStripMenuItem.Checked = false;
            sWINEProfilesToolStripMenuItem.Checked = false;
            lightingPointsToolStripMenuItem.Checked = false;
            fFDVectorsToolStripMenuItem.Checked = false;
            switch (cur_tool)
            {
                case Tool.None:
                    noneToolStripMenuItem.Checked = true;
                    break;
                case Tool.ProjectionFrame:
                    projectionFrameToolStripMenuItem.Checked = true;
                    break;
                case Tool.PolynomialProfiles:
                    polynomialProfilesToolStripMenuItem.Checked = true;
                    break;
                case Tool.SWINEProfiles:
                    sWINEProfilesToolStripMenuItem.Checked = true;
                    break;
                case Tool.LightingPoints:
                    lightingPointsToolStripMenuItem.Checked = true;
                    break;
                case Tool.FreeForm:
                    fFDVectorsToolStripMenuItem.Checked = true;
                    break;
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                source = Undo.pop();
                InvalidateWhole();
            }catch(UndoPopException ex){
                MessageBox.Show(ex.Message);
            }
        }

        private void darnToolStripMenuItem_Click(object sender, EventArgs e) {
            Cursor = Cursors.WaitCursor;
            Undo.push(source);
            if (cur_tool != Tool.ProjectionFrame) {
                MessageBox.Show("You have to select projection frame to do darning");
                return;
            }
            if( !rectangularFrameToolStripMenuItem.Checked) {
                int new_w = (int)(1.5*Math.Max( Dr(P1.X, P1.Y, P2.X, P2.Y), Dr(P3.X, P3.Y, P4.X, P4.Y) ));  // this 1.5 is magic! have to fix it
                int new_h = (int)(1.5*Math.Max( Dr(P1.X, P1.Y, P4.X, P4.Y), Dr(P2.X, P2.Y, P3.X, P3.Y) ));

                ContinuousBitmap csource = new ContinuousBitmap(source);
                if (smoothTransformToolStripMenuItem.Checked) {
                    csource.ChooseInterpolation(ContinuousBitmap.Interpolation.PiecewiseWeight);
                }

                double x1 = P1.X;
                double x2 = P2.X;
                double x3 = P3.X;
                double x4 = P4.X;

                double y1 = P1.Y;
                double y2 = P2.Y;
                double y3 = P3.Y;
                double y4 = P4.Y;

                x1 += Scalar.jitter();
                x2 -= Scalar.jitter();
                x3 -= Scalar.jitter();
                x4 += Scalar.jitter();

                y1 += Scalar.jitter();
                y2 += Scalar.jitter();
                y3 -= Scalar.jitter();
                y4 -= Scalar.jitter();

                double[,] M = Matrix.make_projection(x1, y1, x2, y2, x3, y3, x4, y4);

                Bitmap darned = new Bitmap(source);

                for (int i = 1; i < new_h-1; i++) {
                    double y = i / (double)(new_h);
                    for (int j = 1; j < new_w-1; j++) {
                        double x = j / (double)(new_w);

                        double[] nxy = Matrix.project_point(M, new double[] { x, y });
                        double[] nx0 = Matrix.project_point(M, new double[] { x, 0.0 });
                        double[] nx1 = Matrix.project_point(M, new double[] { x, 1.0 });
                        double[] n0y = Matrix.project_point(M, new double[] { 0.0, y });
                        double[] n1y = Matrix.project_point(M, new double[] { 1.0, y });                   

                        double tj = y;
                        double fj = 1.0 - tj;
                        double ti = x;
                        double fi = 1.0 - ti;

                        Color col_i0 = csource.GetPixel(n0y[0], n0y[1]);
                        Color col_i1 = csource.GetPixel(n1y[0], n1y[1]);
                        Color col_j0 = csource.GetPixel(nx0[0], nx0[1]);
                        Color col_j1 = csource.GetPixel(nx1[0], nx1[1]);

                        double gd = 1.0 / (1.0 / ti + 1.0 / fi + 1.0 / tj + 1.0 / fj);
                        int r = (int)((col_i0.R / ti + col_i1.R / fi + col_j0.R / tj + col_j1.R / fj) * gd);
                        int g = (int)((col_i0.G / ti + col_i1.G / fi + col_j0.G / tj + col_j1.G / fj) * gd);
                        int b = (int)((col_i0.B / ti + col_i1.B / fi + col_j0.B / tj + col_j1.B / fj) * gd);

                        darned.SetPixel((int)nxy[0], (int)nxy[1], Color.FromArgb(r, g, b));                        
                    }
                }
                source = darned;
            }else{                
                Debug.Assert(P1.X < P3.X);
                Debug.Assert(P1.Y < P3.Y);

                Bitmap darned = new Bitmap(source);
                for(int i = P1.Y+1; i < P3.Y-1; i++){
                    for(int j = P1.X+1; j < P3.X-1; j++){
                        double tj = (double)(j - P1.X)/(P3.X - P1.X);
                        double fj = 1.0 - tj;
                        double ti = (double)(i - P1.Y)/(P3.Y - P1.Y);
                        double fi = 1.0 - ti;
                        Color col_i0 = source.GetPixel(j, P1.Y);
                        Color col_i1 = source.GetPixel(j, P3.Y);
                        Color col_j0 = source.GetPixel(P1.X, i);
                        Color col_j1 = source.GetPixel(P3.X, i);
 
                        double gd = 1.0 / (1.0 / ti + 1.0 / fi + 1.0 / tj + 1.0 / fj);
                        int r = (int)((col_i0.R / ti + col_i1.R / fi + col_j0.R / tj + col_j1.R / fj) * gd);
                        int g = (int)((col_i0.G / ti + col_i1.G / fi + col_j0.G / tj + col_j1.G / fj) * gd);
                        int b = (int)((col_i0.B / ti + col_i1.B / fi + col_j0.B / tj + col_j1.B / fj) * gd);

                        darned.SetPixel(j, i, Color.FromArgb(r, g, b));                        
                    }
                }
                source = darned;
            }
            InvalidateWhole();
        }

        private void sWINEProfilesToolStripMenuItem_Click(object sender, EventArgs e) {
            cur_tool = Tool.SWINEProfiles;
            InvalidateWhole();
        }

        private void notRealMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show("This is just a tip, you have to press " + ((ToolStripMenuItem)sender).ShortcutKeyDisplayString + " on your keyboard.");
        }
        
        private void frameToCursorToolStripMenuItem1_Click(object sender, EventArgs e) {
            cur_tool = Tool.ProjectionFrame;
            P1 = new Point(s_to_x(last_mouse_pos.X - FRAME_DEFAULT), s_to_y(last_mouse_pos.Y - FRAME_DEFAULT));
            P2 = new Point(s_to_x(last_mouse_pos.X + FRAME_DEFAULT), s_to_y(last_mouse_pos.Y - FRAME_DEFAULT));
            P3 = new Point(s_to_x(last_mouse_pos.X + FRAME_DEFAULT), s_to_y(last_mouse_pos.Y + FRAME_DEFAULT));
            P4 = new Point(s_to_x(last_mouse_pos.X - FRAME_DEFAULT), s_to_y(last_mouse_pos.Y + FRAME_DEFAULT));
            InvalidateWhole();
        }

        private void selectFrameToolStripMenuItem1_Click(object sender, EventArgs e) {
            cur_tool = Tool.ProjectionFrame;
            InvalidateWhole();
        }

        private void resetFrameToolStripMenuItem1_Click(object sender, EventArgs e) {
            cur_tool = Tool.ProjectionFrame;
            P1 = new Point(0, 0);
            P2 = new Point(source.Width, 0);
            P3 = new Point(source.Width, source.Height);
            P4 = new Point(0, source.Height);
            InvalidateWhole();
        }

        private void selectPolynomialProfilesToolStripMenuItem1_Click(object sender, EventArgs e) {
            cur_tool = Tool.PolynomialProfiles;
            InvalidateWhole();
        }

        private void resetPolynomialProfilesToolStripMenuItem1_Click(object sender, EventArgs e) {
            cur_tool = Tool.PolynomialProfiles;
            int y = s_to_y(last_mouse_pos.Y);
            if (Math.Abs(y - line1) < Math.Abs(y - line2)) {
                carcas1 = new List<Point>();
                pol1 = new Polynomial(pol_n, carcas1, firm_carcas1);
            } else {
                carcas2 = new List<Point>();
                pol2 = new Polynomial(pol_n, carcas2, firm_carcas1);
            }
            InvalidateWhole();
        }

        private void selectLightingPointsToolStripMenuItem1_Click(object sender, EventArgs e) {
            cur_tool = Tool.LightingPoints;
            InvalidateWhole();
        }

        private void resetLightingPointsToolStripMenuItem1_Click(object sender, EventArgs e) {
            cur_tool = Tool.LightingPoints;
            light_points = new List<Point>();
            InvalidateWhole();
        }

        private void printScreenToolStripMenuItem1_Click(object sender, EventArgs e) {
            if (menuStrip1.Visible) {
                menuStrip1.Hide();
            }else if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                Rectangle bounds = RectangleToScreen(this.ClientRectangle);
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height)) {
                    using (Graphics g = Graphics.FromImage(bitmap)) {                     
                        g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                        menuStrip1.Show();
                    }
                    bitmap.Save(saveFileDialog1.FileName, ImageFormat.Png);
                }
            }
        }

        private void deselectToolToolStripMenuItem_Click(object sender, EventArgs e) {
            cur_tool = Tool.None;
            InvalidateWhole();
        }

        private void ChangeSWINEBasis(double[][] carcas, Point p, Mswine.BasisFunction[] asb, Mswine.BasisFunction[] lb, Mswine.BasisFunction[] cb) {
            int x1 = x_to_s(p.X);
            if (carcas.Length < max_points) {
                int i = -1;
                for (int j = 0; j < carcas.Length; j++) {
                    int x2 = x_to_s((int)carcas[j][0]);
                    double d = Math.Abs(x2 - x1);
                    if (d < 5) {
                        i = j;
                    }
                }
                if (i >= 0) {
                    if (asb[i] == lb[i]) {
                        asb[i] = cb[i];
                    } else {
                        asb[i] = lb[i];
                    }
                }
            }
        }

        private void changeSWINEBasisToolStripMenuItem1_Click(object sender, EventArgs e) {
            int y = s_to_y(last_mouse_pos.Y);
            int x = s_to_x(last_mouse_pos.X);
            if (Math.Abs(y - line1) < Math.Abs(y - line2)) {
                ChangeSWINEBasis(swine_carcas1, new Point(x, y - line1), swine_basis1, swine_linear_basis1, swine_constant_basis1);
            } else {
                ChangeSWINEBasis(swine_carcas2, new Point(x, y - line2), swine_basis2, swine_linear_basis2, swine_constant_basis2);
            }
            InvalidateWhole();
        }

        private void freeFormDeformationToolStripMenuItem1_Click(object sender, EventArgs e) {
            cur_tool = Tool.FreeForm;
            InvalidateWhole();
        }

        private void fFDVectorsToolStripMenuItem_Click(object sender, EventArgs e) {
            cur_tool = Tool.FreeForm;
            InvalidateWhole();
        }

        private void approximateFFDToolStripMenuItem_Click(object sender, EventArgs e) {
            Cursor = Cursors.WaitCursor;
            Undo.push(source);
            if (ffd_points.Count < 8*2) {
                MessageBox.Show("There should be at least 9 vectors for successful polynomial approximation");
                return;
            }
            source = ImageTransformer.ByPolynomialModel(source, ffd_points);
            InvalidateWhole();
            Cursor = Cursors.Arrow;
        }
        
    }
}
