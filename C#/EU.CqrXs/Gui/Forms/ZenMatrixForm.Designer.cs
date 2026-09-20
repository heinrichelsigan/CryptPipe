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
            menuForms = new ToolStripMenuItem();
            menuOptionsModesComplex = new ToolStripMenuItem();
            menuModeZenMatrix = new ToolStripMenuItem();
            menuOptionsModesSimple = new ToolStripMenuItem();
            menuOptionsModes123Fish = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            menuOptionsMenuVisualModes = new ToolStripMenuItem();
            menuVisualModesItemClassic = new ToolStripMenuItem();
            menuVisualModesItemDark = new ToolStripMenuItem();
            menuVisualModesItemSystem = new ToolStripMenuItem();
            menuHelp = new ToolStripMenuItem();
            menuAbout = new ToolStripMenuItem();
            menuHelpHelp = new ToolStripMenuItem();
            zenMatrixUserControl = new ZenMatrixUserControl();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, menuForms, toolsToolStripMenuItem, menuHelp });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(707, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripSeparator, printToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Font = new Font("Lucida Sans Typewriter", 10F);
            fileToolStripMenuItem.ForeColor = SystemColors.MenuText;
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(51, 20);
            fileToolStripMenuItem.Text = "Main";
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(166, 6);
            // 
            // printToolStripMenuItem
            // 
            printToolStripMenuItem.BackColor = SystemColors.Menu;
            printToolStripMenuItem.Enabled = false;
            printToolStripMenuItem.ForeColor = SystemColors.MenuText;
            printToolStripMenuItem.Image = (Image)resources.GetObject("printToolStripMenuItem.Image");
            printToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            printToolStripMenuItem.Name = "printToolStripMenuItem";
            printToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.P;
            printToolStripMenuItem.Size = new Size(169, 22);
            printToolStripMenuItem.Text = "&Print";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(166, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.BackColor = SystemColors.Menu;
            exitToolStripMenuItem.ForeColor = SystemColors.MenuText;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(169, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // menuForms
            // 
            menuForms.BackColor = SystemColors.MenuBar;
            menuForms.DropDownItems.AddRange(new ToolStripItem[] { menuOptionsModesComplex, menuModeZenMatrix, menuOptionsModesSimple, menuOptionsModes123Fish });
            menuForms.Font = new Font("Lucida Sans Typewriter", 10F);
            menuForms.ForeColor = SystemColors.MenuText;
            menuForms.Name = "menuForms";
            menuForms.Size = new Size(59, 20);
            menuForms.Text = "Forms";
            // 
            // menuOptionsModesComplex
            // 
            menuOptionsModesComplex.BackColor = SystemColors.Menu;
            menuOptionsModesComplex.ForeColor = SystemColors.MenuText;
            menuOptionsModesComplex.Name = "menuOptionsModesComplex";
            menuOptionsModesComplex.Size = new Size(180, 22);
            menuOptionsModesComplex.Text = "Mode Complex";
            // 
            // menuModeZenMatrix
            // 
            menuModeZenMatrix.BackColor = SystemColors.GradientInactiveCaption;
            menuModeZenMatrix.Checked = true;
            menuModeZenMatrix.CheckState = CheckState.Checked;
            menuModeZenMatrix.Enabled = false;
            menuModeZenMatrix.ForeColor = SystemColors.MenuText;
            menuModeZenMatrix.Name = "menuModeZenMatrix";
            menuModeZenMatrix.Size = new Size(180, 22);
            menuModeZenMatrix.Text = "ZenMatrix";
            // 
            // menuOptionsModesSimple
            // 
            menuOptionsModesSimple.BackColor = SystemColors.Menu;
            menuOptionsModesSimple.ForeColor = SystemColors.MenuText;
            menuOptionsModesSimple.Name = "menuOptionsModesSimple";
            menuOptionsModesSimple.Size = new Size(180, 22);
            menuOptionsModesSimple.Text = "Mode Simple";
            // 
            // menuOptionsModes123Fish
            // 
            menuOptionsModes123Fish.BackColor = SystemColors.Menu;
            menuOptionsModes123Fish.Enabled = false;
            menuOptionsModes123Fish.ForeColor = SystemColors.MenuText;
            menuOptionsModes123Fish.Name = "menuOptionsModes123Fish";
            menuOptionsModes123Fish.Size = new Size(180, 22);
            menuOptionsModes123Fish.Text = "123-Fish";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.BackColor = SystemColors.MenuBar;
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuOptionsMenuVisualModes });
            toolsToolStripMenuItem.Font = new Font("Lucida Sans Typewriter", 10F);
            toolsToolStripMenuItem.ForeColor = SystemColors.MenuText;
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(75, 20);
            toolsToolStripMenuItem.Text = "Options";
            // 
            // menuOptionsMenuVisualModes
            // 
            menuOptionsMenuVisualModes.BackColor = SystemColors.MenuBar;
            menuOptionsMenuVisualModes.DropDownItems.AddRange(new ToolStripItem[] { menuVisualModesItemClassic, menuVisualModesItemDark, menuVisualModesItemSystem });
            menuOptionsMenuVisualModes.ForeColor = SystemColors.MenuText;
            menuOptionsMenuVisualModes.Name = "menuOptionsMenuVisualModes";
            menuOptionsMenuVisualModes.Size = new Size(170, 22);
            menuOptionsMenuVisualModes.Text = "Visual Modes";
            // 
            // menuVisualModesItemClassic
            // 
            menuVisualModesItemClassic.BackColor = SystemColors.Menu;
            menuVisualModesItemClassic.ForeColor = SystemColors.MenuText;
            menuVisualModesItemClassic.Name = "menuVisualModesItemClassic";
            menuVisualModesItemClassic.Size = new Size(130, 22);
            menuVisualModesItemClassic.Text = "Classic";
            // 
            // menuVisualModesItemDark
            // 
            menuVisualModesItemDark.BackColor = SystemColors.Menu;
            menuVisualModesItemDark.ForeColor = SystemColors.MenuText;
            menuVisualModesItemDark.Name = "menuVisualModesItemDark";
            menuVisualModesItemDark.Size = new Size(130, 22);
            menuVisualModesItemDark.Text = "Dark";
            // 
            // menuVisualModesItemSystem
            // 
            menuVisualModesItemSystem.BackColor = SystemColors.Menu;
            menuVisualModesItemSystem.ForeColor = SystemColors.MenuText;
            menuVisualModesItemSystem.Name = "menuVisualModesItemSystem";
            menuVisualModesItemSystem.Size = new Size(130, 22);
            menuVisualModesItemSystem.Text = "System";
            // 
            // menuHelp
            // 
            menuHelp.DropDownItems.AddRange(new ToolStripItem[] { menuAbout, menuHelpHelp });
            menuHelp.Font = new Font("Lucida Sans Typewriter", 10F);
            menuHelp.ForeColor = SystemColors.MenuText;
            menuHelp.Name = "menuHelp";
            menuHelp.Size = new Size(27, 20);
            menuHelp.Text = "?";
            // 
            // menuAbout
            // 
            menuAbout.BackColor = SystemColors.MenuBar;
            menuAbout.ForeColor = SystemColors.MenuText;
            menuAbout.Name = "menuAbout";
            menuAbout.Size = new Size(161, 22);
            menuAbout.Text = "About";
            menuAbout.Click += menuAbout_Click_1;
            // 
            // menuHelpHelp
            // 
            menuHelpHelp.BackColor = SystemColors.MenuBar;
            menuHelpHelp.ForeColor = SystemColors.MenuText;
            menuHelpHelp.Name = "menuHelpHelp";
            menuHelpHelp.ShortcutKeys = Keys.Alt | Keys.F3;
            menuHelpHelp.Size = new Size(161, 22);
            menuHelpHelp.Text = "Help";
            menuHelpHelp.Click += menuHelpHelp_Click;
            // 
            // zenMatrixUserControl
            // 
            zenMatrixUserControl.Location = new Point(1, 20);
            zenMatrixUserControl.Name = "zenMatrixUserControl";
            zenMatrixUserControl.Size = new Size(691, 559);
            zenMatrixUserControl.TabIndex = 6;
            // 
            // ZenMatrixForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(707, 585);
            Controls.Add(zenMatrixUserControl);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
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
        private ZenMatrixUserControl zenMatrixUserControl;
        private ToolStripMenuItem menuForms;
        protected internal ToolStripMenuItem menuOptionsModesComplex;
        protected internal ToolStripMenuItem menuModeZenMatrix;
        protected internal ToolStripMenuItem menuOptionsModesSimple;
        private ToolStripMenuItem menuOptionsModes123Fish;
        private ToolStripMenuItem menuOptionsMenuVisualModes;
        private ToolStripMenuItem menuVisualModesItemClassic;
        private ToolStripMenuItem menuVisualModesItemDark;
        private ToolStripMenuItem menuVisualModesItemSystem;
    }
}