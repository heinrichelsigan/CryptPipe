using EU.CqrXs.Util;
using System.Text;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Cms;
using System.Net.Http.Headers;

namespace EU.CqrXs.Crypt.Cipher.Aead
{
    public class AeadCipher
    {
        static char[] alfaBetaGammaDelta = ((string)("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz._@!:#$'´` \\\t\r\n\"")).ToCharArray();

        
        public static byte[] Encrypt(CipherEnum cipher, KeyParameter keyParam, byte[] plainTextData)
        {
            switch (cipher)
            {
                case CipherEnum.AES_CCM:
                    return CcmAeadCipher.ccmAEADNoPaddingEncrypt(keyParam, plainTextData);
                case CipherEnum.AES_GCM:
                    return GcmAeadCipher.gcmAEADEncrypt(keyParam, plainTextData);
                case CipherEnum.ASCON:
                    return AsconAeadCipher.asconAEADEncrypt(keyParam, plainTextData);
                case CipherEnum.CHACHA_POLY:
                    return ChaChaPoly.chacha20poly1305AEADEncrypt(keyParam, plainTextData);
                case CipherEnum.FF3_1_FPE:
                    char[] plainTextChars = Encoding.UTF8.GetString(plainTextData).ToCharArray();
                    char[] encryptedChars = FF3_1_FPE.ff3_1FPEEncrypt(keyParam, alfaBetaGammaDelta, plainTextChars);
                    return Encoding.UTF8.GetBytes(encryptedChars);
                case CipherEnum.WRAP_KEY_RFC3394:
                    return WrapKeyRFC3394.wrapKeyRFC3394WithIV(keyParam, new byte[8], plainTextData);
                default:
                    throw new System.Exception("Unsupported AEAD cipher");
            }
            
        }

        public static byte[] Decrypt(CipherEnum cipher, KeyParameter keyParam, byte[] cipherTextData)
        {
            switch (cipher)
            {
                case CipherEnum.AES_CCM:
                    return CcmAeadCipher.ccmAEADNoPaddingDecrypt(keyParam, cipherTextData);
                case CipherEnum.AES_GCM:
                    return GcmAeadCipher.gcmAEADDecrypt(keyParam, cipherTextData);
                case CipherEnum.ASCON:
                    return AsconAeadCipher.asconAEADDecrypt(keyParam, cipherTextData);
                case CipherEnum.CHACHA_POLY:
                    return ChaChaPoly.chacha20poly1305AEADDecrypt(keyParam, cipherTextData);
                case CipherEnum.FF3_1_FPE:
                    char[] cipherTextChars = Encoding.UTF8.GetString(cipherTextData).ToCharArray();            
                    char[] decryptedChars = FF3_1_FPE.ff3_1FPEDecrypt(keyParam, alfaBetaGammaDelta, cipherTextChars);
                    return Encoding.UTF8.GetBytes(decryptedChars);
                case CipherEnum.WRAP_KEY_RFC3394:
                    return WrapKeyRFC3394.unwrapKeyRFC3394WithIV(keyParam, new byte[8], cipherTextData);
                default:
                    throw new System.Exception("Unsupported AEAD cipher");
            }
        }

    }
}
