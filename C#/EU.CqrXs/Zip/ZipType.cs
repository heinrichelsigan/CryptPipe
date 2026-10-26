using System.ComponentModel;

namespace EU.CqrXs.Zip
{

    [DefaultValue(None)]
    public enum ZipType
    {
        None = 0x00,
        Zip = 0x10,
        GZip = 0x20,
        BZip2 = 0x30,
        Z7 = 0x40
    }


}
