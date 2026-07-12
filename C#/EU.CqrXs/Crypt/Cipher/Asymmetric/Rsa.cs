using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace EU.CqrXs.Crypt.Cipher.Asymmetric
{
    /// <summary>
    /// Rsa Asymmetric cipher 
    /// #!/usr/bin/bash
    ///     KLEN=512
    ///     if [ $# -gt 0 ] ; then KLEN=$1; fi
    ///     if [ $KLEN -lt 512 ] ; then KLEN=512; fi
    ///     openssl genpkey -algorithm rsa -pkeyopt rsa_keygen_bits:$KLEN > /tmp/rsa_$KLEN.pk8 2>/dev/null
    ///     cat /tmp/rsa_$KLEN.pk8
    ///     openssl rsa -in /tmp/rsa_$KLEN.pk8 -pubout | tee rsa_$KLEN.spki
    ///     sleep 1
    ///     rm -f /tmp/rsa_$KLEN.pk8
    /// </summary>
    public static class Rsa
    {
       
        private static AsymmetricCipherKeyPair rsaKeyPair;       
        internal static AsymmetricCipherKeyPair RsaKeyPair { get => rsaKeyPair; }

        static Rsa()
        {
            GenerateNewRsaKeyPair();
        }

        public static AsymmetricCipherKeyPair RsaGenWithKey(string pub, string priv)
        {
            if (rsaKeyPair != null)
                return RsaKeyPair;
            rsaKeyPair = GetRsaKeyPair(pub, priv);
            return RsaKeyPair;
        }

        /// <summary>
        /// GenerateNewRsaKeyPair - generates a new rsa key pair
        /// </summary>
        /// <returns><see cref="AsymmetricCipherKeyPair"/></returns>
        internal static AsymmetricCipherKeyPair GenerateNewRsaKeyPair()
        {
            if (rsaKeyPair != null)
                return RsaKeyPair;

            RsaKeyPairGenerator rsaKeyGen = new RsaKeyPairGenerator();            
            SecureRandom rand = new SecureRandom(new VmpcRandomGenerator(), 2048);
            KeyGenerationParameters keyParams = new KeyGenerationParameters(rand, 2048);
            rsaKeyGen.Init(keyParams);

            rsaKeyPair = rsaKeyGen.GenerateKeyPair();
            return RsaKeyPair;
        }

        /// <summary>
        /// Get Rsa Key Pair by private and public key
        /// </summary>
        /// <param name="pubKey"></param>
        /// <param name="privKey"></param>
        /// <returns><see cref="AsymmetricCipherKeyPair"/></returns>
        internal static AsymmetricCipherKeyPair GetRsaKeyPair(string pubKey, string privKey)
        {            
            Pkcs1Encoding rsaCipher = new Pkcs1Encoding(new RsaEngine());
            AsymmetricKeyParameter keyParameterPublic;
            RsaPrivateCrtKeyParameters keyParameterPrivate;

            using (StringReader stringReader = new StringReader(pubKey))
            {
                keyParameterPublic = (AsymmetricKeyParameter)new PemReader(stringReader).ReadObject();
            }

            using (var txtreader = new StringReader(privKey))
            {
                keyParameterPrivate = (RsaPrivateCrtKeyParameters)new PemReader(txtreader).ReadObject();
            }

            rsaKeyPair = new AsymmetricCipherKeyPair(keyParameterPublic, keyParameterPrivate);
            return RsaKeyPair;
        }


        #region EncryptDecryptBytes

        public static byte[] Encrypt(byte[] bytesToEncrypt, AsymmetricCipherKeyPair pair) => EncryptWithPublic(bytesToEncrypt, pair);

        public static byte[] Decrypt(byte[] bytesToDecrypt, AsymmetricCipherKeyPair pair) => DecryptWithPrivate(bytesToDecrypt, pair);


        /// <summary>
        /// Rsa encrypt bytes with public key
        /// </summary>
        /// <param name="bytesToEncrypt"><see cref="T:byte[]">bytes to encrypt</see></param>
        /// <param name="pair"></param>
        /// <returns>encrypted <see cref="T:yte[]"/></returns>
        public static byte[] EncryptWithPublic(byte[] bytesToEncrypt, AsymmetricCipherKeyPair pair)
        {
            var encryptEngine = new Pkcs1Encoding(new RsaEngine());
            AsymmetricKeyParameter keyParameter = (rsaKeyPair != null) ? rsaKeyPair.Public : (AsymmetricKeyParameter)pair.Public;
            encryptEngine.Init(true, keyParameter);

            byte[] encryptedBytes = encryptEngine.ProcessBlock(bytesToEncrypt, 0, bytesToEncrypt.Length);
            return encryptedBytes;

        }

        public static byte[] EncryptWithPrivate(byte[] bytesToEncrypt, AsymmetricCipherKeyPair pair)
        {
            var encryptEngine = new Pkcs1Encoding(new RsaEngine());
            encryptEngine.Init(true, pair.Private);

            byte[] encryptedBytes = encryptEngine.ProcessBlock(bytesToEncrypt, 0, bytesToEncrypt.Length);
            return encryptedBytes;
        }


        /// <summary>
        /// Rsa DecryptWithPublic key
        /// </summary>
        /// <param name="bytesToDecrypt"></param>
        /// <param name="pair"></param>
        /// <returns></returns>
        public static byte[] DecryptWithPublic(byte[] bytesToDecrypt, AsymmetricCipherKeyPair pair)
        {
            var decryptEngine = new Pkcs1Encoding(new RsaEngine());
            AsymmetricKeyParameter keyParameter = (rsaKeyPair != null) ? rsaKeyPair.Public : (AsymmetricKeyParameter)pair.Public;
            decryptEngine.Init(false, keyParameter);

            byte[] decrypted = decryptEngine.ProcessBlock(bytesToDecrypt, 0, bytesToDecrypt.Length);
            return decrypted;
        }

        public static byte[] DecryptWithPrivate(byte[] bytesToDecrypt, AsymmetricCipherKeyPair pair)
        {
            AsymmetricCipherKeyPair keyPair;
            var decryptEngine = new Pkcs1Encoding(new RsaEngine());
            decryptEngine.Init(false, pair.Private);

            byte[] decryptedBytes = decryptEngine.ProcessBlock(bytesToDecrypt, 0, bytesToDecrypt.Length);
            return decryptedBytes;
        }

        #endregion EncryptDecryptBytes

    }

}
