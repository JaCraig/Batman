using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Utilities.IO.ExtensionMethods;

namespace NuspecUpdater
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Reversioning packages");
            ReversionPackages();
            Console.WriteLine("Setting up dependencies");
            SetupDependencies();
            Console.WriteLine("Setting up internal dependencies");
            SetupInternalDependencies();
            Console.WriteLine("Setting up default package");
            SetupDefaultPackage();
            Console.WriteLine("Sending to nuget");
            SendToNuget();
        }

        private static void ReversionPackages()
        {
            foreach (FileInfo File in new DirectoryInfo("..\\..\\..\\BatmanPackages\\").EnumerateFiles("*.nuspec", SearchOption.AllDirectories))
            {
                Console.WriteLine("Revisioning {0}", File.FullName);
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
        }

        private static void SendToNuget()
        {
            Console.WriteLine("Running package.bat");
            Process PackagingProcess = new FileInfo("..\\..\\..\\BatmanPackages\\Package.bat").Execute();
            PackagingProcess.WaitForExit();

            foreach (FileInfo File in new DirectoryInfo("..\\..\\..\\BatmanPackages\\").EnumerateFiles("*.nupkg", SearchOption.AllDirectories))
            {
                Console.WriteLine("Deleting {0}", File.FullName);
                File.Delete();
            }
        }

        private static void SetupDefaultPackage()
        {
            string FileContent = new FileInfo("..\\..\\..\\BatmanPackages\\Batman.MVC.Default.nuspec").Read();
            string CurrentVersion = Regex.Match(FileContent, "<version>(?<VersionNumber>.*)</version>").Groups["VersionNumber"].Value;
            foreach (Match TempMatch in Regex.Matches(FileContent, @"<dependency id=""(?<Dependency>[^""]*)"" version=""(?<VersionNumber>[^""]*)"" />"))
            {
                if (TempMatch.Value.Contains("Batman"))
                {
                    FileContent = FileContent.Replace(TempMatch.Value, "<dependency id=\"" + TempMatch.Groups["Dependency"] + "\" version=\"[" + CurrentVersion + "]\" />");
                }
            }
            new FileInfo("..\\..\\..\\BatmanPackages\\Batman.MVC.Default.nuspec").Save(FileContent);
        }

        private static void SetupDependencies()
        {
            foreach (FileInfo File in new DirectoryInfo("..\\..\\..\\BatmanPackages\\").EnumerateFiles("*.nuspec", SearchOption.AllDirectories))
            {
                var PackagesFile = new FileInfo("..\\..\\..\\" + File.Name.Replace(".nuspec", "") + "\\packages.config");
                if (PackagesFile.Exists)
                {
                    Console.WriteLine("Setting up ", File.FullName);
                    string PackagesContent = PackagesFile.Read();
                    string FileContent = File.Read();
                    foreach (Match Package in Regex.Matches(PackagesContent, @"<package id=""(?<Package>[^""]*)"" version=""(?<Version>[^""]*)"""))
                    {
                        if (Regex.IsMatch(FileContent, @"<dependency id=""" + Package.Groups["Package"].Value + @""" version=""(?<VersionNumber>[^""]*)"" />"))
                        {
                            Match TempMatch = Regex.Match(FileContent, @"<dependency id=""" + Package.Groups["Package"].Value + @""" version=""(?<VersionNumber>[^""]*)"" />");
                            FileContent = FileContent.Replace(TempMatch.Value, @"<dependency id=""" + Package.Groups["Package"].Value + @""" version=""[" + Package.Groups["Version"].Value + @"]"" />");
                        }
                    }
                    File.Save(FileContent);
                }
            }
        }

        private static void SetupInternalDependencies()
        {
            foreach (FileInfo File in new DirectoryInfo("..\\..\\..\\BatmanPackages\\").EnumerateFiles("*.nuspec", SearchOption.AllDirectories))
            {
                Console.WriteLine("Setting up ", File.FullName);
                string FileContent = File.Read();
                string CurrentVersion = Regex.Match(FileContent, "<version>(?<VersionNumber>.*)</version>").Groups["VersionNumber"].Value;
                if (Regex.IsMatch(FileContent, @"<dependency id=""Batman.MVC"" version=""(?<VersionNumber>[^""]*)"" />"))
                {
                    Match TempMatch = Regex.Match(FileContent, @"<dependency id=""Batman.MVC"" version=""(?<VersionNumber>[^""]*)"" />");
                    FileContent = FileContent.Replace(TempMatch.Value, @"<dependency id=""Batman.MVC"" version=""[" + CurrentVersion + @"]"" />");
                }
                if (Regex.IsMatch(FileContent, @"<dependency id=""Batman.Core"" version=""(?<VersionNumber>[^""]*)"" />"))
                {
                    Match TempMatch = Regex.Match(FileContent, @"<dependency id=""Batman.Core"" version=""(?<VersionNumber>[^""]*)"" />");
                    FileContent = FileContent.Replace(TempMatch.Value, @"<dependency id=""Batman.Core"" version=""[" + CurrentVersion + @"]"" />");
                }
                File.Save(FileContent);
            }
        }
    }
}