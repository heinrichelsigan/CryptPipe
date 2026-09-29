using EU.CqrXs.Crypt.Hash;
using System.Configuration;
using System.Text;
using System.Drawing;

namespace EU.CqrXs.Util
{

    /// <summary>
    /// static Constants including static application settings
    /// </summary>
    public static class Constants
    {

        #region public const
#pragma warning disable CA1707 // Identifiers should not contain underscores
        
        public const int BACKLOG = 8;
        public const int CHAT_PORT = 7777;
        public const int MAX_KEY_LEN = 4096;

        public const int PIPE_MAX_LEN = 8; // 0xc; 
        public const int PIPE_IMG_HEIGHT = 108;
        public const int PIPE_IMG_WIDTH = 640; // 960; 
        public const int PIPE_IMG_WIDTH_OFFSET = 60; // 57;
        public const int PIPE_REVERSE_FROM = 7; // 0xb;
        public const int PIPE_KEY_HASH_LEN = 0x10; // 0x20;

        public const bool PIPE_BUILD_MULTI_SAME_CIPHERS = false;

        public const bool CQR_ENCRYPT = true;
        public const bool ZEN_MATRIX_SYMMETRIC = false;
        
        public const char ANNOUNCE = ':';
        public const char DATE_DELIM = '-';
        public const char WHITE_SPACE = ' ';
        public const char UNDER_SCORE = '_';

        public const string APP_NAME = "Area23.At";
        public const string APP_NAME_WINFORM = "Area23.At.WinForm.CryptFormCore";
        public const string APP_NAME_CONSOLE = "EU.CqrXs.Console.exe";
        public const string APP_DIR = "net";
        public const string APP_ERROR = "AppError";
        public const string VERSION = "v2.26.704";
        public const string PIPE_STAGE = "PipeStage";


        public const string AREA23_URL = "https://area23.at";
        public const string APP_PATH = "https://area23.at/net/";
        public const string RPN_URL = "https://area23.at/net/RpnCalc.aspx";
        public const string GIT_URL = "https://github.com/heinrichelsigan/area23.at";
        public const string URL_PIC = "https://area23.at/net/res/img/";
        public const string URL_PREFIX = "https://area23.at/net/res/";
        public const string AREA23_S = "https://area23.at/s/";
        public const string URL_SHORT = "https://area23.at/s/?";
        public const string AREA23_UTF8_URL = "https://area23.at/u/";

        public const string AREA23_AT = "area23.at";
        public const string VIRGINA_AREA23_AT = "virginia.area23.at";
        public const string PARIS_AREA23_AT = "paris.area23.at";
        public const string PARISIENNE_AREA23_AT = "parisienne.area23.at";
        public const string CQRXS_EU = "cqrxs.eu";
        public const string IPV4_CQRXS_EU = "ipv4.cqrxs.eu";
        public const string IPV6_CQRXS_EU = "ipv6.cqrxs.eu";

        public const string SPAIN_CQRXS_EU = "cqrxs.eu";
        public const string ES_CQRXS_EU = "es.cqrxs.eu";
        public const string MADRID_CQRXS_EU = "madrid.cqrxs.eu";
        public const string BARCELONA_CQRXS_EU = "barcelona.cqrxs.eu";


        public const string ALL_KEYS = "AllKeys";
        public const string CHATROOMS = "ChatRooms";
        public const string CQRXS_URL = "https://cqrxs.eu/";
        public const string CQRXS_HELP_URL = "https://cqrxs.eu/help/";
        public const string DECRYPTED_TEXT_AREA = "<textarea cols = \"48\" rows=\"10\" name=\"TextBoxDecrypted\" id=\"TextBoxDecrypted\" title=\"TextBox Current Message\" ValidateRequestMode=\"Enabled\" style=\"width:480px;\" >";
        public const string DECRYPTED_TEXT_BOX = "TextBoxDecrypted";
        public const string DECRYPTED_TEXT_AREA_END = "</textarea>";
        public const string CQRXS_TEST_FORM = "CqrXsTestForm";
        public const string FISH_ON_AES_ENGINE = "FishOnAesEngine";

        public const string ACK = "Ack";
        public const string NACK = "Nack";
        public const string ENTER_SECRET_KEY = "[enter secret key here]";
        public const string ENTER_IP_CONTACT = "[Enter IPv4/IPv6 or select Contact]";
        public const string ENTER_IP = "[Enter peer IPv4/IPv6]";
        public const string ENTER_CONTACT = "[Select Contact]";

        public const string ACCEPT_LANGUAGE = "Accept-Language";
        public const string AES_ENVIROMENT_KEY = "APP_ENCRYPTION_SECRET_KEY";
        public const string AUTHOR = "Heinrich Elsigan";
        public const string AUTHOR_EMAIL = "heinrich.elsigan@area23.at";
        public const string AUTHOR_IV = "6865696e726963682e656c736967616e406172656132332e6174";
        public const string AREA23_EMAIL = "zen@area23.at";
        public const string AUTHOR_SIGNATURE = "-- \nHeinrich G.Elsigan\nTheresianumgasse 6/28, A-1040 Vienna\n phone: +43 650 752 79 28 \nmobile: +43 670 406 89 83 \nemails: heinrich.elsigan @gmail.com\n        heinrich.elsigan@live.at\n        sites: area23.at cqrxs.eu\nweblog: blog.area23.at\n   wko: https://firmen.wko.at/DetailsKontakt.aspx?FirmaID=19800fbd-84a2-456d-890e-eb1fa213100f";

        public const string APP_CONCURRENT_DICT = "APP_CONCURRENT_DICT";
        public const string APP_FIRST_REG = "APP_FIRST_REG";
        public const string APP_TRANSPARENT_BADGE = "APP_TRANSPARENT_BADGE";
        public const string APP_SERVER_KEY = "APP_SERVER_KEY";
        public const string APP_INPUT_DIALOG = "APP_INPUT_DIALOG";
        public const string APP_MY_CONTACT = "APP_MY_CONTACT";

        public const string APP_DIR_PATH_WIN = "AppDirPathWin";
        public const string BASE_APP_PATH_WIN = "BaseAppPathWin";
        public const string APP_DIR_PATH_UNIX = "AppDirPathUnix";
        public const string BASE_APP_PATH_UNIX = "BaseAppPathUnix";

        public const string BIN_DIR = "bin";
        public const string CALC_DIR = "Calc";
        public const string CSS_DIR = "css";
        public const string CRYPT_DIR = "Crypt";
        public const string ENCODE_DIR = "Crypt";
        public const string GAMES_DIR = "Gamez";
        public const string IMG_DIR = "img";
        public const string IMG_FOLDER = "Image";
        public const string JS_DIR = "js";
        public const string JSON_DIR = "json";
        public const string LOG_DIR = "log";
        public const string LOG_EXT = ".log";
        public const string LOG_EXCEPTION_STATIC = "LogExceptionStatic";
        public const string OUT_DIR = "out";
        public const string QR_DIR = "Qr";
        public const string RES_DIR = "res";
        public const string RES_FOLDER = "res";
        public const string TEXT_DIR = "text";
        public const string TMP_DIR = "tmp";
        public const string UNIX_DIR = "Unix";
        public const string UTF8_DIR = "Utf8";
        public const string UU_DIR = "uu";

        public const string OBJ_DIR = "obj";
        public const string RELEASE_DIR = "Release";
        public const string DEBUG_DIR = "Debug";
        public const string NET9_WINDOWS7 = "net9.0-windows7.0";
        public const string NET9_WINDOWS8 = "net9.0-windows8.0";
        public const string NET9_WINDOWS10 = "net9.0-windows10";
        public const string NET9_WINDOWS11 = "net9.0-windows11";
        public const string WIN_X86 = "win-x86";
        public const string WIN_X64 = "win-x86";
        public const string MIME_EXT = ".mime";
        public const string BASE64_EXT = ".base64";
        public const string ATTACH_FILES_DIR = "AttachFiles";
        public const string UPSAVED_FILE = "SavedFile";

        public const string UTF8_JSON = "utf8symol.json";
        public const string JSON_SAVE_FILE = "urlshort.json";
        public const string JSON_APPDICT_FILE = "appdict.json";
        public const string JSON_CONTACTS = "contacts";
        public const string JSON_CONTACTS_FILE = "contacts.json";
        public const string JSON_SETTINGS_FILE = "settings.json";
        public const string CQR_CHAT_FILE = "cqr{0}chat.json";
        public const string PREVIOUS_EXCEPTION = "previous_exception";
        public const string LAST_EXCEPTION = "last_exception";
        public const string COOL_CRYPT_SPLIT = "+-;,:→⇛\t ";
        public const string APPDIRPATHUNIX = "AppDirPathUnix";
        
        public const string UNKNOWN = "UnKnown";
        public const string DEFAULT_MIMETYPE = "application/octet-stream";
        public const string RPN_STACK = "rpnStack";
        public const string CHANGE_CLICK_EVENTCNT = "change_Click_EventCnt";
        public const string BC_START_MSG = "bc 1.07.1\r\nCopyright 1991-1994, 1997, 1998, 2000, 2004, 2006, 2008, 2012-2017 Free Software Foundation, Inc.\r\nThis is free software with ABSOLUTELY NO WARRANTY.\r\nFor details type `warranty'.\r\n";

        public const string BACK_COLOR = "BackColor";
        public const string QR_COLOR = "QrColor";
        public const string BACK_COLOR_STRING = "BackColorString";
        public const string QR_COLOR_STRING = "QrColorString";
        public const string IMAGE_UPLOAD_CLICK = "click_here_to_upload";
        public const string IMAGE_UPLOAD_EXTENSION = ".png";

        public const string ROACH_DESKTOP_WINDOW = "Roach.Desktop.Window";
        public const string MUTEX_REGOPS = "Mutex.Registry.Operations";

        public const string EXE_COMMAND_CMD = "cmd";
        public const string EXE_POWER_SHELL = "powershell";

        public const string EXE_WIN_INIT = "wininit";
        public const string EXE_SERVICES = "services";
        public const string EXE_SVC_HOST = "svchost";
        public const string EXE_TASK_HOST = "taskhostw";
        public const string EXE_DLL_HOST = "dllhost";
        public const string EXE_SCHEDULER = "scheduler";
        public const string EXE_VM_COMPUTE = "vmcompute";
        public const string EXE_WIN_DEFENDER = "MsMpEng";
        public const string EXE_LASS = "lsass";                     // local Security Authority Subsystem Service. 
        public const string EXE_CSRSS = "csrss";                    // hosts the server side of the Win32 subsystem

        public const string EXE_WIN_LOGON = "winlogon";             // windows logon handler for current logon
        public const string EXE_DESKTOP_WINDOW_MANAGER = "dwm";     // window manager for current logon

        public const string STRING_EMPTY = "";
        public const string STRING_NULL = null;
        public const string SNULL = "(null)";

        
        public const string RSA_PUB = "-----BEGIN PUBLIC KEY-----\n" +
            "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDERBy7FD7m9dq1Nu90B5U63uMl\n" +
            "LIGbxU90dGQ6U7QmjsK1Lyuc5ma941EjaNWPvIfyBkJZc9ij6/9buv12naHg1K6P\n" +
            "6CBycNKd2l1N5/XG3IKTHomgFQKrMX90KI7F772caVDFDzi4N5XjqG8HMVAjPL1w\n" +
            "tqelayYErbUrJMzRRwIDAQAB\n" +
            "-----END PUBLIC KEY-----";

        public const string RSA_PRV = "-----BEGIN PRIVATE KEY-----\n" +
            "MIICdwIBADANBgkqhkiG9w0BAQEFAASCAmEwggJdAgEAAoGBAMREHLsUPub12rU2\n" +
            "73QHlTre4yUsgZvFT3R0ZDpTtCaOwrUvK5zmZr3jUSNo1Y+8h/IGQllz2KPr/1u6\n" +
            "/XadoeDUro/oIHJw0p3aXU3n9cbcgpMeiaAVAqsxf3QojsXvvZxpUMUPOLg3leOo\n" +
            "bwcxUCM8vXC2p6VrJgSttSskzNFHAgMBAAECgYBo0l/t4sA9pi0q/64f8DTZflUe\n" +
            "c4i9Y0IuVkN5i176AOEo14qZf8x7uj6JhOIapHyO3JzvHZok4lQ9776TbVkYxCVh\n" +
            "EP6XlGTCOc7urCYghNkw8URNXZDVNUnrsPo6ge0l/MsySHIcZFUHuYjy8zPG2x5c\n" +
            "KfRhwW4n40BJyn80UQJBAPwvTyu1pkxPjvoleKN1NvKbCXR+Haf+mKzQQdljV2Vw\n" +
            "86rTv05uFpntCsR5rvPdIWRMcN6xPwle13vZbjaA2+sCQQDHPDv+b0TyJWvjtKXl\n" +
            "I10tdRtD7yvB6fEI1nM/9RfxYjTZVhNYScE83z9TCVZWWt77JOWC6bDHrOy8ExTs\n" +
            "8ZUVAkA2KgMbJDy/jybqWzn6Aab3nIz/VEcSWgB4vZInGsseoo/zVN91/PclwF/b\n" +
            "qzcEca5GWJS1f+RGIvStSRn+4tZZAkEAhl94sxUGsi5NAumuzck5KdSGzB2+LG4E\n" +
            "9An29xbtzA6JSGAGchBkdRK42d89TMbDBy2OYeoNIc7eZ8aS4W/aNQJBAI0pZHau\n" +
            "UqtKUFBoMzn3qQXZubzV4oXAhHQe3BT4riGYbpiVdfTuiQxcYZE9Kfkn7orQgMJv\n" +
            "rca/fxpvMlzLUR0=\n" +
            "-----END PRIVATE KEY-----\n";


#pragma warning restore CA1707 // Identifiers should not contain underscores
        #endregion public const

        #region public static readonly fields

        public static readonly char SEP_CHAR = Path.DirectorySeparatorChar;

        public static readonly string AES_KEY = "AES_KEY";
        public static readonly string AES_IV = "AES_IV";
        public static readonly string DES3_KEY = "DES3_KEY";
        public static readonly string DES3_IV = "DES3_IV";
        public static readonly string BOUNCEK = Convert.ToBase64String(Encoding.UTF8.GetBytes("BOUNCE"));
        public static readonly string BOUNCE4 = KeyHash.SCrypt.Hash(BOUNCEK);


        public static readonly string[] EXE_WIN_SYSTEM = { EXE_WIN_INIT, EXE_SERVICES,
            EXE_SVC_HOST, EXE_TASK_HOST, EXE_DLL_HOST,
            EXE_SCHEDULER, EXE_VM_COMPUTE, EXE_WIN_DEFENDER, EXE_LASS, EXE_CSRSS,
            EXE_WIN_LOGON, EXE_DESKTOP_WINDOW_MANAGER
        };

        public static readonly string[] DENIED_EXTENSIONS = {
            ".asp", ".asax", ".aspx", ".ascx", ".asmx", ".ashx", ".svc", ".master", ".config",
            ".php", ".js", ".html", ".xhtml", ".htm",
            ".razor", ".cshtml", ".javascript", ".cgi"
        };


        public static readonly string[] ALLOWED_EXTENSIONS = {

            ".base", ".hex",
            ".hex16", ".base16", ".base32", ".hex32", ".uu", ",base58", ".base64", ".mime",

            ".md", ".txt", ".text", ".cfg",
            ".css", ".js", ".htm", ".html", ".xhtml", ".json", ".rdf",

            ".avif", ".bmp", ".exif", ".gif", ".ico", ".ief", ".jpg", ".jpeg", ".pcx", ".pic", ".png", ".psd", ".tif", ".xcf", ".xif",
            ".3pg", ".3g2", ".aif", ".au", ".m3u", ".mid", ".midi", ".mp4", ".mpeg", ".ogg", ".webm", ".wav", ".wax", ".wma", ".mp3",
            ".avi", ".f4v", ".flx", ".m4u", ".m4v", ".mov", ".mpg", ".wmv",

            ".pdf", ".ps", ".gs", ".dvi", ".tex",
            ".ods", ".odt", ".rtf", ".doc", ".dot", ".xls", ".xlt", ".csv", ".mdb", ".ppt", ".vsx", ".vst", ".mpp",

            ".ttf", ".woff",

            ".eml", ".mbox", ".vcs", ".vcf", ".msg",

            ".zip",
            ".z", ".gz", ".bz", ".bz2", ".tar", ".tgz", ".tbz",
            ".arj", ".arc", ".rar",
            ".7z", ".xz",


            ".pki", ".cer", ".der", ".crl", ".p10", ".p7c", ".p7s",

            ".exe", ".dll", ".oct", ".bin", ".tmp", ".img"
        };

        #endregion public static readonly fields

        #region public static properties

        private static bool _unix = false;
        public static bool UNIX
        {
            get
            {
                if (_unix)
                    return _unix;

                string pathUnix = "";

                if (ConfigurationManager.AppSettings[Constants.APPDIRPATHUNIX] != null)
                    pathUnix = ConfigurationManager.AppSettings[Constants.APPDIRPATHUNIX];

                _unix = AppDomain.CurrentDomain.BaseDirectory.ToString().Contains("/") &&
                            !AppDomain.CurrentDomain.BaseDirectory.ToString().Contains("\\")
                        || Directory.Exists(pathUnix);

                return _unix;
            }
        }

        private static bool _win32 = false;

        public static bool WIN32
        {
            get
            {
                if (_win32)
                    return _win32;

                string pathWin32 = "";

                if (ConfigurationManager.AppSettings["AppDirPathWin"] != null)
                    pathWin32 = ConfigurationManager.AppSettings["AppDirPathWin"];

                _win32 = AppDomain.CurrentDomain.BaseDirectory.Contains("\\") &&
                            !AppDomain.CurrentDomain.BaseDirectory.Contains("/")
                        || Directory.Exists(pathWin32);

                return _win32;
            }
        }


        public static bool NOLog { get; set; } = false;

        public static bool DirCreate { get; set; } = true;

        /// <summary>
        /// AppLogFile - logfile with <see cref="EU.CqrXs.Util.Extensions.Area23Date(DateTime)"/> prefix
        /// </summary>
        public static string AppLogFile { get => DateTime.UtcNow.Area23Date() + UNDER_SCORE + APP_NAME + LOG_EXT; }


        public static string Json_Example { get => ResReader.GetValue("json_sample0"); }

        private static System.Globalization.CultureInfo locale = null;
        private static string defaultLang = null;

        /// <summary>
        /// Culture Info from HttpContext.Current.Request.Headers[ACCEPT_LANGUAGE]
        /// </summary>
        public static System.Globalization.CultureInfo Locale
        {
            get
            {
                if (locale == null)
                {
                    defaultLang = "en";
                    locale = new System.Globalization.CultureInfo(defaultLang);
                }
                return locale;
            }
        }

        public static string ISO2Lang { get => Locale.TwoLetterISOLanguageName; }

        /// <summary>
        /// UT DateTime @area23.at including seconds
        /// </summary>
        public static string DateArea23Seconds { get => DateTime.UtcNow.ToString("yyyy-MM-dd_HH:mm:ss"); }

        /// <summary>
        /// UTC DateTime Formated
        /// </summary>
        public static string DateArea23
        {
            get => DateTime.UtcNow.ToString("yyyy") + DATE_DELIM +
                DateTime.UtcNow.ToString("MM") + DATE_DELIM +
                DateTime.UtcNow.ToString("dd") + WHITE_SPACE +
                DateTime.UtcNow.ToString("HH") + ANNOUNCE +
                DateTime.UtcNow.ToString("mm") + ANNOUNCE + WHITE_SPACE;
        }

        /// <summary>
        /// UTC DateTime File Prefix
        /// </summary>
        public static string DateFile { get => DateArea23.Replace(WHITE_SPACE, UNDER_SCORE).Replace(ANNOUNCE, UNDER_SCORE); }

        private static readonly string backColorString = "#ffffff";
        public static string BackColorString
        {

            get => (AppDomain.CurrentDomain.GetData(BACK_COLOR_STRING) != null) ? (string)AppDomain.CurrentDomain.GetData(BACK_COLOR_STRING) : backColorString;
            set
            {
                AppDomain.CurrentDomain.SetData(BACK_COLOR, Utils.FromHtml(value));
                AppDomain.CurrentDomain.SetData(BACK_COLOR_STRING, value);
            }
        }

        private static readonly string qrColorString = "#000000";
        public static string QrColorString
        {
            get => (AppDomain.CurrentDomain.GetData(QR_COLOR_STRING) != null) ? (string)AppDomain.CurrentDomain.GetData(QR_COLOR_STRING) : qrColorString;
            set
            {
                AppDomain.CurrentDomain.SetData(QR_COLOR, Utils.FromHtml(value));
                AppDomain.CurrentDomain.SetData(QR_COLOR_STRING, value);
            }
        }

        public static Color BackColor
        {
            get => (AppDomain.CurrentDomain.GetData(BACK_COLOR) != null) ? (Color)AppDomain.CurrentDomain.GetData(BACK_COLOR) : Utils.FromHtml(backColorString);
            set
            {
#pragma warning disable CS8073 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (value != null)
                {
                    AppDomain.CurrentDomain.SetData(BACK_COLOR_STRING, value.ToXrgb());
                    AppDomain.CurrentDomain.SetData(BACK_COLOR, value);
                }
                else
                {
                    AppDomain.CurrentDomain.SetData(BACK_COLOR_STRING, backColorString);
                    AppDomain.CurrentDomain.SetData(BACK_COLOR, Utils.FromHtml(backColorString)); 
                }
#pragma warning restore CS8073 // The result of the expression is always the same since a value of this type is never equal to 'null'
            }
        }

        public static Color QrColor
        {
            get => (AppDomain.CurrentDomain.GetData(BACK_COLOR) != null) ? (Color)AppDomain.CurrentDomain.GetData(QR_COLOR) : Utils.FromHtml(qrColorString);
            set
            {
#pragma warning disable CS8073 // The result of the expression is always the same since a value of this type is never equal to 'null'
                if (value != null)
                {
                    AppDomain.CurrentDomain.SetData(QR_COLOR_STRING, value.ToXrgb());
                    AppDomain.CurrentDomain.SetData(QR_COLOR, value);
                }
                else
                {
                    AppDomain.CurrentDomain.SetData(QR_COLOR_STRING, qrColorString);
                    AppDomain.CurrentDomain.SetData(QR_COLOR, Utils.FromHtml(qrColorString));
                }
#pragma warning restore CS8073 // The result of the expression is always the same since a value of this type is never equal to 'null'
            }
        }

        private static bool _fortuneBool = false;
        public static bool FortuneBool
        {
            get
            {
                _fortuneBool = !_fortuneBool;
                return _fortuneBool;
            }
        }

        public static bool RandomBool { get => DateTime.Now.Millisecond % 2 == 0; }

        #endregion public static properties

        /// <summary>
        /// AppSettingsValueByKey 
        /// </summary>
        /// <param name="key">key to lookup up in AppSettings key value collection</param>
        /// <returns><see cref="string"/> AppSettingsValue</returns>
        public static string AppSettingsValueByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            return (ConfigurationManager.AppSettings[key] != null)
                ? ConfigurationManager.AppSettings[key].ToString()
                : null;
        }


    }

}