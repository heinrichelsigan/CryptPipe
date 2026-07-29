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
    public partial class ZenMatrixForm : Form
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
                foreach (byte b in z.MatrixPermutationKey)
                {
                    this.textBoxPermKey.Text += b.ToString("X1");
                }
                
            }
        }

        protected void SetTableMapping(byte[] data)
        {
            zenMatrixVControl.SetPermutationKey(data);
            int r = 0;
            foreach (byte b in data)
            {
                ;

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
