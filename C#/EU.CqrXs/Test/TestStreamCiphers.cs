using EU.CqrXs.Crypt.Cipher;
using EU.CqrXs.Crypt.EnDeCoding;
using EU.CqrXs.Crypt.Hash;
using EU.CqrXs.Util;
using EU.CqrXs.Zip;
using System.Reflection;

namespace EU.CqrXs.Test
{
    /// <summary>
    /// TestStreamCiphers tests all encryption / decryption via <see cref="CipherEnum"/> = <see cref="CipherEnumExtensions.GetCipherTypes()" />
    /// Aes, BlowFish, Camellia, Cast6, Des3, Fish2, Fish3, ...        
    /// </summary>
    [TestClass]
    public sealed class TestStreamCiphers
    {
        internal static readonly string Email = Constants.AUTHOR_EMAIL;

        static TestStreamCiphers()
        {
            try
            {
                Email = RegistryAccessor.GetEmailFromRegistry();
            }
            catch
            {
                Email = Constants.AUTHOR_EMAIL;
            }
        }
        
        [TestMethod]
        public void TestAllStreamCiphers()
        {
            string className = "TestStreamCiphers";
            string methodBase = "TestAllStreamCiphers";
            try
            {
                className = MethodBase.GetCurrentMethod().DeclaringType.Name;
                methodBase = MethodBase.GetCurrentMethod().Name;
            }
            catch
            {
                className = this.GetType().BaseType.Name;
                methodBase = "TestAllEncryptionSingleAlgo";
            }
            Console.WriteLine($"{DateTime.Now.Area23DateTimeWithSeconds()} \t{className}.{methodBase}() \t[started]");

            DateTime startOp = DateTime.Now, midOp = DateTime.Now, endOp = DateTime.Now;
            TimeSpan encOpTime, decOpTime, allOpTime = TimeSpan.Zero;
            string fileBytesTest = AppContext.BaseDirectory + Path.DirectorySeparatorChar + "2025-09-23_Stats.gif";
            string fileTextTest = AppContext.BaseDirectory + Path.DirectorySeparatorChar + "README.MD";
            string fileCsvOut = AppContext.BaseDirectory + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyy-MM-dd_hh_") + $"{className}_{methodBase}.csv";
            
            Assert.IsTrue(File.Exists(fileTextTest));
            CipherEnum[] cipherTypes =
            {
                CipherEnum.ChaCha, CipherEnum.XChaCha20, CipherEnum.ChaCha7539,
                CipherEnum.Salsa20, CipherEnum.XSalsa20,
                CipherEnum.HC128, CipherEnum.HC256,
                CipherEnum.Isaac, CipherEnum.RC4,
                CipherEnum.VmpcKsa3
            };
            CipherEnum cipherType = CipherEnum.Des3;
            ZipType[] zTypes = new ZipType[] { ZipType.None };
            KeyHash kHash = KeyHash.Hex;
            ZipType zType = ZipType.None;
            EncodingType[] encodingTypes = EncodingTypesExtensions.GetEncodingTypes();
            EncodingType encType = EncodingType.Base64;            
            string plainText = File.ReadAllText(fileTextTest);
            byte[] plainBytes = File.ReadAllBytes(fileBytesTest);
            foreach (CipherEnum cipherEnum in cipherTypes)
            {
                // cipherType = (cipherEnum == CipherEnum.Rsa) ? CipherEnum.Des3 : cipherEnum;
                cipherType = cipherEnum;

                CipherEnum[] cipherEnums = new CipherEnum[] { cipherType };
                CipherPipe pipe = new CipherPipe(cipherEnums); // new CipherPipe(Encoding.UTF8.GetBytes(Constants.AUTHOR_EMAIL), 0);
                try
                {
                    startOp = DateTime.Now;
                    byte[] cipherBytes = pipe.EncryptEncodeBytes(plainBytes, Email, KeyHash.Hex.Hash(Email),
                                                encType, zType, kHash);
                   Assert.IsNotNull(cipherBytes);
                    
                    midOp = DateTime.Now;
                    encOpTime = midOp.Subtract(startOp);
                    byte[] deCodedBytes = pipe.DecodeDecrpytBytes(cipherBytes, Email, KeyHash.Hex.Hash(Email),
                                            encType, zType, kHash);
                    Assert.IsTrue(plainBytes != null && deCodedBytes != null &&  deCodedBytes.Length > 0 && 
                        ((plainBytes.Length == deCodedBytes.Length) || 
                         Math.Abs(deCodedBytes.Length - plainBytes.Length) <= Constants.PIPE_BYTES_DELTA_SMALL));

                    endOp = DateTime.Now;
                    decOpTime = endOp.Subtract(midOp);
                    allOpTime = endOp.Subtract(startOp);

                    if (deCodedBytes == null || deCodedBytes.Length < 1 ||
                        (deCodedBytes.Length != plainBytes.Length && 
                         Math.Abs(deCodedBytes.Length - plainBytes.Length) > Constants.PIPE_BYTES_DELTA_SMALL))
                    {
                        Console.WriteLine($"{cipherType} for {Email}\tencrypt in {encOpTime.ToString("ss'.'ffff")} \tdecrypt in {decOpTime.ToString("ss'.'ffff")} \ttotal {allOpTime.ToString("ss'.'ffff")} [failed]");
                        Console.WriteLine($"          \tdeCodedBytes.Length ({deCodedBytes.Length}) != plainBytes.Length ({plainBytes.Length})");
                        Assert.Fail();
                    }
                    double size = deCodedBytes.Length / (1024);
                    Console.WriteLine($"{size}KB {cipherType} for {Email} \tencrypt in {encOpTime.ToString("ss'.'ffff")} \tdecrypt in {decOpTime.ToString("ss'.'ffff")} \ttotal {allOpTime.ToString("ss'.'ffff")} [passed]");
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{cipherType} for {Email} \tException: {e.GetType()} \t{e.Message}\r\n      \t{e.StackTrace}");
                }                

            }
            Console.WriteLine($"{DateTime.Now.Area23DateTimeWithSeconds()} \t{className}.{methodBase}() \t[finished]");
            return;
        }

    }
}
