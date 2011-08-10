using Ninject.Modules;
using Pion.ApplicationServices;
using Pion.Domain;
using Pion.Infrastructure.Internet;
using Pion.UI.ViewModels;

namespace Pion.UI
{
    /// <summary>
    /// Represents a NinjectModule which initializes the IOC Container.
    /// </summary>
    public sealed class ApplicationServiceLocatorModule : NinjectModule
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationServiceLocatorModule class.
        /// </summary>
        public ApplicationServiceLocatorModule()
        {
        }

        /// <summary>
        /// Initializes the IOC Container.
        /// </summary>
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
