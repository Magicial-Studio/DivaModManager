﻿using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using System.Threading;
using DivaModManager.UI.i18n;
using System;

namespace DivaModManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected static bool AlreadyRunning()
        {
            bool running = false;
            try
            {
                // Getting collection of process  
                Process currentProcess = Process.GetCurrentProcess();

                // Check with other process already running   
                foreach (var p in Process.GetProcesses())
                {
                    if (p.Id != currentProcess.Id) // Check running process   
                    {
                        if (p.ProcessName.Equals(currentProcess.ProcessName) && p.MainModule.FileName.Equals(currentProcess.MainModule.FileName))
                        {
                            running = true;
                            break;
                        }
                    }
                }
            }
            catch { }
            return running;
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            ShutdownMode = ShutdownMode.OnMainWindowClose;

            DispatcherUnhandledException += App_DispatcherUnhandledException;
            RegistryConfig.InstallGBHandler();
            Global.i18n.UpdateUserInterfaceLanguage();
            MainWindow mw = new MainWindow();
            bool running = AlreadyRunning();
            if (!running)
                mw.Show();
            if (e.Args.Length > 1 && e.Args[0] == "-download")
                new ModDownloader().Download(e.Args[1], running);
            else if (running)
                MessageBox.Show(Global.i18n.GetTranslation("Diva Mod Manager is already running."), Global.i18n.GetTranslation("Warning"), MessageBoxButton.OK, MessageBoxImage.Exclamation); 

        }
        private static void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"{Global.i18n.GetTranslation("Unhandled exception occured:")}\n{e.Exception.Message}\n\n{Global.i18n.GetTranslation("Inner Exception:")}\n:\n{e.Exception.InnerException}" +
                $"\n\n{Global.i18n.GetTranslation("Stack Trace:")}\n{e.Exception.StackTrace}", $"{Global.i18n.GetTranslation("Error")}", MessageBoxButton.OK,
                             MessageBoxImage.Error);

            e.Handled = true;
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                ((MainWindow)Current.MainWindow).ModGrid.IsEnabled = true;
                ((MainWindow)Current.MainWindow).ConfigButton.IsEnabled = true;
                ((MainWindow)Current.MainWindow).LaunchButton.IsEnabled = true;
                ((MainWindow)Current.MainWindow).OpenModsButton.IsEnabled = true;
                ((MainWindow)Current.MainWindow).UpdateButton.IsEnabled = true;
                ((MainWindow)Current.MainWindow).GameBox.IsEnabled = true;
                ((MainWindow)Current.MainWindow).LoadoutBox.IsEnabled = true;
                ((MainWindow)Current.MainWindow).EditLoadoutsButton.IsEnabled = true;
                ((MainWindow)Current.MainWindow).DropBox.Visibility = Visibility.Collapsed;
            });
        }
    }
}
