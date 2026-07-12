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
import org.bouncycastle.openssl.PEMKeyPair;
import org.bouncycastle.openssl.PEMParser;
import org.bouncycastle.util.io.pem.PemReader;

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

    public static AsymmetricCipherKeyPair rsaKeyPair;

    /**
     * getRsaKeyPair
     * @return {@link AsymmetricCipherKeyPair}
     */
    public static AsymmetricCipherKeyPair getRsaKeyPair() {
        if (rsaKeyPair == null)
            rsaKeyPair = (new RsaCipher()).generateNewRsaKeyPair();
        return rsaKeyPair;
    }

    /**
     * rsaGenWithKey generates asymmetric RSA cipher with
     * @param pub public key
     * @param priv private key
     * @return {@link RsaCipher}
     */
    public static RsaCipher rsaGenWithKey(String pub, String priv) {
        RsaCipher rsac = new RsaCipher(pub, priv);
        return rsac;
    }


    /**
     * RsaCipher ctor
     */
    public RsaCipher() {
        rsaKeyPair = generateNewRsaKeyPair();
    }

    /**
     * RsaCipher ctor with
     * @param pub
     * @param priv
     */
    public RsaCipher(String pub, String priv) {
        rsaKeyPair = getRsaKeyPair(pub, priv);
    }


    /**
     * generateNewRsaKeyPair - generates a new rsa key pair
     * @return {@link AsymmetricCipherKeyPair}
     */
    protected AsymmetricCipherKeyPair generateNewRsaKeyPair() {

        if (rsaKeyPair != null)
            return getRsaKeyPair();

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
        return rsaKeyPair;
    }

    /**
     * getRsaKeyPair
     * @param pubKey
     * @param privKey
     * @return {@link AsymmetricCipherKeyPair}
     */
    protected AsymmetricCipherKeyPair getRsaKeyPair(String pubKey, String privKey) {
        PKCS1Encoding rsaCipher = new PKCS1Encoding(new RSAEngine());
        AsymmetricKeyParameter keyParameterPublic;
        // RSAPrivateCrtKeyParameters keyParameterPrivate;
        AsymmetricKeyParameter keyParameterPrivate;

        InputStream ispub = new ByteArrayInputStream(pubKey.getBytes(StandardCharsets.UTF_8));
        keyParameterPublic = (AsymmetricKeyParameter)loadPublicKey(ispub);

        InputStream isprv = new ByteArrayInputStream(privKey.getBytes(StandardCharsets.UTF_8));
        // keyParameterPrivate = (RSAPrivateCrtKeyParameters)(RSAKeyParameters)loadPrivateKey(isprv);
        keyParameterPrivate = (AsymmetricKeyParameter)loadPrivateKey(isprv);

        rsaKeyPair = new AsymmetricCipherKeyPair(keyParameterPublic, keyParameterPrivate);
        return getRsaKeyPair();
    }

    /**
     * loadPublicKey
     * @param is {@link InputStream}
     @return {@link AsymmetricKeyParameter}
     */
    protected AsymmetricKeyParameter loadPublicKey(InputStream is) {
        DbgWriter.msg("loadPublicKey(InputStream is);", false);
        DbgWriter.msg("SubjectPublicKeyInfo spki = (SubjectPublicKeyInfo) readPemObject(is);", false);
        SubjectPublicKeyInfo spki = (SubjectPublicKeyInfo) readPemObject(is);
        try {
            DbgWriter.msg("return PublicKeyFactory.createKey(spki);", false);
            return PublicKeyFactory.createKey(spki);
        } catch (IOException ex) {
            throw new RuntimeException("Cannot create public key object based on input data", ex);
        }
    }

    /**
     * loadPrivateKey
     * @param is {@link InputStream}
     * @return {@link AsymmetricKeyParameter}
     */
    protected AsymmetricKeyParameter loadPrivateKey(InputStream is) {
        DbgWriter.msg("loadPrivateKey(InputStream is);", false);
        DbgWriter.msg("Object o = readPemObject(is);", false);
        Object o = readPemObject(is);
        DbgWriter.msg("PrivateKeyInfo pki = ((PrivateKeyInfo)o);", false);
        PrivateKeyInfo pki = ((PrivateKeyInfo)o);
        try {
            DbgWriter.msg("return PrivateKeyFactory.createKey(pki);", false);
            return PrivateKeyFactory.createKey(pki);
        } catch (IOException ex) {
            throw new RuntimeException("Cannot create private key object based on input data", ex);
        }
    }

    /**
     * readPemObject
     * @param is {@link InputStream}
     * @return {@link Object}
     */
    protected Object readPemObject(InputStream is) {
        try {
            // Validate.notNull(is, "Input data stream cannot be null");
            DbgWriter.msg("InputStreamReader isr = new InputStreamReader(is, \"UTF-8\");", false);
            InputStreamReader isr = new InputStreamReader(is, "UTF-8");

            DbgWriter.msg("PEMParser pemParser = new PEMParser(isr);", false);
            PEMParser pemParser = new PEMParser(isr);

            DbgWriter.msg("Object obj = pemParser.readObject();", false);
            Object obj = pemParser.readObject();
            if (obj == null) {
                throw new Exception("No PEM object found");
            }
            return obj;
        } catch (Throwable ex) {
            throw new RuntimeException("Cannot read PEM object from input data", ex);
        }
    }

    /**
     * encrypt calls encryptWithPublicKey
     * @param plainBytes {@link byte[]}
     * @return {@link byte[]}
     */
    public byte[] encrypt(byte[] plainBytes) {
        byte[] encrypted = plainBytes;
        try {
            encrypted = encryptWithPublicKey(plainBytes, RsaCipher.getRsaKeyPair().getPublic());
        } catch (Exception exEncrypt) {
            DbgWriter.msgInfoEx("Rsa encrypt exception", exEncrypt, false);
        }
        return encrypted;
    }

    /**
     * decrypt calls decryptWithPrivateKey
     * @param encryptedBytes
     * @return {@link byte[]}
     */
    public byte[] decrypt(byte[] encryptedBytes) {
        byte[] plainBytes = encryptedBytes;
        try {
            plainBytes =  decryptWithPrivateKey(encryptedBytes, RsaCipher.getRsaKeyPair().getPrivate());
        } catch (Exception exDecrypt) {
            DbgWriter.msgInfoEx("Rsa decrypt exception", exDecrypt, false);
        }
        return plainBytes;
    }




    /**
    * encryptWithPublicKey
    * @param plainBytes
    * @param publicKey
    * @return encrypted :yte[]
    * @throws Exception
    */
    public byte[] encryptWithPublicKey(byte[] plainBytes, AsymmetricKeyParameter publicKey) throws Exception{
//	Source: http://www.cs.berkeley.edu/~jonah/bc/org/bouncycastle/crypto/engines/RSAEngine.html
        Security.addProvider(new BouncyCastleProvider());

        RSAEngine engine = new RSAEngine();
        engine.init(true, publicKey); //true if encrypt

        byte[] hexEncodedCipher = engine.processBlock(plainBytes, 0, plainBytes.length);

        return hexEncodedCipher;
    }

    /**
     * decryptWithPrivateKey
     * @param encryptedBytes
     * @param privateKey
     * @return decrypted bytes
     * @throws InvalidCipherTextException
     */
    public byte[] decryptWithPrivateKey(byte[] encryptedBytes, AsymmetricKeyParameter privateKey) throws InvalidCipherTextException{
//	Source: http://www.mysamplecode.com/2011/08/java-rsa-decrypt-string-using-bouncy.html

        Security.addProvider(new BouncyCastleProvider());

        AsymmetricBlockCipher engine = new RSAEngine();
        engine.init(false, privateKey); //false for decryption

        byte[] decrypted = engine.processBlock(encryptedBytes, 0, encryptedBytes.length);
        return decrypted;
    }

}
