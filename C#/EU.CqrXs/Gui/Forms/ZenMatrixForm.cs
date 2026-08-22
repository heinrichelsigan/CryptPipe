using EU.CqrXs.Crypt.Cipher.Symmetric;
using EU.CqrXs.Gui.Properties;

namespace EU.CqrXs.Gui.Forms
{

    /// <summary>
    /// ZenMatrix Form for displaying and managing permutation keys of <see cref="ZenMatrix"/>
    /// </summary>
    public partial class ZenMatrixForm : EncryptFormBase
    {
        public ZenMatrixForm()
        {
            InitializeComponent();
        }

        #region menu options modes click

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

        #endregion menu options modes click

        #region menu about help exit click

        
        private void menuAbout_Click_1(object sender, EventArgs e)
        {
            AboutDialog aboutDialog = new AboutDialog();
            aboutDialog.ShowDialog();
        }

        private void menuHelpHelp_Click(object sender, EventArgs e)
        {
            // System.Windows.Forms.Help.ShowHelp(this, Resources.HelpUrl);
            System.Windows.Forms.Help.ShowHelp(this, Resources.HelpUrl, HelpNavigator.TableOfContents, "cqrxs.eu");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion menu about help exit click

    }
}
