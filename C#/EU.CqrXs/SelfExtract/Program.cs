using EU.CqrXs.Crypt.Cipher;
using EU.CqrXs.Crypt.EnDeCoding;
using EU.CqrXs.Crypt.Hash;
using EU.CqrXs.Util;
using EU.CqrXs.Zip;
using Org.BouncyCastle.Crypto;
using System;
using System.Text;

namespace EU.CqrXs.SelfExtract
{


    /// <summary>
    /// SelfExtract app pipe for crypt/decrypt zip/unzip encode/decode md5sum/shaSum     
    /// 
    /// EU.CqrXs.SelfExtract.Program 
    /// -i | --inFile= | --inText={string|EnviromentVariable} | --inStd    
    /// -k | --key=mykey
    /// -H | --hash={Blake2xs|BCrypt|CShake|Dstu7564|Hex|MD5|Oct|RipeMD256|SCrypt|Sha1|Sha256|Sha384|Sha512|Whirlpool|TupleHash}
    ///         default: Hex
    /// -z | --zip={bzip2|gzip|zip} 
    ///         default: none
    /// -C | --CipherAlgos={[aes,des3,blowfish,fish2,fish3]|key}
    /// -M | --mode={ECB|CBC|CFB}   
    ///         default: ECB
    /// -e | --encode={raw|hex16|hex32|base32|base64|uu}
    ///         default: base64
    /// -o | --outFile= | --outText=EnviromentVariable | --outStd      
    /// -S | --secureCipher
    /// -X | --xexe
    /// -D | --Decrypt 
    /// -? | --gethelp
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
    internal class Program
    {
        static readonly string? progName = System.Environment.ProcessPath;
        static readonly string? progDirectory = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        static string? inName = null, outName = null, outEnviron = null, key = null;
        static bool reverseDirection = false, verbose = false, secureCipher = false, xexe = false;
        static FileInfo? inFile = null, outFile = null;
        static byte[]? inBytes = null, outBytes = null;
        static string passKey = "";
        static CipherMode2 cmode2 = CiffreMode.defaultCipherMode2;
        static ZipType zipType = ZipType.None;
        static EncodingType encodingType = EncodingType.None;
        static KeyHash keyHash = KeyHash.Hex;

        // size 36.501.412 bytes 
        // TODO: enter a sequence, where enrrypted file starts
        // Maybe .Net FW all in compiled is too huge, think to compile it without fw or trim it better

        /// <summary>
        /// SelfExtract Console app pipe for crypt/decrypt zip/unzip encode/decode md5sum/shaSum
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string[] algos = new List<string>().ToArray();
            encodingType = EncodingType.None;
            Constants.DirCreate = false;
            Constants.NOLog = true;
            string encryptOptLater = "";

            if (args.Length <= 0)
                Usage();

            // experimental
            FileInfo f = new FileInfo(progName);
            if (f.Length > 36501412)
            {
                byte[] bytesInExe = File.ReadAllBytes(progName);
                byte[] inBytes = new byte[f.Length - 36501412];
                Array.Copy(bytesInExe, 36501412, inBytes, 0, f.Length - 36501412);
                inName = progName.Replace(".exe", "");
                File.WriteAllBytes(inName, inBytes);
                outBytes = inBytes;                
            }

            for (int i = 0; i < args.Length; i++)
            {
                // string optStr = GetOption(... => out OptEnum optEnum)
                string[] optArgs = args[i].GetOption();                
                OptEnum optEnum = Enum.Parse<OptEnum>(optArgs[0]);
                string optStr = optArgs[1];
                switch (optEnum)
                {
                    case OptEnum.InParam:
                        inName = optStr;
                        if (string.IsNullOrEmpty(inName))
                            ; // Else
                        else
                            if (args[i].ToLower().Contains("file") || File.Exists(inName) || File.Exists(Path.Combine(progDirectory, inName)))
                        {
                            if (System.IO.File.Exists(Path.Combine(progDirectory, inName)))
                            {
                                inFile = new FileInfo(Path.Combine(progDirectory, inName));
                                inBytes = File.ReadAllBytes(Path.Combine(progDirectory, inName));
                            }
                            else if (File.Exists(inName))
                            {
                                inFile = new FileInfo(inName);
                                inBytes = File.ReadAllBytes(inName);
                            }
                        }
                        else
                            if (args[i].ToLower().Contains("text") || !string.IsNullOrEmpty(inName))
                        {
                            string? inStr = Environment.GetEnvironmentVariable(inName.TrimStart("$".ToCharArray()));
                            if (inStr == null || inStr.Length == 0)
                                inStr = inName;
                            inBytes = Encoding.UTF8.GetBytes(inStr);
                        }
                        else
                            Usage($"unrecognized option: {args[i]}.");
                        break;
                    case OptEnum.OutP:
                        outName = optStr;
                        if (string.IsNullOrEmpty(outName))
                            ; // to stdout                    
                        else
                            if (args[i].ToLower().Contains("file") || optStr.Contains(Area23Log._sepCh) || optStr.Contains('.') || !args[i].ToLower().Contains("text"))
                        {
                            string odir = Path.GetDirectoryName(optStr);
                            if (Directory.Exists(odir))
                                outFile = new FileInfo(outName);
                            else
                                outFile = new FileInfo(Path.Combine(progDirectory, outName.Replace(".\\", "")));
                        }
                        else
                            if (!string.IsNullOrEmpty(outName) || args[i].ToLower().Contains("text") || optStr.StartsWith("$"))
                            outEnviron = optStr;
                        break;
                    case OptEnum.Zip:
                        if (optStr.ToLower().Contains("gz") || optStr.ToLower().Contains("gunzip"))
                            zipType = ZipType.GZip;
                        else if (optStr.ToLower().Contains("bz") || optStr.ToLower().Contains("bunzip") || optStr.ToLower().Contains("2"))
                            zipType = ZipType.BZip2;
                        else if (optStr.ToLower().Contains("zip") || optStr.ToLower().Contains("unzip"))
                            zipType = ZipType.Zip;
                        else
                            Usage($"urecognized zip option: {optStr}");
                        break;
                    case OptEnum.Mode:
                        if (!Enum.TryParse<CipherMode2>(optStr, out cmode2))
                            cmode2 = CiffreMode.defaultCipherMode2;
                        break;
                    case OptEnum.Encode:
                        encodingType = EncodingTypesExtensions.GetEnum(optStr);
                        break;
                    case OptEnum.Key:
                        passKey = optStr;
                        break;
                    case OptEnum.Hash:
                        keyHash = KeyHash_Extensions.GetKeyHashFromString(optStr);
                        break;
                    case OptEnum.CipherAlgos:
                        encryptOptLater = optStr;
                        break;
                    case OptEnum.SecureCipher:                        
                        secureCipher = true;                             
                        break;
                    case OptEnum.DeCrypt:
                        reverseDirection = true;
                        break;
                    case OptEnum.eXe:
                        xexe = true;
                        break;
                    case OptEnum.Verbose:
                        verbose = true;
                        break;
                    case OptEnum.Help:
                    case OptEnum.Usage:
                    default:
                        Usage(string.IsNullOrEmpty(optStr) ? "" : optStr);
                        break;
                }
            }

            // when string / array is not null, fetch array for crypt pipe
            if (!string.IsNullOrEmpty(encryptOptLater))
            {
                if (string.IsNullOrEmpty(passKey) || string.IsNullOrWhiteSpace(passKey))
                    Usage($"urecognized crypt option \"{encryptOptLater}\" without --key=secretKey ");

                encryptOptLater = encryptOptLater.Replace("(", "").Replace("{", "").Replace("[", "").Replace("]", "").Replace("}", "").Replace(")", "");
                algos = encryptOptLater.Split(",;:".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                
            }


            // read from stdin, when no inName specified
            if (string.IsNullOrEmpty(inName))
            {
                System.Console.WriteLine("Reading from stdin, enter \r\n^Z (Enter Strg - z Enter) to stop reading from stdin");
                using (Stream stdin = System.Console.OpenStandardInput())
                {
                    List<byte> listBytes = new List<byte>();
                    byte[] buffer = new byte[2048];
                    int bytes;
                    while ((bytes = stdin.Read(buffer, 0, buffer.Length)) > 0)
                        listBytes.AddRange(buffer);

                    outBytes = EnDeCodeHelper.GetBytesTrimCrLfNulls(listBytes.ToArray());
                    inBytes = new byte[outBytes.Length];
                    Array.Copy(outBytes, 0, inBytes, 0, outBytes.Length);
                }
            }

            if (secureCipher)
            {
                zipType = ZipType.GZip;
                encodingType = EncodingType.Base64;

                // Create cipher pipe for en-/decryption
                SecureCipherPipe cpipe = (algos.Length > 0 || string.IsNullOrEmpty(passKey)) ?
                                new SecureCipherPipe(algos, Constants.PIPE_MAX_LEN, cmode2) :
                                new SecureCipherPipe(passKey, cmode2, verbose);

                PrintSecureCipherPipe(cpipe, reverseDirection);
                outBytes = cpipe.CryptCodeBytes(inBytes, passKey ?? "", reverseDirection, cmode2);

                inBytes = outBytes;

                if (outFile != null)
                {
                    File.WriteAllBytes(outFile.FullName, outBytes);
                    if (xexe)
                        GenerateExecuteBatch(outFile.FullName,
                            Path.Combine(Path.GetDirectoryName(outFile.FullName), Path.GetFileName(outFile.FullName) + ".exe"));
                }
                else if (string.IsNullOrEmpty(outName))
                    System.Console.WriteLine(Encoding.UTF8.GetString(outBytes));
                else if (!string.IsNullOrEmpty(outEnviron))
                    System.Environment.SetEnvironmentVariable(outEnviron, Encoding.UTF8.GetString(outBytes));

                return;
            }

            // Create cipher pipe for en-/decryption
            CipherPipe pipe = (algos.Length > 0 || string.IsNullOrEmpty(passKey)) ?
                            new CipherPipe(algos, Constants.PIPE_MAX_LEN, encodingType, zipType, keyHash, cmode2) :
                            new CipherPipe(passKey, keyHash.Hash(passKey), encodingType, zipType, keyHash, cmode2, verbose);

            PrintCipherPipe(pipe, reverseDirection);
            outBytes = pipe.CryptCodeBytes(inBytes,
                passKey ?? "", string.IsNullOrEmpty(passKey) ? "" : keyHash.Hash(passKey),
                reverseDirection, encodingType, zipType, keyHash);

            inBytes = outBytes;

            if (outFile != null)
            {
                File.WriteAllBytes(outFile.FullName, outBytes);
                if (xexe)
                    GenerateExecuteBatch(outFile.FullName, Path.Combine(Path.GetDirectoryName(outFile.FullName), Path.GetFileName(outFile.FullName) + ".exe");
            }
            else if (string.IsNullOrEmpty(outName))
                System.Console.WriteLine(Encoding.UTF8.GetString(outBytes));
            else
                if (!string.IsNullOrEmpty(outEnviron))
                    System.Environment.SetEnvironmentVariable(outEnviron, Encoding.UTF8.GetString(outBytes));

            return;
        }

        /// <summary>
        /// Usage shows the usage of console application
        /// </summary>
        static void Usage(string errMsg = "")
        {
            if (!string.IsNullOrEmpty(errMsg))
                System.Console.Error.WriteLine(errMsg);

            System.Console.Out.WriteLine("Usage:\t" + Path.GetFileName(progName) + @"
    -i  | --inFile= | --inText={string|EnviromentVariable} | --inStd    
    -k  | --key=passKey encrypt    
    -H  | --Hash={Blake2xs|BCrypt|CShake|Dstu7564|Hex|MD5|Oct|RipeMD256|SCrypt|Sha256|Sha512|Whirlpool|...}
        |   default: Hex
    -z  | --zip={gzip|bzip2|zip|none} 
        |   default: none
    -C  | --CipherAlgost={algo1,algo2,...}
        |   algo:
        |     Aes,AesLight,Rijndael,Des,Des3,Dstu7624,
        |       Aria,Camellia,CamelliaLight,Cast5,Cast6,
        |       BlowFish,Fish2,Fish3,
        |       Gost28147,Idea,Noekeon,
        |       RC2,RC532,RC564,RC6,
        |       Seed,SkipJack,Serpent,SM4,
        |       Tea,Tnepres,XTea,
        |       ZenMatrix,ZenMatrix2
    -e  | --encode={raw|hex16|base16|hex32|base32|hex64|base64|uu|xx|ascii85}
        |   default: base64
    -D  | --Decrypt [ = Inverse_Pipe_Direction ]  
    -S  | --secureCipher (uses SecureCipherPipe with always GZip, Base64 and different hashes in every pipe stage)
    -o  | --outFile= | --outText=EnviromentVariable | --outStd   
    -X  | --xexe (create self-extracting executable)
    -V  | --verbose 
    -?  | --gethelp");

            System.Console.Out.WriteLine($"\nExamples: " + @"

    EU.CqrXs.SelfExtract.exe -V -i=.\README.MD -k=io.cqrxs.eu -z=gzip -e=base64 -o=.\README_MD.base64.crypt.gzíp
    EU.CqrXs.SelfExtract.exe -V -i=.\README.MD -S -k=io.cqrxs.eu -z=gzip -o=.\README_MD.base64.scrypt.gzíp

    EU.CqrXs.SelfExtract.exe -V -D -i=.\README_MD.base64.crypt.gzíp -k=io.cqrxs.eu -z=gzip -e=base64 -o=.\README_MD_1.txt
    EU.CqrXs.SelfExtract.exe -V -D -i=.\README_MD.base64.scrypt.gzíp -S -k=io.cqrxs.eu -z=gzip -o=.\README_MD_2.txt
    \n\n");

            System.Environment.Exit(0);
        }

        #region print only debug info
        
        public static void PrintCipherPipe(CipherPipe cipherPipe, bool outPipe = false)
        {
            if (verbose)
            {
                CipherEnum[] ciphers = (outPipe) ? cipherPipe.OutPipe : cipherPipe.InPipe;
                System.Console.Write((string)((outPipe) ? "Out:\t" : " In:\t"));             
                foreach (CipherEnum cipher in ciphers)
                    System.Console.Write($"{cipher}=>");                
                System.Console.WriteLine($"\r\nCipherPipe: KeyHash={cipherPipe.KHash} ZipType={cipherPipe.ZType} " +
                    $"EncodeType={cipherPipe.EncodeType} PipeString={cipherPipe.PipeString}");
            }
        }

        public static void PrintSecureCipherPipe(SecureCipherPipe sCipherPipe, bool outPipe = false)
        {
            if (verbose)
            {
                CipherEnum[] ciphers = (outPipe) ? sCipherPipe.OutPipe : sCipherPipe.InPipe;
                System.Console.Write((string)((outPipe) ? "Out:\t" : " In:\t"));
                foreach (CipherEnum cipher in ciphers)
                    System.Console.Write($"{cipher}=>");
                System.Console.WriteLine($"\r\nSecureCipherPipe: ZipType={sCipherPipe.ZType} EncodeType={sCipherPipe.EncodeType} " +
                    $"ChipherMode={sCipherPipe.CMode2} PipeString={sCipherPipe.PipeString}");
            }
        }

        public static void GenerateExecuteBatch(string outFile, string exeName)
        {
            string selfExtractingBase = progName;
            if (!File.Exists(selfExtractingBase))
            {
                if (File.Exists(selfExtractingBase + ".exe"))
                    selfExtractingBase = selfExtractingBase + ".exe";
            }


            string suffix = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string batName = Path.Combine(Path.GetDirectoryName(outFile), $"GenSelf{suffix}.bat");
            using (StreamWriter sw = new StreamWriter(batName, false))
            {
                sw.WriteLine($"@echo off\n");
                sw.WriteLine($"timeout 2 > NUL");
                sw.WriteLine($"copy /b \"{selfExtractingBase}\" + \"{outFile}\"  \"{exeName}\"");
                sw.WriteLine($"if exist \"{exeName}\" (");
                sw.WriteLine($"    echo SelfExtract exe created: \"{exeName}\"");
                sw.WriteLine($"    del \"{outFile}\"");
                sw.WriteLine($") else (");
                sw.WriteLine($"    echo SelfExtract exe creation failed: \"{exeName}\"");
                sw.WriteLine($")");
            }
            Task.Run(() =>
            {
                ProcessCmd.Execute("start", $"{batName}");
            });
        }
        
        #endregion print only debug info

    }

}
