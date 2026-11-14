/**
 * @author           <a href="mailto:heinrich.elsigan@cqrxs.eu">Heinrich Elsigan</a>
 * @version          V 2.26.428
 * @since            API 27 Oreo 8.1
 *
 * Coded 2021-2033 by <a href="mailto:he@area23.at">Heinrich Elsigan</a>
 * <a href="https://heinrichelsigan.area23.at">heinrichelsigan.area23.at</a>
 */

package eu.cqrxs.crypt.cipher;

import eu.cqrxs.crypt.cipher.symmetric.JAes;
import eu.cqrxs.crypt.cipher.symmetric.ZenMatrix;
import org.bouncycastle.crypto.BlockCipher;
import org.bouncycastle.crypto.CipherParameters;
import org.bouncycastle.crypto.StreamCipher;
import org.bouncycastle.crypto.engines.*;
// import org.bouncycastle.crypto.engines.
import eu.cqrxs.crypt.hash.KeyHash;
import eu.cqrxs.util.Constants;
import org.bouncycastle.crypto.params.KeyParameter;
import org.bouncycastle.crypto.params.ParametersWithIV;

import java.nio.charset.StandardCharsets;


/**
 * Crypt Params
 */
@SuppressWarnings("deprecation")
public class CryptParams {

    public CipherEnum cipher;

    public String getAlgorithmName() {
        return cipher.getName();
    }

    public void setAlgorithmName(String algo) {
        cipher = CipherEnum.valueOf(algo);
    }

    /**
     * getMode()
     * @return {@link String} with 3 characters for {@link CipherMode2}
     */
    public String getMode() { return cmode2.getName(); }

    public String key, hash;

    public CipherMode2 cmode2;

    public int size, blockSize;
    public int keyLen, ivLen;

    public KeyHash keyHashing;

    public BlockCipher blockCipher;
    public StreamCipher streamCipher;
    public CipherParameters streamCipherParameters;


    /**
     * CryptParams(CipherEnum cparameter less default constructor
     */
    public CryptParams() {
        cipher = CipherEnum.Aes;
        size = 256;
        keyLen = 32;
		ivLen = 32;
        ivLen = 32;
        cmode2 = CipherMode2.ECB;
        //noinspection deprecation
        blockCipher = new AESEngine();
        keyHashing = KeyHash.Hex;
        blockSize = blockCipher.getBlockSize();
    }



    /**
     * CryptParams(CipherEnum constructor
     * @param cipherAlgo {@link CipherEnum}
     */
    public CryptParams(CipherEnum cipherAlgo) {
        cipher = cipherAlgo;
        size = 256;
        keyLen = 32;
        ivLen = 32;
        cmode2 = CipherMode2.ECB;
        keyHashing = KeyHash.Hex;

        switch (cipher) {
            case Aes:
                blockCipher = new  AESEngine();
                break;
            case JAes:
                blockCipher = new JAes(size);
                size = 128;
                break;
            case AesLight:
                blockCipher = new AESLightEngine();
                size = 128;
                break;
            case Aria:
                blockCipher = new ARIAEngine();
                size = 128;
                break;
            case BlowFish: ;
                blockCipher = new BlowfishEngine();
                size = 64;
                keyLen = 8;
                ivLen = 8;
                break;
            case Fish2:
                blockCipher = new TwofishEngine();
                size = 128;
                keyLen = 16;
                ivLen = 16;
                break;
            case Fish3:
				blockCipher = new ThreefishEngine(size);
                break;                            
            case Camellia:
                blockCipher = new CamelliaEngine();
                size = 128;
                keyLen = 16;
                ivLen = 16;
                break;
            case CamelliaLight:
                blockCipher = new CamelliaLightEngine();
                size = 128;
                keyLen = 16;
                ivLen = 16;
                break;
            case Cast5:
                blockCipher = new CAST5Engine();
                size = 128;
                keyLen = 16;
                ivLen = 16;
                break;
            case Cast6:
                blockCipher = new CAST6Engine();
                break;
            case Des:
                blockCipher = new DESEngine();
                size = 64;
                keyLen = 8;
                ivLen = 8;
                break;
            case Des3:
                blockCipher = new DESedeEngine();
                size = 128;
                keyLen = 16;
                ivLen = 8;
                break;
            case Dstu7624:
                blockCipher = new DSTU7624Engine(size);
                size = 128;
                keyLen = 16;
                ivLen = 16;
                break;
            case Gost28147:
                blockCipher = new GOST28147Engine();
                break;
            case Idea:
                blockCipher = new IDEAEngine();
                break;
            case Noekeon:
                blockCipher = new NoekeonEngine();
                size = 128;
                keyLen = 16;
                ivLen = 16;
                break;
            case RC2:
                blockCipher = new RC2Engine();
                size = 128;
                break;
            case RC532:
                blockCipher = new RC532Engine();
                break;
            case RC564:
                blockCipher = new RC564Engine();
                size = 64;
                break;
            case RC6:
                blockCipher = new RC6Engine();
                break;
	        case Rijndael:
				blockCipher = new RijndaelEngine();
				break;
            case Seed:
                blockCipher = new SEEDEngine();
                size = 128;
                keyLen = 16;
                ivLen = 16;
                break;
            case Serpent:
                blockCipher = new org.bouncycastle.crypto.engines.SerpentEngine();
                size = 128;
                keyLen = 16;
                ivLen = 16;
                break;
            case SM4:
                blockCipher = new SM4Engine();
                size = 128;
                keyLen = 16;
                break;
            case SkipJack:
                blockCipher = new SkipjackEngine();
                break;
            case Tea:
                blockCipher = new TEAEngine();
                size = 128;
                keyLen = 16;
                ivLen = 16;
                break;
            case Tnepres:
                blockCipher = new TnepresEngine();
                size = 128;
                keyLen = 16;
                ivLen = 16;
                break;
            case XTea:
                blockCipher = new XTEAEngine();
                size = 128;
                keyLen = 16;
                ivLen = 16;
                break;
            case ZenMatrix:
                size = 16;
                keyLen = 16;
                ivLen = 16;
                // TODO: port it to java
                blockCipher = new ZenMatrix(size);
                break;
            /*)
            case ZenMatrix2:
                // throw new NotImplementedException("ZenMatrix2 IBlockCipher interface not implemented");)
                size = 32;
                keyLen = 16;
                // TODO: port it to java
                // blockCipher = new ZenMatrix2();
                // break;
             */
            case ChaCha:
                streamCipher = new ChaChaEngine();
                keyLen = 32;
                ivLen = 8;
                break;
            case ChaCha7539:
                streamCipher = new ChaCha7539Engine();
                keyLen = 32;
                ivLen = 12;
                break;
            case XChaCha20:
                streamCipher = new XChaCha20Engine();
                keyLen = 32;
                ivLen = 24;
                break;
            case HC128:
                streamCipher = new HC128Engine();
                keyLen = 16;
                ivLen = 16;
                break;
            case HC256:
                streamCipher = new HC256Engine();
                keyLen = 32;
                ivLen = 16;
                break;
            case RC4:
                streamCipher = new RC4Engine();
                keyLen = 32;
                ivLen = 32;
                break;
            case Isaac:
                streamCipher = new ISAACEngine();
                keyLen = 32;
                ivLen = 32;
                break;
            case Salsa20:
                streamCipher = new Salsa20Engine();
                keyLen = 32;
                ivLen = 8;
                break;
            case XSalsa20:
                streamCipher = new XSalsa20Engine();
                keyLen = 32;
                ivLen = 24;
                break;
            case CipherEnum.Vmpc: // VmpcK
                streamCipher = new VMPCEngine();
                break;
            case CipherEnum.VmpcKsa3: // VmpcK
                streamCipher = new VMPCKSA3Engine();
                break;

            default:
                blockCipher = new AESEngine();
                break;
        }

        if (ivLen < 1)
            ivLen = keyLen;

        if (streamCipher == null)
            blockSize = blockCipher.getBlockSize();

    }


    /**
     * CryptParams(CipherEnum constructor
     * @param cipherAlgo {@link CipherEnum}
     * @param secretKey user key for encryption {@link String}
     * @param keyHashed hashed user key
     * @param keyHash {@link KeyHash}
     * @param cMode2 {@link CipherMode2}
     */
    public CryptParams(CipherEnum cipherAlgo,
                       String secretKey,
                       String keyHashed,
                       KeyHash keyHash,
                       CipherMode2 cMode2
                    ) {
        this(cipherAlgo);
        keyHashing = keyHash;
        cmode2 = cMode2;
        key = (secretKey == null || secretKey.isEmpty()) ? Constants.AUTHOR_EMAIL : secretKey;
        hash = (keyHashed == null || keyHashed.isEmpty()) ? keyHashing.hash(secretKey) : keyHashed;
    }

    /**
     * CryptParams(CipherEnum constructor
     * @param cipherAlgo {@link CipherEnum}
     * @param secretKey user key for encryption {@link String}
     * @param keyHash {@link KeyHash}
     * @param cMode2 {@link CipherMode2}
     */
    public CryptParams(CipherEnum cipherAlgo,
                       String secretKey,
                       KeyHash keyHash,
                       CipherMode2 cMode2) {
        this(cipherAlgo, secretKey, keyHash.hash(secretKey), keyHash, cMode2);
    }

    /***
     * constructs a  CryptParams object by {@link CipherEnum}
     * @param cipherAlgo {@link CipherEnum}
     * @param secretKey user key for encryption {@link String}
     * @param keyHashed hashed user key
     * @param cMode2 {@link CipherMode2}
     */
    public CryptParams(CipherEnum cipherAlgo,
                       String secretKey,
                       String keyHashed,
                       CipherMode2 cMode2) {
        this(cipherAlgo);
        cmode2 = cMode2;
        key = (secretKey == null || secretKey.isEmpty()) ? Constants.AUTHOR_EMAIL : secretKey;
        hash = (keyHashed == null || keyHashed.isEmpty()) ? keyHashing.hash(secretKey) : keyHashed;
        setStreamCipherParameters(secretKey.getBytes(StandardCharsets.UTF_8),  hash.getBytes(StandardCharsets.UTF_8));
    }

    /***
     * constructs a  CryptParams object by {@link CipherEnum}
     * @param cipherAlgo {@link CipherEnum}
     * @param secretKey user key for encryption {@link String}
     * @param keyHashed hashed user key
     */
    public CryptParams(CipherEnum cipherAlgo,
                       String secretKey,
                       String keyHashed) {
        this(cipherAlgo, secretKey, keyHashed, CipherMode2.ECB);
    }

    /***
     * Constructs instance via another object instance
     * @param cryptParams another instance
     */
    public CryptParams(CryptParams cryptParams) {
        this(cryptParams.cipher, cryptParams.key, cryptParams.hash,
            cryptParams.keyHashing, cryptParams.cmode2);
    }


    public void setStreamCipherParameters(byte[] keyed, byte[] hashed)  {
        if (streamCipher != null)  {
            if (keyLen > 0)
                keyed = CryptHelper.stretchCutToLength(keyed, keyLen);
            if (ivLen > 0)
                hashed = CryptHelper.stretchCutToLength(hashed, ivLen);
            KeyParameter keyParamStreamC = new KeyParameter(keyed);
            streamCipherParameters = (cipher == CipherEnum.Isaac || cipher == CipherEnum.RC4) ?
                    keyParamStreamC : new ParametersWithIV(keyParamStreamC, hashed);
        }
    }



    /***
     *  static way to get valid CryptParams for a requested  {@link CipherEnum}
     * @param cipherAlgo {@link CipherEnum}
     * @return new constructed CryptParams
     */
    @Deprecated
    public static CryptParams RequestAlgorithm(CipherEnum cipherAlgo) {

        return new CryptParams(cipherAlgo);
    }

    @Deprecated
    public static CryptParams GetCryptParams(CryptParams cParams) {

        return new CryptParams(cParams);
    }

    @Deprecated
    public static BlockCipher GetBlockCipher(CipherEnum cipherAlgo) {
        return (new CryptParams(cipherAlgo)).blockCipher;
    }


}
