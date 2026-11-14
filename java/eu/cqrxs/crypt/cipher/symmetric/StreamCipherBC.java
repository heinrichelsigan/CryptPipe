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

import eu.cqrxs.crypt.cipher.CryptParams;
import org.bouncycastle.crypto.*;
import org.bouncycastle.crypto.engines.ChaChaEngine;
import org.bouncycastle.crypto.modes.*;

/**
 * StreamCipherBC generic crypt wrapper class
 * great thanks to the legion of bouncycastle.com
 */
public class StreamCipherBC implements StreamCipher {

    private boolean initialised = false, encryption;

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
     * Generic CryptBounceCastle encrypt member function
     * @param plainData plain data byte[] array
     * @return encrypted byte[] array
     */
    public byte[] encrypt(byte[] plainData) {

        byte[] cipherData = new byte[plainData.length];

        for (int j = 0; j < plainData.length; j++)  {
            cipherData[j] = returnByte(plainData[j]);
        }

        return cipherData;
    }

    /**
     * Generic decrypt member function
     * @param cipherData encrypted byte[] arrey
     * @return decrypted plain byte[] data
     */
    public byte[] decrypt(byte[] cipherData) {

        byte[] plainData = new byte[cipherData.length];

        for (int j = 0; j < cipherData.length; j++) {
            plainData[j] = returnByte(cipherData[j]);
        }

        return plainData;
    }

}