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
    public CipherParameters cipherParameters;


    /**
     * CryptParams(CipherEnum cparameter less default constructor
     */
    public CryptParams() {
        cipher = CipherEnum.Aes;
        size = 256;
        keyLen = 32;
        ivLen = 32;
        cmode2 = CipherMode2.ECB;
        //noinspection deprecation
        blockCipher = null;
        streamCipher = null;
        keyHashing = KeyHash.Hex;
        // blockSize = blockCipher.getBlockSize();
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
        this();
        cipher = cipherAlgo;
        keyHashing = keyHash;
        key = (secretKey == null || secretKey.isEmpty()) ? Constants.AUTHOR_EMAIL : secretKey;
        hash = (keyHashed == null || keyHashed.isEmpty()) ? keyHashing.hash(secretKey) : keyHashed;
        cmode2 = cMode2;

        setCipherEngineWithKeyIvSize(cipher,
                secretKey.getBytes(StandardCharsets.UTF_8),
                hash.getBytes(StandardCharsets.UTF_8));
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
        this(cipherAlgo, secretKey, keyHashed, KeyHash.Hex, cMode2);
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


    /**
     * setCipherEngineWithKeyIvSize
     * creates a blockCipherEngine or StreamCipherEngine with correct sizes
     * @param cipher {@link CipherEnum}
     * @param keyed byze[]  array of key
     * @param hashed byze[]  array of hash iv
     */
    public void setCipherEngineWithKeyIvSize(
            CipherEnum cipher, byte[] keyed, byte[] hashed)  {

        KeyParameter keyParam;;

        switch (cipher) {
            case Aes:
                blockCipher = new AESEngine();
                break;
            case JAes:
                size = 128;
                blockCipher = new JAes(size);
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
                setSizeKeyIvLen(64, 8,  8);
                break;
            case Fish2:
                blockCipher = new TwofishEngine();
                setSizeKeyIvLen(128, 16,  16);
                break;
            case Fish3:
                blockCipher = new ThreefishEngine(size);
                break;
            case Camellia:
                blockCipher = new CamelliaEngine();
                setSizeKeyIvLen(128, 16,  16);
                break;
            case CamelliaLight:
                blockCipher = new CamelliaLightEngine();
                setSizeKeyIvLen(128, 16,  16);
                break;
            case Cast5:
                blockCipher = new CAST5Engine();
                setSizeKeyIvLen(128, 16,  16);
                break;
            case Cast6:
                blockCipher = new CAST6Engine();
                break;
            case Des:
                blockCipher = new DESEngine();
                setSizeKeyIvLen(64, 8,  8);
                break;
            case Des3:
                blockCipher = new DESedeEngine();
                setSizeKeyIvLen(128, 16,  8);
                break;
            case Dstu7624:
                setSizeKeyIvLen(128, 32,  16);
                blockCipher = new DSTU7624Engine(size);
                break;
            case Gost28147:
                blockCipher = new GOST28147Engine();
                break;
            case Idea:
                blockCipher = new IDEAEngine();
                break;
            case Noekeon
                setSizeKeyIvLen(128, 16,  16);:
                blockCipher = new NoekeonEngine();
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
                size = 128;
                break;
            case Rijndael:
                blockCipher = new RijndaelEngine();
                break;
            case Seed:
                blockCipher = new SEEDEngine();
                setSizeKeyIvLen(128, 16,  16);
                break;
            case Serpent:
                blockCipher = new org.bouncycastle.crypto.engines.SerpentEngine();
                setSizeKeyIvLen(128, 16,  16);
                break;
            case SM4:
                blockCipher = new SM4Engine();
                setSizeKeyIvLen(128, 16,  16);
                break;
            case SkipJack:
                blockCipher = new SkipjackEngine();
                break;
            case Tea:
                blockCipher = new TEAEngine();
                setSizeKeyIvLen(128, 16,  16);
                break;
            case Tnepres:
                blockCipher = new TnepresEngine();
                setSizeKeyIvLen(128, 16,  16);
                break;
            case XTea:
                blockCipher = new XTEAEngine();
                setSizeKeyIvLen(128, 16,  16);
                break;
            case ZenMatrix:
                setSizeKeyIvLen(16, 16,  16);
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
                setKeyIvLen(32, 8);
                break;
            case ChaCha7539:
                streamCipher = new ChaCha7539Engine();
                setKeyIvLen(32, 12);
                break;
            case XChaCha20:
                streamCipher = new XChaCha20Engine();
                setKeyIvLen(32, 24);
                break;
            case HC128:
                streamCipher = new HC128Engine();
                setKeyIvLen(16, 16);
                break;
            case HC256:
                streamCipher = new HC256Engine();
                setKeyIvLen(32, 16);
                break;
            case RC4:
                streamCipher = new RC4Engine();
                setKeyIvLen(32, 32);
                break;
            case Isaac:
                streamCipher = new ISAACEngine();
                break;
            case Salsa20:
                streamCipher = new Salsa20Engine();
                setKeyIvLen(32, 8);
                break;
            case XSalsa20:
                streamCipher = new XSalsa20Engine();
                setKeyIvLen(32, 24);
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

        if (streamCipher == null)
            blockSize = blockCipher.getBlockSize();

        if (keyLen > 0)
            keyed = CryptHelper.stretchCutToLength(keyed, keyLen);
        if (ivLen > 0)
            hashed = CryptHelper.stretchCutToLength(hashed, ivLen);

        keyParam = new KeyParameter(keyed);
        cipherParameters =  new ParametersWithIV(keyParam, hashed);

        if (streamCipher != null)  {
            cipherParameters = (cipher == CipherEnum.Isaac || cipher == CipherEnum.RC4) ?
                    keyParam : new ParametersWithIV(keyParam, hashed);
        }

        // return ;

    }


    protected void setKeyIvLen(int keylen, int ivlen) {
        keyLen = (keylen < 1) ? 32 : keylen;
        ivLen = (ivlen < 1) ? 32 : ivlen;
    }

    protected void setSizeKeyIvLen(int blocksize, int keylen, int ivlen) {
        setKeyIvLen(keylen, ivlen);
        size = (blocksize < 0) ? 256 : blocksize;
    }

}
