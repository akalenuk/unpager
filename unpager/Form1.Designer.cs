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
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripSeparator();
            this.projectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flattenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
            this.grayscaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripSeparator();
            this.darnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hotKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frameToCursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectPolynomialProfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetPolynomialProfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.printScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.deselectToolToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.realHotKeysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectFrameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.frameToCursorToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.resetFrameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.selectPolynomialProfilesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.resetPolynomialProfilesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.selectMSWINEProfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeSWINEBasisToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.printScreenToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.deselectToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.rectangularFrameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
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
            this.trivialitiesToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.trivialitiesToolStripMenuItem.Text = "Trivialities";
            // 
            // turnClockwiseToolStripMenuItem
            // 
            this.turnClockwiseToolStripMenuItem.Name = "turnClockwiseToolStripMenuItem";
            this.turnClockwiseToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.turnClockwiseToolStripMenuItem.Text = "Turn clockwise";
            this.turnClockwiseToolStripMenuItem.Click += new System.EventHandler(this.turnClockwiseToolStripMenuItem_Click);
            // 
            // turnContrclockwiseToolStripMenuItem
            // 
            this.turnContrclockwiseToolStripMenuItem.Name = "turnContrclockwiseToolStripMenuItem";
            this.turnContrclockwiseToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.turnContrclockwiseToolStripMenuItem.Text = "Turn contrclockwise";
            this.turnContrclockwiseToolStripMenuItem.Click += new System.EventHandler(this.turnContrclockwiseToolStripMenuItem_Click);
            // 
            // mirrorToolStripMenuItem
            // 
            this.mirrorToolStripMenuItem.Name = "mirrorToolStripMenuItem";
            this.mirrorToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.mirrorToolStripMenuItem.Text = "Mirror";
            this.mirrorToolStripMenuItem.Click += new System.EventHandler(this.mirrorToolStripMenuItem_Click);
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.rectangularFrameToolStripMenuItem1,
            this.projectionFrameToolStripMenuItem,
            this.polynomialProfilesToolStripMenuItem});
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.toolToolStripMenuItem.Text = "Tool";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.noneToolStripMenuItem.Text = "None";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.noneToolStripMenuItem_Click);
            // 
            // projectionFrameToolStripMenuItem
            // 
            this.projectionFrameToolStripMenuItem.Name = "projectionFrameToolStripMenuItem";
            this.projectionFrameToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.projectionFrameToolStripMenuItem.Text = "Projection frame";
            this.projectionFrameToolStripMenuItem.Click += new System.EventHandler(this.projectionFrameToolStripMenuItem_Click);
            // 
            // polynomialProfilesToolStripMenuItem
            // 
            this.polynomialProfilesToolStripMenuItem.Name = "polynomialProfilesToolStripMenuItem";
            this.polynomialProfilesToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.polynomialProfilesToolStripMenuItem.Text = "Polynomial profiles";
            this.polynomialProfilesToolStripMenuItem.Click += new System.EventHandler(this.polynomialProfilesToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.toolStripMenuItem15,
            this.projectToolStripMenuItem,
            this.flattenToolStripMenuItem,
            this.toolStripMenuItem13,
            this.grayscaleToolStripMenuItem,
            this.normalizeToolStripMenuItem,
            this.toolStripMenuItem14,
            this.darnToolStripMenuItem});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(32, 20);
            this.processToolStripMenuItem.Text = "Do";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(149, 6);
            // 
            // projectToolStripMenuItem
            // 
            this.projectToolStripMenuItem.Name = "projectToolStripMenuItem";
            this.projectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.projectToolStripMenuItem.Text = "Projection";
            this.projectToolStripMenuItem.Click += new System.EventHandler(this.projectToolStripMenuItem_Click);
            // 
            // flattenToolStripMenuItem
            // 
            this.flattenToolStripMenuItem.Name = "flattenToolStripMenuItem";
            this.flattenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.flattenToolStripMenuItem.Text = "Flattenning";
            this.flattenToolStripMenuItem.Click += new System.EventHandler(this.flattenToolStripMenuItem_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(149, 6);
            // 
            // grayscaleToolStripMenuItem
            // 
            this.grayscaleToolStripMenuItem.Name = "grayscaleToolStripMenuItem";
            this.grayscaleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.grayscaleToolStripMenuItem.Text = "Grayscale";
            this.grayscaleToolStripMenuItem.Click += new System.EventHandler(this.grayscaleToolStripMenuItem_Click);
            // 
            // normalizeToolStripMenuItem
            // 
            this.normalizeToolStripMenuItem.Name = "normalizeToolStripMenuItem";
            this.normalizeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.normalizeToolStripMenuItem.Text = "Normalize";
            this.normalizeToolStripMenuItem.Click += new System.EventHandler(this.normalizeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(149, 6);
            // 
            // darnToolStripMenuItem
            // 
            this.darnToolStripMenuItem.Name = "darnToolStripMenuItem";
            this.darnToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.darnToolStripMenuItem.Text = "Darn";
            this.darnToolStripMenuItem.Click += new System.EventHandler(this.darnToolStripMenuItem_Click);
            // 
            // hotKeysToolStripMenuItem
            // 
            this.hotKeysToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectFrameToolStripMenuItem,
            this.frameToCursorToolStripMenuItem,
            this.resetFrameToolStripMenuItem,
            this.toolStripMenuItem1,
            this.selectPolynomialProfilesToolStripMenuItem,
            this.resetPolynomialProfilesToolStripMenuItem,
            this.toolStripMenuItem2,
            this.printScreenToolStripMenuItem,
            this.toolStripMenuItem10,
            this.deselectToolToolStripMenuItem1});
            this.hotKeysToolStripMenuItem.Name = "hotKeysToolStripMenuItem";
            this.hotKeysToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.hotKeysToolStripMenuItem.Text = "Hot keys";
            // 
            // selectFrameToolStripMenuItem
            // 
            this.selectFrameToolStripMenuItem.Name = "selectFrameToolStripMenuItem";
            this.selectFrameToolStripMenuItem.ShortcutKeyDisplayString = "Alt + Q";
            this.selectFrameToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.selectFrameToolStripMenuItem.Text = "Select frame";
            this.selectFrameToolStripMenuItem.Click += new System.EventHandler(this.notRealMenuItem_Click);
            // 
            // frameToCursorToolStripMenuItem
            // 
            this.frameToCursorToolStripMenuItem.Name = "frameToCursorToolStripMenuItem";
            this.frameToCursorToolStripMenuItem.ShortcutKeyDisplayString = "Alt + A";
            this.frameToCursorToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.frameToCursorToolStripMenuItem.Text = "Frame to cursor";
            this.frameToCursorToolStripMenuItem.Click += new System.EventHandler(this.notRealMenuItem_Click);
            // 
            // resetFrameToolStripMenuItem
            // 
            this.resetFrameToolStripMenuItem.Name = "resetFrameToolStripMenuItem";
            this.resetFrameToolStripMenuItem.ShortcutKeyDisplayString = "Alt + Z";
            this.resetFrameToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.resetFrameToolStripMenuItem.Text = "Reset frame";
            this.resetFrameToolStripMenuItem.Click += new System.EventHandler(this.notRealMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(282, 6);
            // 
            // selectPolynomialProfilesToolStripMenuItem
            // 
            this.selectPolynomialProfilesToolStripMenuItem.Name = "selectPolynomialProfilesToolStripMenuItem";
            this.selectPolynomialProfilesToolStripMenuItem.ShortcutKeyDisplayString = "Alt + W";
            this.selectPolynomialProfilesToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.selectPolynomialProfilesToolStripMenuItem.Text = "Select polynomial profiles";
            this.selectPolynomialProfilesToolStripMenuItem.Click += new System.EventHandler(this.notRealMenuItem_Click);
            // 
            // resetPolynomialProfilesToolStripMenuItem
            // 
            this.resetPolynomialProfilesToolStripMenuItem.Name = "resetPolynomialProfilesToolStripMenuItem";
            this.resetPolynomialProfilesToolStripMenuItem.ShortcutKeyDisplayString = "Alt + S";
            this.resetPolynomialProfilesToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.resetPolynomialProfilesToolStripMenuItem.Text = "Reset polynomial profile";
            this.resetPolynomialProfilesToolStripMenuItem.Click += new System.EventHandler(this.notRealMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(282, 6);
            // 
            // printScreenToolStripMenuItem
            // 
            this.printScreenToolStripMenuItem.Name = "printScreenToolStripMenuItem";
            this.printScreenToolStripMenuItem.ShortcutKeyDisplayString = "Alt + P, Alt + P";
            this.printScreenToolStripMenuItem.Size = new System.Drawing.Size(285, 22);
            this.printScreenToolStripMenuItem.Text = "Hide menu and print screen";
            this.printScreenToolStripMenuItem.Click += new System.EventHandler(this.notRealMenuItem_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(282, 6);
            // 
            // deselectToolToolStripMenuItem1
            // 
            this.deselectToolToolStripMenuItem1.Name = "deselectToolToolStripMenuItem1";
            this.deselectToolToolStripMenuItem1.ShortcutKeyDisplayString = "Alt + O";
            this.deselectToolToolStripMenuItem1.Size = new System.Drawing.Size(285, 22);
            this.deselectToolToolStripMenuItem1.Text = "Deselect tool";
            // 
            // realHotKeysToolStripMenuItem
            // 
            this.realHotKeysToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectFrameToolStripMenuItem1,
            this.frameToCursorToolStripMenuItem1,
            this.resetFrameToolStripMenuItem1,
            this.toolStripMenuItem4,
            this.selectPolynomialProfilesToolStripMenuItem1,
            this.resetPolynomialProfilesToolStripMenuItem1,
            this.toolStripMenuItem5,
            this.selectMSWINEProfilesToolStripMenuItem,
            this.changeSWINEBasisToolStripMenuItem1,
            this.toolStripMenuItem8,
            this.printScreenToolStripMenuItem1,
            this.toolStripMenuItem9,
            this.deselectToolToolStripMenuItem});
            this.realHotKeysToolStripMenuItem.Name = "realHotKeysToolStripMenuItem";
            this.realHotKeysToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.realHotKeysToolStripMenuItem.Text = "Real hot keys";
            this.realHotKeysToolStripMenuItem.Visible = false;
            // 
            // selectFrameToolStripMenuItem1
            // 
            this.selectFrameToolStripMenuItem1.Name = "selectFrameToolStripMenuItem1";
            this.selectFrameToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Q)));
            this.selectFrameToolStripMenuItem1.Size = new System.Drawing.Size(232, 22);
            this.selectFrameToolStripMenuItem1.Text = "Select frame";
            this.selectFrameToolStripMenuItem1.Click += new System.EventHandler(this.selectFrameToolStripMenuItem1_Click);
            // 
            // frameToCursorToolStripMenuItem1
            // 
            this.frameToCursorToolStripMenuItem1.Name = "frameToCursorToolStripMenuItem1";
            this.frameToCursorToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.frameToCursorToolStripMenuItem1.Size = new System.Drawing.Size(232, 22);
            this.frameToCursorToolStripMenuItem1.Text = "Frame to cursor";
            this.frameToCursorToolStripMenuItem1.Click += new System.EventHandler(this.frameToCursorToolStripMenuItem1_Click);
            // 
            // resetFrameToolStripMenuItem1
            // 
            this.resetFrameToolStripMenuItem1.Name = "resetFrameToolStripMenuItem1";
            this.resetFrameToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Z)));
            this.resetFrameToolStripMenuItem1.Size = new System.Drawing.Size(232, 22);
            this.resetFrameToolStripMenuItem1.Text = "Reset frame";
            this.resetFrameToolStripMenuItem1.Click += new System.EventHandler(this.resetFrameToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(229, 6);
            // 
            // selectPolynomialProfilesToolStripMenuItem1
            // 
            this.selectPolynomialProfilesToolStripMenuItem1.Name = "selectPolynomialProfilesToolStripMenuItem1";
            this.selectPolynomialProfilesToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.W)));
            this.selectPolynomialProfilesToolStripMenuItem1.Size = new System.Drawing.Size(232, 22);
            this.selectPolynomialProfilesToolStripMenuItem1.Text = "Select polynomial profiles";
            this.selectPolynomialProfilesToolStripMenuItem1.Click += new System.EventHandler(this.selectPolynomialProfilesToolStripMenuItem1_Click);
            // 
            // resetPolynomialProfilesToolStripMenuItem1
            // 
            this.resetPolynomialProfilesToolStripMenuItem1.Name = "resetPolynomialProfilesToolStripMenuItem1";
            this.resetPolynomialProfilesToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.resetPolynomialProfilesToolStripMenuItem1.Size = new System.Drawing.Size(232, 22);
            this.resetPolynomialProfilesToolStripMenuItem1.Text = "Reset polynomial profile";
            this.resetPolynomialProfilesToolStripMenuItem1.Click += new System.EventHandler(this.resetPolynomialProfilesToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(229, 6);
            // 
            // selectMSWINEProfilesToolStripMenuItem
            // 
            this.selectMSWINEProfilesToolStripMenuItem.Name = "selectMSWINEProfilesToolStripMenuItem";
            this.selectMSWINEProfilesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.selectMSWINEProfilesToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.selectMSWINEProfilesToolStripMenuItem.Text = "Select MSWINE profiles";
            this.selectMSWINEProfilesToolStripMenuItem.Click += new System.EventHandler(this.sWINEProfilesToolStripMenuItem_Click);
            // 
            // changeSWINEBasisToolStripMenuItem1
            // 
            this.changeSWINEBasisToolStripMenuItem1.Name = "changeSWINEBasisToolStripMenuItem1";
            this.changeSWINEBasisToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.changeSWINEBasisToolStripMenuItem1.Size = new System.Drawing.Size(232, 22);
            this.changeSWINEBasisToolStripMenuItem1.Text = "Change SWINE basis";
            this.changeSWINEBasisToolStripMenuItem1.Click += new System.EventHandler(this.changeSWINEBasisToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(229, 6);
            // 
            // printScreenToolStripMenuItem1
            // 
            this.printScreenToolStripMenuItem1.Name = "printScreenToolStripMenuItem1";
            this.printScreenToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.printScreenToolStripMenuItem1.Size = new System.Drawing.Size(232, 22);
            this.printScreenToolStripMenuItem1.Text = "Print screen";
            this.printScreenToolStripMenuItem1.Click += new System.EventHandler(this.printScreenToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(229, 6);
            // 
            // deselectToolToolStripMenuItem
            // 
            this.deselectToolToolStripMenuItem.Name = "deselectToolToolStripMenuItem";
            this.deselectToolToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.deselectToolToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.deselectToolToolStripMenuItem.Text = "Deselect tool";
            this.deselectToolToolStripMenuItem.Click += new System.EventHandler(this.deselectToolToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // rectangularFrameToolStripMenuItem1
            // 
            this.rectangularFrameToolStripMenuItem1.Name = "rectangularFrameToolStripMenuItem1";
            this.rectangularFrameToolStripMenuItem1.Size = new System.Drawing.Size(163, 22);
            this.rectangularFrameToolStripMenuItem1.Text = "Rectangular frame";
            this.rectangularFrameToolStripMenuItem1.Click += new System.EventHandler(this.rectangularFrameToolStripMenuItem1_Click);
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
        private System.Windows.Forms.ToolStripMenuItem flattenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectionFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polynomialProfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grayscaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trivialitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turnClockwiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turnContrclockwiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mirrorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem realHotKeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frameToCursorToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem resetFrameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectFrameToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem selectPolynomialProfilesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem resetPolynomialProfilesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem selectMSWINEProfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem printScreenToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem deselectToolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeSWINEBasisToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem13;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem14;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem hotKeysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frameToCursorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem selectPolynomialProfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetPolynomialProfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem printScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem deselectToolToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem rectangularFrameToolStripMenuItem1;
    }
}

