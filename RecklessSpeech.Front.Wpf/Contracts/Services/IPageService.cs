using System;
using System.Windows.Controls;

namespace RecklessSpeech.Front.Wpf.Contracts.Services
{
    public interface IPageService
    {
        Type GetPageType(string key);

        Page GetPage(string key);
    }
}
