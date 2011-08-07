using System;
using System.Windows;
using Pion.Infrastructure.Common;
using Pion.UI.ViewModels;
using Pion.UI.Views;

namespace Pion.UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitializeServiceLocator();

            MainWindow mainWindow = new MainWindow();

            mainWindow.DataContext = ServiceLocator.Resolve<MainWindowViewModel>();

            mainWindow.Closed += MainWindowClosedEventHandler;

            mainWindow.Show();
        }

        void InitializeServiceLocator()
        {
            ServiceLocator.Initialize(new ApplicationServiceLocatorModule());
        }

        void MainWindowClosedEventHandler(object sender, EventArgs e)
        {
            ServiceLocator.Dispose();
        }
    }
}
