using EU.CqrXs.Crypt.Cipher;
using EU.CqrXs.Crypt.EnDeCoding;
using EU.CqrXs.Crypt.Hash;
using EU.CqrXs.Util;
using EU.CqrXs.Zip;
using System.Reflection;

namespace EU.CqrXs.Test
{

    /// <summary>
    /// TestCompression test zipping <see cref="ZipType">enum ZipType</see>
    /// ZipType.GZip, ZipType.BZip2, ZipType.Zip 
    /// </summary>
    [TestClass]
    public sealed class TestCompression
    {

        static readonly string Email = Constants.AUTHOR_EMAIL;
        
        [TestMethod]
        public void TestTextCompression()
        {
            string className = "TestCompression";
            string methodBase = "TestAllCompression";
            try
            {
                className = MethodBase.GetCurrentMethod().DeclaringType.Name;
                methodBase = MethodBase.GetCurrentMethod().Name;
            }
            catch
            {
                className = this.GetType().BaseType.Name;
                methodBase = "TestAllCompression";
            }
            Console.WriteLine($"{DateTime.Now.Area23DateTimeWithSeconds()} \t{className}.{methodBase}() \t[started]");
            
            DateTime startOp = DateTime.Now, midOp = DateTime.Now, endOp = DateTime.Now;
            TimeSpan encOpTime, decOpTime, allOpTime = TimeSpan.Zero;
            string fileByesTest = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "2025-09-23_Stats.gif";
            string fileTextTest = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "README.MD";

            Assert.IsTrue(File.Exists(fileTextTest));
            CipherEnum[] cipherEnums = new CipherEnum[] { CipherEnum.Aes, CipherEnum.BlowFish, CipherEnum.Des3, CipherEnum.Fish2 };
            ZipType[] zTypes = new ZipType[] { ZipType.GZip, ZipType.BZip2, ZipType.Zip };
            KeyHash kHash = KeyHash.Hex;
            // ZipType zType = ZipType.None;            
            EncodingType[] encodingTypes = EncodingTypesExtensions.GetEncodingTypes();
            CipherPipe pipe = new CipherPipe(cipherEnums); //  new CipherPipe(Encoding.UTF8.GetBytes(Constants.AUTHOR_EMAIL), 0);
            string plainText = File.ReadAllText(fileTextTest);
            foreach (EncodingType encType in encodingTypes)
            {
                if (encType == EncodingType.None)
                    continue;
                foreach (ZipType zType in zTypes)
                {
                    try
                    {
                        startOp = DateTime.Now;
                        string cipherText = pipe.EncrpytTextGoRounds(plainText, Constants.AUTHOR_EMAIL, KeyHash.Hex.Hash(Constants.AUTHOR_EMAIL),
                                                    encType, zType, kHash);
                        Assert.IsNotNull(cipherText);

                        midOp = DateTime.Now;
                        encOpTime = midOp.Subtract(startOp);
                        string deCodedText = pipe.DecryptTextRoundsGo(cipherText, Constants.AUTHOR_EMAIL, KeyHash.Hex.Hash(Constants.AUTHOR_EMAIL),
                                                encType, zType, kHash);
                        Assert.AreEqual<string>(deCodedText, plainText);

                        endOp = DateTime.Now;
                        decOpTime = endOp.Subtract(midOp);
                        allOpTime = endOp.Subtract(startOp);

                        if (string.IsNullOrEmpty(deCodedText) || !deCodedText.Equals(plainText, StringComparison.Ordinal))
                        {
                            Console.WriteLine($"{zType}/{encType} \tzipped in {encOpTime.ToString("ss'.'ffff")} \tunzipped in {decOpTime.ToString("ss'.'ffff")} \ttotal {allOpTime.ToString("ss'.'ffff")} [failed]");
                            Console.WriteLine($"                 \tdeCodedText Length=[{deCodedText.Length}] != plainText Length[{plainText.Length}]");
                            Assert.Fail();
                        }
                        Console.WriteLine($"{zType}/{encType}  \tzipped in {encOpTime.ToString("ss'.'ffff")} \tunzipped in {decOpTime.ToString("ss'.'ffff")} \ttotal {allOpTime.ToString("ss'.'ffff")} [passed]");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"{zType}/{encType} \tException: {e.GetType()} \t{e.Message}\r\n      \t{e.StackTrace}");
                    }

                }
            }

            Console.WriteLine($"{DateTime.Now.Area23DateTimeWithSeconds()} \t{className}.{methodBase}() \t[finished]");
            return;
        }

        [TestMethod]
        public void TestBinaryCompression()
        {
            string className = "TestCompression";
            string methodBase = "TestAllCompression";
            try
            {
                className = MethodBase.GetCurrentMethod().DeclaringType.Name;
                methodBase = MethodBase.GetCurrentMethod().Name;
            }
            catch
            {
                className = this.GetType().BaseType.Name;
                methodBase = "TestAllCompression";
            }
            Console.WriteLine($"{DateTime.Now.Area23DateTimeWithSeconds()} \t{className}.{methodBase}() \t[started]");

            DateTime startOp = DateTime.Now, midOp = DateTime.Now, endOp = DateTime.Now;
            TimeSpan encOpTime, decOpTime, allOpTime = TimeSpan.Zero;
            string fileByesTest = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "2025-09-23_Stats.gif";
            string fileTextTest = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + "README.MD";
            string fileCsvOut = AppContext.BaseDirectory + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyy-MM-dd_hh_") + $"{className}_{methodBase}.csv";            

            Assert.IsTrue(File.Exists(fileTextTest));
            CipherEnum[] cipherEnums = new CipherEnum[] { CipherEnum.Aes, CipherEnum.BlowFish, CipherEnum.Des3, CipherEnum.Fish2 };
            ZipType[] zTypes = new ZipType[] { ZipType.GZip, ZipType.BZip2, ZipType.Zip };
            KeyHash khash = KeyHash.OpenBSDCrypt;
            EncodingType[] encodingTypes = new EncodingType[] { EncodingType.Uu, EncodingType.Xx, EncodingType.Base64, EncodingType.Hex32, EncodingType.Hex16, EncodingType.Ascii85 };
            string plainText = File.ReadAllText(fileTextTest);
            byte[] plainBytes = File.ReadAllBytes(fileByesTest);
            KeyHash[] khs = KeyHash_Extensions.GetHashTypes();
            int khcnt = 0;
            CipherPipe pipe = new CipherPipe(cipherEnums); //  new CipherPipe(Encoding.UTF8.GetBytes(Constants.AUTHOR_EMAIL), 0);
            foreach (EncodingType encType in encodingTypes)
            {
                if (encType == EncodingType.None)
                    continue;
                foreach (ZipType zType in zTypes)
                {
                    try
                    {
                        startOp = DateTime.Now;
                        byte[] cipherBytes = pipe.EncryptEncodeBytes(plainBytes, Email, KeyHash.Hex.Hash(Email),
                            encType, zType, khash);
                        Assert.IsNotNull(cipherBytes);
                        string cipherText = pipe.EncrpytTextGoRounds(plainText, Constants.AUTHOR_EMAIL,
                            KeyHash.Hex.Hash(Constants.AUTHOR_EMAIL),
                            encType, zType, khash);
                        Assert.IsNotNull(cipherText);

                        midOp = DateTime.Now;
                        encOpTime = midOp.Subtract(startOp);
                        byte[] deCodedBytes = pipe.DecodeDecrpytBytes(cipherBytes, Email, KeyHash.Hex.Hash(Email),
                            encType, zType, khash);
                        Assert.IsTrue(plainBytes != null && deCodedBytes != null && deCodedBytes.Length > 0 &&
                                      ((plainBytes.Length == deCodedBytes.Length) ||
                                       Math.Abs(deCodedBytes.Length - plainBytes.Length) <= 16));

                        endOp = DateTime.Now;
                        decOpTime = endOp.Subtract(midOp);
                        allOpTime = endOp.Subtract(startOp);

                        if (deCodedBytes != null && deCodedBytes.Length > 0 &&
                            (Math.Abs(deCodedBytes.LongLength - plainBytes.LongLength) < 16))
                        {
                            long difference = deCodedBytes.CompareBytes(plainBytes, true);
                            if (difference > 0)
                            {
                                Console.WriteLine(
                                    $"{khash}/{encType}/{zType} for bin gif\tencrypt in {encOpTime.ToString("ss'.'ffff")} \tdecrypt in {decOpTime.ToString("ss'.'ffff")} \ttotal {allOpTime.ToString("ss'.'ffff")} [failed]");
                                Console.WriteLine(
                                    $"                              \tdeCodedBytes.Length ({deCodedBytes.Length}) != plainBytes.Length ({plainBytes.Length})");
                                Assert.Fail();
                            }
                        }

                        double size = deCodedBytes.Length / (1024);
                        Console.WriteLine(
                            $"{khash}/{encType}/{zType} for bin gif {size}KB\tencrypt in {encOpTime.ToString("ss'.'ffff")} \tdecrypt in {decOpTime.ToString("ss'.'ffff")} \ttotal {allOpTime.ToString("ss'.'ffff")} [passed]");

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(
                            $"{khash}/{encType}/{zType} for bin gif \tException: {e.GetType()} \t{e.Message}\r\n      \t{e.StackTrace}");
                    }

                }
            }

            Console.WriteLine($"{DateTime.Now.Area23DateTimeWithSeconds()} \t{className}.{methodBase}() \t[finished]");
            return;
        }

    }
}
