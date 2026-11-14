using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

namespace EU.CqrXs.Crypt.Cipher.Symmetric
{
    
    /// <summary>
    /// StreamCipherBC class to perform all StreamCiphers
    /// </summary>
    public class StreamCipherBC : IStreamCipher
    {

        #region fields

        protected internal bool initialised = false, encryption;

        #endregion fields

        #region properties
        
        /// <summary>
        /// Base symmetric key block cipher interface, contains at runtime block cipher instance to constructor
        /// </summary>
        public IStreamCipher CryptoStreamCipher { get; private set; }

        public ICipherParameters StreamCipherParameters { get; private set; } 
        
        #endregion properties
        
        #region IStreamCipher interface

        private const string SYMMCIPHERALGONAME2 = "StreamCipher";

        public new string AlgorithmName => (CryptoStreamCipher.AlgorithmName == null)
            ? SYMMCIPHERALGONAME2
            : CryptoStreamCipher.AlgorithmName; 

        public void Reset() => CryptoStreamCipher.Reset();

        public void Init(bool forEncryption, ICipherParameters parameters)
        {
            if (!(parameters is KeyParameter) && !(parameters is ParametersWithIV))
                throw new ArgumentException("only KeyParameter or ParametersWithIV expected.", "parameters");
            
            encryption = forEncryption;
            CryptoStreamCipher.Init(forEncryption, StreamCipherParameters);
            initialised = true;
        }

        public byte ReturnByte(byte input) => CryptoStreamCipher.ReturnByte(input);

        public void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
        {            
            output = new byte[input.Length];
            
            int i = inOff;
            int j = outOff;
            for (i = inOff; i < (inOff + length); i++)
            {
                output[j++] = CryptoStreamCipher.ReturnByte(input[i]);
            }
        }

        public void ProcessBytes(ReadOnlySpan<byte> input, Span<byte> output)
        {           
            output = new byte[input.Length];
            
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = ReturnByte(input[i]);
            }
        }

        #endregion IStreamCipher interface


        public StreamCipherBC(CryptParams cparams, bool init = true)
        {
            CryptoStreamCipher = cparams.StreamCipher == null ? new ChaChaEngine() : cparams.StreamCipher;

            StreamCipherParameters = cparams.StreamCipherParameters;
            Init(init, StreamCipherParameters);
        }


        public byte[] Encrypt(byte[] plainTextData)
        {           
            byte[] cipherTextData = new byte[plainTextData.Length];
            
            for (int j = 0; j < plainTextData.Length; j++)
            {
                cipherTextData[j] = ReturnByte(plainTextData[j]);
            }

            return cipherTextData;
        }


        public byte[] Decrypt(byte[] cipherTextData)
        {
            byte[] plainTextData = new byte[cipherTextData.Length];
            
            for (int j = 0; j < cipherTextData.Length; j++)
            {
                plainTextData[j] = ReturnByte(cipherTextData[j]);
            }

            return plainTextData;
        }
    }
}