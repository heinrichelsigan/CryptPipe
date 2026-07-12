using EU.CqrXs.Crypt.Hash;
using System.Configuration;
using System.Text;

namespace EU.CqrXs.Util
{

    /// <summary>
    /// static Constants including static application settings
    /// </summary>
    public static class Constants
    {

        #region public const
#pragma warning disable CA1707 // Identifiers should not contain underscores
        
        public const int PIPE_MAX_LEN = 8; // 0xc; 
        public const int PIPE_IMG_HEIGHT = 108;
        public const int PIPE_IMG_WIDTH = 640; // 960; 
        public const int PIPE_IMG_WIDTH_OFFSET = 60; // 57;
        public const int PIPE_REVERSE_FROM = 7; // 0xb;
        public const int PIPE_KEY_HASH_LEN = 0x10; // 0x20;

        public const bool PIPE_BUILD_MULTI_SAME_CIPHERS = false;

        public const bool CQR_ENCRYPT = true;
        public const bool ZEN_MATRIX_SYMMETRIC = false;

        #region obsolete cqrjd constants

        public const int BACKLOG = 8;
        public const int CHAT_PORT = 7777;
        public const int MAX_KEY_LEN = 4096;
      
        public const int MAX_SERVER_SOCKET_ADDRESSES = 16;
        public const int CLOSING_TIMEOUT = 6000;
        public const int MIN_SOCKET_BYTE_BUFFEER = 65536;       // 64 KB Buffer
        public const int SOCKET_BYTE_BUFFEER = 1048576;         //  1 MB Buffer
        public const int MAX_BYTE_BUFFEER = 4194240;            //  4 MB Buffer
        public const int MAX_SOCKET_BYTE_BUFFEER = 33554432;    //  32 MB Buffer  2^25
        public const int BGWORKWE_BUSYWAITING_SLEEP = 360000;

        #endregion obsolete cqrjd constants

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
        public const string VALKEY_CACHE_HOST = "cqrcachecqrxseu-53g0xw.serverless.eus2.cache.amazonaws.com";
        public const int VALKEY_CACHE_PORT = 6379;
        public const string VALKEY_CACHE_HOST_PORT = "cqrcachecqrxseu-53g0xw.serverless.eus2.cache.amazonaws.com:6379";
        public const string VALKEY_CACHE_HOST_PORT_KEY = "ValkeyCacheHostPort";
        public const string EXTERNAL_CLIENT_IP = "ExternalClientIP";
        public const string EXTERNAL_CLIENT_IP_V4 = "ExternalClientIPv4";
        public const string SERVER_IP_V4 = "ServerIPv4";
        public const string SERVER_IP_V6 = "ServerIPv6";
        public const string CQR_SERVICE_SOAP = "CqrServiceSoap";
        public const string CQR_SERVICE_SOAP12 = "CqrServiceSoap12";
        public const string CQR_SRV_SOAP = "CqrSrvSoap";
        public const string CQR_SRV_SOAP12 = "CqrSrvSoap12";


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

        public const string IT_CQRXS_EU = "it.cqrxs.eu";
        public const string MILAN_CQRXS_EU = "milan.cqrxs.eu";
        public const string SICILIENNE_CQRXS_EU = "sicilienne.cqrxs.eu";


        public const string FR_CQRXS_EU = "fr.cqrxs.eu";
        public const string PARIS_CQRXS_EU = "paris.cqrxs.eu";
        public const string PARISIENNSE_CQRXS_EU = "parisienne.cqrxs.eu";

        public const string DE_CQRXS_EU = "de.cqrxs.eu";
        public const string FRANKFURT_CQRXS_EU = "frankfurt.cqrxs.eu";
        public const string BERLINERIN_CQRXS_EU = "berlinerin.cqrxs.eu";

        public const string SE_CQRXS_EU = "se.cqrxs.eu";
        public const string STOCKHOLM_CQRXS_EU = "stockholm.cqrxs.eu";

        public const string IE_CQRXS_EU = "ie.cqrxs.eu";
        public const string DUBLIN_CQRXS_EU = "dublin.cqrxs.eu";
        public const string GALWAY_CQRXS_EU = "galway.cqrxs.eu";

        public const string UK_CQRXS_EU = "uk.cqrxs.eu";
        public const string LONDON_CQRXS_EU = "london.cqrxs.eu";
        public const string EDINBURGH_CQRXS_EU = "edinburgh.cqrxs.eu";

        public const string CH_CQRXS_EU = "ch.cqrxs.eu";
        public const string ZURICH_CQRXS_EU = "zurich.cqrxs.eu";
        public const string BERNERIN_CQRXS_EU = "bernerin.cqrxs.eu";


        public const string ALL_KEYS = "AllKeys";
        public const string CHATROOMS = "ChatRooms";
        public const string CQRXS_URL = "https://cqrxs.eu/";
        public const string CQRXS_HELP_URL = "https://cqrxs.eu/help/";
        public const string DECRYPTED_TEXT_AREA = "<textarea cols = \"48\" rows=\"10\" name=\"TextBoxDecrypted\" id=\"TextBoxDecrypted\" title=\"TextBox Current Message\" ValidateRequestMode=\"Enabled\" style=\"width:480px;\" >";
        public const string DECRYPTED_TEXT_BOX = "TextBoxDecrypted";
        public const string DECRYPTED_TEXT_AREA_END = "</textarea>";
        public const string CQRXS_TEST_FORM = "CqrXsTestForm";
        public const string FISH_ON_AES_ENGINE = "FishOnAesEngine";
        public const string CQRXS_DELETE_DATA_ON_CLOSE = "CqrXsDeleteDataOnClose";
        public const string PERSIST_MSG_IN = "PersistMsgIn";
        public const string PERSIST_MSG_IN_APPLICATION_STATE = "ApplicationState";
        public const string PERSIST_MSG_IN_AMAZON_ELASTIC_CACHE = "AmazonElasticCache";
        public const string PERSIST_MSG_IN_FILE_SYSTEM = "FileSystem";

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


        public const string JSON_SAMPLE = @"{ 
 	""quiz"": { 
 		""sport"": { 
 			""q1"": { 
 				""question"": ""Which one is correct team name in NBA?"", 
 					""options"": [ 
 						""New York Bulls"", 
 							""Los Angeles Kings"", 
 							""Golden State Warriros"", 
 							""Huston Rocket"" 
 						], 
 					""answer"": ""Huston Rocket"" 
 				} 
 			}, 
 		""maths"": { 
 			""q1"": { 
 				""question"": ""5 + 7 = ?"", 
 					""options"": [ 
 						""10"", 
 						""11"", 
 						""12"", 
 						""13"" 
 					], 
 					""answer"": ""12"" 
				}, 
 			""q2"": { 
 				""question"": ""12 - 8 = ?"", 
 				""options"": [ 
 						""1"", 
 						""2"", 
 						""3"", 
 						""4"" 
 						], 
 					""answer"": ""4"" 
 				}, 
 		} 
 	} 
 }";

        public const string XML_SAMPLE = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<ns2:Invoice xmlns=""http://www.w3.org/2000/09/xmldsig#"" xmlns:ns2=""http://www.ebinterface.at/schema/4p1/"" xmlns:ns3=""http://www.ebinterface.at/schema/4p1/extensions/sv"" xmlns:ns4=""http://www.ebinterface.at/schema/4p1/extensions/ext"" ns2:GeneratingSystem=""AUSTRIAPRO.ebInterface.Formular"" ns2:DocumentType=""Invoice"" ns2:InvoiceCurrency=""EUR"" ns2:ManualProcessing=""false"" ns2:DocumentTitle=""20240808"" ns2:Language=""deu"">
    <ns2:InvoiceNumber>20240808</ns2:InvoiceNumber><ns2:InvoiceDate>2024-08-08</ns2:InvoiceDate>
    <ns2:Delivery><ns2:Date>2024-08-08</ns2:Date></ns2:Delivery>
    <ns2:Biller>
        <ns2:VATIdentificationNumber>ATU72804824</ns2:VATIdentificationNumber>
        <ns2:Address>   
                <ns2:AddressIdentifier ns2:AddressIdentifierType=""GLN"">9110005479907</ns2:AddressIdentifier>
            <ns2:Name>Heinrich Georg Elsigan</ns2:Name>
            <ns2:Street>Theresianumgasse 6/28</ns2:Street>
            <ns2:Town>Wien</ns2:Town>
            <ns2:ZIP>1040</ns2:ZIP>
            <ns2:Country>AT</ns2:Country>
            <ns2:Phone>+43 650 7527928</ns2:Phone>
            <ns2:Email>office.area23@gmail.com</ns2:Email>
            <ns2:Contact>Herr Heinrich Elsigan </ns2:Contact>
        </ns2:Address>
    </ns2:Biller>
    <ns2:InvoiceRecipient>
        <ns2:VATIdentificationNumber>ATU54760904</ns2:VATIdentificationNumber>
        <ns2:OrderReference>
            <ns2:OrderID>pooler_Office2PDF</ns2:OrderID>
        </ns2:OrderReference>
        <ns2:Address>
            <ns2:AddressIdentifier ns2:AddressIdentifierType=""GLN"">9110016452449</ns2:AddressIdentifier>
            <ns2:Name>Logic4BIZ Informationstechnologie Gmbh</ns2:Name>
            <ns2:Street>Reisnerstraße 53, Hofhaus</ns2:Street>
            <ns2:Town>Wien</ns2:Town>
            <ns2:ZIP>1030</ns2:ZIP>
            <ns2:Country>AT</ns2:Country>
            <ns2:Phone>+43 1 877 18 81</ns2:Phone>
            <ns2:Email>office@logic4biz.com</ns2:Email>
            <ns2:Contact>Herr Peter Fasol </ns2:Contact>
        </ns2:Address>
    </ns2:InvoiceRecipient>
    <ns2:Details>
        <ns2:ItemList>
            <ns2:HeaderDescription>
                Der am 14.05.2024 beauftragte Office2PDF Spooler [ Quelle privates Github repository:
                github.com/heinrichelsigan/Spooler_Office2PDF ] ist seit heute für den letzten
                Integrationstest bereit.
                Release: https://github.com/heinrichelsigan/Spooler_Office2PDF/releases/tag/2024-08-
                08-final_PDF_Converter_Spooler
                Ich stelle daher in Absprache mit Matthias Wohlmann den Betrag von 3.696€ inkl. USt. für
                „Leistung Erstellung PDF Converter Spooler“ Rechnungsnummer 20240808 in Rechnung:
            </ns2:HeaderDescription>
        </ns2:ItemList>
    </ns2:Details>
    <ns2:Tax>
        <ns2:VAT/>
    </ns2:Tax>
    <ns2:TotalGrossAmount>0</ns2:TotalGrossAmount>
    <ns2:PayableAmount>0</ns2:PayableAmount>
    <ns2:PaymentMethod>
        <ns2:UniversalBankTransaction>
            <ns2:BeneficiaryAccount>
                <ns2:BIC>BKAUATWW</ns2:BIC>
                <ns2:IBAN>AT88 1100 0104 7029 6400</ns2:IBAN>
                <ns2:BankAccountOwner>Heinrich Elsigan</ns2:BankAccountOwner>
            </ns2:BeneficiaryAccount>
            <ns2:PaymentReference>20240808</ns2:PaymentReference>
        </ns2:UniversalBankTransaction>
    </ns2:PaymentMethod>
    <ns2:PaymentConditions>
        <ns2:DueDate>2033-01-13</ns2:DueDate>
    </ns2:PaymentConditions>
</ns2:Invoice>";

        public const string RSA_PUB = @"-----BEGIN PUBLIC KEY-----
MIIDIjANBgkqhkiG9w0BAQEFAAOCAw8AMIIDCgKCAwEAwN6v0r356den36gKoHc+
URX8wBE9wjTTSBHJF0LMjNkRASVwHDeOza3/BA3l3fDEPWlFgBSjukUNt8e/rDml
1QZZBDJWwqaqQddxEg08srS3WK0L/yqQ4N0JhwS3NNpb0zQR5rbxIlsVfj4TO8UO
5Ez44ts6bIghwl2lKp4ztn57JYRlXwwXN5KPc9LpD25uXUYZCsiAFAJyaEAqN2a3
YRaybvjXFQ/2680DhqXB+phH2tNpM391US/VkX7HSrfNCRsnzl+2ad0ryyfuLXtk
onLiP8qWfJ31Esko8xG1kU+nXZXGBEYakvuSWiKLb39CGq0yhsnvcVXBEsWdeIPp
ly188HtcO/uCRGyvg3VcPncTdRSyjGgVcg2fom/gfJ5bCPnuT5VhoBdbenWuYnx1
c8JwL+gsrYhMc5gha4cs3Yij4hTQ2kdPF2I4VX1qte3a0FDGaHuAD2R3af3OiJG4
956LmUKP2EuZAmR0UoXQb1mop9YIbzCWi0vlxaV/c9bSnDAqgA7uV4Prr5Pyv8TZ
0WfObOQR623RqTiQmDI61xUdSJZD0NG40O5gwmCPxSYsLGSDMvLvmvEySl+MPltK
oFzfovIKyt0dE149xFroXgaeNRTwvBkC2Uv+cmJkc8zuCt9+ZvBBgbcJHPz4OCCb
7R3sg7djpuIticJXarhrEtbhaI8xKmf5J2lf4ciCsiQ0YfLz4EdEZQRf4OpHEZ1t
Z2vK7GHg6T0w1Qjm39QBlnfO+/O6+1VPoTXgbSv0qOn4t+824qFrE8eNJ4XAv8m8
jFyVsR/EqxjZQOwLXlz+VAD/T+QXsAc+b6Iy6yhMOecg6IqIbywz2pb3uxh6ulCl
NxXRDM4eL63aL6aj2h3eAZeSyhW3z1RXYvmB23SkAPCTS2A/88kEl445VRzyV/+7
8vArYCfwrF9VbspjCqc1GC8FP1g0vkSwuHwy3wxS8Gw8pPikgOHih2R9CAueVsKv
Rp32YyLCCVT8nHuN3xwVarPLeIEXDFYjnm8ZpMNlfvw7AgMBAAE=
-----END PUBLIC KEY-----";

        public const string RSA_PRV = @"-----BEGIN PRIVATE KEY-----
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
kz2rdcboO5eIyQ+QLucLEnwRC50UHQM=
-----END PRIVATE KEY-----";


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

                if (ConfigurationManager.AppSettings["AppDirPathUnix"] != null)
                    pathUnix = ConfigurationManager.AppSettings["AppDirPathUnix"];

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
            try
            {
                if (ConfigurationManager.AppSettings[key] != null)
                {
                    return ConfigurationManager.AppSettings[key].ToString();
                }
            }
            catch { }

            return null;
        }


    }

}