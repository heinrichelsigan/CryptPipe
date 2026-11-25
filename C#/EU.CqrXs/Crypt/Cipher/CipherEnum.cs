using System.ComponentModel;

namespace EU.CqrXs.Crypt.Cipher
{

    /// <summary>
    /// CipherEnum maps BlockCipher algorithms <see cref="Org.BouncyCastle.Crypto.IBlockCipher"/>
    /// </summary>
    [Serializable]
    [DefaultValue("Aes")]
    public enum CipherEnum : byte
    {
        Aes = 0x0, 
        BlowFish = 0x1,
        Camellia = 0x2,
        Cast6 = 0x3,
        Des3 = 0x4,
        Fish2 = 0x5,
        Fish3 = 0x6,
        Gost28147 = 0x7,
        Idea = 0x8,
        RC532 = 0x9,
        Seed = 0xa,
        SkipJack = 0xb,
        Serpent = 0xc,
        Tea = 0xd,
        XTea = 0xe,
        SM4 = 0xf,

        Cast5 = 0x10,
        Rijndael = 0x11,
        Noekeon = 0x12,
        RC2 = 0x13,
        RC564 = 0x14,
        RC6 = 0x15,
        Tnepres = 0x16,
        Des = 0x17,
        Aria = 0x18,
        CamelliaLight = 0x19,
        Dstu7624 = 0x1a,        
        AesLight = 0x1b,
        ZenMatrix = 0x1c,     
                        
        Salsa20 = 0x1d,
        ChaCha = 0x1e,
        ChaCha7539 = 0x1f,
        RC4 = 0x20,
        Isaac = 0x21,
        HC128 = 0x22,
        HC256 = 0x23,
        Vmpc = 0x24,
        VmpcKsa3 = 0x25,
        XChaCha20 = 0x26,
        XSalsa20 = 0x27,

        AES_CCM = 0x28,
        AES_GCM = 0x29,
        ASCON = 0x2a,
        CHACHA_POLY = 0x2b,
        WRAP_KEY_RFC3394 = 0x2c,
        FF3_1_FPE = 0x2d,


        Des3Net = 0x30,
        AesNet = 0x31,
        ZenMatrix2 = 0x32,

        Rsa = 0x40
        // Dsa = 0x41,
        // DH = 0x42,
        // Ecdsa = 0x43,
        // Ed25519 = 0x44,
        // Ed448 = 0x45,
        // X25519 = 0x46,
        // X448 = 0x47,
        // GPG = 0x48


        // Ascon = 0x1d

        // None = 0xff

    }

}
