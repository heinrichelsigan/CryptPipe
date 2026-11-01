using System.Security.Cryptography;

namespace EU.CqrXs.Crypt.Cipher
{

    /// <summary>
    /// Extension methods for <see cref="CipherMode2">enum CipherMode2</see>
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
    public static class CipherModeExtensions
    {
        
        public static readonly CipherMode defaultCipherMode = CipherMode.ECB;
        public static readonly CipherMode2 defaultCipherMode2 = CipherMode2.ECB;
        
        /// <summary>
        /// Extension method for <see cref="T:CipherMode2"/> to convert
        /// <see cref="T:CipherMode2"/> tp <see cref="T:CipherMode"/> 
        /// </summary>
        /// <param name="mode"><see cref="T:CipherMode2"/></param>
        /// <returns><see cref="T:CipherMode"/></returns>
        public static CipherMode ToCipherMode(this CipherMode2 mode)
        {
            return mode switch
            {
                CipherMode2.CBC => CipherMode.CBC,
                CipherMode2.CCM => CipherMode.CBC,
                CipherMode2.CFB => CipherMode.CFB,
                CipherMode2.CTS => CipherMode.CTS,                
                CipherMode2.EAX => CipherMode.CBC,
                CipherMode2.ECB => CipherMode.ECB,
                CipherMode2.GOFB => CipherMode.OFB,
                _ => defaultCipherMode,
            };
        }

        public static CipherMode2 FromCipherMode(this CipherMode mode)
        {
            return mode switch
            {
                CipherMode.CBC => CipherMode2.CBC,
                CipherMode.CFB => CipherMode2.CFB,
                CipherMode.CTS => CipherMode2.CTS,
                CipherMode.ECB => CipherMode2.ECB,
                CipherMode.OFB => CipherMode2.GOFB,
                _ => defaultCipherMode2,
            };
        }

        public static CipherMode[] GetCipherModes(bool working = false)
        {
            List<CipherMode> list = new List<CipherMode>();
            foreach (string encName in Enum.GetNames(typeof(CipherMode)))
            {
                CipherMode cmode = (CipherMode)Enum.Parse(typeof(CipherMode), encName);
                if (working && cmode != CipherMode.CTS && cmode != CipherMode.OFB)
                    list.Add(cmode);
            }

            return list.ToArray();
        }

        public static string GetCipherMode2Extension(this CipherMode2 cmode2)
        {
            switch (cmode2)
            {
                case CipherMode2.CBC: return ".CBC";
                case CipherMode2.CCM: return ".CCM";
                case CipherMode2.CFB: return ".CFB";
                case CipherMode2.CTS: return ".CTS";
                case CipherMode2.EAX: return ".EAX";
                case CipherMode2.ECB: return ".ECB";
                case CipherMode2.GOFB: return ".GOFB";
                default: break;
            }
            
            return "." + cmode2.ToString();
        }
        
        public static CipherMode2[] GetCipherModes2(bool working = false)
        {
            List<CipherMode2> list = new List<CipherMode2>();
            foreach (string encName in Enum.GetNames(typeof(CipherMode2)))
            {
                CipherMode2 cmode2 = (CipherMode2)Enum.Parse(typeof(CipherMode2), encName);
                if (working &&   
                        cmode2 != CipherMode2.CCM && cmode2 != CipherMode2.CTS &&
                        cmode2 != CipherMode2.EAX && cmode2 != CipherMode2.GOFB)
                    list.Add(cmode2);
            }

            return list.ToArray();
        }
        
        /// <summary>
        /// parses pipe semicolon separated pipe string to CipherList
        /// </summary>
        /// <param name="text">semicolon separated pipe string to CipherList </param>
        /// <returns><see cref="T:CipherMode2"/> array of ciphers for the pipe</returns>
        public static CipherMode2 ParseText(string text)
        {
            CipherMode2 cipherMode2 = defaultCipherMode2;
            List<CipherMode2> cipherList = new List<CipherMode2>();
            text = text ?? "";

            if (Enum.TryParse<CipherMode2>(text, out cipherMode2))
                return cipherMode2;
            
            if (text.Contains("CBC"))
                return CipherMode2.CBC;
            if (text.Contains("CFB")) 
                return CipherMode2.CFB;
            if (text.Contains("ECB")) 
                return CipherMode2.ECB;
            if (text.Contains("CTS"))
                return CipherMode2.CTS;
            if (text.Contains("EAX"))
                return CipherMode2.EAX;
            if (text.Contains("OFB"))
                return CipherMode2.GOFB;
            
            return defaultCipherMode2;
        }

    }

}
