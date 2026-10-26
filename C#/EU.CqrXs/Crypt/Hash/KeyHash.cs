using EU.CqrXs.Crypt.Cipher;
using EU.CqrXs.Crypt.EnDeCoding;
using EU.CqrXs.Util;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Utilities.Encoders;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace EU.CqrXs.Crypt.Hash
{

    /// <summary>
    /// KeyHash 
    /// </summary>
    [Serializable]
    [DefaultValue(KeyHash.Empty)]
    public enum KeyHash : short
    {
        Empty = 0x00,
        Hex = 0x01,
        Sha1 = 0x02,
        OpenBSDCrypt = 0x03,
        BCrypt = 0x04,
        SCrypt = 0x05,
        MD5 = 0x06,
        Sha256 = 0x07,
        Sha384 = 0x08,
        Oct = 0x09,
        Sha512 = 0x0a,
        Whirlpool = 0x0b,
        Blake2xs = 0x0c,
        CShake = 0x0d,
        Dstu7564 = 0x0e,
        RipeMD256 = 0x0f,
        TupleHash = 0x11
    }

}
