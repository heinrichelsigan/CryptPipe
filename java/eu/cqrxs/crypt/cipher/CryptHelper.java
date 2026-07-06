/**
 * @author           <a href="mailto:heinrich.elsigan@area23.at">Heinrich Elsigan</a>
 * @version          V 1.0.1
 * @since            API 27 Oreo 8.1
 *
 * Coded 2021-2033 by <a href="mailto:he@area23.at">Heinrich Elsigan</a>
 * <a href="https://heinrichelsigan.area23.at">heinrichelsigan.area23.at</a>
 */
package eu.cqrxs.crypt.cipher;

// import androidx.core.content.res.TypedArrayUtils;

// import com.google.common.primitives.Bytes;
import eu.cqrxs.crypt.encoding.EncodeEnum;
import eu.cqrxs.crypt.encoding.Hex16Coder;
import eu.cqrxs.crypt.hash.KeyHash;
import eu.cqrxs.util.Constants;
import eu.cqrxs.util.DbgWriter;
import eu.cqrxs.util.NotImplementedError;
import eu.cqrxs.zip.ZipType;

import java.util.List;
import java.nio.ByteBuffer;
import java.nio.charset.Charset;
import java.util.ArrayList;

/**
 * CryptHelper
 */
public class CryptHelper {


    public static byte[] tarBytes(byte[] byteArray0, byte[] byteArray1) {
        int tarByteSize = byteArray0.length + byteArray1.length;
        ByteBuffer byteBuffer = ByteBuffer.allocate(tarByteSize);
        byteBuffer.put(byteArray0);
        byteBuffer.put(byteArray1);

        return byteBuffer.array();
    }

    public static byte[] tarBytes(byte[] byteArray0, byte[] byteArray1, byte[] byteArray2) {
        int tarByteSize = byteArray0.length + byteArray1.length + byteArray2.length;
        ByteBuffer byteBuffer = ByteBuffer.allocate(tarByteSize);
        byteBuffer.put(byteArray0);
        byteBuffer.put(byteArray1);
        byteBuffer.put(byteArray2);

        return byteBuffer.array();
    }

    public static byte[] tarBytes(byte[] byteArray0, byte[] byteArray1,
                                  byte[] byteArray2, byte[] byteArray3) {
        int tarByteSize = byteArray0.length + byteArray1.length + byteArray2.length + byteArray3.length;
        ByteBuffer byteBuffer = ByteBuffer.allocate(tarByteSize);
        byteBuffer.put(byteArray0);
        byteBuffer.put(byteArray1);
        byteBuffer.put(byteArray2);
        byteBuffer.put(byteArray3);

        return byteBuffer.array();
    }
	
    /**
     * PrivateUserKey
     * @param secretKey
     * @return private user key
     */
    public static String PrivateUserKey(String secretKey)
    {
        if (secretKey == null || secretKey.length() == 0)
            return Constants.AUTHOR_EMAIL;
        return  secretKey;
    }

    /***
     *
     * @param secKey users private secret key
     * @param hashedKey users private secret key hash
     * @return doubled concatendated string of (secretKey + hash)
     */
    public static String PrivateKeyWithUserHash(String secKey, String hashedKey) {
        if (secKey == null || secKey.length() < 1)
            throw new IllegalArgumentException("secKey");

        if (hashedKey == null || hashedKey.length() == 0)
            hashedKey = KeyHash.Hex.hash(secKey);

        String concatenation = String.format("%s%s", secKey, hashedKey);
        return concatenation;
    }


    /**
     *
     * @param key users private key
     * @param keyHash key hash
     * @param merge do merge
     * @return doubled concatendated string of (secretKey + hash)
     * @throws IllegalArgumentException key
     */
	 @Deprecated
    public static byte[] KeyUserHashBytes(String key, String keyHash, boolean merge)  {
        if (key == null || key.length() < 1)
            throw new IllegalArgumentException("key");

        if (keyHash == null || keyHash.length() == 0)
            keyHash = KeyHash.Hex.hash(key);

        byte[] keyBytes = key.getBytes(Charset.forName("UTF-8"));
        byte[] hashBytes = keyHash.getBytes(Charset.forName("UTF-8"));

        return keyHashBytes(keyBytes, hashBytes, merge);
    }

    /***
     * KeyHashBytes
     * @param keyBytes user keyBytes
     * @param hashBytes user hashBytes
     * @param merge
     * @return merged byte array
     */
    public static byte[] keyHashBytes(byte[] keyBytes, byte[] hashBytes, boolean merge) {
        if (keyBytes == null || keyBytes.length == 0)
            throw new IllegalArgumentException("keyBytes");

        if (hashBytes == null || hashBytes.length == 0)
            throw new IllegalArgumentException("hashBytes");

        if (!merge)
            return tarBytes(keyBytes, hashBytes);

        List<Byte> outBytes = new ArrayList<Byte>();

        int kb = 0, hb = 0;
        for (int ob = 0; (ob < (keyBytes.length + hashBytes.length)); ob++)  {
            if (kb < keyBytes.length)
                outBytes.add(keyBytes[kb++]);
            if (hb < hashBytes.length)
                outBytes.add(hashBytes[hb++]);
            if (hb < hashBytes.length)
                outBytes.add(hashBytes[hashBytes.length - hb]);
            hb++;
            if (kb < keyBytes.length)
                outBytes.add(keyBytes[keyBytes.length - kb]);
            kb++;

            ob = outBytes.size();
        }

        byte[] outOut = new byte[outBytes.size()];
        for (int arrcp = 0; arrcp < outBytes.size(); arrcp++) // manually array copy
            outOut[arrcp] = ((Byte)outBytes.get(arrcp)).byteValue();

        return outOut;
    }


    /**
     * getKeyBytesSingle gets single user key bytes from users key
     * @param key users secret key
     * @param keyLen length that keybytes should have afterwards
     * @return generated user keybyte array from key and hash
     */
    public static byte[] getKeyBytesSingle(String key, int keyLen)  {

        if (key == null || key.length() == 0)
            throw new IllegalArgumentException("key");

		return getKeyBytesSingle(key.getBytes(Charset.forName("UTF-8")), keyLen);
	}
	
	/**
     * getKeyBytesSingle gets single user key bytes from users keyBytes
     * @param keyBytes users secret keyBytes
     * @param keyLen length that keybytes should have afterwards
     * @return generated user keybyte array from keyBytes
     */
	public static byte[] getKeyBytesSingle(byte[] keyBytes, int keyLen) {

        if (keyBytes == null || keyBytes.length == 0)
            throw new IllegalArgumentException("keyBytes");

        int o = 0;
		byte[] outBytes = new byte[keyLen];
        for (o = 0; o < keyLen; o++)
            outBytes[o] = (byte)0;

        if (keyBytes.length >= keyLen)
            System.arraycopy(keyBytes, 0, outBytes, 0, keyLen);
        else
            System.arraycopy(keyBytes, 0, outBytes, 0, keyBytes.length);

        return outBytes;
    }

    /**
     * getKeyBytesSimple gets simplö user key bytes from users key and key hash
     * @param key users secret key
     * @param keyHash hashed users key
     * @param keyLen length that keybytes should have afterwards
     * @return generated user keybyte array from key and hash
     */
    public static byte[] getKeyBytesSimple(String key, String keyHash, int keyLen) {
        int o=0;
        if (key == null || key.length() == 0)
            throw new IllegalArgumentException("key");

        byte[] keyBytes = key.getBytes(Charset.forName("UTF-8"));
		byte[] hashBytes = keyHash.getBytes(Charset.forName("UTF-8"));

        byte[] outBytes = new byte[keyLen];
        for (o = 0; o < keyLen; o++) {
            outBytes[o] = (byte)0;
        }

		byte[] keyHashBytes = tarBytes(keyBytes, hashBytes);

        if (keyHashBytes.length >= keyLen)
            System.arraycopy(keyHashBytes, 0, outBytes, 0, keyLen);
        else
            System.arraycopy(keyHashBytes, 0, outBytes, 0, keyHashBytes.length);
        
		/* only debug code, because of Sha* is UPPERCASE when using Microsoft.Net basic sha methods	
			String ddmsg = "", dxmsg = "";
			for (int i = 0; i < keyLen; i++) {
				ddmsg += String.format("%3d", outBytes[i]) + " "; 
				dxmsg += String.format("%2x", outBytes[i]) + "  "; 
			}		
			DbgWriter.dbgmsg(ddmsg, 2, false);
			DbgWriter.dbgmsg(dxmsg, 2, false);
		*/
        return outBytes;
    }


       /***
        * getUserKeyBytes
        * @param key users secret key
        * @param keyHash hashed key
        * @param keyLen total length of new generated key bytes
        * @return user key hash bytes
        */
        public static byte[] getUserKeyBytes(String key, String keyHash, int keyLen)  {
            if (key == null || key.length() == 0)
                throw new IllegalArgumentException("key");

            byte[] keyBytes = key.getBytes(Charset.forName("UTF-8"));
            byte[] hashBytes  = keyHash.getBytes(Charset.forName("UTF-8"));

			return getKeyHashBytes(keyBytes, hashBytes, keyLen);
		}

		/**
		 * getKeyHashBytes
         * @param keyBytes users secret keyBytes
		 * @param hashBytes
         * @param keyLen total length of new generated key bytes
         * @return user key hash bytes
         */
        public static byte[] getKeyHashBytes(byte[] keyBytes, byte[] hashBytes, int keyLen)  {
            int o=0;
            if (keyBytes == null || keyBytes.length == 0)
                throw new IllegalArgumentException("keyBytes");
			if (hashBytes == null || hashBytes.length == 0)
				hashBytes = new byte[0];

            byte[] outKeyBytes = new byte[keyLen];
            byte[] outHashBytes = new byte[keyLen];
            for (o = 0; o < keyLen; o++) {
                outKeyBytes[o] = (byte)0;
                outHashBytes[o] = (byte)0;
            }

            if (keyBytes.length >= keyLen)
                System.arraycopy(keyBytes, 0, outKeyBytes, 0, keyLen);
            else
                System.arraycopy(keyBytes, 0, outKeyBytes, 0, keyBytes.length);

            byte[] smallBytes = keyHashBytes(keyBytes, hashBytes, true);

            if (hashBytes.length >= keyLen)
                System.arraycopy(hashBytes, 0, outHashBytes, 0, keyLen);
            else
                System.arraycopy(hashBytes, 0, outHashBytes, 0, hashBytes.length);

            byte[] keyHashBytes = tarBytes(outKeyBytes, outHashBytes);
            return keyHashBytes;
        }


        /**
         * getKeyBytesFromBytes
         * @param keyBytes users keybytes
         * @param keyLen maximum length, that wil be needed for stretching key bytes
         * @return key bytes stretched to length by adding one or many different key hashes
         */
        public static byte[] getKeyBytesFromBytes(byte[] keyBytes, int keyLen)  {
            int o = 0;
            if (keyBytes == null || keyBytes.length == 0)
                throw new IllegalArgumentException("keyBytes");

            byte[] outBytes = new byte[keyLen];
            for (o = 0; o < keyLen; o++)
                outBytes[o] = (byte)0;

            if (keyBytes.length >= keyLen)
                System.arraycopy(keyBytes, 0, outBytes, 0, keyLen);
            else
                System.arraycopy(keyBytes, 0, outBytes, 0, keyBytes.length);

            return outBytes;
        }

        // #endregion GetUserKeyBytes
}

