using EU.CqrXs.Util;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System.Text;

namespace EU.CqrXs.Crypt.Cipher.Aead
{

    public class GcmAeadCipher
    {

        public static byte[] gcmAEADEncrypt(KeyParameter keyParam, byte[] plainTextData)
        {
            IBlockCipher cipher = new AesEngine();
            int macSize = 8 * cipher.GetBlockSize();
            byte[] nonce = Encoding.UTF8.GetBytes(Constants.AUTHOR_IV);
            byte[] associatedText = Encoding.UTF8.GetBytes(Constants.AUTHOR_EMAIL);
            AeadParameters keyParamAead = new AeadParameters(keyParam, macSize, nonce, associatedText);
            GcmBlockCipher cipherMode = new GcmBlockCipher(cipher);
            cipherMode.Init(true, keyParamAead);

            int outputSize = cipherMode.GetOutputSize(plainTextData.Length);
            byte[] cipherTextData = new byte[outputSize];
            int result = cipherMode.ProcessBytes(plainTextData, 0, plainTextData.Length, cipherTextData, 0);
            cipherMode.DoFinal(cipherTextData, result);
            return cipherTextData;
        }
        public static byte[] gcmAEADDecrypt(KeyParameter keyParam, byte[] cipherTextData)
        {
            IBlockCipher cipher = new AesEngine();
            int macSize = 8 * cipher.GetBlockSize();
            byte[] nonce = Encoding.UTF8.GetBytes(Constants.AUTHOR_IV);
            byte[] associatedText = Encoding.UTF8.GetBytes(Constants.AUTHOR_EMAIL);
            AeadParameters keyParamAead = new AeadParameters(keyParam, macSize, nonce, associatedText);
            GcmBlockCipher cipherMode = new GcmBlockCipher(cipher);
            cipherMode.Init(false, keyParamAead);

            int outputSize = cipherMode.GetOutputSize(cipherTextData.Length);
            byte[] plainTextData = new byte[outputSize];
            int result = cipherMode.ProcessBytes(cipherTextData, 0, cipherTextData.Length,
            plainTextData, 0);
            cipherMode.DoFinal(plainTextData, result);
            return plainTextData;
        }

    }

}
