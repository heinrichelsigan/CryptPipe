using EU.CqrXs.Crypt.EnDeCoding;
using EU.CqrXs.Util;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace EU.CqrXs.Crypt.Cipher.Symmetric
{

    /// <summary>
    /// Generic BlockCipherBC BounceCastle Encryption / Decryption class
    /// supports <see cref="CamelliaEngine"/>, <see cref="Gost28147Engine"/>, <see cref="RC2Engine"/>,
    /// <see cref="RC532Engine"/>, <see cref="RC6Engine"/>, <see cref="RijndaelEngine">RijndaelEngine is standard AES</see>, 
    /// <see cref="SkipjackEngine"/>, <see cref="TeaEngine"/>, <see cref="TnepresEngine"/>,
    /// <see cref="XteaEngine"/>, ... and many more
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <listheader>code changes</listheader>
    /// <item>
    /// 2026-02-11 alert-fix-13 changed mode from "ECB" to "CFB"     
    /// Reason: Git security scans
    /// consequences: no more fully deterministic math bijective proper symmertric cipher en-/decryption in pipe
    /// fixed attacks: not so easy REPLY attacks with binary format header and heuristic key collection
    /// </item>
    /// <item>
    /// 2026-mm-dd [enter pull request name here] [enter what you did here]
    /// Reason: [enter a senseful reason]
    /// consequences: [describe most impactful consequences of bugfix or code change request]
    /// fixed [vulnerability, code smell]: [Describe understandable precise in 1-2 setences]
    /// </item>
    /// </list>
    /// </remarks>
    public class BlockCipherBC : IBlockCipher
    {
        
        #region fields and properties

        private bool encryption = true, initialised = false;

        private byte[] key = new byte[0], iv = new byte[0];
        
        /// <summary>
        /// Block Size
        /// </summary>
        public int Size { get; private set; }
        
        /// <summary>
        /// CipherParameters is used to store <see cref="KeyParameter"/> or <see cref="ICipherBuilderWithKey"/>
        /// </summary>
        internal ICipherParameters CipherParameters { get; private set; }
        
        /// <summary>
        /// Base symmetric key block cipher interface, contains at runtime block cipher instance to constructor
        /// </summary>
        public IBlockCipher CryptoBlockCipher { get; private set; }

        /// <summary>
        /// BufBlockCiffre is used to imlement several <see cref="CipherMode2"/>
        /// </summary>
        internal BufferedBlockCipher BufBlockCiffre { get; private set; }
        
        /// <summary>
        /// IBlockCipherPadding BlockCipherPadding mode
        /// </summary>
        public IBlockCipherPadding CryptoBlockCipherPadding { get; private set; }

        /// <summary>
        /// Valid modes are currently "CBC", "ECB", "CFB", "CCM", "CTS", "EAX", "GOFB"
        /// <see cref="Org.BouncyCastle.Crypto.Modes"/> for crypto modes details.
        /// </summary>
        public string Mode { get; private set; }

        /// <summary>
        /// CipherMode2
        /// </summary>
        public CipherMode2 CMode { get; private set; }

        #endregion fields and properties
        
        #region IBlockCipher Interface
        
        public void Init(bool forEncryption, ICipherParameters parameters)
        {
            if (!(parameters is KeyParameter) && !(parameters is ParametersWithIV))
                throw new ArgumentException("only KeyParameter or ParametersWithIV expected.", "parameters");
           
            if (parameters is KeyParameter)
            {
                key = ((KeyParameter)parameters).GetKey();
            }
            
            if (parameters is ParametersWithIV)
            {
                if (((ParametersWithIV)parameters).Parameters is KeyParameter)
                    key = ((KeyParameter)((ParametersWithIV)parameters).Parameters).GetKey();
                iv = ((ParametersWithIV)parameters).GetIV();
            }
            
            key = (key == null || key.Length == 0) ? new byte[0] : key;
            iv = (iv == null || iv.Length == 0) ? new byte[0] : iv;
            if (key.Length == 0 && iv.Length == 0)
                throw new ArgumentNullException("parameters",
                    "KeyParameter and/or ParametersWithIV contain a null or empty key or iv.");

            CipherParameters = parameters; 
            encryption = forEncryption;
            
            ICipherParameters keyParam = CryptoBlockCipher.AlgorithmName == "RC564" || CryptoBlockCipher.AlgorithmName == "RC5-64" ?
                new RC5Parameters(key, 2) : // RC5-64 with 2 rounds key initialization
                new KeyParameter(key);      // default KeyParameter
            ICipherParameters keyParamIV = new ParametersWithIV(keyParam, iv);  // KeyParameter with init vector
            
            CipherParameters = keyParamIV;
            
            try
            {
                CryptoBlockCipher.Init(forEncryption, CipherParameters);
            }
            catch (Exception exInit)
            {
                Area23Log.LogOriginMsgEx("CryptBounceCastle",
                    $"CryptBounceCastle CryptoBlockCipher {CryptoBlockCipher.AlgorithmName}: Exceptíon on cipherMode.Init with IV, trying without IV",
                    exInit);
                CryptoBlockCipher.Init(forEncryption, keyParam);
            }
            
            BufBlockCiffre = new PaddedBufferedBlockCipher(new EcbBlockCipher(CryptoBlockCipher), CryptoBlockCipherPadding);
            switch (CMode)
            {
                case CipherMode2.CBC:
                    BufBlockCiffre = new PaddedBufferedBlockCipher(new CbcBlockCipher(CryptoBlockCipher), CryptoBlockCipherPadding);
                    break;
                case CipherMode2.ECB:
                    BufBlockCiffre = new PaddedBufferedBlockCipher(new EcbBlockCipher(CryptoBlockCipher), CryptoBlockCipherPadding);
                    break;
                case CipherMode2.CFB:
                    BufBlockCiffre = new PaddedBufferedBlockCipher(new CfbBlockCipher(CryptoBlockCipher, Size), CryptoBlockCipherPadding);
                    break;
                case CipherMode2.CCM:
                    CcmBlockCipher ccmCipher = new CcmBlockCipher(CryptoBlockCipher);
                    BufBlockCiffre = new PaddedBufferedBlockCipher((IBlockCipher)ccmCipher, CryptoBlockCipherPadding);
                    break;
                case CipherMode2.CTS:
                    CtsBlockCipher ctsCipher = new CtsBlockCipher(CryptoBlockCipher);
                    BufBlockCiffre = ctsCipher;
                    break;
                case CipherMode2.EAX:
                    EaxBlockCipher eaxCipher = new EaxBlockCipher(CryptoBlockCipher);
                    BufBlockCiffre = new PaddedBufferedBlockCipher((IBlockCipher)eaxCipher, CryptoBlockCipherPadding);
                    break;
                case CipherMode2.GOFB:
                    GOfbBlockCipher gOfbCipher = new GOfbBlockCipher(CryptoBlockCipher);
                    BufBlockCiffre = new PaddedBufferedBlockCipher((IBlockCipher)gOfbCipher, CryptoBlockCipherPadding);
                    break;
                default:
                    if (iv.IsNullByteArray())
                    {
                        Mode = "ECB";
                        CMode = CipherMode2.ECB;
                        BufBlockCiffre = new PaddedBufferedBlockCipher(new EcbBlockCipher(CryptoBlockCipher), CryptoBlockCipherPadding);
                    }                     
                    break;
            }
            
            // cipherMode init with initialization vector only when Mode isn't ECB and Algo is IV init capable
            if (CMode == CipherMode2.ECB || !CanAlgoKeyIV(CryptoBlockCipher))
                CipherParameters = keyParam;
            
            try
            {
                BufBlockCiffre.Init(forEncryption, CipherParameters);
            }
            catch (Exception exInit)
            {
                Area23Log.LogOriginMsgEx("CryptBounceCastle",
                    $"CryptBounceCastle {BufBlockCiffre.AlgorithmName}: Exceptíon on cipherMode.Init with IV, trying without IV",
                    exInit);
                CipherParameters = keyParam;
                BufBlockCiffre.Init(forEncryption, CipherParameters);
            }
            
            initialised = true;
        }
        
        

        public int GetBlockSize() => CryptoBlockCipher.GetBlockSize();

        public int ProcessBlock(byte[] inBuf, int inOff, byte[] outBuf, int outOff)
        {
            int len = inBuf.Length + (Size - (inBuf.Length % Size));
            outBuf = new byte[len];
            return CryptoBlockCipher.ProcessBlock(inBuf, inOff, outBuf, outOff);
        }

        public int ProcessBlock(ReadOnlySpan<byte> input, Span<byte> output) => 
            CryptoBlockCipher.ProcessBlock(input, output);

        public string AlgorithmName => CryptoBlockCipher.AlgorithmName;
        
        #endregion IBlockCipher Interface
        
        #region ctor_init_gen

        /// <summary>
        /// parameterless default constructor
        /// </summary>
        public BlockCipherBC()
        {
            CryptoBlockCipher = null;
            CryptoBlockCipherPadding = null;
            CipherParameters = null;
            Size = 256;
            CMode = CipherModeExtensions.defaultCipherMode2;
            Mode = CMode.ToString();
        }

       
        /// <summary>
        /// Generic BlockCipherBC constructor
        /// </summary>
        /// <param name="cparams">parameters to crypt</param>
        /// <param name="init">init <see cref="BlockCipherBC"/> first time with a new key</param>
        public BlockCipherBC(CryptParams cparams, bool init = true)
        {
            CryptoBlockCipher = cparams.BlockCipher == null ? new AesEngine() : cparams.BlockCipher;
            if (CryptoBlockCipher.AlgorithmName == "RC564" || CryptoBlockCipher.AlgorithmName == "RC5-64")
                CryptoBlockCipher = new RC564Engine();
            CryptoBlockCipherPadding = new ZeroBytePadding();
            CipherParameters = cparams.CipherParameters; 
            Size = Math.Min(cparams.Size, CryptoBlockCipher.GetBlockSize());
            
            CMode = cparams.CMode2;
            Mode = CMode.ToString();

            Init(init, CipherParameters);
        }

        #endregion ctor_init_gen


/*
        /// <summary>
        /// GetUserKeyBytes gets symetric chiffer private byte[KeyLen] encryption / decryption key
        /// </summary>
        /// <param name="secretKey">user secret key, default email address</param>
        /// <param name="secretHash">user host ip address</param>
        /// <returns>Array of byte with length KeyLen</returns>
        internal byte[] GetUserKeyBytes(string secretKey, string secretHash)
        {
            if (!string.IsNullOrEmpty(secretKey))
                throw new ArgumentNullException(secretKey);
            
            privateKey = secretKey;
            privateHash = string.IsNullOrEmpty(secretHash) ? "" : secretHash;

            tmpKey = new byte[KeyLen];
            tmpIv = new byte[IvLen];
            for (int i = 0; i < KeyLen; i++)
            {
                tmpKey[i] = 0;
                if (i < IvLen)
                    tmpIv[i] = 0;
            }

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(privateKey);
            Array.Copy(bytes, 0, tmpKey, 0, Math.Min(bytes.Length, KeyLen));
            
            bytes = System.Text.Encoding.UTF8.GetBytes(privateHash);
            Array.Copy(bytes, 0, tmpIv, 0, Math.Min(bytes.Length, IvLen));

            return tmpKey.TarBytes(tmpIv);
        }
*/

        /// <summary>
        /// can this SymmBlockCipherAlgo key with initialization vector?
        /// </summary>
        /// <returns>true, if it hanldes init with key and initialization vector iv, otherwise false</returns>
        protected bool CanAlgoKeyIV(IBlockCipher cryptoBlockCipher = null)
        {
            if (cryptoBlockCipher == null)
                cryptoBlockCipher = CryptoBlockCipher;
            string algoName = cryptoBlockCipher.AlgorithmName.ToUpper();
            if (algoName.StartsWith("AES", StringComparison.CurrentCultureIgnoreCase) || algoName.StartsWith("RIJNDAEL", StringComparison.CurrentCultureIgnoreCase))
                return false;
            if (algoName.StartsWith("ARIA", StringComparison.CurrentCultureIgnoreCase) || algoName.StartsWith("ASCON", StringComparison.CurrentCultureIgnoreCase))
                return false;
            if (algoName.StartsWith("CAST", StringComparison.CurrentCultureIgnoreCase) || algoName.StartsWith("GOST", StringComparison.CurrentCultureIgnoreCase))
                return false;
            if (algoName.StartsWith("IDEA", StringComparison.CurrentCultureIgnoreCase) || algoName.StartsWith("RC", StringComparison.CurrentCultureIgnoreCase)) 
                return false;
            if (algoName.StartsWith("SKIPJACK", StringComparison.CurrentCultureIgnoreCase) || algoName.Equals("DESEDE"))
                return false;
            if (algoName.StartsWith("TEA", StringComparison.CurrentCultureIgnoreCase) || algoName.StartsWith("XTEA", StringComparison.CurrentCultureIgnoreCase))
                return false;
            return true;
        }

        #region EncryptDecryptBytes

        /// <summary>
        /// Generic CryptBounceCastle Encrypt member function
        /// difference between out parameter encryptedData and return value, are 2 different encryption methods, but with the same result at the end
        /// </summary>
        /// <param name="plainData">plain data as <see cref="T:byte[]"/></param>
        /// <returns>encrypted data <see cref="T:byte[]">bytes</see></returns>
        public byte[] Encrypt(byte[] plainData)
        {
            plainData = CryptoBlockCipher.AlgorithmName == "RC564" || CryptoBlockCipher.AlgorithmName == "RC5-64" ? 
                EnDeCodeHelper.GetBytesFromBytes(plainData) : plainData;
            
            int outputSize = BufBlockCiffre.GetOutputSize(plainData.Length);
            byte[] cipherData = new byte[outputSize];
            int result = BufBlockCiffre.ProcessBytes(plainData, 0, plainData.Length, cipherData, 0);
            BufBlockCiffre.DoFinal(cipherData, result);

            return cipherData;
        }

        /// <summary>
        /// Generic CryptBounceCastle Decrypt member function
        /// difference between out parameter decryptedData and return value, are 2 different decryption methods, but with the same result at the end
        /// </summary>
        /// <param name="cipherData">encrypted <see cref="T:byte[]">bytes</see></param>
        /// <returns>decrypted plain byte[] data</returns>
        public byte[] Decrypt(byte[] cipherData)
        {
            int outputSize = BufBlockCiffre.GetOutputSize(cipherData.Length);
            byte[] plainData = new byte[outputSize];
            byte[] decryptedData = new byte[outputSize];
            try
            {
                int result = BufBlockCiffre.ProcessBytes(cipherData, 0, cipherData.Length, plainData, 0);
                BufBlockCiffre.DoFinal(plainData, result);
            }
            catch (Exception exDecrypt)
            {
                Area23Log.LogOriginMsgEx("CryptBounceCastle", $"CryptBounceCastle {BufBlockCiffre.AlgorithmName}: Exceptíon on decrypting final block", exDecrypt);
                try
                {
                    plainData = new byte[outputSize];
                    plainData = BufBlockCiffre.ProcessBytes(cipherData, 0, cipherData.Length);
                }
                catch (Exception exDecrypt2)
                {
                    Area23Log.LogOriginMsgEx("CryptBounceCastle", $"CryptBounceCastle {BufBlockCiffre.AlgorithmName}: Exceptíon on 2x decrypting final block", exDecrypt2);
                    plainData = new byte[outputSize];
                    plainData = BufBlockCiffre.ProcessBytes(cipherData);
                }
            }

            return CryptoBlockCipher.AlgorithmName == "RC564" || CryptoBlockCipher.AlgorithmName == "RC5-64" ? 
                EnDeCodeHelper.GetBytesTrimNulls(plainData) : plainData;

            // return plainData;
        }

        #endregion EncryptDecryptBytes

        #region EnDecryptString

        /// <summary>
        /// Generic CryptBounceCastle Encrypt String method
        /// </summary>
        /// <param name="inString">plain string to encrypt</param>
        /// <param name="encodingType">
        /// beware of using <see cref="EncodingType.Uu"/>; 
        /// default <see cref="EncodingType.Base64"/>
        /// </param>
        /// <returns>encoded encrypted string, default base64 encoded</returns>
        public string EncryptString(string inString, EncodingType encodingType = EncodingType.Base64)
        {
            byte[] plainTextData = EnDeCodeHelper.GetBytes(inString);
            byte[] encryptedBytes = Encrypt(plainTextData);
            string encryptedString = EnDeCodeHelper.EncodeBytes(encryptedBytes, encodingType);

            return encryptedString;
        }

        /// <summary>
        /// Generic CryptBounceCastle Decrypt String method
        /// </summary>
        /// <param name="inCryptString">encoded encrypted string, default base64 encoded</param>
        /// <param name="encodingType">
        /// beware of using <see cref="EncodingType.Uu"/>; 
        /// default <see cref="EncodingType.Base64"/>
        /// </param>
        /// <returns>plain text decrypted string</returns>
        public string DecryptString(string inCryptString, EncodingType encodingType = EncodingType.Base64)
        {
            byte[] cipherBytes = EnDeCodeHelper.DecodeText(inCryptString, encodingType);
            byte[] plainData = Decrypt(cipherBytes);
            string plainTextString = EnDeCodeHelper.GetString(plainData).TrimEnd('\0');

            return plainTextString;
        }

        #endregion EnDecryptString

    }

}
