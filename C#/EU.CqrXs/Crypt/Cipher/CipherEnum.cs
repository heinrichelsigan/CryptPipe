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
        Des3Net = 0x1d,
        AesNet = 0x1e,

        ZenMatrix2 = 0x1f,

        Rsa = 0x20
        // Dsa = 0x21,
        // DH = 0x22,
        // Ecdsa = 0x23,
        // Ed25519 = 0x24,
        // Ed448 = 0x25,
        // X25519 = 0x26,
        // X448 = 0x27,
        // GPG = 0x28

        // DH = 0x22,
        // Ascon = 0x1d

    }

}
