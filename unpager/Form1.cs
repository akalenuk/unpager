using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            LightingPoints
        };
        
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

        List<Point> light_points = new List<Point>();

        public Form1()
        {
            InitializeComponent();
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseWheel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            openFileDialog1.Filter = "Pictures (*.png, *.jpg, ...)|*.jpg;*.jpeg;*.png;*.bmp|All Files (*.*)|*.*";
            saveFileDialog1.Filter = "PNG (*.png)|*.png|Everything (*.*)|*.*";
            SetMenuChecks();
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
                Invalidate();
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
                case Tool.LightingPoints:
                    scaled_w = (int)(source.Width * source_scale);
                    scaled_h = (int)(source.Height * source_scale);
                    e.Graphics.DrawImage(source, source_point.X, source_point.Y, scaled_w, scaled_h);
                    foreach (Point p in light_points) 
                    {
                        e.Graphics.DrawEllipse(red_pen, x_to_s(p.X) - 2, y_to_s(p.Y) - 2, 4, 4);
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
            switch(cur_tool)
            {
                case Tool.None:
                    if (rpoint_captured) {
                        source_point.X += e.X - capture_point.X;
                        source_point.Y += e.Y - capture_point.Y;
                        capture_point = new Point(e.X, e.Y);
                        Invalidate();
                    }
                    break;
                case Tool.ProjectionFrame:
                    if (rpoint_captured) {
                        source_point.X += e.X - capture_point.X;
                        source_point.Y += e.Y - capture_point.Y;
                        capture_point = new Point(e.X, e.Y);
                        Invalidate();
                    }
                    if (lpoint_captured) {
                        CheckProjPoint(new Point(e.X, e.Y));
                    }
                    break;
                case Tool.PolynomialProfiles:
                    if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                        CheckLine(e.Y);
                    }
                    break;
            }
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e) {
            if (cur_tool == Tool.ProjectionFrame || cur_tool == Tool.None)
            {
                source_scale += e.Delta / 5000.0;
                Invalidate();
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
                        Invalidate();
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
                            Invalidate();
                        }
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
                if (recttangularFrameToolStripMenuItem.Checked) {
                    P2.Y = sy;
                    P4.X = sx;
                }
            } else if (D2 <= D3 && D2 <= D4) {
                P2.X = sx;
                P2.Y = sy;
                if (recttangularFrameToolStripMenuItem.Checked) {
                    P1.Y = sy;
                    P3.X = sx;
                }
            } else if (D3 <= D4) {
                P3.X = sx;
                P3.Y = sy;
                if (recttangularFrameToolStripMenuItem.Checked) {
                    P2.X = sx;
                    P4.Y = sy;
                }
            } else {
                P4.X = sx;
                P4.Y = sy;
                if (recttangularFrameToolStripMenuItem.Checked) {
                    P1.X = sx;
                    P3.Y = sy;
                }
            }
            Invalidate();
        }

        private void CheckLine(int sy){
            int y = s_to_y(sy);
            if (Math.Abs(y - line1) < Math.Abs(y - line2)) {
                line1 = y;
            } else {
                line2 = y;
            }
            Invalidate();
        }

        private void CheckCarcas(Point p) {
            int y = s_to_y(p.Y);
            int x = s_to_x(p.X);
            if (Math.Abs(y - line1) < Math.Abs(y - line2)) {
                AddCarcas(carcas1, new Point(x, y - line1));
                pol1 = new Polynomial(pol_n, carcas1, firm_carcas1);
            } else {
                AddCarcas(carcas2, new Point(x, y - line2));
                pol2 = new Polynomial(pol_n, carcas2, firm_carcas2);
            }
            Invalidate();
        }

        private void AddCarcas(List<Point> carcas, Point p){
            if (carcas.Count < max_points) {
                carcas.Add(p);
            } else {
                carcas.RemoveAt(0);
                carcas.Add(p);
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
            cur_tool = Tool.PolynomialProfiles;
            source_scale = 0.8F * (double)ClientSize.Height / source.Height;
            source_point.X = ClientRectangle.Left + ClientSize.Width / 2 - (int)(source.Width * source_scale) / 2;
            source_point.Y = ClientRectangle.Top + ClientSize.Height / 2 - (int)(source.Height * source_scale) / 2;
            line1 = (int)(source.Height * source_scale) / 10;
            line2 = source.Height - line1;
            firm_carcas1 = new List<Point>();
            firm_carcas1.Add(new Point(0, 0));
            firm_carcas2 = new List<Point>();
            firm_carcas2.Add(new Point(0, 0));
            Invalidate();
            Cursor = Cursors.Arrow;
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
            source = ImageTransformer.ByPolynomialProfiles(csource, pol1, pol2, line1, line2);
            cur_tool = Tool.LightingPoints;
            source_scale = 0.8F * (double)ClientSize.Height / source.Height;
            source_point.X = ClientRectangle.Left + ClientSize.Width / 2 - (int)(source.Width * source_scale) / 2;
            source_point.Y = ClientRectangle.Top + ClientSize.Height / 2 - (int)(source.Height * source_scale) / 2;
            Invalidate();
            Cursor = Cursors.Arrow;
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Undo.push(source);
            source = ImageProcessor.FlattenLight(source, light_points);
            cur_tool = Tool.None;
            Invalidate();
            Cursor = Cursors.Arrow;
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
            SetMenuChecks();
            Invalidate();
        }

        private void polynomialProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cur_tool = Tool.PolynomialProfiles;
            SetMenuChecks();
            Invalidate();
        }

        private void lightingPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cur_tool = Tool.LightingPoints;
            SetMenuChecks();
            Invalidate();
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cur_tool = Tool.None;
            SetMenuChecks();
            Invalidate();
        }

        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Undo.push(source);
            source = ImageProcessor.Reinterpolate(source, 4, 4);
            Invalidate();
            Cursor = Cursors.Arrow;
        }

        private void grayscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Undo.push(source);
            source = ImageProcessor.Grayscale(source);
            Invalidate();
            Cursor = Cursors.Arrow;
        }

        private void normalizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Undo.push(source);
            source = ImageProcessor.Normalize(source);
            Invalidate();
            Cursor = Cursors.Arrow;
        }

        private void findLightingPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            cur_tool = Tool.LightingPoints;
            light_points.Clear();
            light_points = Automatic.FindLightingPoints(source, 10, 10);
            SetMenuChecks();
            Invalidate();
            Cursor = Cursors.Arrow;
        }

        private void turnClockwiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo.push(source);
            source.RotateFlip(RotateFlipType.Rotate90FlipNone);
            Invalidate();
        }

        private void turnContrclockwiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo.push(source);
            source.RotateFlip(RotateFlipType.Rotate270FlipNone);
            Invalidate();
        }

        private void mirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo.push(source);
            source.RotateFlip(RotateFlipType.RotateNoneFlipX);
            Invalidate();
        }

        private void SetMenuChecks() {
            noneToolStripMenuItem.Checked = false;
            projectionFrameToolStripMenuItem.Checked = false;
            polynomialProfilesToolStripMenuItem.Checked = false;
            lightingPointsToolStripMenuItem.Checked = false;
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
                case Tool.LightingPoints:
                    lightingPointsToolStripMenuItem.Checked = true;
                    break;
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                source = Undo.pop();
                Invalidate();
            }catch(UndoPopException ex){
                MessageBox.Show(ex.Message);
            }
        }
    }
}
