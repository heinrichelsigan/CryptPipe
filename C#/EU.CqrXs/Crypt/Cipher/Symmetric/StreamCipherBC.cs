using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace EU.CqrXs.Crypt.Cipher.Symmetric
{
    public class StreamCipherBC : IStreamCipher
    {

        #region fields

        protected internal bool initialised = false, forEncryption;

        #endregion fields

        #region properties

        internal byte[] Key { get; private set; }
        internal byte[] Iv { get; private set; }

        /// <summary>
        /// Block Size
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// KeyLen byte[KeyLen] of Key and Iv
        /// </summary>
        public int KeyLen { get; private set; }

        public int IvLen { get; private set; }

        /// <summary>
        /// Base symmetric key block cipher interface, contains at runtime block cipher instance to constructor
        /// </summary>
        public IStreamCipher CryptoStreamCipher { get; private set; }

        public ICipherParameters StreamCipherParameters { get; private set; } 

        
        #endregion properties
        
        #region IStreamCipher interface

        private const string SYMMCIPHERALGONAME2 = "StreamCipher";

        public void Reset() => CryptoStreamCipher.Reset();

        public new string AlgorithmName => SYMMCIPHERALGONAME2;
        

        public void Init(bool forEncryption, ICipherParameters parameters)
        {
            if (!(parameters is KeyParameter) && !(parameters is ParametersWithIV))
                throw new ArgumentException("only KeyParameter or ParametersWithIV expected.", "parameters");
            
            Key = new byte[KeyLen];
            Iv = new byte[IvLen];
            
            if (parameters is KeyParameter)
            {
                Key = ((KeyParameter)parameters).GetKey();
            }

            if (parameters is ParametersWithIV)
            {
                Key = new byte[0];
                if (((ParametersWithIV)parameters).Parameters is KeyParameter)
                    Key = ((KeyParameter)((ParametersWithIV)parameters).Parameters).GetKey();
                Iv = ((ParametersWithIV)parameters).GetIV();

                Key = Key == null || Key.Length == 0 ? new byte[0] : Key;
                Iv = Iv == null || Iv.Length == 0 ? new byte[0] : Iv;
                if (Key.Length == 0 && Iv.Length == 0)
                    throw new ArgumentNullException("parameters",
                        "KeyParameter and/or ParametersWithIV contain a null or empty key or iv.");
            }
            
            KeyLen = Key.Length;
            IvLen = Iv.Length;
            
            forEncryption = forEncryption;
            initialised = true;
        }

        public byte ReturnByte(byte input) => CryptoStreamCipher.ReturnByte(input);

        public void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
        {
            CryptoStreamCipher.Init(true, StreamCipherParameters);
            output = new byte[input.Length];
            
            int i = inOff;
            int j = outOff;
            // simulate stream
            for (i = inOff; i < (inOff + length); i++)
            {
                output[j++] = CryptoStreamCipher.ReturnByte(input[i]);
            }
        }

        public void ProcessBytes(ReadOnlySpan<byte> input, Span<byte> output)
        {
            CryptoStreamCipher.Init(true, StreamCipherParameters);
            output = new byte[input.Length];
            
            // simulate stream
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = ReturnByte(input[i]);
            }
        }



        #endregion IStreamCipher interface


        public StreamCipherBC(CryptParams cparams, bool init = true)
        {
            CryptoStreamCipher = cparams.StreamCipher == null ? new ChaChaEngine() : cparams.StreamCipher;
            if (CryptoStreamCipher.AlgorithmName.Contains("ChaCha") ||
                CryptoStreamCipher.AlgorithmName.Contains("7539"))
                CryptoStreamCipher = new ChaCha7539Engine();

            StreamCipherParameters = cparams.StreamCipherParameters;
            Init(init, StreamCipherParameters);
        }


        public byte[] Encrypt(byte[] plainTextData)
        {
            CryptoStreamCipher.Init(true, StreamCipherParameters);
            byte[] cipherTextData = new byte[plainTextData.Length];

            // simulate stream
            for (int j = 0; j < plainTextData.Length; j++)
            {
                cipherTextData[j] = CryptoStreamCipher.ReturnByte(plainTextData[j]);
            }

            return cipherTextData;
        }


        public byte[] Decrypt(byte[] cipherTextData)
        {
            CryptoStreamCipher.Init(false, StreamCipherParameters);
            byte[] plainTextData = new byte[cipherTextData.Length];

            // simulate stream
            for (int j = 0; j < cipherTextData.Length; j++)
            {
                plainTextData[j] = CryptoStreamCipher.ReturnByte(cipherTextData[j]);
            }

            return plainTextData;
        }

    }
}