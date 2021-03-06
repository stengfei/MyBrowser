﻿namespace MyBrowser.UI.WinApp
{
    using Microsoft.Shell;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using Xilium.CefGlue;

    internal static class Program
    {
        //private const string _unique = "KMEHosp.UI.WinApp.DrugStore";

        [STAThread]
        private static int Main(string[] args)
        {
            //if (SingleInstance<App>.InitializeAsFirstInstance(_unique))
            //{
                try
                {
                    CefRuntime.Load();
                }
                catch (DllNotFoundException ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 1;
                }
                catch (CefRuntimeException ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 2;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 3;
                }

                var mainArgs = new CefMainArgs(args);
                var cefApp = new BaseCefApp();

                var exitCode = CefRuntime.ExecuteProcess(mainArgs, cefApp);
                if (exitCode != -1)
                {
                    return exitCode;
                }

                var cefSettings = new CefSettings
                {
                    // BrowserSubprocessPath = browserSubprocessPath,
                    SingleProcess = false,
                    WindowlessRenderingEnabled = true,
                    MultiThreadedMessageLoop = true,
                    LogSeverity = CefLogSeverity.Verbose,
                    LogFile = "cef.log",
                };

                try
                {
                    CefRuntime.Initialize(mainArgs, cefSettings, cefApp);
                }
                catch (CefRuntimeException ex)
                {
                    MessageBox.Show(ex.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 4;
                }

                var app = new App();
                app.InitializeComponent();
                app.Run();

                // shutdown CEF
                CefRuntime.Shutdown();
            //    SingleInstance<App>.Cleanup();
            //}
            return 0;
        }
    }
}
