using System.ComponentModel;
using System.Security.Cryptography;

namespace EU.CqrXs.Crypt.Cipher
{

    /// <summary>
    /// CipherMode2 enumeration type for more cipher mode as found in standard <see cref="System.Security.Cryptography.CipherMode"/>
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <listheader>code changes</listheader>
    /// <item>
    /// 2026-02-11 alert-fix-13 changed mode from "ECB" to "CFB"     
    /// Reason: Git security scans
    /// consequences: no more fully deterministic math bijective proper symmertric cipher en-/decryption in pipe
    /// fixed attacks: not so easy REPLY attacks with binary format header and heuristic key collection
    /// </item>
    /// <item>
    /// 2026-mm-dd [enter pull request name here] [enter what you did here]
    /// Reason: [enter a senseful reason]
    /// consequences: [describe most impactful consequences of bugfix or code change request]
    /// fixed [vulnerability, code smell]: [Describe understandable precise in 1-2 setences]
    /// </item>
    /// </list>
    /// </remarks>
    [Serializable]
    [DefaultValue("ECB")]
    public enum CipherMode2 : byte
    {
        CBC = 0x0,
        CCM = 0x1,
        CFB = 0x2,
        CTS = 0x3,
        EAX = 0x4,
        ECB = 0x5,
        GOFB = 0x6
    }
    
    
}
