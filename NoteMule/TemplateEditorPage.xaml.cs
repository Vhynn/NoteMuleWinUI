// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NoteMule
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TemplateEditorPage : Page
    {
        public TemplateEditorPage()
        {
            this.InitializeComponent();
        }

        #region Navigation
        private void NotesMenuClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NotesPage));
        }
        private void PasswordToolMenuClicked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PasswordToolPage));
        }
        private void CreateButtonClicked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TemplateControlPages.TemplateCreationPage));
        }
        private void EditButtonClicked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TemplateControlPages.TemplateEditPage));
        }
        private void DeleteButtonClicked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TemplateControlPages.TemplateDeletePage));
        }
        #endregion

    }
}
