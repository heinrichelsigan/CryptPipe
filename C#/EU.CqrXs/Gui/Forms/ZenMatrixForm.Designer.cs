using EU.CqrXs.Gui.Controls;

namespace EU.CqrXs.Gui.Forms
{
    partial class ZenMatrixForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZenMatrixForm));
            zenMatrixVControl = new ZenMatrixVControl();
            textBoxSymmKey = new TextBox();
            labelSymmKey = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            labelPointsF = new Label();
            labelPointsE = new Label();
            labelPointsD = new Label();
            labelPointsC = new Label();
            labelPointsB = new Label();
            labelPointsA = new Label();
            labelPoints9 = new Label();
            labelPoints8 = new Label();
            labelPoints7 = new Label();
            labelPoints6 = new Label();
            labelPoints5 = new Label();
            labelPoints4 = new Label();
            labelMap0 = new Label();
            labelMap1 = new Label();
            labelMap2 = new Label();
            labelMap3 = new Label();
            labelMap4 = new Label();
            labelMap5 = new Label();
            labelMap6 = new Label();
            labelMap7 = new Label();
            labelMap8 = new Label();
            labelMap9 = new Label();
            labelMapA = new Label();
            labelMapB = new Label();
            labelMapC = new Label();
            labelMapD = new Label();
            labelMapE = new Label();
            labelMapF = new Label();
            labelPoints0 = new Label();
            labelPoints1 = new Label();
            labelPoints2 = new Label();
            labelPoints3 = new Label();
            textBoxPermKey = new TextBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator = new ToolStripSeparator();
            printToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            menuOptionsMenuModes = new ToolStripMenuItem();
            menuOptionsModesComplex = new ToolStripMenuItem();
            menuModeZenMatrix = new ToolStripMenuItem();
            menuOptionsModesSimple = new ToolStripMenuItem();
            menuOptionsModes123Fish = new ToolStripMenuItem();
            menuHelp = new ToolStripMenuItem();
            menuAbout = new ToolStripMenuItem();
            menuHelpHelp = new ToolStripMenuItem();
            labelPermKey = new Label();
            tableLayoutPanel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // zenMatrixVControl
            // 
            zenMatrixVControl.BackgroundImage = (Image)resources.GetObject("zenMatrixVControl.BackgroundImage");
            zenMatrixVControl.Location = new Point(1, 68);
            zenMatrixVControl.Margin = new Padding(2);
            zenMatrixVControl.Name = "zenMatrixVControl";
            zenMatrixVControl.Size = new Size(514, 514);
            zenMatrixVControl.TabIndex = 0;
            // 
            // textBoxSymmKey
            // 
            textBoxSymmKey.Location = new Point(81, 31);
            textBoxSymmKey.Name = "textBoxSymmKey";
            textBoxSymmKey.Size = new Size(296, 23);
            textBoxSymmKey.TabIndex = 1;
            textBoxSymmKey.TextChanged += SymmKey_Changed;
            // 
            // labelSymmKey
            // 
            labelSymmKey.AutoSize = true;
            labelSymmKey.Location = new Point(12, 32);
            labelSymmKey.Name = "labelSymmKey";
            labelSymmKey.Size = new Size(63, 15);
            labelSymmKey.TabIndex = 2;
            labelSymmKey.Text = "SymmKey:";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(labelPointsF, 1, 15);
            tableLayoutPanel1.Controls.Add(labelPointsE, 1, 14);
            tableLayoutPanel1.Controls.Add(labelPointsD, 1, 13);
            tableLayoutPanel1.Controls.Add(labelPointsC, 1, 12);
            tableLayoutPanel1.Controls.Add(labelPointsB, 1, 11);
            tableLayoutPanel1.Controls.Add(labelPointsA, 1, 10);
            tableLayoutPanel1.Controls.Add(labelPoints9, 1, 9);
            tableLayoutPanel1.Controls.Add(labelPoints8, 1, 8);
            tableLayoutPanel1.Controls.Add(labelPoints7, 1, 7);
            tableLayoutPanel1.Controls.Add(labelPoints6, 1, 6);
            tableLayoutPanel1.Controls.Add(labelPoints5, 1, 5);
            tableLayoutPanel1.Controls.Add(labelPoints4, 1, 4);
            tableLayoutPanel1.Controls.Add(labelMap0, 0, 0);
            tableLayoutPanel1.Controls.Add(labelMap1, 0, 1);
            tableLayoutPanel1.Controls.Add(labelMap2, 0, 2);
            tableLayoutPanel1.Controls.Add(labelMap3, 0, 3);
            tableLayoutPanel1.Controls.Add(labelMap4, 0, 4);
            tableLayoutPanel1.Controls.Add(labelMap5, 0, 5);
            tableLayoutPanel1.Controls.Add(labelMap6, 0, 6);
            tableLayoutPanel1.Controls.Add(labelMap7, 0, 7);
            tableLayoutPanel1.Controls.Add(labelMap8, 0, 8);
            tableLayoutPanel1.Controls.Add(labelMap9, 0, 9);
            tableLayoutPanel1.Controls.Add(labelMapA, 0, 10);
            tableLayoutPanel1.Controls.Add(labelMapB, 0, 11);
            tableLayoutPanel1.Controls.Add(labelMapC, 0, 12);
            tableLayoutPanel1.Controls.Add(labelMapD, 0, 13);
            tableLayoutPanel1.Controls.Add(labelMapE, 0, 14);
            tableLayoutPanel1.Controls.Add(labelMapF, 0, 15);
            tableLayoutPanel1.Controls.Add(labelPoints0, 1, 0);
            tableLayoutPanel1.Controls.Add(labelPoints1, 1, 1);
            tableLayoutPanel1.Controls.Add(labelPoints2, 1, 2);
            tableLayoutPanel1.Controls.Add(labelPoints3, 1, 3);
            tableLayoutPanel1.Font = new Font("Lucida Sans Unicode", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tableLayoutPanel1.Location = new Point(661, 68);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 16;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.25F));
            tableLayoutPanel1.Size = new Size(124, 485);
            tableLayoutPanel1.TabIndex = 3;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // labelPointsF
            // 
            labelPointsF.AutoSize = true;
            labelPointsF.Location = new Point(65, 450);
            labelPointsF.Name = "labelPointsF";
            labelPointsF.Size = new Size(18, 20);
            labelPointsF.TabIndex = 31;
            labelPointsF.Text = "F";
            // 
            // labelPointsE
            // 
            labelPointsE.AutoSize = true;
            labelPointsE.Location = new Point(65, 420);
            labelPointsE.Name = "labelPointsE";
            labelPointsE.Size = new Size(18, 20);
            labelPointsE.TabIndex = 30;
            labelPointsE.Text = "E";
            // 
            // labelPointsD
            // 
            labelPointsD.AutoSize = true;
            labelPointsD.Location = new Point(65, 390);
            labelPointsD.Name = "labelPointsD";
            labelPointsD.Size = new Size(21, 20);
            labelPointsD.TabIndex = 29;
            labelPointsD.Text = "D";
            // 
            // labelPointsC
            // 
            labelPointsC.AutoSize = true;
            labelPointsC.Location = new Point(65, 360);
            labelPointsC.Name = "labelPointsC";
            labelPointsC.Size = new Size(20, 20);
            labelPointsC.TabIndex = 28;
            labelPointsC.Text = "C";
            // 
            // labelPointsB
            // 
            labelPointsB.AutoSize = true;
            labelPointsB.Location = new Point(65, 330);
            labelPointsB.Name = "labelPointsB";
            labelPointsB.Size = new Size(18, 20);
            labelPointsB.TabIndex = 27;
            labelPointsB.Text = "B";
            // 
            // labelPointsA
            // 
            labelPointsA.AutoSize = true;
            labelPointsA.Location = new Point(65, 300);
            labelPointsA.Name = "labelPointsA";
            labelPointsA.Size = new Size(20, 20);
            labelPointsA.TabIndex = 26;
            labelPointsA.Text = "A";
            // 
            // labelPoints9
            // 
            labelPoints9.AutoSize = true;
            labelPoints9.Location = new Point(65, 270);
            labelPoints9.Name = "labelPoints9";
            labelPoints9.Size = new Size(19, 20);
            labelPoints9.TabIndex = 25;
            labelPoints9.Text = "9";
            // 
            // labelPoints8
            // 
            labelPoints8.AutoSize = true;
            labelPoints8.Location = new Point(65, 240);
            labelPoints8.Name = "labelPoints8";
            labelPoints8.Size = new Size(19, 20);
            labelPoints8.TabIndex = 24;
            labelPoints8.Text = "8";
            // 
            // labelPoints7
            // 
            labelPoints7.AutoSize = true;
            labelPoints7.Location = new Point(65, 210);
            labelPoints7.Name = "labelPoints7";
            labelPoints7.Size = new Size(19, 20);
            labelPoints7.TabIndex = 23;
            labelPoints7.Text = "7";
            // 
            // labelPoints6
            // 
            labelPoints6.AutoSize = true;
            labelPoints6.Location = new Point(65, 180);
            labelPoints6.Name = "labelPoints6";
            labelPoints6.Size = new Size(19, 20);
            labelPoints6.TabIndex = 22;
            labelPoints6.Text = "6";
            // 
            // labelPoints5
            // 
            labelPoints5.AutoSize = true;
            labelPoints5.Location = new Point(65, 150);
            labelPoints5.Name = "labelPoints5";
            labelPoints5.Size = new Size(19, 20);
            labelPoints5.TabIndex = 21;
            labelPoints5.Text = "5";
            // 
            // labelPoints4
            // 
            labelPoints4.AutoSize = true;
            labelPoints4.Location = new Point(65, 120);
            labelPoints4.Name = "labelPoints4";
            labelPoints4.Size = new Size(19, 20);
            labelPoints4.TabIndex = 20;
            labelPoints4.Text = "4";
            // 
            // labelMap0
            // 
            labelMap0.AutoSize = true;
            labelMap0.Location = new Point(3, 0);
            labelMap0.Name = "labelMap0";
            labelMap0.Size = new Size(50, 20);
            labelMap0.TabIndex = 0;
            labelMap0.Text = "0 =>";
            // 
            // labelMap1
            // 
            labelMap1.AutoSize = true;
            labelMap1.Location = new Point(3, 30);
            labelMap1.Name = "labelMap1";
            labelMap1.Size = new Size(50, 20);
            labelMap1.TabIndex = 1;
            labelMap1.Text = "1 =>";
            // 
            // labelMap2
            // 
            labelMap2.AutoSize = true;
            labelMap2.Location = new Point(3, 60);
            labelMap2.Name = "labelMap2";
            labelMap2.Size = new Size(50, 20);
            labelMap2.TabIndex = 2;
            labelMap2.Text = "2 =>";
            // 
            // labelMap3
            // 
            labelMap3.AutoSize = true;
            labelMap3.Location = new Point(3, 90);
            labelMap3.Name = "labelMap3";
            labelMap3.Size = new Size(50, 20);
            labelMap3.TabIndex = 3;
            labelMap3.Text = "3 =>";
            // 
            // labelMap4
            // 
            labelMap4.AutoSize = true;
            labelMap4.Location = new Point(3, 120);
            labelMap4.Name = "labelMap4";
            labelMap4.Size = new Size(50, 20);
            labelMap4.TabIndex = 4;
            labelMap4.Text = "4 =>";
            // 
            // labelMap5
            // 
            labelMap5.AutoSize = true;
            labelMap5.Location = new Point(3, 150);
            labelMap5.Name = "labelMap5";
            labelMap5.Size = new Size(50, 20);
            labelMap5.TabIndex = 5;
            labelMap5.Text = "5 =>";
            // 
            // labelMap6
            // 
            labelMap6.AutoSize = true;
            labelMap6.Location = new Point(3, 180);
            labelMap6.Name = "labelMap6";
            labelMap6.Size = new Size(50, 20);
            labelMap6.TabIndex = 6;
            labelMap6.Text = "6 =>";
            // 
            // labelMap7
            // 
            labelMap7.AutoSize = true;
            labelMap7.Location = new Point(3, 210);
            labelMap7.Name = "labelMap7";
            labelMap7.Size = new Size(50, 20);
            labelMap7.TabIndex = 7;
            labelMap7.Text = "7 =>";
            // 
            // labelMap8
            // 
            labelMap8.AutoSize = true;
            labelMap8.Location = new Point(3, 240);
            labelMap8.Name = "labelMap8";
            labelMap8.Size = new Size(50, 20);
            labelMap8.TabIndex = 8;
            labelMap8.Text = "8 =>";
            // 
            // labelMap9
            // 
            labelMap9.AutoSize = true;
            labelMap9.Location = new Point(3, 270);
            labelMap9.Name = "labelMap9";
            labelMap9.Size = new Size(50, 20);
            labelMap9.TabIndex = 9;
            labelMap9.Text = "9 =>";
            // 
            // labelMapA
            // 
            labelMapA.AutoSize = true;
            labelMapA.Location = new Point(3, 300);
            labelMapA.Name = "labelMapA";
            labelMapA.Size = new Size(51, 20);
            labelMapA.TabIndex = 10;
            labelMapA.Text = "A =>";
            // 
            // labelMapB
            // 
            labelMapB.AutoSize = true;
            labelMapB.Location = new Point(3, 330);
            labelMapB.Name = "labelMapB";
            labelMapB.Size = new Size(49, 20);
            labelMapB.TabIndex = 11;
            labelMapB.Text = "B =>";
            // 
            // labelMapC
            // 
            labelMapC.AutoSize = true;
            labelMapC.Location = new Point(3, 360);
            labelMapC.Name = "labelMapC";
            labelMapC.Size = new Size(46, 20);
            labelMapC.TabIndex = 12;
            labelMapC.Text = "C=>";
            // 
            // labelMapD
            // 
            labelMapD.AutoSize = true;
            labelMapD.Location = new Point(3, 390);
            labelMapD.Name = "labelMapD";
            labelMapD.Size = new Size(52, 20);
            labelMapD.TabIndex = 13;
            labelMapD.Text = "D =>";
            // 
            // labelMapE
            // 
            labelMapE.AutoSize = true;
            labelMapE.Location = new Point(3, 420);
            labelMapE.Name = "labelMapE";
            labelMapE.Size = new Size(49, 20);
            labelMapE.TabIndex = 14;
            labelMapE.Text = "E =>";
            // 
            // labelMapF
            // 
            labelMapF.AutoSize = true;
            labelMapF.Location = new Point(3, 450);
            labelMapF.Name = "labelMapF";
            labelMapF.Size = new Size(44, 20);
            labelMapF.TabIndex = 15;
            labelMapF.Text = "F=>";
            // 
            // labelPoints0
            // 
            labelPoints0.AutoSize = true;
            labelPoints0.Location = new Point(65, 0);
            labelPoints0.Name = "labelPoints0";
            labelPoints0.Size = new Size(19, 20);
            labelPoints0.TabIndex = 16;
            labelPoints0.Text = "0";
            // 
            // labelPoints1
            // 
            labelPoints1.AutoSize = true;
            labelPoints1.Location = new Point(65, 30);
            labelPoints1.Name = "labelPoints1";
            labelPoints1.Size = new Size(24, 20);
            labelPoints1.TabIndex = 17;
            labelPoints1.Text = "1 ";
            // 
            // labelPoints2
            // 
            labelPoints2.AutoSize = true;
            labelPoints2.Location = new Point(65, 60);
            labelPoints2.Name = "labelPoints2";
            labelPoints2.Size = new Size(19, 20);
            labelPoints2.TabIndex = 18;
            labelPoints2.Text = "2";
            // 
            // labelPoints3
            // 
            labelPoints3.AutoSize = true;
            labelPoints3.Location = new Point(65, 90);
            labelPoints3.Name = "labelPoints3";
            labelPoints3.Size = new Size(19, 20);
            labelPoints3.TabIndex = 19;
            labelPoints3.Text = "3";
            // 
            // textBoxPermKey
            // 
            textBoxPermKey.Location = new Point(661, 32);
            textBoxPermKey.MaxLength = 16;
            textBoxPermKey.Name = "textBoxPermKey";
            textBoxPermKey.Size = new Size(124, 23);
            textBoxPermKey.TabIndex = 4;
            textBoxPermKey.TextChanged += PermKey_Changed;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, toolsToolStripMenuItem, menuHelp });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(797, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripSeparator, printToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 20);
            fileToolStripMenuItem.Text = "Main";
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(177, 6);
            // 
            // printToolStripMenuItem
            // 
            printToolStripMenuItem.Image = (Image)resources.GetObject("printToolStripMenuItem.Image");
            printToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            printToolStripMenuItem.Name = "printToolStripMenuItem";
            printToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            printToolStripMenuItem.Size = new Size(180, 22);
            printToolStripMenuItem.Text = "&Print";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(180, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuOptionsMenuModes });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(61, 20);
            toolsToolStripMenuItem.Text = "Options";
            // 
            // menuOptionsMenuModes
            // 
            menuOptionsMenuModes.BackColor = SystemColors.Menu;
            menuOptionsMenuModes.DropDownItems.AddRange(new ToolStripItem[] { menuOptionsModesComplex, menuModeZenMatrix, menuOptionsModesSimple, menuOptionsModes123Fish });
            menuOptionsMenuModes.Name = "menuOptionsMenuModes";
            menuOptionsMenuModes.Size = new Size(180, 22);
            menuOptionsMenuModes.Text = "Modes";
            // 
            // menuOptionsModesComplex
            // 
            menuOptionsModesComplex.BackColor = SystemColors.Menu;
            menuOptionsModesComplex.Name = "menuOptionsModesComplex";
            menuOptionsModesComplex.Size = new Size(180, 22);
            menuOptionsModesComplex.Text = "Mode Complex";
            menuOptionsModesComplex.Click += menuOptionsModesComplex_Click;
            // 
            // menuModeZenMatrix
            // 
            menuModeZenMatrix.BackColor = SystemColors.GradientInactiveCaption;
            menuModeZenMatrix.Checked = true;
            menuModeZenMatrix.CheckState = CheckState.Checked;
            menuModeZenMatrix.Enabled = false;
            menuModeZenMatrix.Name = "menuModeZenMatrix";
            menuModeZenMatrix.Size = new Size(180, 22);
            menuModeZenMatrix.Text = "ZenMatrix";
            // 
            // menuOptionsModesSimple
            // 
            menuOptionsModesSimple.BackColor = SystemColors.Menu;
            menuOptionsModesSimple.Name = "menuOptionsModesSimple";
            menuOptionsModesSimple.Size = new Size(180, 22);
            menuOptionsModesSimple.Text = "Mode Simple";
            menuOptionsModesSimple.Click += menuOptionsModesSimple_Click;
            // 
            // menuOptionsModes123Fish
            // 
            menuOptionsModes123Fish.BackColor = SystemColors.Menu;
            menuOptionsModes123Fish.Name = "menuOptionsModes123Fish";
            menuOptionsModes123Fish.Size = new Size(180, 22);
            menuOptionsModes123Fish.Text = "123-Fish";
            menuOptionsModes123Fish.Click += menuOptionsModes123Fish_Click;
            // 
            // menuHelp
            // 
            menuHelp.DropDownItems.AddRange(new ToolStripItem[] { menuAbout, menuHelpHelp });
            menuHelp.Font = new Font("Lucida Sans Typewriter", 10F);
            menuHelp.Name = "menuHelp";
            menuHelp.Size = new Size(27, 20);
            menuHelp.Text = "?";
            // 
            // menuAbout
            // 
            menuAbout.BackColor = SystemColors.MenuBar;
            menuAbout.Name = "menuAbout";
            menuAbout.Size = new Size(161, 22);
            menuAbout.Text = "About";
            // 
            // menuHelpHelp
            // 
            menuHelpHelp.BackColor = SystemColors.MenuBar;
            menuHelpHelp.Name = "menuHelpHelp";
            menuHelpHelp.ShortcutKeys = Keys.Alt | Keys.F3;
            menuHelpHelp.Size = new Size(161, 22);
            menuHelpHelp.Text = "Help";
            // 
            // labelPermKey
            // 
            labelPermKey.AutoSize = true;
            labelPermKey.Location = new Point(598, 34);
            labelPermKey.Name = "labelPermKey";
            labelPermKey.Size = new Size(57, 15);
            labelPermKey.TabIndex = 6;
            labelPermKey.Text = "PermKey:";
            // 
            // ZenMatrixForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(797, 580);
            Controls.Add(labelPermKey);
            Controls.Add(textBoxPermKey);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(labelSymmKey);
            Controls.Add(textBoxSymmKey);
            Controls.Add(zenMatrixVControl);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "ZenMatrixForm";
            Text = "ZebMatrixTest";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion


        private ZenMatrixVControl zenMatrixVControl;
        private TextBox textBoxSymmKey;
        private Label labelSymmKey;
        private TableLayoutPanel tableLayoutPanel1;        
        private TextBox textBoxPermKey;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem printToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        internal ToolStripMenuItem menuHelp;
        internal ToolStripMenuItem menuAbout;
        internal ToolStripMenuItem menuHelpHelp;
        private ToolStripMenuItem menuOptionsMenuModes;
        protected internal ToolStripMenuItem menuOptionsModesComplex;
        protected internal ToolStripMenuItem menuOptionsModesSimple;
        private ToolStripMenuItem menuOptionsModes123Fish;        
        private Label labelMap0;
        private Label labelMap1;
        private Label labelMap2;
        private Label labelMap3;
        private Label labelMap4;
        private Label labelMap5;
        private Label labelMap6;
        private Label labelMap7;
        private Label labelMap8;
        private Label labelMap9;
        private Label labelMapA;
        private Label labelMapB;
        private Label labelMapC;
        private Label labelMapD;
        private Label labelMapE;
        private Label labelMapF;
        protected internal ToolStripMenuItem menuModeZenMatrix;
        private Label labelPermKey;
        private TableLayoutPanel tableLayoutPanel2;        
        private Label labelPointsF;
        private Label labelPointsE;
        private Label labelPointsD;
        private Label labelPointsC;
        private Label labelPointsB;
        private Label labelPointsA;
        private Label labelPoints9;
        private Label labelPoints8;
        private Label labelPoints7;
        private Label labelPoints6;
        private Label labelPoints5;
        private Label labelPoints4;
        private Label labelPoints0;
        private Label labelPoints1;
        private Label labelPoints2;
        private Label labelPoints3;
    }
}