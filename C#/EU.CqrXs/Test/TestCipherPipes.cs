using EU.CqrXs.Crypt.Cipher;
using EU.CqrXs.Crypt.EnDeCoding;
using EU.CqrXs.Crypt.Hash;
using EU.CqrXs.Util;
using EU.CqrXs.Zip;
using System.Reflection;
using System.Reflection.Metadata;

namespace EU.CqrXs.Test
{
    /// <summary>
    /// TestCipherPipes tests all en- / decryption with full constructoed <see cref="CipherEnum"/>  algos in <see cref="CipherPipe"/>
    /// </summary>
    [TestClass]
    public sealed class TestCipherPipes
    {
        [TestMethod]
        public void TestAllCipherPipes()
        {
            string className = "TestCipherPipes";
            string methodBase = "TestAllCipherPipes";
            try
            {
                className = MethodBase.GetCurrentMethod().DeclaringType.Name;
                methodBase = MethodBase.GetCurrentMethod().Name;
            }
            catch
            {
                className = this.GetType().BaseType.Name;
                methodBase = "TestAllCipherPipes";
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
            ZipType[] zTypes = new ZipType[] { ZipType.None, ZipType.GZip, ZipType.BZip2, ZipType.Zip };
            KeyHash[] kHashes = KeyHash_Extensions.GetHashTypes();
            KeyHash kHash;
            ZipType zType;
            EncodingType[] encodingTypes = EncodingTypesExtensions.GetEncodingTypes();
            EncodingType encType;
            string plainText = File.ReadAllText(fileTextTest);

            int i = 0, j = 0;
            foreach (string email in Constants.Author_Emails)
            {

                kHash = kHashes[j % kHashes.Length];
                if ((encType = encodingTypes[j % encodingTypes.Length]) == EncodingType.None)
                    encType = encodingTypes[(j + 1) % encodingTypes.Length];
                zType = zTypes[(++j) % zTypes.Length];
                i++;

                string hashIv = kHash.Hash(email);
                CipherPipe pipe = new CipherPipe(email, hashIv, encType, zType, kHash);
                string pipeText = pipe.PipeString;

                string saveFile = "";
                if (Constants.WIN32)
                {
                    System.Drawing.Image bmp = new System.Drawing.Bitmap(pipe.GenerateEncryptPipeImage());
                    saveFile = Path.Combine(consoleDir, $"{pipe.PipeString}.png");
                    bmp.Save(saveFile, System.Drawing.Imaging.ImageFormat.Png);
                }
                else
                {
                    saveFile = Path.Combine(consoleDir, $"{pipe.PipeString}.png");
                    File.Copy(fileByesTest, saveFile, true);
                }
                
                Assert.IsTrue(File.Exists(saveFile));

                byte[] plainBytes = File.ReadAllBytes(saveFile);

                try
                {
                    startOp = DateTime.Now;
                    byte[] cipherBytes = pipe.EncryptEncodeBytes(plainBytes, email, hashIv, encType, zType, kHash);
                    Assert.IsNotNull(cipherBytes);

                    midOp = DateTime.Now;
                    encOpTime = midOp.Subtract(startOp);
                    byte[] deCodedBytes = pipe.DecodeDecrpytBytes(cipherBytes, email, hashIv, encType, zType, kHash);
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
                            Console.WriteLine($"{pipeText} \tencrypt for {email} {zType} {encType} {kHash} in {encOpTime.ToString("ss'.'ffff")} \tdecrypt in {decOpTime.ToString("ss'.'ffff")} \ttotal {allOpTime.ToString("ss'.'ffff")} [failed]");
                            Console.WriteLine($"           \tdeCodedBytes.Length ({deCodedBytes.Length}) != plainBytes.Length ({plainBytes.Length})");
                            Assert.Fail();
                        }
                    }

                    string saveFileDecrypt = Path.Combine(consoleDir, $"{pipe.PipeString}-decrypted.png");
                    File.WriteAllBytes(saveFileDecrypt, deCodedBytes);
                    double size = deCodedBytes.Length / (1024);
                    Console.WriteLine($"{pipeText} {size}KB \tencrypt for {email} {zType} {encType} {kHash} in {encOpTime.ToString("ss'.'ffff")} \tdecrypt in {decOpTime.ToString("ss'.'ffff")} \ttotal {allOpTime.ToString("ss'.'ffff")} [passed]");

                }
                catch (Exception e)
                {
                    Console.WriteLine($"{pipeText} \tException: {e.GetType()} for {email} \t{e.Message}\r\n      \t{e.StackTrace}");
                }

            }

            Console.WriteLine($"{DateTime.Now.Area23DateTimeWithSeconds()} \t{className}.{methodBase}() \t[finished]");
            return;
        }


    }
}
