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
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            labelMap0 = new Label();
            labelMap1 = new Label();
            labelMap2 = new Label();
            textBoxPermKey = new TextBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // zenMatrixVControl
            // 
            zenMatrixVControl.BackgroundImage = (Image)resources.GetObject("zenMatrixVControl.BackgroundImage");
            zenMatrixVControl.Location = new Point(1, 68);
            zenMatrixVControl.Name = "zenMatrixVControl";
            zenMatrixVControl.Size = new Size(514, 514);
            zenMatrixVControl.TabIndex = 0;
            // 
            // textBoxSymmKey
            // 
            textBoxSymmKey.Location = new Point(71, 8);
            textBoxSymmKey.Name = "textBoxSymmKey";
            textBoxSymmKey.Size = new Size(306, 23);
            textBoxSymmKey.TabIndex = 1;
            textBoxSymmKey.TextChanged += SymmKey_Changed;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 2;
            label1.Text = "SynnKey:";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(labelMap0, 0, 0);
            tableLayoutPanel1.Controls.Add(labelMap1, 0, 1);
            tableLayoutPanel1.Controls.Add(labelMap2, 0, 2);
            tableLayoutPanel1.Font = new Font("Lucida Sans Unicode", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tableLayoutPanel1.Location = new Point(592, 68);
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
            tableLayoutPanel1.Size = new Size(193, 514);
            tableLayoutPanel1.TabIndex = 3;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
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
            labelMap1.Location = new Point(3, 32);
            labelMap1.Name = "labelMap1";
            labelMap1.Size = new Size(50, 20);
            labelMap1.TabIndex = 1;
            labelMap1.Text = "1 =>";
            // 
            // labelMap2
            // 
            labelMap2.AutoSize = true;
            labelMap2.Location = new Point(3, 64);
            labelMap2.Name = "labelMap2";
            labelMap2.Size = new Size(50, 20);
            labelMap2.TabIndex = 2;
            labelMap2.Text = "2 =>";
            // 
            // textBoxPermKey
            // 
            textBoxPermKey.Location = new Point(595, 12);
            textBoxPermKey.MaxLength = 16;
            textBoxPermKey.Name = "textBoxPermKey";
            textBoxPermKey.Size = new Size(190, 23);
            textBoxPermKey.TabIndex = 4;
            textBoxPermKey.TextChanged += PermKey_Changed;
            // 
            // ZenMatrixForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(797, 580);
            Controls.Add(textBoxPermKey);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(label1);
            Controls.Add(textBoxSymmKey);
            Controls.Add(zenMatrixVControl);
            Name = "ZenMatrixForm";
            Text = "ZebMatrixTest";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion


        private ZenMatrixVControl zenMatrixVControl;
        private TextBox textBoxSymmKey;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label labelMap0;
        private Label labelMap1;
        private Label labelMap2;
        private TextBox textBoxPermKey;
    }
}