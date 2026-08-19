using EU.CqrXs.Crypt.Cipher.Symmetric;
using EU.CqrXs.Gui.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EU.CqrXs.Gui.Controls
{
    public partial class ZenMatrixÛserControl : UserControl
    {
        
        public ZenMatrixÛserControl()
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
            zenMatrixControl.SetPermutationKey(data);
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


    }

}
