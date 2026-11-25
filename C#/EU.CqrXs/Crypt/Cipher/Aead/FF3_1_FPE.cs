using EU.CqrXs.Crypt.EnDeCoding;
using EU.CqrXs.Crypt.Hash;
using EU.CqrXs.Util;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Fpe;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Utilities;
using System.Security.Cryptography;
using System.Text;

namespace EU.CqrXs.Crypt.Cipher.Aead
{

    /// <summary>
    /// ff3_1FPE Encrypt / Decrypt functions
    /// </summary>
    public class FF3_1_FPE
    {

        const string CQR_TWEAK = "cqrxseu";

        #region en-/decrypt

        public static char[] ff3_1FPEEncrypt(KeyParameter keyParam, char[] alphabet, char[] plainTextData)
        {
            // Create a mapper from our alphabet to indexs
            IAlphabetMapper alphabetMapper = new BasicAlphabetMapper(alphabet);
            // Create FpeParameter object                        
            byte[] tweak = Encoding.UTF8.GetBytes(CQR_TWEAK);
            FpeParameters fpeKeyParam = new FpeParameters(keyParam, alphabetMapper.Radix, tweak);
            IBlockCipher cipher = new AesEngine();
            FpeFf3_1Engine cipherMode = new FpeFf3_1Engine(cipher);
            cipherMode.Init(true, fpeKeyParam);

            byte[] cipherTextData = new byte[plainTextData.Length];
            byte[] convertedPlainTextData = alphabetMapper.ConvertToIndexes(plainTextData);

            int result = cipherMode.ProcessBlock(convertedPlainTextData, 0,
            convertedPlainTextData.Length, cipherTextData, 0);
            char[] convertedCipherTextData = alphabetMapper.ConvertToChars(cipherTextData);
            return convertedCipherTextData;
        }
        public static char[] ff3_1FPEDecrypt(KeyParameter keyParam, char[] alphabet, char[] cipherTextData)
        {
            IAlphabetMapper alphabetMapper = new BasicAlphabetMapper(alphabet);
            byte[] tweak = Encoding.UTF8.GetBytes(CQR_TWEAK);
            FpeParameters fpeKeyParam = new FpeParameters(keyParam, alphabetMapper.Radix, tweak);
            IBlockCipher cipher = new AesEngine();
            FpeFf3_1Engine cipherMode = new FpeFf3_1Engine(cipher);
            cipherMode.Init(false, fpeKeyParam);

            byte[] plainTextData = new byte[cipherTextData.Length];
            byte[] convertedCipherTextData = alphabetMapper.ConvertToIndexes(cipherTextData);

            int result = cipherMode.ProcessBlock(convertedCipherTextData, 0,
            convertedCipherTextData.Length, plainTextData, 0);
            char[] convertedPlainTextData = alphabetMapper.ConvertToChars(plainTextData);
            return convertedPlainTextData;
        }

        #endregion en-/decrypt

    }

}