using EU.CqrXs.Crypt.Cipher.Symmetric;
using EU.CqrXs.Crypt.EnDeCoding;
using EU.CqrXs.Crypt.Hash;
using EU.CqrXs.Util;
using EU.CqrXs.Zip;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using System.Text;

namespace EU.CqrXs.Crypt.Cipher
{

    /// <summary>
    /// Provides a simple asymmetric crypt pipe for <see cref="CipherEnum"/>
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
    /// 2026-03-02 alert changed mode back from  "CFB" to "ECB"
    /// Reason: Ugly bug in following pipeline SM4 => SkipJack => Serpent => Seed => Fish3 => Des3 in CFB mode
    /// consequences:  more fully deterministic math bijective again proper symmertric cipher en-/decryption in pipe
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
    public class AsymmetricCipherPipe : CipherPipe
    {

        #region fields and properties

        protected string asymCipherPublicKey = "";
        protected string asymCipherPrivateKey = "";

        public AsymmetricCipherKeyPair AsymKeyPair { get; set; }

        public byte[] signatureBytes = (new List<byte>()).ToArray();

        public enum AsymmetricCipherEnum
        {
            Rsa,
            Dsa,
            DH, 
            Ecdsa,
            Ed25519,
            Ed448,
            X25519,
            X448,
            GPG
        }
        public AsymmetricCipherEnum AsymmetricCipherAlgo { get; set; } = AsymmetricCipherEnum.Rsa;

        public new string PipeFullExtension
        {
            get
            {
                string miniPipe = (InPipe == null || InPipe.Length == 0) ? "" : "." + CMode2.ToString() + "." + PipeString;
                    // (InPipe == null || InPipe.Length == 0) ? "" : "." + PipeString;
                string miniPipeExt = zType.GetZipTypeExtension() + miniPipe + encodeType.GetEnCodingExtension();
                return miniPipeExt;
            }
        }


        #endregion fields and properties

        #region ctor AsymmetricCipherPipe

        /// <summary>
        /// parameterless default constructor for <see cref="AsymmetricCipherPipe"/>
        /// </summary>
        public AsymmetricCipherPipe()
        {
            asymCipherPublicKey = "";
            asymCipherPrivateKey = "";
            AsymmetricCipherAlgo = AsymmetricCipherEnum.Rsa;
            inPipe = (new List<CipherEnum>()).ToArray();
            encodeType = EncodingType.Base64;
            zType = ZipType.GZip;
            CMode2 = CiffreMode.defaultCipherMode2;
        }


        /// <summary>
        /// AsymmetricCipherPipe constructor with an array of <see cref="T:CipherEnum[]"/> as inpipe
        /// </summary>
        /// <param name="cipherEnums">array of <see cref="T:CipherEnum[]"/> as inpipe</param>
        /// <param name="maxpipe">size of max. pipe stages, can't be greater than <see cref="Constants.PIPE_MAX_LEN"/></param>
        /// <param name="cmode2"><see cref="CipherMode2"/></param>
        public AsymmetricCipherPipe(CipherEnum[] cipherEnums, uint maxpipe, CipherMode2 cmode2)
        {
            // What ever is entered here as parameter, maxpipe has to be not greater Constants.PIPE_MAX_LEN, because of no such agency
            maxpipe = (maxpipe > Constants.PIPE_MAX_LEN) ? Constants.PIPE_MAX_LEN : maxpipe; // if somebody wants more, he/she/it gets less

            int isize = Math.Min(((int)cipherEnums.Length), ((int)maxpipe));
            inPipe = new CipherEnum[isize];
            Array.Copy(cipherEnums, inPipe, isize);

            if (cipherEnums.Length > 0)
            {
                this.AsymmetricCipherAlgo = Enum.TryParse<AsymmetricCipherEnum>(cipherEnums[0].ToString(), true, out AsymmetricCipherEnum asymCipher) ? asymCipher
                    : AsymmetricCipherEnum.Rsa;
            }

            encodeType = EncodingType.Base64;
            zType = ZipType.GZip;
            CMode2 = cmode2;
        }

        /// <summary>
        /// SecureCipherPipe constructor with an array of <see cref="T:string[]"/> cipherAlgos as inpipe
        /// </summary>
        /// <param name="cipherAlgos">array of <see cref="T:string[]"/> as inpipe</param>
        /// <param name="maxpipe">maximum lentgh <see cref="Constants.PIPE_MAX_LEN"/></param>
        /// <param name="cmode2"><see cref="CipherMode2"/></param>
        public AsymmetricCipherPipe(string[] cipherAlgos, uint maxpipe, CipherMode2 cmode2)
        {
            // What ever is entered here as parameter, maxpipe has to be not greater Constants.PIPE_MAX_LEN, because of no such agency
            maxpipe = (maxpipe > Constants.PIPE_MAX_LEN) ? Constants.PIPE_MAX_LEN : maxpipe; // if somebody wants more, he/she/it gets less

            List<CipherEnum> cipherEnums = new List<CipherEnum>();
            int cnt = 0;
            foreach (string algo in cipherAlgos)
            {
                if (!string.IsNullOrEmpty(algo))
                {
                    CipherEnum cipherAlgo = CipherEnum.Aes;
                    if (!Enum.TryParse<CipherEnum>(algo, out cipherAlgo))
                        cipherAlgo = CipherEnum.Aes;

                    cipherEnums.Add(cipherAlgo);

                    if ((cipherEnums.Count > (maxpipe - 1)) || ++cnt > maxpipe)
                        break;
                }
            }

            int pipeSize = Math.Min(cipherEnums.Count, Constants.PIPE_MAX_LEN);
            inPipe = new CipherEnum[pipeSize];
            Array.Copy(cipherEnums.ToArray(), inPipe, pipeSize);

            if (cipherEnums.ToArray().Length > 0)
            {
                this.AsymmetricCipherAlgo = Enum.TryParse<AsymmetricCipherEnum>(cipherEnums.ToArray()[0].ToString(), true, out AsymmetricCipherEnum asymCipher) ? asymCipher
                    : AsymmetricCipherEnum.Rsa;
            }

            zType = ZipType.GZip;
            encodeType = EncodingType.Base64;
            CMode2 = cmode2;
        }

        /// <summary>
        /// AsymmetricCipherPipe ctor with array of user key bytes
        /// </summary>
        /// <param name="keyBytes">user key bytes</param>
        /// <param name="maxpipe">maximum lentgh <see cref="Constants.PIPE_MAX_LEN"/></param>        
        /// <param name="cmode2"><see cref="CipherMode2"/></param>
        /// <param name="verbose"></param>
        /// <exception cref="ArgumentException"></exception>
        public AsymmetricCipherPipe(byte[] keyBytes, uint maxpipe, CipherMode2 cmode2, bool verbose = false)
        {
            // What ever is entered here as parameter, maxpipe has to be not greater Constants.PIPE_MAX_LEN, because of no such agency
            maxpipe = (maxpipe > Constants.PIPE_MAX_LEN) ? Constants.PIPE_MAX_LEN : maxpipe; // if somebody wants more, he/she/it gets less

            List<CipherEnum> pipeList = new List<CipherEnum>();

            HashSet<byte> hashBytes = new HashSet<byte>();
            for (int i = 0, j = 0; i < keyBytes.Length && j < maxpipe && pipeList.Count < maxpipe; i++)
            {
                byte cb = (byte)((int)((int)keyBytes[i] % 0x1d));
                bool addCipherToPUpe = (Constants.PIPE_BUILD_MULTI_SAME_CIPHERS || (maxpipe < 0x16 && !hashBytes.Contains(cb)));
                if (addCipherToPUpe && pipeList.Count < maxpipe + 1)
                {
                    if (!hashBytes.Contains(cb)) // TODO: future design
                    {                        
                        // // mit magic add to generate deterministic more on same bytes
                        // cb = (byte)((int)(cb + Math.Pow(2, i) + keyBytes.Length) % 0x1d);                
                        hashBytes.Add(cb);
                    }
                    
                    hashBytes.Add(cb);
                    CipherEnum cipherEnm = CipherEnumExtensions.ByteCipherDict[cb];
                    pipeList.Add(cipherEnm);

                    if (verbose)
                        Console.Out.WriteLine("keybyts[" + i + "]=" + keyBytes[i] + " byte cb = " + (int)cb + " CipherEnum: " + cipherEnm);
                    j++;
                }
            }

            int pipeSize = Math.Min(pipeList.Count, Constants.PIPE_MAX_LEN);
            inPipe = new CipherEnum[pipeSize];
            Array.Copy(pipeList.ToArray(), inPipe, pipeSize);

            if (pipeList.ToArray().Length > 0)
            {
                this.AsymmetricCipherAlgo = Enum.TryParse<AsymmetricCipherEnum>(pipeList.ToArray()[0].ToString(), true, out AsymmetricCipherEnum asymCipher) ? asymCipher
                    : AsymmetricCipherEnum.Rsa;
            }

            zType = ZipType.GZip;
            encodeType = EncodingType.Base64;
            CMode2 = cmode2;

        }

        /// <summary>
        /// Constructs a <see cref="AsymmetricCipherPipe"/> from key and hash
        /// by getting <see cref="T:byte[]">byte[] keybytes</see> with <see cref="CryptHelper.GetUserKeyBytes(string, string, int)"/>
        /// </summary>
        /// <param name="keyHash">secret key to generate pipe</param>
        /// <param name="cmode2"><see cref="CipherMode2"/></param>
        /// <param name="verbose"></param>
        public AsymmetricCipherPipe(string publicKey, string privateKey, CipherMode2 cmode2, bool verbose = false)
            : this(CryptHelper.GetKeyBytesSingle(publicKey + "\r\n" + privateKey, Constants.PIPE_KEY_HASH_LEN), Constants.PIPE_MAX_LEN, cmode2, verbose)
        {
            asymCipherPublicKey = publicKey;
            asymCipherPrivateKey = privateKey;
            cipherKey = publicKey + "\r\n" + privateKey;
            cipherHash = "";            
        }

        /// <summary>
        /// AsymmetricCipherPipe ctor with only key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="verbose"></param>
        public AsymmetricCipherPipe(string publicKey, string privateKey, bool verbose = false)
            : this(publicKey, privateKey, CiffreMode.defaultCipherMode2, verbose)
        {
            asymCipherPublicKey = publicKey;
            asymCipherPrivateKey = privateKey;
            cipherKey = publicKey + "\r\n" + privateKey;
            cipherHash = "";
        }

        public AsymmetricCipherPipe(CipherPipe ciphPipe) : this()
        {
            if (ciphPipe != null)
            {
                this.inPipe = ciphPipe.InPipe;
                if (inPipe.Length > 0)
                {
                    this.AsymmetricCipherAlgo = Enum.TryParse<AsymmetricCipherEnum>(inPipe[0].ToString(), true, out AsymmetricCipherEnum asymCipher) ? asymCipher
                        : AsymmetricCipherEnum.Rsa;
                }                
                this.cipherKey = ciphPipe.cipherKey;
                this.asymCipherPublicKey = ciphPipe.cipherKey;
                this.cipherHash = ciphPipe.cipherHash;
                this.asymCipherPrivateKey = ciphPipe.cipherHash;
                this.CMode = ciphPipe.CMode;
                this.CMode2 = ciphPipe.CMode2;
                this.encodeType = ciphPipe.EncodeType; 
                this.zType = ciphPipe.ZType; 
            }
        }

        public AsymmetricCipherPipe(AsymmetricCipherPipe aCiphPipe) : this()
        {
            if (aCiphPipe != null)
            {
                this.inPipe = aCiphPipe.InPipe;
                this.AsymmetricCipherAlgo = aCiphPipe.AsymmetricCipherAlgo;
                if (inPipe.Length > 0)
                {
                    this.AsymmetricCipherAlgo = Enum.TryParse<AsymmetricCipherEnum>(inPipe[0].ToString(), true, out AsymmetricCipherEnum asymCipher) ? asymCipher
                        : AsymmetricCipherEnum.Rsa;
                }
                this.cipherKey = aCiphPipe.cipherKey;
                this.asymCipherPublicKey = aCiphPipe.asymCipherPublicKey;
                this.cipherHash = aCiphPipe.cipherHash;
                this.asymCipherPrivateKey = aCiphPipe.asymCipherPrivateKey;
                if (aCiphPipe.AsymKeyPair != null)
                {                  
                    this.AsymKeyPair = aCiphPipe.AsymKeyPair;
                }
                this.CMode = aCiphPipe.CMode;
                this.CMode2 = aCiphPipe.CMode2;
                this.encodeType = aCiphPipe.EncodeType; 
                this.zType = aCiphPipe.ZType; 
            }
        }

        #endregion ctor AsymmetricCipherPipe

        #region json

        /// <summary>
        /// ToJson 
        /// </summary>
        /// <returns>serialized string</returns>
        public override string ToJson() => JsonConvert.SerializeObject(this, Formatting.Indented);

        /// <summary>
        /// FromJson
        /// </summary>
        /// <param name="json">serialized json</param>
        /// <returns><see cref="SecureCipherPipe"/></returns>
        public new AsymmetricCipherPipe FromJson(string json)
        {
            AsymmetricCipherPipe pipe = JsonConvert.DeserializeObject<AsymmetricCipherPipe>(json);
            if (pipe != null)
            {
                this.inPipe = pipe.InPipe;
                this.AsymmetricCipherAlgo = pipe.AsymmetricCipherAlgo;                
                if (inPipe.Length > 0)
                {
                    this.AsymmetricCipherAlgo = Enum.TryParse<AsymmetricCipherEnum>(inPipe[0].ToString(), true, out AsymmetricCipherEnum asymCipher) ? asymCipher
                        : AsymmetricCipherEnum.Rsa;
                }
                this.encodeType = pipe.EncodeType;
                this.zType = pipe.ZType;
                this.asymCipherPublicKey = pipe.asymCipherPublicKey;
                this.asymCipherPrivateKey = pipe.asymCipherPrivateKey;
                this.cipherKey = pipe.cipherKey;
                this.cipherKey = pipe.cipherHash;
                if (pipe.AsymKeyPair != null)
                {

                    this.AsymKeyPair = pipe.AsymKeyPair;
                }
                this.CMode2 = pipe.CMode2;
            }
            return pipe;
        }

        #endregion json

        #region static members EncryptBytesFast DecryptBytesFast

        /// <summary>
        /// Generic encrypt bytes to bytes
        /// </summary>
        /// <param name="inBytes">Array of byte</param>
        /// <param name="cipherAlgo"><see cref="CipherEnum"/> both symmetric and asymetric cipher algorithms</param>
        /// <param name="secretKey">secret key to decrypt</param>
        /// <param name="cmode2"></param>
        /// <returns>encrypted byte Array</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static byte[] EncryptBytesFast(byte[] inBytes, CipherEnum cipherAlgo,
            string publicKey, string privateKey, CipherMode2 cmode2)
        {
            if (string.IsNullOrEmpty(publicKey))
                throw new ArgumentNullException("publicKey");

            CryptParams cpParams = new CryptParams(cipherAlgo, publicKey, privateKey) { CMode2 = cmode2 };
            byte[] encryptBytes = inBytes;
            AsymmetricCipherKeyPair keyPair; 

            switch (cipherAlgo)
            {
                case CipherEnum.Rsa:
                    keyPair = Asymmetric.Rsa.RsaGenWithKey(publicKey, privateKey);
                    encryptBytes = Asymmetric.Rsa.Encrypt(inBytes, keyPair);
                    break;
                case CipherEnum.Dsa:
                    keyPair = Asymmetric.Dsa.GetDsaKeyPairByKeys(privateKey, publicKey);
                    encryptBytes = Asymmetric.Dsa.DsaSign(inBytes);
                    break;
                case CipherEnum.DH:
                case CipherEnum.GPG:
                default:
                    encryptBytes = inBytes;
                    break;                 
            }

            return encryptBytes;
        }

        /// <summary>
        /// Generic decrypt bytes to bytes
        /// </summary>
        /// <param name="cipherBytes">Encrypted array of byte</param>
        /// <param name="cipherAlgo"><see cref="CipherEnum"/>both symmetric and asymetric cipher algorithms</param>
        /// <param name="secretKey">secret key to decrypt</param>
        /// <returns>decrypted byte Array</returns>
        public static byte[] DecryptBytesFast(byte[] cipherBytes, CipherEnum cipherAlgo,
            string publicKey, string privateKey, byte[] signedBytes, CipherMode2 cmode2)
        {
            if (string.IsNullOrEmpty(publicKey))
                throw new ArgumentNullException("publicKey");

            // bool sameKey = true;
            CryptParams cpParams = new CryptParams(cipherAlgo, publicKey, privateKey) { CMode2 = cmode2 };
            byte[] decryptBytes = cipherBytes;
            AsymmetricCipherKeyPair keyPair;
            switch (cipherAlgo)
            {               
                case CipherEnum.Rsa:
                    keyPair = Asymmetric.Rsa.RsaGenWithKey(publicKey, privateKey);
                    decryptBytes = Asymmetric.Rsa.Decrypt(cipherBytes, keyPair);
                    break;
                case CipherEnum.Dsa:                    
                    keyPair = Asymmetric.Dsa.GetDsaKeyPairByKeys(privateKey, publicKey);
                    if (!Asymmetric.Dsa.DsaVerify(cipherBytes, signedBytes))
                        throw new InvalidOperationException("Signature verification failed.");
                    decryptBytes = cipherBytes;
                    break;
                default:
                    decryptBytes = cipherBytes;
                    break;
            }


            return EnDeCodeHelper.GetBytesTrimNulls(decryptBytes);
        }



        /// <summary>
        /// EncrpytT
        /// </summary>
        /// <typeparam name="TRet">
        ///     <see cref="T:string"/>
        ///     <see cref="T:char[]"/>  <see cref="T:IEnumerable{char}"/>
        ///     <see cref="T:bytes[]"/> <see cref="T:IEnumerable{byte}"/>
        /// </typeparam>
        /// <typeparam name="TIn">
        ///     <see cref="T:string"/>
        ///     <see cref="T:char[]"/>  <see cref="T:IEnumerable{char}"/>
        ///     <see cref="T:bytes[]"/> <see cref="T:IEnumerable{byte}"/>
        /// </typeparam>
        /// <param name="tinSource">plain string, char[], byte[], IEnumerable{char}, IEnumerable{bytes}</param>
        /// <param name="cryptKey">Unique deterministic key for either generating the mix of symmetric cipher algorithms in the crypt pipeline 
        /// and unique crypt key for each symmetric cipher algorithm in each stage of the pipe</param>
        /// <param name="cmode2"></param>
        /// <returns>encrypted generic type</returns>
        /// <exception cref="CException">is thrown on unknown type</exception>
        public static TRet EncrpytT<TRet, TIn>(TIn tinSource, string cryptKey, string privKey, CipherMode2 cmode2)
        {
            byte[] stringBytes = new List<byte>().ToArray();
            // construct symmetric cipher pipeline with cryptKey and cmode2
            AsymmetricCipherPipe cipherPipe = new AsymmetricCipherPipe(cryptKey, privKey, cmode2, false);

            if (tinSource is string inString)   // Transform string to bytes
                stringBytes = Encoding.UTF8.GetBytes(inString);
            else if (tinSource is char[] chars)
                stringBytes = Encoding.UTF8.GetBytes(new string(chars));
            else if (tinSource is IEnumerable<char> charsIEnumerable)
                stringBytes = Encoding.UTF8.GetBytes(new string(charsIEnumerable.ToArray()));
            else if (tinSource is byte[] inBytes)
                stringBytes = inBytes;
            else if (tinSource is IEnumerable<byte> bytesEnumerable)
                stringBytes = bytesEnumerable.ToArray();
            else throw new CException($"Unknown type Exception, type {typeof(TIn)} is not supported.");

            // zip GZ
            byte[] zippedBytes = ZipType.GZip.Zip(stringBytes);
            // encrypt in a marry go round way
            byte[] encryptedBytes = cipherPipe.MerryAsymGoRoundEncrpyt(zippedBytes, cryptKey, privKey, cmode2);
            // encode after encryption pipe
            String encryptedString = EncodingType.Base64.GetEnCoder().Encode(encryptedBytes);

            TRet result = default(TRet);
            if (typeof(TRet) == typeof(string))
                result = (TRet)(object)encryptedString;
            else if (typeof(TRet) == typeof(char[]))
                result = (TRet)(object)encryptedString.ToCharArray();
            else if (typeof(TRet) == typeof(IEnumerable<char>))
                result = (TRet)(object)encryptedString.ToCharArray();
            else if (typeof(TRet) == typeof(byte[]))
                result = (TRet)(object)System.Text.Encoding.UTF8.GetBytes(encryptedString);
            else if (typeof(TRet) == typeof(IEnumerable<byte>))
                result = (TRet)(object)System.Text.Encoding.UTF8.GetBytes(encryptedString);
            else throw new CException($"Unknown type Exception, type {typeof(TRet)} is not supported.");

            return result;
        }

        /// <summary>
        ///  DecrpytT generic decryption method
        /// </summary>
        /// <typeparam name="TRet">return type 
        ///     <see cref="T:string"/>
        ///     <see cref="T:char[]"/>  <see cref="T:IEnumerable{char}"/>
        ///     <see cref="T:bytes[]"/> <see cref="T:IEnumerable{byte}"/>
        /// </typeparam>
        /// <typeparam name="TIn"></typeparam>
        /// <param name="tinSource">encrypted message</param>
        /// <param name="cryptKey">Unique deterministic key for either generating the mix of symmetric cipher algorithms in the crypt pipeline 
        /// and unique crypt key for each symmetric cipher algorithm in each stage of the pipe</param>
        /// <param name="cmode2"></param>
        /// <returns>Decrypted generic TRet</returns>
        /// <exception cref="CException">is thrown on unknown type</exception>
        public static TRet DecrpytT<TRet, TIn>(TIn tinSource, string cryptKey, string privKey, CipherMode2 cmode2)
        {
            byte[] stringBytes = new List<byte>().ToArray();
            // create symmetric cipher pipe for decryption with crypt key and pass pipeString as out param
            AsymmetricCipherPipe cPipe = new AsymmetricCipherPipe(cryptKey, privKey, cmode2, false);
            string pipeString = cPipe.PipeString;
            string incomingEncoded = string.Empty;

            if (tinSource is string inString)
                incomingEncoded = inString;
            else if (tinSource is char[] chars)
                incomingEncoded = chars.ToString();
            else if (tinSource is IEnumerable<char> charsIEnumerable)
                incomingEncoded = new string(charsIEnumerable.ToArray());
            else if (tinSource is byte[] inBytes)
                incomingEncoded = System.Text.Encoding.UTF8.GetString(inBytes);
            else if (tinSource is IEnumerable<byte> bytesEnumerable)
                incomingEncoded = System.Text.Encoding.UTF8.GetString(bytesEnumerable.ToArray());
            else throw new CException($"Unknown type Exception, type {typeof(TIn)} is not supported.");

            // get bytes from encrypted encoded string dependent on the encoding type (uu, base64, base32,..)
            byte[] cipherBytes = EncodingType.Base64.GetEnCoder().Decode(incomingEncoded);
            // staged decryption of bytes
            byte[] intermediatBytes = cPipe.DecrpytRoundGoMerryAsym(cipherBytes, cryptKey, privKey, cmode2);
            // Unzip after if necessary
            byte[] decryptedBytes = ZipType.GZip.Unzip(intermediatBytes);

            TRet result = default(TRet);
            if (typeof(TRet) == typeof(string))
                result = (TRet)(object)System.Text.Encoding.UTF8.GetString(decryptedBytes);
            else if (typeof(TRet) == typeof(char[]))
                result = (TRet)(object)System.Text.Encoding.UTF8.GetString(decryptedBytes).ToCharArray();
            else if (result is IEnumerable<char> charsEnumerable)
                result = (TRet)(object)System.Text.Encoding.UTF8.GetString(decryptedBytes).ToCharArray();
            else if (typeof(TRet) == typeof(byte[]))
                result = (TRet)(object)decryptedBytes;
            else if (result is IEnumerable<byte> bytesIEnumerable)
                result = (TRet)(object)decryptedBytes;
            else throw new CException($"Unknown type Exception, type {typeof(TRet)} is not supported.");

            return result;
        }


        #endregion static members EncryptBytesFast DecryptBytesFast

        #region multiple rounds en-de-cryption

        /// <summary>
        /// MerryGoRoundEncrpyt starts merry to go arround from left to right in clock hour cycle
        /// </summary>
        /// <param name="inBytes">plain <see cref="T:byte[]"/> to encrypt</param>
        /// <param name="secretKey">user secret key to use for all symmetric cipher algorithms in the pipe</param>
        /// <param name="cmode2"><see cref="CipherMode2"/></param>
        /// <returns>encrypted byte[]</returns>
        public virtual byte[] MerryAsymGoRoundEncrpyt(byte[] inBytes, string publicKey, string privateKey, CipherMode2 cmode2)
        {
            if (InPipe == null || inPipe.Length == 0)   // return immideate, when zero round cipher merry go round
                return inBytes;
           
            CMode2 = cmode2;
  
            byte[] encryptedBytes = new byte[inBytes.Length];
            foreach (CipherEnum cipher in InPipe)
            {               
                encryptedBytes = EncryptBytesFast(inBytes, cipher, publicKey, privateKey, CMode2);
                inBytes = encryptedBytes;
            }

            return encryptedBytes;
        }

        /// <summary>
        /// DecrpytRoundGoMerry against clock turn -
        /// starts merry to turn arround from right to left against clock hour cycle 
        /// </summary>
        /// <param name="cipherBytes">encrypted byte array</param>
        /// <param name="secretKey">user secret key, normally email address</param>
        /// <param name="cmode2"><see cref="CipherMode2"/></param>
        /// <returns><see cref="T:byte[]"/> plain bytes</returns>
        public virtual byte[] DecrpytRoundGoMerryAsym(byte[] cipherBytes, string publicKey, string privateKey, CipherMode2 cmode2)
        {
            if (OutPipe == null || OutPipe.Length == 0) // when 0 rounds carusell, return immideate inBytes
                return cipherBytes;            
            CMode2 = cmode2;
            

            byte[] decryptedBytes = new byte[cipherBytes.Length];
            foreach (CipherEnum cipher in OutPipe)
            {                
                decryptedBytes = DecryptBytesFast(cipherBytes, cipher, publicKey, privateKey, this.signatureBytes, cmode2);
                cipherBytes = decryptedBytes;
            }

            return decryptedBytes;
        }


        /// <summary>
        /// EncrpytTextGoRounds encrypts text with cipher pipe pipeline
        /// </summary>
        /// <param name="inString">plain text to encrypt</param>
        /// <param name="cryptKey">prviate key for encryption</param>
        /// <param name="cmode2"></param>
        /// <returns>UTF9 emcoded encrypted string without binary data</returns>
        public virtual string EncrpytTextGoRounds(string inString, string publicKey, string privateKey, CipherMode2 cmode2)
        {
            
            // Transform string to bytes
            // byte[] inBytes = EnDeCodeHelper.GetBytesFromString(inString);
            byte[] inBytes = System.Text.Encoding.UTF8.GetBytes(inString);
            // zip if requested
            byte[] zippedBytes = (ZType != ZipType.None) ? ZType.Zip(inBytes) : inBytes;

            // now encrypt with pipe
            byte[] encryptedBytes = MerryAsymGoRoundEncrpyt(zippedBytes, publicKey, privateKey, CMode2);

            // Encode pipes by encodingType, e.g. base64, uu, hex16, ...
            string encrypted = encodeType.GetEnCoder().Encode(encryptedBytes);

            return encrypted;
        }


        /// <summary>
        /// decrypt encoded encrypted text
        /// </summary>
        /// <param name="cryptedEncodedMsg">encoded encrypted ASCII string</param>
        /// <param name="cryptKey">prviate key for encryption</param>
        /// <param name="cmode2"></param>
        /// <returns>decrypted UTF8 string, containing no binary data</returns>
        public virtual string DecryptTextRoundsGo(string cryptedEncodedMsg, string publicKey, string privateKey, CipherMode2 cmode2)
        {
            
            // Decoded encoded bytes first, if necessary
            byte[] cipherBytes = (encodeType != EncodingType.None) ?
                encodeType.GetEnCoder().Decode(cryptedEncodedMsg) :
                System.Text.Encoding.UTF8.GetBytes(cryptedEncodedMsg);


            // perform multi crypt pipe stages
            byte[] intermediatBytes = DecrpytRoundGoMerryAsym(cipherBytes, publicKey, privateKey, CMode2);
            // Unzip after all, if it's necessary
            byte[] decryptedBytes = (ZType != ZipType.None) ? ZType.Unzip(intermediatBytes) : intermediatBytes;

            string decrypted = System.Text.Encoding.UTF8.GetString(decryptedBytes);

            // find first \0 = NULL char in string and truncate all after first \0 apperance in string
            // while (decrypted[decrypted.Length - 1] == '\0')
            //    decrypted = decrypted.Substring(0, decrypted.Length - 1);

            return decrypted;
        }


        public virtual byte[] EncrpytGoRounds(byte[] inBytes, string publicKey, string privateKey, CipherMode2 cmode2)
        {
            CMode2 = cmode2;

            // zip if requested
            byte[] zippedBytes = zType.Zip(inBytes);
            // encrypt in a marry go round way
            return MerryAsymGoRoundEncrpyt(zippedBytes, publicKey, privateKey, cmode2);
        }


        public virtual byte[] DecrpytRoundsGo(byte[] cipherBytes, string publicKey, string privateKey, CipherMode2 cmode2)
        {
            CMode2 = cmode2;

            // perform multi crypt pipe stages
            byte[] intermediatBytes = DecrpytRoundGoMerryAsym(cipherBytes, publicKey, privateKey, cmode2);
            // Unzip after if necessary
            byte[] decryptedBytes = ZType.Unzip(intermediatBytes);

            return decryptedBytes;
        }


        public virtual byte[] EncryptEncodeBytes(byte[] inBytes, string publicKey, string privateKey, CipherMode2 cmode2)
        {
            CMode2 = cmode2;

            // zip if requested
            byte[] zippedBytes = (ZType != ZipType.None) ? ZType.Zip(inBytes) : inBytes;
            // now encrypt with pipe
            byte[] outBytes = MerryAsymGoRoundEncrpyt(zippedBytes, publicKey, privateKey, CMode2);
            // encode after encryption pipe
            if (encodeType == EncodingType.None)
                return outBytes;

            return System.Text.Encoding.UTF8.GetBytes(encodeType.GetEnCoder().Encode(outBytes));
        }

        public virtual byte[] DecodeDecrpytBytes(byte[] encodedBytes, string publicKey, string privateKey, CipherMode2 cmode2)
        {
            CMode2 = cmode2;

            // Decoded encoded bytes first, if necessary
            byte[] cipherBytes = (encodeType != EncodingType.None) ?
                encodeType.GetEnCoder().Decode(System.Text.Encoding.UTF8.GetString(encodedBytes)) :
                encodedBytes;
            // perform multi crypt pipe stages
            byte[] intermediatBytes = DecrpytRoundGoMerryAsym(cipherBytes, publicKey, privateKey, CMode2);
            // Unzip after all, if it's necessary
            byte[] decryptedBytes = (ZType != ZipType.None) ? ZType.Unzip(intermediatBytes) : intermediatBytes;

            return decryptedBytes;
        }



        /// <summary>
        /// Multi functional 
        /// <see cref="EncryptEncodeBytes(byte[], string, CipherMode2)"/>
        /// <see cref="DecodeDecrpytBytes(byte[], string, CipherMode2)"/>
        /// </summary>
        /// <param name="inBytes">incoming bytes</param>
        /// <param name="secretKey">user private key</param>
        /// <param name="directionDecrypt">true for decryption, false for encryption</param>        
        /// <param name="cmode2"></param>
        /// <returns>transformed byte array</returns>
        public virtual byte[] CryptCodeBytes(byte[] inBytes, string publicKey, string privateKey,
            bool directionDecrypt, CipherMode2 cmode2)
        {
            return (!directionDecrypt) ?
                EncryptEncodeBytes(inBytes, publicKey, privateKey, cmode2) :
                DecodeDecrpytBytes(inBytes, publicKey, privateKey, cmode2);
        }


        #endregion multiple rounds en-de-cryption


    }

}
