using System.ComponentModel;

namespace EU.CqrXs.Crypt.EnDeCoding
{
    /// <summary>
    /// EncodingType Enum 
    /// TODO: base58
    /// </summary>
    [DefaultValue(EncodingType.Base64)]
    public enum EncodingType
    {
        None =      0x000,
        Base16 =    0x100,
        Hex16 =     0x101,
        Base32 =    0x200,
        Hex32 =     0x201,
        Uu =        0x400,
        Base64 =    0x401,
        Hex64 =     0x402,        
        Xx =        0x404,
        Ascii85 =   0x550
    }

}
