/**
 * @author           <a href="mailto:heinrich.elsigan@cqrxs.eu">Heinrich Elsigan</a>
 * @version          V 2.26.428
 * @since            API 27 Oreo 8.1
 *
 * Coded 2021-2033 by <a href="mailto:he@area23.at">Heinrich Elsigan</a>
 * <a href="https://heinrichelsigan.area23.at">heinrichelsigan.area23.at</a>
 */

package eu.cqrxs.crypt.cipher;

import eu.cqrxs.util.Constants;

import javax.crypto.Cipher;
import java.io.Serializable;
import java.lang.String;
import java.util.ArrayList;
import java.util.EnumSet;
import java.util.HashMap;
import java.util.List;
import java.util.Comparator;
import java.util.Collections;
import java.util.Set;


/**
 * CipherEnum represents the enumerator for all cipher algorithms
 * implements Serializable
 */
public enum CipherEnum  {
    Aes(0x0),
    BlowFish(0x1),
    Camellia(0x2),
    Cast6(0x3),
    Des3(0x4),
    Fish2(0x5),
    Fish3(0x6),
    Gost28147(0x7),
    Idea(0x8),
    RC532(0x9),
    Seed(0xa),
    SkipJack(0xb),
    Serpent(0xc),
    Tea(0xd),
    XTea(0xe),
    SM4(0xf),

    Cast5(0x10),
    Rijndael(0x11),
    Noekeon(0x12),
    RC2(0x13),
    RC564(0x14),
    RC6(0x15),
    Tnepres(0x16),
    Des(0x17),
    Aria(0x18),
    CamelliaLight(0x19),
    Dstu7624(0x1a),
    AesLight(0x1b),
    ZenMatrix(0x1c),

    ZenMatrix3(0x1d),
    JAes(0x1e),
    ZenMatrix2(0x1f),

    Salsa20(0x20),
    ChaCha( 0x21),
    ChaCha7539(0x22),
    RC4 ( 0x23),
    Isaac (0x24),
    HC256 (0x25),
    HC128 ( 0x26),
    Vmpc ( 0x27),
    VmpcKsa3 ( 0x28),
    XChaCha20 ( 0x29),
    XSalsa20 ( 0x2a),

    Rsa(0x30)
    /* DH(0x22) */
    ;

    /**
     * NOTE: Enum constructor must have private or package scope. You can not use the public access modifier.
     */
    CipherEnum(int value) {
        this.value = value;
    }

    private final int value;

    /**
     * getValue
     * @return (@link int) value
     */
    public int getValue() {
        return value;
    }

    /**
     * getByteValue
     * @return value casted as {@link byte}
     */
    public byte getByteValue() {
        return ((byte) value);
    }

    /**
     * getCipherChar
     * @return upper letter char
     */
    public char getCipherChar() {
        int xvalue = this.getValue();
        CipherEnum cipher = getCipherEnum(xvalue);
        switch (cipher) {
            case Aes:           return 'A';    // Aes
            case AesLight:      return 'L';    // AesLight
            case Rijndael:      return 'j';    // Rijndael

            case Aria:          return 'a';    // Aria

            case BlowFish:      return 'b';    // BlowFish
            case Fish2:         return 'f';    // Fish2
            case Fish3:         return 'F';    // Fish3

            case Camellia:      return 'C';    // Camellia
            case CamelliaLight: return 'l';    // CamelliaLight
            case Cast5:         return 'c';    // Casz5
            case Cast6:         return '6';    // Cast6

            case Des:           return '$';    // Des
            case Des3:          return 'D';    // Des3

            case Dstu7624:      return 'd';     // Dstu7624

            case Gost28147:     return 'g';    // Gost28147
            case Idea:          return 'I';    // Idea
            case Noekeon:       return 'N';    // Noekeon

            case RC2:           return '2';    // RC2
            case RC532:         return '5';    // RC532
            case RC564:         return 'R';    // RC564
            case RC6:           return 'r';    // RC6

            case Seed:          return 's';    // Seed
            case Serpent:       return 'S';    // Serpent
            case SM4:           return '4';    // SM4
            case SkipJack:      return 'J';    // SkipJack

            case Tea:           return 't';    // Tea
            case Tnepres:       return 'T';    // Tnepres
            case XTea:          return 'X';    // XTea

            case ZenMatrix:     return 'z';    // ZenMatrix
            case ZenMatrix3:    return 'U';    // ZenMatrix3
            case JAes:          return 'E';    // JAes
            case ZenMatrix2:    return 'Z';    // ZenMatrix2

            case ChaCha:        return '0';
            case ChaCha7539:    return '7';
            case Salsa20:       return '8';
            case XChaCha20:     return '9';
            case XSalsa20:      return 'x';
            case HC128:         return 'h';
            case HC256:         return 'H';
            case Isaac:         return 'i';
            case RC4:           return 'B';
            case Vmpc:          return 'v';
            case VmpcKsa3:      return 'V';

            case Rsa:           return '%';        // RSA asymmetric cipher

            default:
                break;            // Aes
        }
        return 'A';    // Aes
    }

    /**
     * fromString
     * @param algo cipher algorithm
     * @return {@link CipherEnum}
     */
    public static CipherEnum fromString(String algo) {
        String alg = (algo != null && algo.length() > 0) ? algo : "Aes";
        CipherEnum cEnum = CipherEnum.Aes;
        try {
            cEnum = CipherEnum.valueOf(CipherEnum.class, alg);
        } catch (Exception exEnum) {
            cEnum = CipherEnum.Aes;
        }
        return cEnum;
    }


    /**
     * getChar
     * @return (by default upper case) letter of char
     */
    public char getChar() {
        return getCipherChar();
    }


    /**
     * getCipherEnums
     * @return Set{CipherEnum}
     */
    public static Set<CipherEnum> getCipherEnums() {
        Set<CipherEnum> allElementsInCipherEnum = EnumSet.allOf(CipherEnum.class);
        // allElementsInCipherEnum.stream().sorted().collect(Collectors.toList());
        return allElementsInCipherEnum;
    }


    /**
     * getName
     * @return name of enum
     */
    public String getName() {
        int xvalue = this.getValue();
        CipherEnum cipher = getCipherEnum(xvalue);
        return cipher.toString();
    }


    /**
     * getNames
     * @return all CipherEnum names
     */
    public static String[] getNames() {
        int cnt = 0;
        List<String> cnames = new ArrayList<String>();
        for (CipherEnum cipherEnum : CipherEnum.values()) {
            cnames.add(cipherEnum.getName());
            cnt++;
        }
        Collections.sort(cnames, new Comparator<String>() {
            @Override
            public int compare(String s0, String s1) {
                return s0.compareToIgnoreCase(s1);
            }
        });

        return cnames.toArray(new String[cnt]);
    }

    /***
     * getByteCipherDict()
     * @return a HashMap Map<byte,CipherEnum>
     */
    public static HashMap<Byte, CipherEnum> getByteCipherDict() {

        HashMap<Byte, CipherEnum> map = new HashMap<Byte, CipherEnum>();
        for (CipherEnum cipherEnum : CipherEnum.values()) {
            Byte b = Byte.valueOf(((byte) cipherEnum.getByteValue()));
            map.put(b, cipherEnum);
        }
        return map;
    }


    /***
     * parsePipeText parses a ;, concatenated string into it's parts and converts
     * substrings to CipherEnums
     * @param pipeText concatenated pipe text
     * @return an arreay of CipherEnum
     */
    public static CipherEnum[] parsePipeText(String pipeText) {
        CipherEnum cipher = CipherEnum.Aes;
        List<CipherEnum> cipherList = new ArrayList<CipherEnum>();
        pipeText = (pipeText == null) ? "" : pipeText;

        int pipeCnt = 0;
        String[] algos = pipeText.split(Constants.COOL_CRYPT_SPLIT);
        for (String algo : algos) {
            cipher = CipherEnum.fromString(algo);
            cipherList.add(cipher);
            if ((++pipeCnt) >= 8)
                break;
        }

        CipherEnum[] cipherEnums = new CipherEnum[cipherList.size()];
        for (int ci = 0; ci < cipherList.size(); ci++) {
            cipherEnums[ci] = cipherList.get(ci);
        }
        return cipherEnums;
    }

    /**
     * getEnum
     * @param ch column character
     * @return the enum {@link CipherEnum}
     */
    public static CipherEnum getEnum(char ch) {
        for (CipherEnum cipherEnum : CipherEnum.values()) {
            if (cipherEnum.getChar() == ch)
                return cipherEnum;
        }
        return CipherEnum.Aes;
    }

    /**
     * getEnum
     * @param ciphername {@link String} of cipher enum
     * @return {@link CipherEnum}
     */
    public static CipherEnum getEnum(String ciphername) {
        for (CipherEnum cipherEnum : CipherEnum.values()) {
            if (cipherEnum.getName() == ciphername)
                return cipherEnum;
        }
        return CipherEnum.Aes;
    }

    /**
     * getCipherEnum
     * @param cvalue byte value
     * @return the enum {@link CipherEnum}
     */
    public static CipherEnum getCipherEnum(int cvalue) {
        for (CipherEnum cipherEnum : CipherEnum.values()) {
            if (cipherEnum.getByteValue() == cvalue)
                return cipherEnum;
        }
        return CipherEnum.Aes;
    }

}

