using System;
using System.Windows.Controls;
using System.Windows.Navigation;

using RecklessSpeech.Front.Wpf.Contracts.Services;
using RecklessSpeech.Front.Wpf.Contracts.ViewModels;
using RecklessSpeech.Front.Wpf.Helpers;

namespace RecklessSpeech.Front.Wpf.Services
{
    public class RightPaneService : IRightPaneService
    {
        private readonly IPageService _pageService;
        private Frame _frame;
        private object _lastParameterUsed;

        public event EventHandler PaneOpened;

        public event EventHandler PaneClosed;

        public RightPaneService(IPageService pageService)
        {
            _pageService = pageService;
        }

        public void Initialize(Frame rightPaneFrame)
        {
            _frame = rightPaneFrame;           
            _frame.Navigated += OnNavigated;
        }

        public void CleanUp()
        {
            _frame.Navigated -= OnNavigated;
        }

        public void OpenInRightPane(string pageKey, object parameter = null)
        {
            var pageType = _pageService.GetPageType(pageKey);
            if (_frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParameterUsed)))
            {
                var page = _pageService.GetPage(pageKey);
                var navigated = _frame.Navigate(page, parameter);
                if (navigated)
                {
                    _lastParameterUsed = parameter;
                    var dataContext = _frame.GetDataContext();
                    if (dataContext is INavigationAware navigationAware)
                    {
                        navigationAware.OnNavigatedFrom();
                    }
                }
            }
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (sender is Frame frame)
            {
                frame.CleanNavigation();
                var dataContext = frame.GetDataContext();
                if (dataContext is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedTo(e.ExtraData);
                }
            }
        }

        private void OnPaneClosed(object sender, EventArgs e)
            => PaneClosed?.Invoke(sender, e);
    }
}
