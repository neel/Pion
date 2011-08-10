using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using Pion.ApplicationServices;
using Pion.UI.ViewModels;
using Pion.Domain;
using Pion.Infrastructure.Internet;
using Pion.UI.Views;

namespace Pion.UI
{
    public sealed class ApplicationServiceLocatorModule : NinjectModule
    {
        public ApplicationServiceLocatorModule()
        {
        }

        public override void Load()
        {
            Bind<IApplicationSettings>().To<ApplicationSettings>().InSingletonScope();
            Bind<IDialogService>().To<DialogService>().InSingletonScope();
            Bind<IVideoRepository>().To<VideoRepository>().InSingletonScope();
            Bind<IYouTubeService>().To<YouTubeService>().InSingletonScope();
            Bind<IWebsiteRepository>().To<WebsiteRepository>().InSingletonScope();
            Bind<MainWindowViewModel>().To<MainWindowViewModel>().InSingletonScope();
            Bind<SettingsViewModel>().To<SettingsViewModel>().InSingletonScope();
        }
    }
}
