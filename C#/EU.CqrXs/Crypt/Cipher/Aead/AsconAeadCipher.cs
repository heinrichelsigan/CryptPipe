using EU.CqrXs.Util;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using System.Text;

namespace EU.CqrXs.Crypt.Cipher.Aead
{

    public class AsconAeadCipher
    {
        public static byte[] asconAEADEncrypt(KeyParameter keyParam, byte[] plainTextData)
        {
            AsconEngine cipher = new AsconEngine(AsconEngine.AsconParameters.ascon128a);
            int macSize = 8 * cipher.GetKeyBytesSize();
            byte[] nonce = new byte[cipher.GetIVBytesSize()];
            byte[] nonceIv = Encoding.UTF8.GetBytes(Constants.AUTHOR_IV);
            Array.Copy(nonceIv, nonce, cipher.GetIVBytesSize());
            byte[] associatedText = Encoding.UTF8.GetBytes(Constants.AUTHOR_EMAIL);
            AeadParameters keyParamAead = new AeadParameters(keyParam, macSize, nonce, associatedText);
            cipher.Init(true, keyParamAead);
            int outputSize = cipher.GetOutputSize(plainTextData.Length);
            byte[] cipherTextData = new byte[outputSize];
            int result = cipher.ProcessBytes(plainTextData, 0, plainTextData.Length, cipherTextData, 0);
            cipher.DoFinal(cipherTextData, result);
            return cipherTextData;
        }

        public static byte[] asconAEADDecrypt(KeyParameter keyParam, byte[] cipherTextData)
        {
            AsconEngine cipher = new AsconEngine(AsconEngine.AsconParameters.ascon128a);
            int macSize = 8 * cipher.GetKeyBytesSize();
            byte[] nonce = new byte[cipher.GetIVBytesSize()];
             byte[] nonceIv = Encoding.UTF8.GetBytes(Constants.AUTHOR_IV);
            Array.Copy(nonceIv, nonce, cipher.GetIVBytesSize());          
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
