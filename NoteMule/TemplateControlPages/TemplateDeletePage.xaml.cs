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
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NoteMule.TemplateControlPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TemplateDeletePage : Page
    {
        public TemplateDeletePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            string root = Directory.GetCurrentDirectory() + "\\Templates\\";

            List<string> templates = new List<string>(Directory.GetFiles(root));

            foreach (string template in templates)
            {
                string fileName = "";

                fileName = Regex.Replace(template, @"^" + Regex.Escape(root), "");
                fileName = Regex.Replace(fileName, Regex.Escape(".txt") + "$", "");

                templateSelectName.Items.Add(fileName);
            }
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
        private void EditorMenuClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TemplateEditorPage));
        }
        #endregion

        public async void DeleteButtonClicked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            ContentDialog confirmationDialog = new ContentDialog
            {
                Title = "Delete Template",
                Content = "Do you want to permanantly delete the " + templateSelectName.SelectedItem.ToString() + " template?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No"
            };

            confirmationDialog.XamlRoot = this.Content.XamlRoot;

            ContentDialogResult result = await confirmationDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                string root = Directory.GetCurrentDirectory() + "\\Templates\\";
                string fileName = templateSelectName.SelectedItem.ToString() + ".txt";
                string file = root + fileName;

                File.Delete(file);

                this.Frame.Navigate(typeof(NotesPage));
            }
            else
            {
                return;
            }   
        }
    }
}
