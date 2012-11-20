using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices.ComTypes;
using System.Xml;
using System.IO;
using System.Runtime.InteropServices;
using SunglassCatiaPlugin.Core;
using System.Security.Cryptography;
using System.Reflection;

namespace SunglassCatiaPlugin.Utils
{
    class Utils
    {
        public static class Utility
        {
            public static string GetPluginVersion()
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                string product = ((AssemblyProductAttribute)attributes[0]).Product;
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                return string.Format("{0} {1}", product, version);
            }

            public static string GetOSVersion()
            {
                return System.Environment.OSVersion.VersionString;
            }

            public static void GoToUrl(string url)
            {
                try
                {
                    System.Diagnostics.Process.Start(url);
                }
                catch
                {
                    System.Diagnostics.Debug.Assert(false);
                }
            }

            public static string GetSHA1Hash(string fileName)
            {
                try
                {
                    StringBuilder stringHash = null;

                    if (System.IO.File.Exists(fileName))
                    {
                        using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            using (SHA1Managed sha1 = new SHA1Managed())
                            {
                                byte[] hash = sha1.ComputeHash(fs);
                                stringHash = new StringBuilder(hash.Length);
                                foreach (byte b in hash)
                                {
                                    stringHash.AppendFormat("{0:x2}", b);
                                }
                            }
                        }

                        return stringHash.ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
                catch
                {
                    System.Diagnostics.Debug.Assert(false);
                    return "";
                }
            }
        }

        


    }
}
