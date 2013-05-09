using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utilities.IO.ExtensionMethods;

namespace NuspecUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (FileInfo File in new DirectoryInfo("..\\..\\..\\BatmanPackages\\").EnumerateFiles("*.nuspec", SearchOption.AllDirectories))
            {
                string FileContents = File.Read();
                Match VersionMatch = Regex.Match(FileContents, "<version>(?<VersionNumber>.*)</version>");
                string[] VersionInfo = VersionMatch.Groups["VersionNumber"].Value.Split('.');
                string NewVersion = VersionInfo[0] + "." + VersionInfo[1] + ".";
                if (VersionInfo.Length > 2)
                    NewVersion += (int.Parse(VersionInfo[2]) + 1).ToString("D4");
                else
                    NewVersion += "0001";
                File.Save(Regex.Replace(FileContents, "<version>(?<VersionNumber>.*)</version>", "<version>" + NewVersion + "</version>"));
            }
            Process PackagingProcess = new FileInfo("..\\..\\..\\BatmanPackages\\Package.bat").Execute();
            PackagingProcess.WaitForExit();

            foreach (FileInfo File in new DirectoryInfo("..\\..\\..\\BatmanPackages\\").EnumerateFiles("*.nupkg", SearchOption.AllDirectories))
            {
                File.Delete();
            }
        }
    }
}
