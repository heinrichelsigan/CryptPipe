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
            string statdir = SystemDirPath;            
            if (!string.IsNullOrEmpty(statdir) && Directory.Exists(statdir))
            {
                foreach (string file in Directory.GetFiles(statdir, "*.csv"))
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
            // This method is called after each test method.
            string[] files = { "2025-09-23_Stats.gif", "README.MD" };
            string statdir = SystemDirPath;
            
            foreach (string filename in files)
            {
                if (File.Exists(statdir + filename))
                {
                    try
                    {
                        File.Delete(statdir + filename);
                    }
                    catch (Exception exi)
                    {
                        Area23Log.LogOriginMsg("TestInit", $"{exi.GetType().Name} deleting file {filename} " +
                            $"in dir {statdir}: {exi.Message}", 2);
                    }
                }
            }            
            foreach (string file in Directory.GetFiles(statdir, "*.csv"))
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
            Area23Log.LogOriginMsg("TestInitCleanup", "TestCleanup");
        }

    }
}
