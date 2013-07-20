﻿namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trivialitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnClockwiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turnContrclockwiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mirrorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectionFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polynomialProfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightingPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flattenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sharpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grayscaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.automaticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findLightingPointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smoothTransformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rectangularFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.hotKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frameToCursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.realHotKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frameToCursorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.resetFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetFrameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.selectFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectFrameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.trivialitiesToolStripMenuItem,
            this.toolToolStripMenuItem,
            this.processToolStripMenuItem,
            this.automaticToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.hotKeysToolStripMenuItem,
            this.realHotKeysToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(568, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // trivialitiesToolStripMenuItem
            // 
            this.trivialitiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.turnClockwiseToolStripMenuItem,
            this.turnContrclockwiseToolStripMenuItem,
            this.mirrorToolStripMenuItem});
            this.trivialitiesToolStripMenuItem.Name = "trivialitiesToolStripMenuItem";
            this.trivialitiesToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.trivialitiesToolStripMenuItem.Text = "Trivialities";
            // 
            // turnClockwiseToolStripMenuItem
            // 
            this.turnClockwiseToolStripMenuItem.Name = "turnClockwiseToolStripMenuItem";
            this.turnClockwiseToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.turnClockwiseToolStripMenuItem.Text = "Turn clockwise";
            this.turnClockwiseToolStripMenuItem.Click += new System.EventHandler(this.turnClockwiseToolStripMenuItem_Click);
            // 
            // turnContrclockwiseToolStripMenuItem
            // 
            this.turnContrclockwiseToolStripMenuItem.Name = "turnContrclockwiseToolStripMenuItem";
            this.turnContrclockwiseToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.turnContrclockwiseToolStripMenuItem.Text = "Turn contrclockwise";
            this.turnContrclockwiseToolStripMenuItem.Click += new System.EventHandler(this.turnContrclockwiseToolStripMenuItem_Click);
            // 
            // mirrorToolStripMenuItem
            // 
            this.mirrorToolStripMenuItem.Name = "mirrorToolStripMenuItem";
            this.mirrorToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.mirrorToolStripMenuItem.Text = "Mirror";
            this.mirrorToolStripMenuItem.Click += new System.EventHandler(this.mirrorToolStripMenuItem_Click);
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.projectionFrameToolStripMenuItem,
            this.polynomialProfilesToolStripMenuItem,
            this.lightingPointsToolStripMenuItem});
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.toolToolStripMenuItem.Text = "Tool";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.noneToolStripMenuItem.Text = "None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // projectionFrameToolStripMenuItem
            // 
            this.projectionFrameToolStripMenuItem.Name = "projectionFrameToolStripMenuItem";
            this.projectionFrameToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.projectionFrameToolStripMenuItem.Text = "Projection frame";
            this.projectionFrameToolStripMenuItem.Click += new System.EventHandler(this.projectionFrameToolStripMenuItem_Click);
            // 
            // polynomialProfilesToolStripMenuItem
            // 
            this.polynomialProfilesToolStripMenuItem.Name = "polynomialProfilesToolStripMenuItem";
            this.polynomialProfilesToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.polynomialProfilesToolStripMenuItem.Text = "Polynomial profiles";
            this.polynomialProfilesToolStripMenuItem.Click += new System.EventHandler(this.polynomialProfilesToolStripMenuItem_Click);
            // 
            // lightingPointsToolStripMenuItem
            // 
            this.lightingPointsToolStripMenuItem.Name = "lightingPointsToolStripMenuItem";
            this.lightingPointsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.lightingPointsToolStripMenuItem.Text = "Lighting points";
            this.lightingPointsToolStripMenuItem.Click += new System.EventHandler(this.lightingPointsToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.projectToolStripMenuItem,
            this.flattenToolStripMenuItem,
            this.lightToolStripMenuItem,
            this.sharpenToolStripMenuItem,
            this.grayscaleToolStripMenuItem,
            this.normalizeToolStripMenuItem,
            this.darnToolStripMenuItem});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(34, 20);
            this.processToolStripMenuItem.Text = "Do";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.projectToolStripMenuItem.Text = "Projection";
            this.projectToolStripMenuItem.Click += new System.EventHandler(this.projectToolStripMenuItem_Click);
            // 
            // flattenToolStripMenuItem
            // 
            this.flattenToolStripMenuItem.Name = "flattenToolStripMenuItem";
            this.flattenToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.flattenToolStripMenuItem.Text = "Flattenning";
            this.flattenToolStripMenuItem.Click += new System.EventHandler(this.flattenToolStripMenuItem_Click);
            // 
            // lightToolStripMenuItem
            // 
            this.lightToolStripMenuItem.Name = "lightToolStripMenuItem";
            this.lightToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.lightToolStripMenuItem.Text = "Lighting";
            this.lightToolStripMenuItem.Click += new System.EventHandler(this.lightToolStripMenuItem_Click);
            // 
            // sharpenToolStripMenuItem
            // 
            this.sharpenToolStripMenuItem.Name = "sharpenToolStripMenuItem";
            this.sharpenToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.sharpenToolStripMenuItem.Text = "Soft clustering";
            this.sharpenToolStripMenuItem.Click += new System.EventHandler(this.sharpenToolStripMenuItem_Click);
            // 
            // grayscaleToolStripMenuItem
            // 
            this.grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem";
            this.grayscaleToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.grayscaleToolStripMenuItem.Text = "Grayscale";
            this.grayscaleToolStripMenuItem.Click += new System.EventHandler(this.grayscaleToolStripMenuItem_Click);
            // 
            // normalizeToolStripMenuItem
            // 
            this.normalizeToolStripMenuItem.Name = "normalizeToolStripMenuItem";
            this.normalizeToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.normalizeToolStripMenuItem.Text = "Normalize";
            this.normalizeToolStripMenuItem.Click += new System.EventHandler(this.normalizeToolStripMenuItem_Click);
            // 
            // darnToolStripMenuItem
            // 
            this.darnToolStripMenuItem.Name = "darnToolStripMenuItem";
            this.darnToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.darnToolStripMenuItem.Text = "Darn";
            this.darnToolStripMenuItem.Click += new System.EventHandler(this.darnToolStripMenuItem_Click);
            // 
            // automaticToolStripMenuItem
            // 
            this.automaticToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findLightingPointsToolStripMenuItem});
            this.automaticToolStripMenuItem.Name = "automaticToolStripMenuItem";
            this.automaticToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.automaticToolStripMenuItem.Text = "Automatically";
            // 
            // findLightingPointsToolStripMenuItem
            // 
            this.findLightingPointsToolStripMenuItem.Name = "findLightingPointsToolStripMenuItem";
            this.findLightingPointsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.findLightingPointsToolStripMenuItem.Text = "Find lighting points";
            this.findLightingPointsToolStripMenuItem.Click += new System.EventHandler(this.findLightingPointsToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smoothTransformToolStripMenuItem,
            this.rectangularFrameToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.settingsToolStripMenuItem.Text = "Selectables";
            // 
            // smoothTransformToolStripMenuItem
            // 
            this.smoothTransformToolStripMenuItem.Checked = true;
            this.smoothTransformToolStripMenuItem.CheckOnClick = true;
            this.smoothTransformToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smoothTransformToolStripMenuItem.Name = "smoothTransformToolStripMenuItem";
            this.smoothTransformToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.smoothTransformToolStripMenuItem.Text = "Smooth transform";
            // 
            // rectangularFrameToolStripMenuItem
            // 
            this.rectangularFrameToolStripMenuItem.CheckOnClick = true;
            this.rectangularFrameToolStripMenuItem.Name = "rectangularFrameToolStripMenuItem";
            this.rectangularFrameToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.rectangularFrameToolStripMenuItem.Text = "Recttangular frame";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // hotKeysToolStripMenuItem
            // 
            this.hotKeysToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectFrameToolStripMenuItem,
            this.frameToCursorToolStripMenuItem,
            this.resetFrameToolStripMenuItem});
            this.hotKeysToolStripMenuItem.Name = "hotKeysToolStripMenuItem";
            this.hotKeysToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.hotKeysToolStripMenuItem.Text = "Hot keys";
            // 
            // frameToCursorToolStripMenuItem
            // 
            this.frameToCursorToolStripMenuItem.Name = "frameToCursorToolStripMenuItem";
            this.frameToCursorToolStripMenuItem.ShortcutKeyDisplayString = "Alt + A";
            this.frameToCursorToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.frameToCursorToolStripMenuItem.Text = "Frame to cursor";
            this.frameToCursorToolStripMenuItem.Click += new System.EventHandler(this.frameToCursorToolStripMenuItem_Click);
            // 
            // realHotKeysToolStripMenuItem
            // 
            this.realHotKeysToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectFrameToolStripMenuItem1,
            this.frameToCursorToolStripMenuItem1,
            this.resetFrameToolStripMenuItem1});
            this.realHotKeysToolStripMenuItem.Name = "realHotKeysToolStripMenuItem";
            this.realHotKeysToolStripMenuItem.Size = new System.Drawing.Size(88, 20);
            this.realHotKeysToolStripMenuItem.Text = "Real hot keys";
            this.realHotKeysToolStripMenuItem.Visible = false;
            // 
            // frameToCursorToolStripMenuItem1
            // 
            this.frameToCursorToolStripMenuItem1.Name = "frameToCursorToolStripMenuItem1";
            this.frameToCursorToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.frameToCursorToolStripMenuItem1.Size = new System.Drawing.Size(195, 22);
            this.frameToCursorToolStripMenuItem1.Text = "Frame to cursor";
            this.frameToCursorToolStripMenuItem1.Click += new System.EventHandler(this.frameToCursorToolStripMenuItem1_Click);
            // 
            // resetFrameToolStripMenuItem
            // 
            this.resetFrameToolStripMenuItem.Name = "resetFrameToolStripMenuItem";
            this.resetFrameToolStripMenuItem.ShortcutKeyDisplayString = "Alt + Z";
            this.resetFrameToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.resetFrameToolStripMenuItem.Text = "Reset frame";
            this.resetFrameToolStripMenuItem.Click += new System.EventHandler(this.resetFrameToolStripMenuItem_Click);
            // 
            // resetFrameToolStripMenuItem1
            // 
            this.resetFrameToolStripMenuItem1.Name = "resetFrameToolStripMenuItem1";
            this.resetFrameToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Z)));
            this.resetFrameToolStripMenuItem1.Size = new System.Drawing.Size(195, 22);
            this.resetFrameToolStripMenuItem1.Text = "Reset frame";
            this.resetFrameToolStripMenuItem1.Click += new System.EventHandler(this.resetFrameToolStripMenuItem1_Click);
            // 
            // selectFrameToolStripMenuItem
            // 
            this.selectFrameToolStripMenuItem.Name = "selectFrameToolStripMenuItem";
            this.selectFrameToolStripMenuItem.ShortcutKeyDisplayString = "Alt + Q";
            this.selectFrameToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.selectFrameToolStripMenuItem.Text = "Select frame";
            this.selectFrameToolStripMenuItem.Click += new System.EventHandler(this.selectFrameToolStripMenuItem_Click);
            // 
            // selectFrameToolStripMenuItem1
            // 
            this.selectFrameToolStripMenuItem1.Name = "selectFrameToolStripMenuItem1";
            this.selectFrameToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Q)));
            this.selectFrameToolStripMenuItem1.Size = new System.Drawing.Size(195, 22);
            this.selectFrameToolStripMenuItem1.Text = "Select frame";
            this.selectFrameToolStripMenuItem1.Click += new System.EventHandler(this.selectFrameToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 262);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Unpager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smoothTransformToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flattenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectionFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polynomialProfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lightingPointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sharpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayscaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem automaticToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findLightingPointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trivialitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turnClockwiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turnContrclockwiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirrorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rectangularFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hotKeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frameToCursorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem realHotKeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frameToCursorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem resetFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetFrameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectFrameToolStripMenuItem1;
    }
}

