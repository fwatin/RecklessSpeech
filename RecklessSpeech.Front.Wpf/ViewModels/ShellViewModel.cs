using System.Windows.Input;

using RecklessSpeech.Front.Wpf.Contracts.Services;
using RecklessSpeech.Front.Wpf.Helpers;
using RecklessSpeech.Front.Wpf.Properties;

using Syncfusion.Windows.Shared;

namespace RecklessSpeech.Front.Wpf.ViewModels
{
    public class ShellViewModel : Observable
    {
		private readonly INavigationService _navigationService;
        private readonly IRightPaneService _rightPaneService;
        private ICommand _loadedCommand;
        private ICommand _unloadedCommand;
		private ICommand _viewCommand;

        public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(OnLoaded));

        public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new RelayCommand(OnUnloaded));

        public ShellViewModel(INavigationService navigationService, IRightPaneService rightPaneService)
        {
			_navigationService = navigationService;
            _rightPaneService = rightPaneService;
        }

        private void OnLoaded()
        {
        }

        private void OnUnloaded()
        {
            _rightPaneService.CleanUp();
        }
		
		public ICommand ViewSelectionChangedCommand => _viewCommand ?? (_viewCommand = new DelegateCommand(OnViewSelected));
		
		 private void OnViewSelected(object viewName)
        {
            string header = viewName.ToString();
			if(header == null)
            {
                //_navigationService.NavigateTo(typeof(ItemNameViewModel).FullName, null, true);
            }
            //_navigationService.NavigateTo(typeof(ItemNameViewModel).FullName, null, true);
			else if(header == Resources.ShellMenuItemViewsMainPageHeader)
					_navigationService.NavigateTo(typeof(MainViewModel).FullName, null, true);
        
		}
    }
}
