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
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using NoteMule;
using Microsoft.UI.Xaml.Documents;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NoteMule.TemplateControlPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TemplateCreationPage : Page
    {
        public TemplateCreationPage()
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
        private void EditorMenuClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TemplateEditorPage));
        }
        #endregion

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

            string fileName = templateName.Text + ".txt";

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            string root = Directory.GetCurrentDirectory() + "\\Templates\\";
            string file = root + fileName;

            if (File.Exists(file))
            {
                ContentDialog confirmationDialog = new ContentDialog
                {
                    Title = "Duplicate file",
                    Content = "A file with the same name already exists.\nDo you want to override the old file?",
                    PrimaryButtonText = "Yes",
                    CloseButtonText = "No"
                };

                confirmationDialog.XamlRoot = this.Content.XamlRoot;

                ContentDialogResult result = await confirmationDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                
                }
                else
                {
                    return;
                }
            }

            try
            {
                await File.WriteAllTextAsync(file, templateText);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
            NotesMenuClicked(sender, e);
        }
    }
}
