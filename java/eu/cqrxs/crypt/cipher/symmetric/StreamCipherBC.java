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
import eu.cqrxs.crypt.cipher.CryptHelper;
import eu.cqrxs.crypt.cipher.CryptParams;
import eu.cqrxs.crypt.encoding.EnDeCodeHelper;
import eu.cqrxs.crypt.encoding.EncodeEnum;
import eu.cqrxs.util.DbgWriter;
import org.bouncycastle.crypto.*;
import org.bouncycastle.crypto.engines.AESEngine;
import org.bouncycastle.crypto.engines.ChaChaEngine;
import org.bouncycastle.crypto.engines.RC564Engine;
import org.bouncycastle.crypto.modes.*;
import org.bouncycastle.crypto.paddings.BlockCipherPadding;
import org.bouncycastle.crypto.paddings.PaddedBufferedBlockCipher;
import org.bouncycastle.crypto.params.ParametersWithIV;

import java.io.IOException;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;

/**
 * StreamCipherBC generic crypt wrapper class
 * great thanks to the legion of bouncycastle.com
 */
public class StreamCipherBC implements StreamCipher {

    private boolean initialised = false, encryption;
    protected byte[] key, iv;
    protected int keyLen, ivLen;

    public StreamCipher cryptoStreamCipher;
    public CipherParameters streamCipherParameters;


    public void init(boolean forEncryption, CipherParameters params) {
        encryption = forEncryption;
        cryptoStreamCipher.init(forEncryption, params);
        initialised = true;
    }

    public String getAlgorithmName() {
        return cryptoStreamCipher.getAlgorithmName();
    }

    public byte returnByte(byte in) {
        return cryptoStreamCipher.returnByte(in);
    }

    public int processBytes(byte[] in, int inOff, int len, byte[] out, int outOff) {

        out = new byte[in.length];

        int i = inOff;
        int j = outOff;
        int l= 0;
        // simulate stream
        for (i = inOff; i < (inOff + len); i++) {
            out[j++] = cryptoStreamCipher.returnByte(in[i]);
            l++;
        }

        return  l;
    }

    public void reset() {
        cryptoStreamCipher.reset();
    }


    /**
     * parameterless default constructor
     */
    public StreamCipherBC()  {
        cryptoStreamCipher = null;
        streamCipherParameters = null;
    }

    /**
     * Generic StreamCipherBC constructor
     * @param cparams parameters to crypt
     * @param init init first time with a new key
     */
    public StreamCipherBC(CryptParams cparams, boolean init)  {

        cryptoStreamCipher = (cparams.streamCipher== null) ? new ChaChaEngine() : cparams.streamCipher;
        streamCipherParameters = cparams.streamCipherParameters;
        init(init, streamCipherParameters);
    }


    /**
     *  Generic CryptBounceCastle encrypt member function
     *  difference between out parameter encryptedData and return value, are 2 different encryption methods, but with the same result at the end
     * @param plainData plain data byte[] array
     * @return encrypted byte[] array
     */
    public byte[] encrypt(byte[] plainData) {

        byte[] cipherTextData = new byte[plainData.length];

        // simulate stream
        for (int j = 0; j < plainData.length; j++)
        {
            cipherTextData[j] = returnByte(plainData[j]);
        }

        return cipherTextData;
    }

    /**
     * Generic CryptBounceCastle decrypt member function
     * difference between out parameter decryptedData and return value, are 2 different decryption methods, but with the same result at the end
     * @param cipherData encrypted byte[] arrey
     * @return decrypted plain byte[] data
     */
    public byte[] decrypt(byte[] cipherData) {

        byte[] plainTextData = new byte[cipherData.length];

        // simulate stream
        for (int j = 0; j < cipherData.length; j++)
        {
            plainTextData[j] = returnByte(cipherData[j]);
        }

        return plainTextData;
    }


}