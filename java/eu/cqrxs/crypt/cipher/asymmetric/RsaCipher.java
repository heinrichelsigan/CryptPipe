/**
 * @author           <a href="mailto:heinrich.elsigan@cqrxs.eu">Heinrich Elsigan</a>
 * @version          V 1.0.1
 * @since            API 27 Oreo 8.1
 *
 * Coded 2021-2033 by <a href="mailto:he@area23.at">Heinrich Elsigan</a>
 * <a href="https://heinrichelsigan.area23.at">heinrichelsigan.area23.at</a>
 */

package eu.cqrxs.crypt.cipher.asymmetric;

import eu.cqrxs.crypt.cipher.CryptHelper;
import eu.cqrxs.crypt.hash.KeyHash;
import eu.cqrxs.util.DbgWriter;
import eu.cqrxs.util.NotImplementedError;

import java.io.ByteArrayInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.nio.charset.StandardCharsets;
import java.security.SecureRandom;
import java.security.Security;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;

import org.bouncycastle.asn1.pkcs.PrivateKeyInfo;
import org.bouncycastle.asn1.x509.SubjectPublicKeyInfo;
import org.bouncycastle.crypto.*;
import org.bouncycastle.crypto.BlockCipher;
import org.bouncycastle.crypto.encodings.PKCS1Encoding;
import org.bouncycastle.crypto.engines.RSAEngine;
import org.bouncycastle.crypto.engines.VMPCKSA3Engine;
import org.bouncycastle.crypto.generators.RSAKeyPairGenerator;
import org.bouncycastle.crypto.params.*;
import org.bouncycastle.crypto.prng.VMPCRandomGenerator;
import org.bouncycastle.crypto.util.PrivateKeyFactory;
import org.bouncycastle.crypto.util.PublicKeyFactory;
import org.bouncycastle.jce.provider.BouncyCastleProvider;
// import org.bouncycastle.openssl.PEMKeyPair;
// import org.bouncycastle.openssl.PEMParser;
// import org.bouncycastle.util.io.pem.PemReader;

/**
 * RsaCipher generic crypt wrapper class
 * great thanks to the legion of bouncycastle.com
 * #!/usr/bin/bash
 * KLEN=512
 * if [ $# -gt 0 ] ; then KLEN=$1; fi
 * if [ $KLEN -lt 512 ] ; then KLEN=512; fi
 * openssl genpkey -algorithm rsa -pkeyopt rsa_keygen_bits:$KLEN > /tmp/rsa_$KLEN.pk8 2>/dev/null
 * cat /tmp/rsa_$KLEN.pk8
 * openssl rsa -in /tmp/rsa_$KLEN.pk8 -pubout | tee rsa_$KLEN.spki
 * sleep 1; rm -f /tmp/rsa_$KLEN.pk8
 */
public class RsaCipher {

    private static AsymmetricCipherKeyPair rsaKeyPair;
    public  static AsymmetricCipherKeyPair GetRsaKeyPair() { return rsaKeyPair; }

    public RsaCipher() {
        generateNewRsaKeyPair();
    }

    /**
     * rsaGenWithKey generates asymmetric RSA cipher with
     * @param pub public key
     * @param priv private key
     * @return {@link AsymmetricCipherKeyPair}
     */
    public static AsymmetricCipherKeyPair rsaGenWithKey(String pub, String priv) {
        if (rsaKeyPair != null)
            return GetRsaKeyPair();
        rsaKeyPair = generateNewRsaKeyPair(); // getRsaKeyPair(pub, priv);
        return GetRsaKeyPair();
    }

    /**
     * generateNewRsaKeyPair - generates a new rsa key pair
     * @return {@link AsymmetricCipherKeyPair}
     */
    static AsymmetricCipherKeyPair generateNewRsaKeyPair() {

        if (rsaKeyPair != null)
            return GetRsaKeyPair();

        RSAKeyPairGenerator  rsaKeyGen = new RSAKeyPairGenerator();
        SecureRandom rand = new SecureRandom();
        KeyGenerationParameters keyParams;
        try {
            keyParams = new KeyGenerationParameters(
                    SecureRandom.getInstance("SHA1PRNG"),
                    4096);
        } catch (Exception e) {
            keyParams = new KeyGenerationParameters(rand, 4096);
        }
        rsaKeyGen.init(keyParams);

        rsaKeyPair = rsaKeyGen.generateKeyPair();
        return GetRsaKeyPair();
    }


    public static String getHexString(byte[] b) throws Exception {
        String result = "";
        for (int i=0; i < b.length; i++) {
            result +=
                    Integer.toString( ( b[i] & 0xff ) + 0x100, 16).substring( 1 );
        }
        return result;
    }

    public static byte[] hexStringToByteArray(String s) {
        int len = s.length();
        byte[] data = new byte[len / 2];
        for (int i = 0; i < len; i += 2) {
            data[i / 2] = (byte) ((Character.digit(s.charAt(i), 16) << 4) + Character.digit(s.charAt(i+1), 16));
        }
        return data;
    }

    /*
    public static AsymmetricKeyParameter loadPublicKey(InputStream is) {
        SubjectPublicKeyInfo spki = (SubjectPublicKeyInfo) readPemObject(is);
        try {
            return PublicKeyFactory.createKey(spki);
        } catch (IOException ex) {
            throw new RuntimeException("Cannot create public key object based on input data", ex);
        }
    }

    public static AsymmetricKeyParameter loadPrivateKey(InputStream is) {
        PEMKeyPair keyPair = (PEMKeyPair) readPemObject(is);
        PrivateKeyInfo pki = keyPair.getPrivateKeyInfo();
        try {
            return PrivateKeyFactory.createKey(pki);
        } catch (IOException ex) {
            throw new RuntimeException("Cannot create private key object based on input data", ex);
        }
    }

    private static Object readPemObject(InputStream is) {
        try {
            // Validate.notNull(is, "Input data stream cannot be null");
            InputStreamReader isr = new InputStreamReader(is, "UTF-8");
            PEMParser pemParser = new PEMParser(isr);

            Object obj = pemParser.readObject();
            if (obj == null) {
                throw new Exception("No PEM object found");
            }
            return obj;
        } catch (Throwable ex) {
            throw new RuntimeException("Cannot read PEM object from input data", ex);
        }
    }



    static AsymmetricCipherKeyPair getRsaKeyPair(String pubKey, String privKey) {
        PKCS1Encoding rsaCipher = new PKCS1Encoding(new RSAEngine());
        AsymmetricKeyParameter keyParameterPublic;
        RSAPrivateCrtKeyParameters keyParameterPrivate;

        InputStream ispub = new ByteArrayInputStream(pubKey.getBytes(StandardCharsets.UTF_8));
        keyParameterPublic = (AsymmetricKeyParameter)loadPublicKey(ispub);

        InputStream isprv = new ByteArrayInputStream(privKey.getBytes(StandardCharsets.UTF_8));
        keyParameterPrivate = (RSAPrivateCrtKeyParameters) loadPrivateKey(isprv);

        rsaKeyPair = new AsymmetricCipherKeyPair(keyParameterPublic, keyParameterPrivate);
        return GetRsaKeyPair();
    }

    */

    /**
     * Rsa encrypt bytes with public key
     * @param data
     * @param publicKey
     * @return encrypted :yte[]
     */
    public static String Encrypt(byte[] data, AsymmetricKeyParameter publicKey) throws Exception{
//	Source: http://www.cs.berkeley.edu/~jonah/bc/org/bouncycastle/crypto/engines/RSAEngine.html
        Security.addProvider(new BouncyCastleProvider());

        RSAEngine engine = new RSAEngine();
        engine.init(true, publicKey); //true if encrypt

        byte[] hexEncodedCipher = engine.processBlock(data, 0, data.length);

        return getHexString(hexEncodedCipher);
    }

    public static String Decrypt(String encrypted, AsymmetricKeyParameter privateKey) throws InvalidCipherTextException{
//	Source: http://www.mysamplecode.com/2011/08/java-rsa-decrypt-string-using-bouncy.html

        Security.addProvider(new BouncyCastleProvider());

        AsymmetricBlockCipher engine = new RSAEngine();
        engine.init(false, privateKey); //false for decryption

        byte[] encryptedBytes = hexStringToByteArray(encrypted);
        byte[] hexEncodedCipher = engine.processBlock(encryptedBytes, 0, encryptedBytes.length);

        return new String (hexEncodedCipher);
    }


}
