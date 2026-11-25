using EU.CqrXs.Util;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Fpe;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace EU.CqrXs.Crypt.Cipher.Aead
{
    public class WrapKeyRFC3394
    {

        public static byte[] wrapKeyRFC3394WithIV(ICipherParameters keyParam, byte[] IV, byte[] unwrappedData)
        {
            ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParam, IV);

            int restLen = (8 - (unwrappedData.Length % 8));
            if (restLen > 0)
            {
                byte[] restBytes = new byte[restLen];
                unwrappedData = unwrappedData.TarBytes(restBytes.FillWithZeros(restLen));
            }
            IBlockCipher symmetricBlockCipher = new AesEngine();
            Rfc3394WrapEngine wrapEngine = new Rfc3394WrapEngine(symmetricBlockCipher);
            wrapEngine.Init(true, keyParamWithIV);
            return wrapEngine.Wrap(unwrappedData, 0, unwrappedData.Length);
        }

        public static byte[] unwrapKeyRFC3394WithIV(ICipherParameters keyParam, byte[] IV, byte[] wrappedData)
        {
            ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParam, IV);
            IBlockCipher symmetricBlockCipher = new AesEngine();
            Rfc3394WrapEngine wrapEngine = new Rfc3394WrapEngine(symmetricBlockCipher);
            wrapEngine.Init(false, keyParamWithIV);
            return wrapEngine.Unwrap(wrappedData, 0, wrappedData.Length);
        }

    }
}
