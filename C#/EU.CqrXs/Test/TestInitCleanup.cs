using EU.CqrXs.Util;
using System.Reflection;

namespace EU.CqrXs.Test
{

    /// <summary>
    /// TestInitCleanup is a test class that provides initialization and cleanup methods for unit tests. 
    /// It ensures that any temporary files created during the tests are deleted before and after each test method is executed. 
    /// The class also provides a static property to retrieve the system directory path where the tests are running.
    /// </summary>
    [TestClass]
    public sealed class TestInitCleanup
    {

        private static string systemDirPath = string.Empty;
        public static string SystemDirPath
        {
            get
            {
                if (string.IsNullOrEmpty(systemDirPath))
                {
                    for (int sysDirTry = 0; sysDirTry < Constants.PIPE_MAX_LEN; sysDirTry++)
                    {
                        try
                        {
                            switch (sysDirTry)
                            {
                                case 0: systemDirPath = Path.GetFullPath(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName); break;
                                case 1: systemDirPath = Path.GetFullPath(Environment.ProcessPath); break;
                                case 2: systemDirPath = Path.GetFullPath(Assembly.GetExecutingAssembly().Location); break;
                                case 3: systemDirPath = AppContext.BaseDirectory; break;
                                case 4: if (AppDomain.CurrentDomain != null) systemDirPath = AppDomain.CurrentDomain.BaseDirectory; break;
                                case 5: systemDirPath = Path.GetFullPath(Assembly.GetExecutingAssembly().CodeBase); break;
                                case 6: systemDirPath = Path.GetFullPath(Environment.GetCommandLineArgs()[0]); break;
                                default:
                                    systemDirPath = Path.GetFullPath(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName); break;
                            }
                        }
                        catch (Exception exPath)
                        {
                            Area23Log.LogOriginEx("SystemDirPath", exPath);
                        }

                        if (!string.IsNullOrEmpty(systemDirPath) && Directory.Exists(systemDirPath))
                            break;
                    }

                    if (!systemDirPath.EndsWith(Path.DirectorySeparatorChar))
                        systemDirPath += Path.DirectorySeparatorChar.ToString();
                }
                return systemDirPath;
            }
        }

        [TestInitialize]
        public void TestInit()
        {
            string tmp = Environment.GetEnvironmentVariable("TEMP");
            if (string.IsNullOrEmpty(tmp) || !Directory.Exists(tmp))
                tmp = Environment.GetEnvironmentVariable("TMP");
            if (string.IsNullOrEmpty(tmp) || !Directory.Exists(tmp))
                tmp = AppDomain.CurrentDomain.BaseDirectory;

            string consoleDir = Path.Combine(tmp, Constants.APP_NAME_CONSOLE);
            if (!string.IsNullOrEmpty(consoleDir) && Directory.Exists(consoleDir))
            {
                foreach (string file in Directory.GetFiles(consoleDir, "*.*"))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception exi)
                    {
                        Area23Log.LogOriginEx("TestInit", exi);
                    }
                }
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            string tmp = Environment.GetEnvironmentVariable("TEMP");
            if (string.IsNullOrEmpty(tmp) || !Directory.Exists(tmp))
                tmp = Environment.GetEnvironmentVariable("TMP");
            if (string.IsNullOrEmpty(tmp) || !Directory.Exists(tmp))
                tmp = AppDomain.CurrentDomain.BaseDirectory;

            string consoleDir = Path.Combine(tmp, Constants.APP_NAME_CONSOLE);
            if (!string.IsNullOrEmpty(consoleDir) && Directory.Exists(consoleDir))
            {
                foreach (string file in Directory.GetFiles(consoleDir, "*.*"))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception exi)
                    {
                        Area23Log.LogOriginEx("TestInit", exi);
                    }
                }
            }
        }
    }
}
