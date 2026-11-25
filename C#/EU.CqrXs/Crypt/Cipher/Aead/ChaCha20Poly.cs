using EU.CqrXs.Util;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System.Text;

namespace EU.CqrXs.Crypt.Cipher.Aead
{
    public class ChaChaPoly
    {

        public static byte[] chacha20poly1305AEADEncrypt(KeyParameter keyParam, byte[] plainTextData)
        {
            ChaCha20Poly1305 cipher = new ChaCha20Poly1305();
            int macSize = 128;
            byte[] nonce = new byte[12];
            byte[] nonceIv = Encoding.UTF8.GetBytes(Constants.AUTHOR_IV);
            Array.Copy(nonceIv, nonce, 12);
            byte[] associatedText = Encoding.UTF8.GetBytes(Constants.AUTHOR_EMAIL);
            AeadParameters keyParamAead = new AeadParameters(keyParam, macSize, nonce, associatedText);

            cipher.Init(true, keyParamAead);

            int outputSize = cipher.GetOutputSize(plainTextData.Length);
            byte[] cipherTextData = new byte[outputSize];
            int result = cipher.ProcessBytes(plainTextData, 0, plainTextData.Length, cipherTextData, 0);
            cipher.DoFinal(cipherTextData, result);
            return cipherTextData;
        }

        public static byte[] chacha20poly1305AEADDecrypt(KeyParameter keyParam, byte[] cipherTextData)
        {
            ChaCha20Poly1305 cipher = new ChaCha20Poly1305();
            int macSize = 128;
            byte[] nonce = new byte[12];
            byte[] nonceIv = Encoding.UTF8.GetBytes(Constants.AUTHOR_IV);
            Array.Copy(nonceIv, nonce, 12);
            byte[] associatedText = Encoding.UTF8.GetBytes(Constants.AUTHOR_EMAIL);
            AeadParameters keyParamAead = new AeadParameters(keyParam, macSize, nonce, associatedText);
            cipher.Init(false, keyParamAead);
            int outputSize = cipher.GetOutputSize(cipherTextData.Length);
            byte[] plainTextData = new byte[outputSize];
            int result = cipher.ProcessBytes(cipherTextData, 0, cipherTextData.Length, plainTextData, 0);
            cipher.DoFinal(plainTextData, result);
            return plainTextData;
        }


    }

}
