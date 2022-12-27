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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using XKCDPasswordGen;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace NoteMule
{
    public sealed partial class NotesPage : Page
    {
        public NotesPage()
        {
            this.InitializeComponent();
            notesEditor.ClipboardCopyFormat = RichEditClipboardFormat.AllFormats;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            instantiateMenu();
            instantiateTemplateList();
        }

        private void instantiateMenu()
        {
            Menu.Items.Clear();

            #region Add Menu Tools
            tools = new MenuBarItem();
            tools.Name = "tools";
            tools.Title = "Tools";

            MenuFlyoutItem toolNotes = new MenuFlyoutItem();
            toolNotes.Text = "Notes";
            tools.Items.Add(toolNotes);

            MenuFlyoutItem toolPw = new MenuFlyoutItem();
            toolPw.Text = "Password Generator";
            toolPw.Click += PasswordToolMenuClicked;
            tools.Items.Add(toolPw);

            MenuFlyoutItem toolEdit = new MenuFlyoutItem();
            toolEdit.Text = "Template Editor";
            toolEdit.Click += EditorMenuClicked;
            tools.Items.Add(toolEdit);

            Menu.Items.Add(tools);
            #endregion

            templateBar = new MenuBarItem();
            templateBar.Name = "templateBar";
            templateBar.Title = "Templates";

            Menu.Items.Add(templateBar);

            templateBar.Items.Clear();
        }

        private void instantiateTemplateList()
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            string root = Directory.GetCurrentDirectory() + "\\Templates\\";

            List<string> templates = new List<string>(Directory.GetFiles(root));

            foreach (string template in templates)
            {
                string fileName = "";

                fileName = Regex.Replace(template, @"^" + Regex.Escape(root), "");
                fileName = Regex.Replace(fileName, Regex.Escape(".txt") + "$", "");

                MenuFlyoutItem newFlyout = new MenuFlyoutItem();
                newFlyout.Name = fileName;
                newFlyout.Text = fileName;
                newFlyout.Click += TemplateClicked;

                templateBar.Items.Add(newFlyout);
            }
        }

        #region Navigation
        private void PasswordToolMenuClicked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(PasswordToolPage));
        }
        private void EditorMenuClicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TemplateEditorPage));
        }
        #endregion

        public void TemplateClicked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {

            string template = string.Empty;
            string fileName = string.Empty;

            try
            {
                fileName = ((MenuFlyoutItem)sender).Name + ".txt";
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

            notesEditor.Document.SetText(Microsoft.UI.Text.TextSetOptions.None, template);
        }

        public void CopyClicked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            notesEditor.ClipboardCopyFormat = RichEditClipboardFormat.AllFormats;
            string value = string.Empty;
            notesEditor.Document.GetText(Microsoft.UI.Text.TextGetOptions.None, out value);

            int length = value.Length;

            Microsoft.UI.Text.ITextRange notes = notesEditor.Document.GetRange(0, length);

            notes.Copy();

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            string copyFile = Directory.GetCurrentDirectory() + "\\copyPage.rtf";

            

            //Debug.WriteLine("Reee!");

            //Stream st = new MemoryStream();
            //IRandomAccessStream fuckMS = new InMemoryRandomAccessStream();
            //notesEditor.Document.SaveToStream(Microsoft.UI.Text.TextGetOptions.FormatRtf, fuckMS);

            //Debug.WriteLine("Reee!!!");

            //st = fuckMS.AsStream();
            //DataPackage hurrdurrMSSucks = new DataPackage();
            //List<byte> killme = new List<byte>();

            ////int kysms = 666;
            ////while(kysms != -1)
            ////{
            ////    kysms = st.ReadByte();
            ////    if(kysms != -1)
            ////        killme.Add((byte)kysms);
            ////}

            ////hurrdurrMSSucks.SetData(typeof(byte[]).ToString(), killme.ToArray());
            //hurrdurrMSSucks.SetData("rawbinary", st);
            //IDataObject kysms420 = new DataObject();

            //Debug.WriteLine("Reee?");

            //Clipboard.SetContent(hurrdurrMSSucks);
            //Debug.WriteLine("Reee???");
        }

        private async void ClearClicked(object sender, RoutedEventArgs e)
        {
            ContentDialog confirmationDialog = new ContentDialog
            {
                Title = "Clear Notes",
                Content = "Do you want to clear all notes?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No"
            };

            confirmationDialog.XamlRoot = this.Content.XamlRoot;

            ContentDialogResult result = await confirmationDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                string clear = "";
                notesEditor.Document.SetText(Microsoft.UI.Text.TextSetOptions.None, clear);
            }
            else
            {
                return;
            }
        }
    }
}
