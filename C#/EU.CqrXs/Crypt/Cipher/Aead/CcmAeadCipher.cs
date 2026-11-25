using EU.CqrXs.Util;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System.Text;

namespace EU.CqrXs.Crypt.Cipher.Aead
{
    public class CcmAeadCipher
    {


        public static byte[] ccmAEADNoPaddingEncrypt(KeyParameter keyParam, byte[] plainTextData)
        {
            IBlockCipher symmetricBlockCipher = new AesEngine();
            int macSize = 8 * symmetricBlockCipher.GetBlockSize();
            byte[] nonce = new byte[12];
            byte[] associatedText = Encoding.UTF8.GetBytes(Constants.AUTHOR_EMAIL);
            byte[] ivNonce = Encoding.UTF8.GetBytes(Constants.AUTHOR_IV);
            Array.Copy(ivNonce, nonce, nonce.Length);
            AeadParameters keyParamAead = new AeadParameters(keyParam, macSize, nonce, associatedText);
            CcmBlockCipher cipherMode = new CcmBlockCipher(symmetricBlockCipher);
            cipherMode.Init(true, keyParamAead);
            cipherMode.ProcessBytes(plainTextData, 0, plainTextData.Length, null, 0);

            int outputSize = cipherMode.GetOutputSize(0);
            byte[] cipherTextData = new byte[outputSize];
            cipherMode.DoFinal(cipherTextData, 0);
            return cipherTextData;
        }

        public static byte[] ccmAEADNoPaddingDecrypt(KeyParameter keyParam, byte[] cipherTextData)
        {
            IBlockCipher symmetricBlockCipher = new AesEngine();
            int macSize = 8 * symmetricBlockCipher.GetBlockSize();
            byte[] nonce = new byte[12];
            byte[] associatedText = Encoding.UTF8.GetBytes(Constants.AUTHOR_EMAIL);
            byte[] ivNonce = Encoding.UTF8.GetBytes(Constants.AUTHOR_IV);
            Array.Copy(ivNonce, nonce, nonce.Length);
            AeadParameters keyParamAead = new AeadParameters(keyParam, macSize, nonce, associatedText);

            CcmBlockCipher cipherMode = new CcmBlockCipher(symmetricBlockCipher);
            cipherMode.Init(false, keyParamAead);
            cipherMode.ProcessBytes(cipherTextData, 0, cipherTextData.Length, null, 0);

            int outputSize = cipherMode.GetOutputSize(0);
            byte[] plainTextData = new byte[outputSize];

            cipherMode.DoFinal(plainTextData, 0);

            return plainTextData;

        }


    }

}
