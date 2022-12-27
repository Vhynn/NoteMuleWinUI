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
using Windows.ApplicationModel.DataTransfer;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using XKCDPasswordGen;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NoteMule
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PasswordToolPage : Page
    {
        public PasswordToolPage()
        {
            this.InitializeComponent();
        }

        #region Navigation
        private void NotesMenuClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NotesPage));
        }
        private void EditorMenuClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TemplateEditorPage));
        }
        private void CopyButtonClicked(object sender, RoutedEventArgs e)
        {
            DataPackage pw = new DataPackage();
            pw.SetText(GeneratedPassword.Text);
            Clipboard.SetContent(pw);
        }
        #endregion
        private void GenerateButtonClicked(object sender, RoutedEventArgs e)
        {
            XkcdPasswordGen xkpw = new XkcdPasswordGen();

            string twoWordPassword = xkpw.Generate(2);

            GeneratedPassword.Text = twoWordPassword;
        }
    }
}
