/**
 * @author           <a href="mailto:heinrich.elsigan@cqrxs.eu">Heinrich Elsigan</a>
 * @version          V 1.0.1
 * @since            API 27 Oreo 8.1
 *
 *
 * Coded 2021-2026 by
 * <a href="mailto:he@area23.at">Heinrich.Elsigan</a><a href="https://area23.at">area23.at</a>
 */

package eu.cqrxs.crypt.cipher.symmetric;

import eu.cqrxs.crypt.cipher.CipherMode2;
import eu.cqrxs.crypt.cipher.CryptParams;
import eu.cqrxs.crypt.encoding.EnDeCodeHelper;
import eu.cqrxs.util.DbgWriter;
import org.bouncycastle.crypto.*;
import org.bouncycastle.crypto.engines.AESEngine;
import org.bouncycastle.crypto.engines.ChaChaEngine;
import org.bouncycastle.crypto.engines.RC564Engine;
import org.bouncycastle.crypto.modes.*;
import org.bouncycastle.crypto.paddings.BlockCipherPadding;
import org.bouncycastle.crypto.paddings.PaddedBufferedBlockCipher;
import org.bouncycastle.crypto.paddings.ZeroBytePadding;
import org.bouncycastle.crypto.params.KeyParameter;
import org.bouncycastle.crypto.params.ParametersWithIV;

import static javax.swing.text.StyleConstants.Size;

/**
 * BlockCipherBC generic crypt wrapper class
 * great thanks to the legion of bouncycastle.com
 */
public class BlockCipherBC implements BlockCipher {

    private boolean initialised = false, encryption;
    private byte[] key, iv;
    public int size, keyLen, ivLen;

    public BlockCipher cryptoBlockCipher;;
    public CipherParameters cipherParameters;
    public BlockCipherPadding cryptoBlockCipherPadding;
    protected BufferedBlockCipher bufBChipher;

    public CipherMode2 cmode2;

    //region BlockCipher Interface

    public void init(boolean forEncryption, CipherParameters parameters) {

        if (!(parameters instanceof KeyParameter) && !(parameters instanceof ParametersWithIV))
            throw new IllegalArgumentException("only KeyParameter or ParametersWithIV expected.");

        if (parameters instanceof KeyParameter) {
            key = ((KeyParameter)parameters).getKey();
        }

        if (parameters instanceof ParametersWithIV) {

            CipherParameters ivParams = ((ParametersWithIV)parameters).getParameters();
            if (ivParams instanceof KeyParameter)
                key = ((KeyParameter)ivParams).getKey();
            iv = ((ParametersWithIV)parameters).getIV();
        }

        key = (key == null || key.length == 0) ? new byte[0] : key;
        iv = (iv == null || iv.length == 0) ? new byte[0] : iv;
        if (key.length == 0 && iv.length == 0)
            throw new IllegalArgumentException(
                    "KeyParameter and/or ParametersWithIV contain a null or empty key or iv.");

        cipherParameters = parameters;
        encryption = forEncryption;

        CipherParameters keyParam = (cryptoBlockCipher.getAlgorithmName() == "RC564" ||
                cryptoBlockCipher.getAlgorithmName() == "RC5-64") ?
                new org.bouncycastle.crypto.params.RC5Parameters(key, 2) :
                new org.bouncycastle.crypto.params.KeyParameter(key);

        CipherParameters keyParamIV = new org.bouncycastle.crypto.params.ParametersWithIV(keyParam, iv);

        cipherParameters = keyParamIV;

        try  {
            cryptoBlockCipher.init(forEncryption, cipherParameters);
        }
        catch (Exception exInit) {
            DbgWriter.msgInfoEx("BlockCiphherBC CryptoBlockCipher {CryptoBlockCipher.AlgorithmName}:"  +
                            "Exceptíon on cipherMode.Init with IV, trying without IV", exInit, false);
            try {
                cryptoBlockCipher.init(forEncryption, keyParam);
            }
            catch (Exception exInit2) {
                DbgWriter.msgInfoEx("BlockCipherBC CryptoBlockCipher {CryptoBlockCipher.AlgorithmName}: Exceptíon on " +
                        "cipherMode.Init with IV, trying without IV", exInit2, false);
            }
        }

        switch (cmode2) {
            case CipherMode2.CBC:
                bufBChipher = new PaddedBufferedBlockCipher(new CBCBlockCipher(cryptoBlockCipher), cryptoBlockCipherPadding);
                break;
            case CipherMode2.ECB:
                bufBChipher = new PaddedBufferedBlockCipher(cryptoBlockCipher, cryptoBlockCipherPadding);
                break;
            case CipherMode2.CFB:
                bufBChipher = new PaddedBufferedBlockCipher(new CFBBlockCipher(cryptoBlockCipher, size), cryptoBlockCipherPadding);
                break;
            // case "CCM":
            //     org.bouncycastle.crypto.modes.CCMBlockCipher ccmCipher = new CCMBlockCipher(CryptoBlockCipher);
            //     cipherMode = new PaddedBufferedBlockCipher((BlockCipher)ccmCipher, CryptoBlockCipherPadding);
            //     break;
            case CipherMode2.CTS:
                org.bouncycastle.crypto.modes.CTSBlockCipher ctsCipher = new CTSBlockCipher(cryptoBlockCipher);
                bufBChipher = ctsCipher;
                break;
            case CipherMode2.EAX:
                org.bouncycastle.crypto.modes.EAXBlockCipher eaxCipher = new EAXBlockCipher(cryptoBlockCipher);
                bufBChipher = new PaddedBufferedBlockCipher((BlockCipher)eaxCipher, cryptoBlockCipherPadding);
                break;
            case CipherMode2.GOFB:
                org.bouncycastle.crypto.modes.GOFBBlockCipher gOfbCipher = new GOFBBlockCipher(cryptoBlockCipher);
                bufBChipher = new PaddedBufferedBlockCipher((BlockCipher)gOfbCipher, cryptoBlockCipherPadding);
                break;
            default:
                if (isNullByteArray(iv)) {
                    bufBChipher = new PaddedBufferedBlockCipher(cryptoBlockCipher, cryptoBlockCipherPadding);
                    cmode2 = CipherMode2.ECB;
                }
                break;
        }

        // cipherMode init with initialization vector only when Mode isn't ECB and Algo is IV init capable
        if (cmode2 == CipherMode2.ECB || !canAlgoKeyIV(cryptoBlockCipher))
            cipherParameters = keyParam;

        try {
            bufBChipher.init(forEncryption, cipherParameters);
        }
        catch (Exception exInit) {
            DbgWriter.msgInfoEx("CryptBounceCastle {BufBlockCiffre.AlgorithmName}: Exceptíon on cipherMode.Init with IV, trying without IV",
                    exInit, false);
            cipherParameters = keyParam;
            try {
                bufBChipher.init(forEncryption, cipherParameters);
            }
            catch (Exception exInit2) {
                DbgWriter.msgInfoEx("BlockCipherBC " + bufBChipher.getUnderlyingCipher().getAlgorithmName() +
                    " Exceptíon on cipherMode.Init with IV, trying without IV", exInit2, false);
            }
        }

        initialised = true;
    }


    public int getBlockSize() {
        return cryptoBlockCipher.getBlockSize();
    }

    public int processBlock(byte[] input, int inOff, byte[] output, int outOff)  {
        int len = input.length + (size - (input.length % size));
        output = new byte[len];
        return cryptoBlockCipher.processBlock(input, inOff, output, outOff);
    }


    public String getAlgorithmName() {
        return cryptoBlockCipher.getAlgorithmName();
    }


    public void reset() {
        cryptoBlockCipher.reset();
    }

    //endregion IBlockCipher Interface

    /**
     * parameterless default constructor
     */
    public BlockCipherBC()  {
        cryptoBlockCipher = null;
        cipherParameters = null;
        bufBChipher = null;
        cryptoBlockCipherPadding = new ZeroBytePadding();
    }

    /**
     * Generic BlockCipherBC constructor
     * @param cparams parameters to crypt
     * @param init init first time with a new key
     */
    public BlockCipherBC(CryptParams cparams, boolean init)  {

        cryptoBlockCipher = cparams.blockCipher == null ? new AESEngine() : cparams.blockCipher;
        if (cryptoBlockCipher.getAlgorithmName() == "RC564" || cryptoBlockCipher.getAlgorithmName() == "RC5-64")
            cryptoBlockCipher = new RC564Engine();
        cryptoBlockCipherPadding = new ZeroBytePadding();
        cipherParameters = cparams.cipherParameters;
        size = Math.min(cparams.size, cryptoBlockCipher.getBlockSize());

        cmode2 = cparams.cmode2;

        init(init, cipherParameters);
    }


    /**
     * canAlgoKeyIV
     * @param blockCipher {@link BlockCipher}
     * @return true, if algo handles initialization with key and initialization vector
     */
    protected boolean canAlgoKeyIV(BlockCipher blockCipher) {
        if (blockCipher == null)
            blockCipher = cryptoBlockCipher;
        String algoName = blockCipher.getAlgorithmName().toUpperCase();
        if (algoName.startsWith("AES") || algoName.startsWith("RIJNDAEL"))
            return false;
        if (algoName.startsWith("ARIA") || algoName.startsWith("ASCON"))
            return false;
        if (algoName.startsWith("CAST") || algoName.startsWith("GOST"))
            return false;
        if (algoName.startsWith("RC") || algoName.startsWith("IDEA"))
            return false;
        if (algoName.startsWith("SKIPJACK") || algoName == "DESEDE")
            return false;
        if (algoName.startsWith("TEA") || algoName.startsWith("XTEA"))
            return false;
        return true;
    }

    /**
     * Generic CryptBounceCastle encrypt member function
     * @param plainData plain data byte[] array
     * @return encrypted byte[] array
     */
    public byte[] encrypt(byte[] plainData) throws InvalidCipherTextException {
        plainData = cryptoBlockCipher.getAlgorithmName() == "RC564" ||
                cryptoBlockCipher.getAlgorithmName() == "RC5-64" ?
                EnDeCodeHelper.getBytesFromBytes(plainData, 6, true) : plainData;

        int outputSize = bufBChipher.getOutputSize(plainData.length);
        byte[] cipherData = new byte[outputSize];
        int result = bufBChipher.processBytes(plainData, 0, plainData.length, cipherData, 0);
        bufBChipher.doFinal(cipherData, result);

        return cipherData;
    }


    /**
     * Generic decrypt member function
     * @param cipherData encrypted byte[] arrey
     * @return decrypted plain byte[] data
     */
    public byte[] decrypt(byte[] cipherData) {

        int outputSize = bufBChipher.getOutputSize(cipherData.length);
        byte[] plainData = new byte[outputSize];
        byte[] decryptedData = new byte[outputSize];
        int result = 0;
        try {
            result = bufBChipher.processBytes(cipherData, 0, cipherData.length, plainData, 0);
            bufBChipher.doFinal(plainData, result);
        }
        catch (Exception exDecrypt) {
            DbgWriter.msgInfoEx("BlockCipherBC "  +  bufBChipher.getUnderlyingCipher().getAlgorithmName() +
                            ": Exceptíon on decrypting final block",
                    exDecrypt, false);
            try {
                plainData = new byte[outputSize];
                result = bufBChipher.processBytes(plainData, 0, cipherData.length, cipherData, 0);
            }
            catch (Exception exDecrypt2) {
                DbgWriter.msgInfoEx("BlockCipherBC"  +  bufBChipher.getUnderlyingCipher().getAlgorithmName() +
                        ": Exceptíon on 2x decrypting final block",
                        exDecrypt2, false);
                plainData = new byte[outputSize];
                result = bufBChipher.processBytes(plainData, 0, cipherData.length, cipherData, 0);
            }
        }

        return cryptoBlockCipher.getAlgorithmName() == "RC564" ||
                cryptoBlockCipher.getAlgorithmName() == "RC5-64" ?
                EnDeCodeHelper.getBytesTrimNulls(plainData) : plainData;

        // return plainData;
    }


    public boolean isNullByteArray(byte[] bytes) {
        if (bytes == null || bytes.length < 1)
            return true;
        for (int i = 0; i < bytes.length; i++) {
            if (bytes[i] != (byte)0)
                return false;
        }

        return true;
    }

}