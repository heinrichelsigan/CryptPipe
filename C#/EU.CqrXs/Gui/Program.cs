#define CLR2COMPATIBILITY
using EU.CqrXs.Gui.Forms;
using EU.CqrXs.Util;
using System.Drawing.Imaging;
using System.Reflection;

namespace EU.CqrXs.Gui
{

    #region enum FormMode
    public enum FormMode
    {
        Simple = 0,
        Complex = 1,
        OneTwoThreeFish = 2,
        ZenMatrix = 3,
        Asymmetric = 4
    }
    #endregion enum FormMode

    #region program

    /// <summary>
    /// Main Program
    /// </summary>
    public static class Program
    {

        #region static fields

        private static readonly Lock _lock = new Lock();      
        private static Thread t;
        public static readonly string ProgFilePath = ExecFullPath ?? "";
        public static readonly string ProgName = string.IsNullOrEmpty(ProgFilePath) ? Constants.APP_NAME_WINFORM : Path.GetFileName(ProgFilePath);
        public static readonly string ProgDirPazh = Path.GetDirectoryName(ProgFilePath);

        public static ulong ProgramCount = 0x0;
        internal static Mutex? mutex;

        internal static EncryptFormBase[] formsLaunched = new EncryptFormBase[5] { null, null, null, null, null };

        internal static string mainFormName = "";

        internal static ApplicationContext applicationContext;
        internal static SystemColorMode colorMode = SystemColorMode.System;
        internal static FormMode formMode = FormMode.Complex;
        // internal static CipherPipe? ciperPipe;
        #endregion static fields

        #region Properties

        internal static string ExecFullPath
        {
            get
            {
                string execFilePath = "";

                for (int i = 0; i < 6; i++)
                {
                    switch (i)
                    {
                        case 0: execFilePath = Path.GetFullPath(Environment.CommandLine); break;
                        case 1: execFilePath = Path.GetFullPath(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName); break;
                        case 2: execFilePath = Path.GetFullPath(System.Environment.ProcessPath); break;
                        case 3: execFilePath = Path.GetFullPath(Assembly.GetExecutingAssembly().Location); break;
                        case 4: execFilePath = Path.GetFullPath(Assembly.GetExecutingAssembly().CodeBase); break;
                        case 5: execFilePath = Path.GetFullPath(System.Environment.GetCommandLineArgs()[0]); break;
                        default:
                            execFilePath = Path.GetFullPath(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName); break;
                    }

                    if (string.IsNullOrEmpty(execFilePath))
                        continue;

                    if (File.Exists(execFilePath))
                        return execFilePath;
                }

                return execFilePath;
            }
        }

        internal static EncryptFormMultiControls formComplex
        {
            get => (formsLaunched[0] != null && formsLaunched[0] is EncryptFormMultiControls) ? (EncryptFormMultiControls)formsLaunched[0] : null;
            set => formsLaunched[0] = value;
        }
        internal static EncryptFormSimple formSimple
        {
            get => (formsLaunched[1] != null && formsLaunched[1] is EncryptFormSimple) ? (EncryptFormSimple)formsLaunched[1] : null;
            set => formsLaunched[1] = value;
        }
        internal static OneTwoThreeFish form123Fish
        {
            get => (formsLaunched[2] != null && formsLaunched[2] is OneTwoThreeFish) ? (OneTwoThreeFish)formsLaunched[2] : null;
            set => formsLaunched[2] = value;
        }
        internal static ZenMatrixForm formZenMatrix
        {
            get => (formsLaunched[3] != null && formsLaunched[3] is ZenMatrixForm) ? (ZenMatrixForm)formsLaunched[3] : null;
            set => formsLaunched[3] = value;
        }
        internal static EncryptFormAsymmetric formAsymmetric
        {
            get => (formsLaunched[4] != null && formsLaunched[4] is EncryptFormAsymmetric) ? (EncryptFormAsymmetric)formsLaunched[4] : null;
            set => formsLaunched[4] = value;
        }
        
        #endregion Properties

        #region Main

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// <param name="args">arguments</param>
        [STAThread]
        internal static void Main(string[] args)
        {

            if (args.Length > 0)
            {
                foreach (string arg in args)
                {
                    if (arg.Contains("dark", StringComparison.CurrentCultureIgnoreCase))
                        colorMode = SystemColorMode.Dark;
                    if (arg.Contains("classic", StringComparison.CurrentCultureIgnoreCase))
                        colorMode = SystemColorMode.Classic;
                    if (arg.Contains("system", StringComparison.CurrentCultureIgnoreCase))
                        colorMode = SystemColorMode.System;


                    if (arg.Contains("simple", StringComparison.CurrentCultureIgnoreCase))
                        formMode = FormMode.Simple;
                    if (arg.Contains("onetwothree", StringComparison.CurrentCultureIgnoreCase) ||
                        arg.Contains("123fish", StringComparison.CurrentCultureIgnoreCase) ||
                        arg.Contains("123", StringComparison.CurrentCultureIgnoreCase) ||
                        arg.Contains("fish", StringComparison.CurrentCultureIgnoreCase))
                        formMode = FormMode.OneTwoThreeFish;
                    if (arg.Contains("zen", StringComparison.CurrentCultureIgnoreCase) ||
                        arg.Contains("matrix", StringComparison.CurrentCultureIgnoreCase))
                        formMode = FormMode.ZenMatrix;
                }
            }


            mutex = (mutex == null) ? new Mutex(false, Constants.APP_NAME_WINFORM) : mutex;

            if (!mutex.WaitOne(2000, false))
            {
                // show MsgBox and exit
                MessageBox.Show($"Another instance of {ProgName} is already running!", "Attention");
                return;
            }

            // set Application basic settings
            Application.EnableVisualStyles();
            Application.SetColorMode(colorMode);
            //plication.SetCompatibleTextRenderingDefault(true);
            Application.SetCompatibleTextRenderingDefault(false);
            Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.ClientAndNonClientAreasEnabled;
            //´Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);

            // Task task = new Task(() => PollingLoop("Main", new EventArgs()));
            // task.Start();

            applicationContext = new ApplicationContext();
            if (formMode == FormMode.Simple)
            {
                Program.formSimple = new EncryptFormSimple();
                Program.mainFormName = Program.formSimple.Name;
                applicationContext.MainForm = Program.formSimple;
                applicationContext.Tag = formSimple.Name;
            }
            else if (formMode == FormMode.OneTwoThreeFish)
            {
                Program.form123Fish = new OneTwoThreeFish();
                Program.mainFormName = Program.form123Fish.Name;
                applicationContext.MainForm = Program.form123Fish;
                applicationContext.Tag = Program.form123Fish.Name;
            }
            else if (formMode == FormMode.ZenMatrix)
            {
                Program.formZenMatrix = new ZenMatrixForm();
                Program.mainFormName = Program.formZenMatrix.Name;
                applicationContext.MainForm = Program.formZenMatrix;
                applicationContext.Tag = Program.formZenMatrix.Name;
            }
            else 
            {                
                Program.formComplex = new EncryptFormMultiControls();
                Program.mainFormName = Program.formComplex.Name;
                applicationContext.MainForm = Program.formComplex;
                applicationContext.Tag = formComplex.Name;
            }


            // Run application
            Application.Run(applicationContext);

            // Release, Close, Dispose Mutal Exclusion
            ReleaseCloseDisposeMutex();
        }

        #endregion Main


        #region ReleaseCloseDisposeMutex

        public static void ReleaseCloseDisposeMutex()
        {
            Exception? ex = null;
            if (Program.mutex != null)
            {
                var safeWaitHandle = Program.mutex.GetSafeWaitHandle();
                if (safeWaitHandle != null && !safeWaitHandle.IsInvalid && !safeWaitHandle.IsClosed)
                {
                    try
                    {
                        Program.mutex.ReleaseMutex();
                    }
                    catch (Exception exRelease)
                    {
                        ex = exRelease;
                    }
                    try
                    {
                        Program.mutex.Close();
                    }
                    catch (Exception exClose)
                    {
                        if (ex == null)
                            ex = exClose;
                    }
                    try
                    {
                        Program.mutex.Dispose();
                    }
                    catch (Exception exDispose)
                    {
                        ex = exDispose;
                    }

                }
            }
            try
            {
                Program.mutex = null;
            }
            catch (Exception exNull)
            {
                ex = exNull;
            }
            finally
            {
                if (ex != null)
                    throw ex;
            }
        }

        #endregion ReleaseCloseDisposeMutex
    }

    #endregion program

}