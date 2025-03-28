using RecklessSpeech.Front.Wpf.Contracts.Services;
using RecklessSpeech.Front.Wpf.Helpers;
using RecklessSpeech.Front.Wpf.Pages.Main;
using RecklessSpeech.Front.Wpf.Properties;
using Syncfusion.Windows.Shared;
using System.Windows.Input;

namespace RecklessSpeech.Front.Wpf.Pages.Shell
{
    public class ShellViewModel : Observable
    {
		private readonly INavigationService _navigationService;
        private readonly IRightPaneService _rightPaneService;
        private ICommand _loadedCommand;
        private ICommand _unloadedCommand;
		private ICommand _viewCommand;

        public ICommand LoadedCommand => this._loadedCommand ?? (this._loadedCommand = new RelayCommand(this.OnLoaded));

        public ICommand UnloadedCommand => this._unloadedCommand ?? (this._unloadedCommand = new RelayCommand(this.OnUnloaded));

        public ShellViewModel(INavigationService navigationService, IRightPaneService rightPaneService)
        {
			this._navigationService = navigationService;
            this._rightPaneService = rightPaneService;
        }

        private void OnLoaded()
        {
        }

        private void OnUnloaded()
        {
            this._rightPaneService.CleanUp();
        }
		
		public ICommand ViewSelectionChangedCommand => this._viewCommand ?? (this._viewCommand = new DelegateCommand(this.OnViewSelected));
		
		 private void OnViewSelected(object viewName)
        {
            string header = viewName.ToString();
			if(header == null)
            {
                //_navigationService.NavigateTo(typeof(ItemNameViewModel).FullName, null, true);
            }
            //_navigationService.NavigateTo(typeof(ItemNameViewModel).FullName, null, true);
			else if(header == Resources.ShellMenuItemViewsMainPageHeader)
					this._navigationService.NavigateTo(typeof(MainViewModel).FullName, null, true);
        
		}
    }
}
