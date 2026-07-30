using EU.CqrXs.Crypt.Cipher.Symmetric;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EU.CqrXs.Gui.Forms
{
    public partial class ZenMatrixForm : EncryptFormBase
    {
        public ZenMatrixForm()
        {
            InitializeComponent();
        }


        protected void SymmKey_Changed(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBoxSymmKey.Text))
            {
                ZenMatrix z = new ZenMatrix(this.textBoxSymmKey.Text, Crypt.Hash.KeyHash.Hex, false);
                SetTableMapping(z.MatrixPermutationKey);
                this.textBoxPermKey.Text = "";
                foreach (byte b in z.MatrixPermutationKey)
                {
                    this.textBoxPermKey.Text += b.ToString("X1");
                }

            }
        }

        protected void SetTableMapping(byte[] data)
        {
            zenMatrixVControl.SetPermutationKey(data);
            int l = 0;
            foreach (byte b in data)
            {
                Label lbl = this.Controls.Find("labelPoints" + l.ToString("X1"), true).FirstOrDefault() as Label;
                if (lbl != null)
                {
                    lbl.Text = b.ToString("X1");
                    lbl.Font = new Font("Lucida Sans Unicode", 16F, FontStyle.Bold);
                }
                l++;
            }
        }


        protected void tableLayoutPanel1_Paint(object sender, EventArgs e)
        {
            ; //
        }

        private void PermKey_Changed(object sender, EventArgs e)
        {
            List<byte> bytes = new List<byte>();
            if (!string.IsNullOrEmpty(textBoxPermKey.Text))
            {
                foreach (char ch in textBoxPermKey.Text)
                {
                    byte b;
                    if (Char.IsAsciiHexDigit(ch))
                    {
                        if (Char.IsAsciiDigit(ch))
                            b = (byte)((int)(ch) - (int)'0');
                        else if (Char.IsAsciiHexDigitLower(ch))
                            b = (byte)((int)(ch) + 10 - (int)'a');
                        else
                            b = (byte)((int)(ch) + 10 - (int)'A');

                        bytes.Add(b);
                    }
                }
                SetTableMapping(bytes.ToArray());
            }
        }

        private void menuOptionsModesComplex_Click(object sender, EventArgs e)
        {
            if (Program.formComplex == null || Program.formComplex.Disposing)
                Program.formComplex = new EncryptFormMultiControls();
            try
            {
                Program.formComplex.Show();
            }
            catch (Exception)
            {
                Program.formComplex = new EncryptFormMultiControls();
                Program.formComplex.Show();
            }
            try
            {
                if (Program.formZenMatrix != null && !Program.formZenMatrix.Disposing)
                    Program.formZenMatrix.Hide();
                if (Program.form123Fish != null && !Program.form123Fish.Disposing)
                    Program.form123Fish.Hide();
                if (Program.formSimple != null && !Program.formSimple.Disposing)
                    Program.formSimple.Hide();
            }
            catch (Exception)
            {
            }
            this.Hide();
            Program.formComplex.Focus();
        }

        private void menuOptionsModesSimple_Click(object sender, EventArgs e)
        {
            if (Program.formSimple == null || Program.formSimple.Disposing)
                Program.formSimple = new EncryptFormSimple();
            try
            {
                Program.formSimple.Show();
            }
            catch (Exception)
            {
                Program.formSimple = new EncryptFormSimple();
                Program.formSimple.Show();
            }
            try
            {
                if (Program.formZenMatrix != null && !Program.formZenMatrix.Disposing)
                    Program.formZenMatrix.Hide();
                if (Program.formComplex != null && !Program.formComplex.Disposing)
                    Program.formComplex.Hide();
                if (Program.form123Fish != null && !Program.form123Fish.Disposing)
                    Program.form123Fish.Hide();
            }
            catch (Exception)
            {
            }
            this.Hide();

            Program.formSimple.Focus();
        }

        private void menuOptionsModes123Fish_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.form123Fish == null || Program.form123Fish.Disposing)
                {
                    OneTwoThreeFish ofish = new OneTwoThreeFish();
                    Program.form123Fish = ofish;
                    ofish.Show();
                }
            }
            catch (Exception)
            {
                Program.form123Fish = new OneTwoThreeFish();
                Program.form123Fish.Show();
            }
            try
            {
                if (Program.formZenMatrix != null && !Program.formZenMatrix.Disposing)
                    Program.formZenMatrix.Hide();
                if (Program.formSimple != null && !Program.formSimple.Disposing)
                    Program.formSimple.Hide();
                if (Program.formComplex != null && !Program.formComplex.Disposing)
                    Program.formComplex.Hide();
            }
            catch (Exception) { }
            this.Hide();

            Program.form123Fish.Focus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(0);
        }
    }
}
