using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Pion.UI.ViewModels
{
    public sealed class SettingsViewModel : INotifyPropertyChanged
    {
        readonly Lazy<ICommand> _changeDownloadLocationCommand;
        readonly IDialogService _dialogService;
        readonly IApplicationSettings _settings;

        public SettingsViewModel(IApplicationSettings settings, IDialogService dialogService)
        {
            _settings = settings;
            _dialogService = dialogService;

            _changeDownloadLocationCommand = new Lazy<ICommand>(() => new RelayCommand(obj => this.ChangeDownloadLocation()));
        }

        public ICommand ChangeDownloadLocationCommand
        {
            get
            {
                return _changeDownloadLocationCommand.Value;
            }
        }

        public string CurrentDownloadLocation
        {
            get
            {
                return _settings.DownloadLocation;
            }

            set
            {
                _settings.ChangeDownloadLocation(value);

                RaisePropertyChanged("CurrentDownloadLocation");
            }
        }

        void ChangeDownloadLocation()
        {
            string newDownloadLocation = _dialogService.ChooseFolder();

            if (string.IsNullOrWhiteSpace(newDownloadLocation))
            {
                return;
            }

            CurrentDownloadLocation = newDownloadLocation;
        }

        void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
