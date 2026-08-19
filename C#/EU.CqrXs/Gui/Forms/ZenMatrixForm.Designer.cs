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
            zenMatrixÛserControl1 = new ZenMatrixÛserControl();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, toolsToolStripMenuItem, menuHelp });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(707, 24);
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
            toolStripSeparator.Size = new Size(137, 6);
            // 
            // printToolStripMenuItem
            // 
            printToolStripMenuItem.Image = (Image)resources.GetObject("printToolStripMenuItem.Image");
            printToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            printToolStripMenuItem.Name = "printToolStripMenuItem";
            printToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            printToolStripMenuItem.Size = new Size(140, 22);
            printToolStripMenuItem.Text = "&Print";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(137, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(140, 22);
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
            menuOptionsMenuModes.Size = new Size(110, 22);
            menuOptionsMenuModes.Text = "Modes";
            // 
            // menuOptionsModesComplex
            // 
            menuOptionsModesComplex.BackColor = SystemColors.Menu;
            menuOptionsModesComplex.Name = "menuOptionsModesComplex";
            menuOptionsModesComplex.Size = new Size(155, 22);
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
            menuModeZenMatrix.Size = new Size(155, 22);
            menuModeZenMatrix.Text = "ZenMatrix";
            // 
            // menuOptionsModesSimple
            // 
            menuOptionsModesSimple.BackColor = SystemColors.Menu;
            menuOptionsModesSimple.Name = "menuOptionsModesSimple";
            menuOptionsModesSimple.Size = new Size(155, 22);
            menuOptionsModesSimple.Text = "Mode Simple";
            menuOptionsModesSimple.Click += menuOptionsModesSimple_Click;
            // 
            // menuOptionsModes123Fish
            // 
            menuOptionsModes123Fish.BackColor = SystemColors.Menu;
            menuOptionsModes123Fish.Name = "menuOptionsModes123Fish";
            menuOptionsModes123Fish.Size = new Size(155, 22);
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
            // zenMatrixÛserControl1
            // 
            zenMatrixÛserControl1.Location = new Point(1, 20);
            zenMatrixÛserControl1.Name = "zenMatrixÛserControl1";
            zenMatrixÛserControl1.Size = new Size(691, 559);
            zenMatrixÛserControl1.TabIndex = 6;
            // 
            // ZenMatrixForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(707, 585);
            Controls.Add(zenMatrixÛserControl1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.None;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "ZenMatrixForm";
            Text = "ZebMatrixTest";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
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
        protected internal ToolStripMenuItem menuModeZenMatrix;
        private TableLayoutPanel tableLayoutPanel2;
        private ZenMatrixÛserControl zenMatrixÛserControl1;
    }
}