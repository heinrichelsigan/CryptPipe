using EU.CqrXs.Crypt.Cipher;
using EU.CqrXs.Crypt.Cipher.Symmetric;
using EU.CqrXs.Util;
using System.Reflection;
using System.Security;

namespace EU.CqrXs.Test
{
    /// <summary>
    /// TestSecurePipes tests all en- / decryption with full <see cref="SecureCipherPipe"/>
    /// </summary>
    [TestClass]
    public sealed class TestSecurePipes
    {
        [TestMethod]
        public void TestAllCipherPipes()
        {
            string className = "TestSecurePipes";
            string methodBase = "TestAllSecurePipes";
            try
            {
                className = MethodBase.GetCurrentMethod().DeclaringType.Name;
                methodBase = MethodBase.GetCurrentMethod().Name;
            }
            catch
            {
                className = this.GetType().BaseType.Name;
                methodBase = "TestAllSecurePipes";
            }
            Console.WriteLine($"{DateTime.Now.Area23DateTimeWithSeconds()} \t{className}.{methodBase}() \t[started]");

            DateTime startOp = DateTime.Now, midOp = DateTime.Now, endOp = DateTime.Now;
            TimeSpan encOpTime, decOpTime, allOpTime = TimeSpan.Zero;
            string fileByesTest = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "2025-09-23_Stats.gif";
            string fileTextTest = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "README.MD";
            string fileCsvOut = AppContext.BaseDirectory + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyy-MM-dd_hh_") + $"{className}_{methodBase}.csv";
            string tmp = Environment.GetEnvironmentVariable("TEMP");
            if (string.IsNullOrEmpty(tmp) || !Directory.Exists(tmp))
                tmp = Environment.GetEnvironmentVariable("TMP");
            if (string.IsNullOrEmpty(tmp) || !Directory.Exists(tmp))
                tmp = AppDomain.CurrentDomain.BaseDirectory;

            string consoleDir = Path.Combine(tmp, Constants.APP_NAME_CONSOLE);
            if (!Directory.Exists(consoleDir))
                Directory.CreateDirectory(consoleDir);
            
            CipherEnum[] cipherEnums = CipherEnumExtensions.GetCipherTypes();
            CipherMode2[] cModes = CipherModeExtensions.GetCipherModes2(true);

            int i = 0, j = 0;
            foreach (string email in Constants.Author_Emails)
            {
                CipherMode2 cMode = cModes[(++i) % cModes.Length];

                SecureCipherPipe spipe = new SecureCipherPipe(email, cMode, false);
                string pipeText = spipe.PipeString;

                string saveFile = "";
                if (Constants.WIN32)
                {
                    System.Drawing.Image bmp = new System.Drawing.Bitmap(spipe.GenerateEncryptPipeImage());
                    saveFile = Path.Combine(consoleDir, $"{spipe.PipeString}.png");
                    bmp.Save(saveFile, System.Drawing.Imaging.ImageFormat.Png);
                }
                else
                {
                    saveFile = Path.Combine(consoleDir, $"{spipe.PipeString}.png");
                    File.Copy(fileByesTest, saveFile, true);
                }
                
                Assert.IsTrue(File.Exists(saveFile));

                byte[] plainBytes = File.ReadAllBytes(saveFile);

                try
                {
                    startOp = DateTime.Now;
                    byte[] cipherBytes = spipe.EncryptEncodeBytes(plainBytes, email, cMode);
                    Assert.IsNotNull(cipherBytes);

                    midOp = DateTime.Now;
                    encOpTime = midOp.Subtract(startOp);
                    byte[] deCodedBytes = spipe.DecodeDecrpytBytes(cipherBytes, email, cMode);
                    Assert.IsTrue(plainBytes != null && deCodedBytes != null && deCodedBytes.Length > 0 &&
                        (Math.Abs(deCodedBytes.Length - plainBytes.Length) <= Constants.PIPE_BYTES_DELTA));

                    Assert.IsTrue(plainBytes != null && deCodedBytes != null && 
                        deCodedBytes.Length > 0 && deCodedBytes.Length > 0 &&
                        (Math.Abs(deCodedBytes.LongLength - plainBytes.LongLength) < Constants.PIPE_BYTES_DELTA) &&
                        plainBytes[0] == deCodedBytes[0] && plainBytes[1] == deCodedBytes[1] &&
                        plainBytes[i + Constants.PIPE_BYTES_DELTA] == deCodedBytes[i + Constants.PIPE_BYTES_DELTA] && 
                        plainBytes[i + Constants.PIPE_MAX_LEN] == deCodedBytes[i + Constants.PIPE_MAX_LEN]);

                    endOp = DateTime.Now;
                    decOpTime = endOp.Subtract(midOp);
                    allOpTime = endOp.Subtract(startOp);

                    if (deCodedBytes != null && deCodedBytes.Length > 0 &&
                        (Math.Abs(deCodedBytes.LongLength - plainBytes.LongLength) > Constants.PIPE_BYTES_DELTA))
                    {
                        long difference = deCodedBytes.CompareBytes(plainBytes, true);
                        if (difference > 0)
                        {
                            Console.WriteLine($"{pipeText} \tencrypt for {email} {cMode} in {encOpTime.ToString("ss'.'ffff")} \tdecrypt in {decOpTime.ToString("ss'.'ffff")} \ttotal {allOpTime.ToString("ss'.'ffff")} [failed]");
                            Console.WriteLine($"           \tdeCodedBytes.Length ({deCodedBytes.Length}) != plainBytes.Length ({plainBytes.Length})");
                            Assert.Fail();
                        }
                    }

                    string saveFileDecrypt = Path.Combine(consoleDir, $"{spipe.PipeString}-decrypted.png");
                    File.WriteAllBytes(saveFileDecrypt, deCodedBytes);
                    double size = deCodedBytes.Length / (1024);
                    Console.WriteLine($"{pipeText} {size}KB \tencrypt for {email} {cMode} in {encOpTime.ToString("ss'.'ffff")} \tdecrypt in {decOpTime.ToString("ss'.'ffff")} \ttotal {allOpTime.ToString("ss'.'ffff")} [passed]");

                }
                catch (Exception e)
                {
                    Console.WriteLine($"{pipeText} \tException: {e.GetType()} for {email} {cMode} \t{e.Message}\r\n      \t{e.StackTrace}");
                }

            }

            Console.WriteLine($"{DateTime.Now.Area23DateTimeWithSeconds()} \t{className}.{methodBase}() \t[finished]");
            return;
        }

    }
}
