using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using RegFileParser;

namespace RegFixer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Registry Fixer v1.0 by Hack4Fun";
            string path;
            if (args.Length > 0) path = args[0];
            else
            {
                Console.Write("Enter EditReg file path: ");
                path = Console.ReadLine();
            }
            path = @"C:\Users\alper\Downloads\EditReg_23.reg";
            if (File.Exists(path))
            {
                string[][] HexArray = new string[3][] { new string[2] { "", "" }, new string[4] { "", "", "", "" }, new string[2] { "", "" } };
                Console.WriteLine("Loading registry export file...");
                RegFileObject regfile = new RegFileObject(path);
                Console.WriteLine("Parsing structures...");
                foreach (KeyValuePair<String, Dictionary<String, RegValueObject>> entry in regfile.RegValues)
                {
                    int ctr1 = 0, ctr2 = 0;
                    foreach (KeyValuePair<String, RegValueObject> item in entry.Value)
                    {
                        if (entry.Key.Contains(@"SOFTWARE\WOW6432Node\Microsoft\Cryptography")) HexArray[2][1] = item.Value.Value;
                        else if (entry.Key.Contains(@"SOFTWARE\Microsoft\Cryptography")) HexArray[2][0] = item.Value.Value;
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
                Console.WriteLine("File could not found! Try again...");
                System.Threading.Thread.Sleep(2000);
            }
        }

        private static void PatchHexValues(string[][] HexArray)
        {
            Console.WriteLine("Scanning for valid key names...");
            string[] keyNames = ScankeyNames(Registry.CurrentUser), keyNames1 = ScankeyNames(Registry.CurrentUser.OpenSubKey("Software\\Microsoft"));

            RegistryKey guid = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\Cryptography", RegistryKeyPermissionCheck.ReadWriteSubTree);
            RegistryKey guid1 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"SOFTWARE\Microsoft\Cryptography", RegistryKeyPermissionCheck.ReadWriteSubTree);

            Console.WriteLine("Patching bytes...");
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
                            key.SetValue(valueNames[i], ConvertHexString(HexArray[0][i].Replace(",", "")));
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
                        for (int i = 0; i < valueNames.Length; i++) key.SetValue(valueNames[i], ConvertHexString(HexArray[1][i].Replace(",", "")));
                        key.Close();
                    }
                }
            }

            if (guid != null) guid.SetValue("MachineGuid", HexArray[2][1]);
            if (guid1 != null) guid1.SetValue("MachineGuid", HexArray[2][0]);

            Console.WriteLine("Done.");
            System.Threading.Thread.Sleep(2000);
        }

        public static byte[] ConvertHexString(string hex)
        {
            return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }

        private static string[] ScankeyNames(RegistryKey baseKey)
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
    }
}
