using EU.CqrXs.Crypt.Cipher.Symmetric;
using EU.CqrXs.Crypt.Hash;
using System.ComponentModel;

namespace EU.CqrXs.Gui.Controls
{

    /// <summary>
    /// ZenMatrix UserControl for displaying and managing permutation keys ot <see cref="ZenMatrix"/>
    /// </summary>
    public partial class ZenMatrixUserControl : UserControl
    {

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public List<byte> PermKeys { get; private set; }

        /// <summary>
        /// default parameterless ctor
        /// </summary>
        public ZenMatrixUserControl()
        {
            InitializeComponent();
        }


        /// <summary>
        /// ctor with permutation key
        /// </summary>
        /// <param name="secretKey"></param>
        public ZenMatrixUserControl(string secretKey) : this()
        {
            ZenMatrix zenMatrix = new ZenMatrix(secretKey, KeyHash.Hex, false);
            if (zenMatrix != null)
            {
                SetPermutationKey(zenMatrix.MatrixPermutationKey);
            }
        }


        /// <summary>
        /// Event when <see cref="textBoxSymmKey">textBoxSymmKey</see> is changed, 
        /// updates <see cref="PermKeys"/> and <see cref="textBoxPermKey">textBoxPermKey</see>
        /// </summary>
        /// <param name="sender"><see cref="object">object sender</see></param>
        /// <param name="e"><see cref="EventArgs">EventArgs e</see></param>
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

        /// <summary>
        /// Event when <see cref="textBoxPermKey">textBoxPermKey</see> is changed,
        /// </summary>
        /// <param name="sender"><see cref="object">object sender</see></param>
        /// <param name="e"><see cref="EventArgs">EventArgs e</see></param>
        protected void PermKey_Changed(object sender, EventArgs e)
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

                if (bytes.Count >= 15)
                {
                    SetTableMapping(bytes.ToArray());
                }
            }
        }

        /// <summary>
        /// Sets permutation key and updates <see cref="Label">Labels</see> in <see cref="tableLayoutPanel1"/>
        /// </summary>
        /// <param name="data"></param>
        protected void SetTableMapping(byte[] data)
        {
            SetPermutationKey(data);
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


        /// <summary>
        /// Sets permutation key and updates <see cref="TextBox">Textfields</see> in <see cref="panelZenMatrix"/>
        /// </summary>
        /// <param name="pernKey">permutation key as <see cref="T:byte[]"/></param>
        public void SetPermutationKey(byte[] pernKey)
        {
            if (pernKey != null && pernKey.Length >= 15)
            {
                PermKeys = new List<byte>(pernKey);
                // Update the control with the provided key
                int i = 0;
                foreach (byte b in PermKeys)
                {
                    Control[] controls = panelZenMatrix.Controls.Find("textBox" + i.ToString("X1"), true);
                    if (controls.Length < 1)
                    {
                        controls = Controls.Find("textBox" + i.ToString("X1"), true);
                    }
                    if (controls.Length > 0)
                    {
                        TextBox tbx = controls.FirstOrDefault() as TextBox;
                        if (tbx != null)
                        {
                            tbx.Visible = true;
                            tbx.Text = b.ToString("X1");
                        }
                    }
                    i++;
                }
            }
        }


        protected void tableLayoutPanel1_Paint(object sender, EventArgs e)
        {
            ; //
        }
       

    }

}
