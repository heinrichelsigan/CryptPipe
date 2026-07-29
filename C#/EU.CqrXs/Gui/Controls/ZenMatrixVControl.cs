using EU.CqrXs.Crypt.Cipher.Symmetric;
using EU.CqrXs.Crypt.Hash;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EU.CqrXs.Gui.Controls
{
    public partial class ZenMatrixVControl : UserControl
    {

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public List<byte> PermKeys { get; internal set; }

        public ZenMatrixVControl()
        {
            InitializeComponent();
        }

        public ZenMatrixVControl(byte[] pernKey) : this() 
        {
            SetPermutationKey(pernKey);                
        }

        public ZenMatrixVControl(string secretKey) : this()
        {
            ZenMatrix zenMatrix = new ZenMatrix(secretKey, KeyHash.Hex, false);
            if (zenMatrix != null)
            {
                SetPermutationKey(zenMatrix.MatrixPermutationKey);                
            }
        }


        public void SetPermutationKey(byte[] pernKey)
        {
            if (pernKey != null && pernKey.Length == 16)
            {
                PermKeys = new List<byte>(pernKey);
                // Update the control with the provided key
                int i = 0;
                foreach (byte b in PermKeys)
                {
                    TextBox tbx = this.Controls.Find("textBox" + i, true).FirstOrDefault() as TextBox;
                    if (tbx != null)
                    {
                        tbx.Text = b.ToString("X1");
                    }
                    i++;
                }
            }
        }

    }
}
