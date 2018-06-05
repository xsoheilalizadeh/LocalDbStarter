using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Win32;

namespace SqlLocalDbStarter {
    class Program {

        private static Process _proccess;
        private static RegistryKey _registerKey;
        private static string _startupKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        private static string _appName = "SqlLocalDbStarter";

        static void Main (string[] args) => Run ();

        public static void Run () {
            RunProcess ();
            SetStartUp ();
        }

        public static void RunProcess () {

            _proccess = new Process {
                StartInfo = new ProcessStartInfo {
                FileName = "sqllocaldb",
                Arguments = "s \"MSSQLLocalDb\"",
                CreateNoWindow = true,
                UseShellExecute = false
                }
            };

            _proccess.Start ();
        }

        public static void SetStartUp () {

            var currentAppPath = Assembly.GetExecutingAssembly ().Location.Replace ("dll", "exe");

            _registerKey = Registry.CurrentUser.OpenSubKey (_startupKey, true);

            _registerKey.SetValue (_appName, currentAppPath);
        }
    }
}