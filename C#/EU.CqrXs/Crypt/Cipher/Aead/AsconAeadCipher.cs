using EU.CqrXs.Util;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using System.Text;

namespace EU.CqrXs.Crypt.Cipher.Aead
{

    /// <summary>
    /// Ascon Aead Cipher
    /// </summary>
    public class AsconAeadCipher
    {

        /// <summary>
        /// ascon AEAD Encrypt
        /// </summary>
        /// <param name="keyParam"></param>
        /// <param name="plainData"></param>
        /// <returns>byte[] of encrypted data</returns>
        public static byte[] asconAEADEncrypt(KeyParameter keyParam, byte[] plainData)
        {
            AsconEngine cipher = new AsconEngine(AsconEngine.AsconParameters.ascon128a);
            int macSize = 8 * cipher.GetKeyBytesSize();
            byte[] nonce = new byte[cipher.GetIVBytesSize()];
            byte[] nonceIv = Encoding.UTF8.GetBytes(Constants.AUTHOR_IV);
            Array.Copy(nonceIv, nonce, cipher.GetIVBytesSize());
            byte[] associatedText = Encoding.UTF8.GetBytes(Constants.AUTHOR_EMAIL);
            AeadParameters keyParamAead = new AeadParameters(keyParam, macSize, nonce, associatedText);
            cipher.Init(true, keyParamAead);
            int outputSize = cipher.GetOutputSize(plainData.Length);
            byte[] cipherTextData = new byte[outputSize];
            int result = cipher.ProcessBytes(plainData, 0, plainData.Length, cipherTextData, 0);
            cipher.DoFinal(cipherTextData, result);
            return cipherTextData;
        }


        /// <summary>
        /// Ascon AEAD Decrypt
        /// </summary>
        /// <param name="keyParam"></param>
        /// <param name="cipherData"></param>
        /// <returns>decrypted byte[]</returns>
        public static byte[] asconAEADDecrypt(KeyParameter keyParam, byte[] cipherData)
        {
            AsconEngine cipher = new AsconEngine(AsconEngine.AsconParameters.ascon128a);
            int macSize = 8 * cipher.GetKeyBytesSize();
            byte[] nonce = new byte[cipher.GetIVBytesSize()];
             byte[] nonceIv = Encoding.UTF8.GetBytes(Constants.AUTHOR_IV);
            Array.Copy(nonceIv, nonce, cipher.GetIVBytesSize());          
            byte[] associatedText = Encoding.UTF8.GetBytes(Constants.AUTHOR_EMAIL);
            AeadParameters keyParamAead = new AeadParameters(keyParam, macSize, nonce, associatedText);
            cipher.Init(false, keyParamAead);
            int outputSize = cipher.GetOutputSize(cipherData.Length);
            byte[] plainTextData = new byte[outputSize];
            int result = cipher.ProcessBytes(cipherData, 0, cipherData.Length, plainTextData, 0);
            cipher.DoFinal(plainTextData, result);
            return plainTextData;
        }
    }

}
