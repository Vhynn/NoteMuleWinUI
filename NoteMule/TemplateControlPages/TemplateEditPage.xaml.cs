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
using Windows.System.Diagnostics;
using Windows.System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NoteMule.TemplateControlPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TemplateEditPage : Page
    {
        public TemplateEditPage()
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

        private void SetFile(object sender, RoutedEventArgs e)
        {
            string template = string.Empty;
            string fileName = string.Empty;

            templateName.Text = templateSelectName.SelectedItem.ToString();

            try
            {
                fileName = templateSelectName.SelectedItem.ToString() + ".txt";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            string Root = Directory.GetCurrentDirectory() + "\\Templates\\";
            string file = Root + fileName;

            try
            {
                template = File.ReadAllText(file);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            templateEditBox.Document.SetText(Microsoft.UI.Text.TextSetOptions.None, template);
        }

        public async void SaveButtonClicked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (templateName.Text == "")
            {
                ContentDialog alertDialog = new ContentDialog
                {
                    Title = "Please add a name",
                    Content = "'Name' is a required field",
                    CloseButtonText = "OK"
                };

                alertDialog.XamlRoot = this.Content.XamlRoot;

                ContentDialogResult result = await alertDialog.ShowAsync();

                return;
            }

            string templateText = "";
            templateEditBox.Document.GetText(Microsoft.UI.Text.TextGetOptions.None, out templateText);

            if (templateText.Trim() == "")
            {
                ContentDialog alertDialog = new ContentDialog
                {
                    Title = "Empty template",
                    Content = "The 'Template Contents' field is empty",
                    CloseButtonText = "OK"
                };

                alertDialog.XamlRoot = this.Content.XamlRoot;

                ContentDialogResult result = await alertDialog.ShowAsync();

                return;
            }

            string newFileName = templateName.Text + ".txt";

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            string root = Directory.GetCurrentDirectory() + "\\Templates\\";
            string newFile = root + newFileName;

            string fileName = templateSelectName.SelectedItem.ToString() + ".txt";
            string file = root + fileName;

            if (file == newFile)
            {
                try
                {
                    await File.WriteAllTextAsync(file, templateText);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            else
            {
                try
                {
                    File.Delete(file);
                    await File.WriteAllTextAsync(newFile, templateText);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }

            NotesMenuClicked(sender, e);
        }
    }
}
