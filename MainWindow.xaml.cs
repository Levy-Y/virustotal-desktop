using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using virustotal_desktop.Models;
using static virustotal_desktop.Utils.Utilities;

namespace virustotal_desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Gets or sets the path of the selected file.
    /// </summary>
    private string SelectedFile { get; set; } = "";

    /// <summary>
    /// Initializes a new instance of the MainWindow class and sets the window's height and width relative to the screen size.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        Height = (SystemParameters.PrimaryScreenHeight * 0.25);
        Width = (SystemParameters.PrimaryScreenWidth * 0.12);
    }
    
    public MainWindow(string filePath): this()
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            InsertOpenedFileAttributes(filePath);
        }
    }
    
    /// <summary>
    /// Opens a file selection dialog, allows the user to select a file.
    /// If the user confirms the file, the InsertOpenedFileAttributes method is called with the file's path as an argument; if canceled or no file is selected, no action is taken.
    /// </summary>
    private void OpenFile()
    {
        var dialog = new Microsoft.Win32.OpenFileDialog();
        dialog.FileName = "*";
        dialog.DefaultExt = "Any";
        dialog.Filter = "Any files (*.*)|*.*";

        bool? result = dialog.ShowDialog();
        
        if (result != true) return;
        string filename = dialog.FileName;

        MessageBoxResult msgBox = MessageBox.Show($"File opened: \n {filename} \n Is it alright?", "Opened file",
            MessageBoxButton.YesNoCancel, MessageBoxImage.None, MessageBoxResult.Yes);

        switch (msgBox)
        {
            case MessageBoxResult.Yes:
                InsertOpenedFileAttributes(filename);
                break;
            case MessageBoxResult.No:
                OpenFile();
                break;
            case MessageBoxResult.Cancel:
                break;
        }
    }

    /// <summary>
    /// Takes the absolute path of a file as a string, and displays its attributes (name, extension, and size).
    /// </summary>
    /// <param name="filePath">Absolute path of the file</param>
    private void InsertOpenedFileAttributes(string filePath)
    {
        SelectedFile = filePath;
        UIElement[] attributes =
        [
            new Label() { Content = $"Name: {new System.IO.FileInfo(filePath).Name}" },
            new Label()
            {
                Content = $"Extension: {new System.IO.FileInfo(filePath).Extension.Trim('.')}"
            },
            new Label() { Content = $"Size: {(new System.IO.FileInfo(filePath).Length) / 1024} KB" },
        ];

        attributes.ToList().ForEach(element => SelectedFileAttributes.Children.Add(element));
    }

    /// <summary>
    /// Handles the click event for selecting a file. It opens the file dialog and displays the file attributes if a file is selected.
    /// </summary>
    /// <param name="sender">The object that raised the event (typically a button).</param>
    /// <param name="e">The event data associated with the button click event.</param>
    private void SelecFileOnClick(object sender, RoutedEventArgs e)
    {
        OpenFile();
    }

    /// <summary>
    /// Clears the selected file attributes and resets the selected file to an empty string.
    /// </summary>
    /// <param name="sender">The object that raised the event (typically a button).</param>
    /// <param name="e">The event data associated with the button click event.</param>
    private void ClearSelectionOnClick(object sender, RoutedEventArgs e)
    {
        SelectedFileAttributes.Children.Clear();
        SelectedFile = "";
    }

    /// <summary>
    /// Opens a dialog for editing the API key when triggered by a button click.
    /// </summary>
    /// <param name="sender">The object that raised the event (typically a button).</param>
    /// <param name="e">The event data associated with the button click event.</param>
    private void EditApiKeyOnClick(object sender, RoutedEventArgs e)
    {
        new ApiKeyPrompt().ShowDialog();
    }

    /// <summary>
    /// Clears the API key and displays a success message upon button click.
    /// </summary>
    /// <param name="sender">The object that raised the event (typically a button).</param>
    /// <param name="e">The event data associated with the button click event.</param>
    private void DeleteApiKeyOnClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Successfully cleared API key!", "Success!", MessageBoxButton.OK, MessageBoxImage.Information,
            MessageBoxResult.Yes);
        WriteApiKey("");
    }

    /// <summary>
    /// Displays the current API key in a message box. If no API key is set, informs the user that it hasn't been set.
    /// </summary>
    /// <param name="sender">The object that raised the event (typically a button).</param>
    /// <param name="e">The event data associated with the button click event.</param>
    private void ShowApiKeyOnClick(object sender, RoutedEventArgs e)
    {
        var apiKey = GetApiKey();
        MessageBox.Show(apiKey == "" ? "You haven't set you API key yet!" : $"Your API key is: \n {apiKey}", "API key",
            MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.Yes);
    }

    /// <summary>
    /// Handles the file upload process. It checks if the API key and selected file are set, uploads the file, and displays success or failure messages.
    /// Resets the selected file and its attributes after the upload attempt.
    /// </summary>
    /// <param name="sender">The object that raised the event (typically a button).</param>
    /// <param name="e">The event data associated with the button click event.</param>
    private async void UploadFileOnClick(object sender, RoutedEventArgs e)
    {
        var apiKey = GetApiKey();

        if (apiKey == "")
        {
            MessageBox.Show("You haven't set you API key yet!");
        }
        else if (SelectedFile == "")
        {
            MessageBox.Show("You haven't selected file yet!");
        }
        else
        {
            bool success;
            
            if (new System.IO.FileInfo(SelectedFile).Length >= 32000)
            {
                success = await UploadLargeFile(apiKey, SelectedFile);
            }
            else
            {
                success = await UploadFile(apiKey, SelectedFile);
            };

            if (success)
            {
                AddScanEntry(ComputeSha256Hash(SelectedFile));
                MessageBox.Show("Successful upload!", "Request Status", MessageBoxButton.OK,
                    MessageBoxImage.Information, MessageBoxResult.Yes);
            }
            else
            {
                MessageBox.Show("Failed upload!", "Request Status", MessageBoxButton.OK, MessageBoxImage.Error,
                    MessageBoxResult.Yes);
            }

            SelectedFileAttributes.Children.Clear();
            SelectedFile = "";
        }
    }

    /// <summary>
    /// Handles the selection change event for the main tab control. 
    /// Clears the existing scan history and populates it with either a "No previous scans to show" message or a list of available scan IDs with corresponding report buttons, based on the selected tab.
    /// </summary>
    /// <param name="sender">The object that raised the event (typically the tab control).</param>
    /// <param name="e">The event data associated with the selection change event.</param>
    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ScanHistory.Children.Clear();

        if (MainTabControl.SelectedIndex == 1)
        {
            var rowIndex = 0;

            var scanHistory = GetScanHistory();

            if (scanHistory.Count.Equals(0))
            {
                ScanHistory.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

                var label = new Label()
                {
                    Content = "No previous scans to show.", Height = 35, Margin = new Thickness(0, 2, 0, 2),
                    VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center,
                };

                Grid.SetRow(label, rowIndex);
                Grid.SetColumnSpan(label, 2);

                ScanHistory.Children.Add(label);
            }

            scanHistory.ForEach(id =>
            {
                ScanHistory.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

                var trunkedId = string.Concat(id.AsSpan(0, 12), "...");

                var label = new Label()
                {
                    Content = trunkedId, Height = 35, Margin = new Thickness(0, 2, 0, 2),
                    VerticalAlignment = VerticalAlignment.Center
                };
                var button = new Button()
                {
                    Content = "Get Report", Height = 35, Margin = new Thickness(0, 2, 0, 2),
                    VerticalAlignment = VerticalAlignment.Center, Tag = id
                };
                button.Click += (sender, e) => DynamicButtonOnClick(sender, e, id);

                Grid.SetRow(label, rowIndex);
                Grid.SetColumn(label, 0);

                Grid.SetRow(button, rowIndex);
                Grid.SetColumn(button, 1);

                ScanHistory.Children.Add(label);
                ScanHistory.Children.Add(button);

                rowIndex++;
            });
        }
    }

    /// <summary>
    /// Handles the click event of a dynamic button and retrieves scan results for a given file ID.
    /// Displays a message box showing the scan status with detailed analysis statistics.
    /// </summary>
    /// <param name="sender">The object that raised the event (typically the dynamic button).</param>
    /// <param name="e">The event data associated with the button click event.</param>
    /// <param name="id">The unique identifier of the file whose scan results are to be retrieved.</param>
    private static async void DynamicButtonOnClick(object sender, RoutedEventArgs e, string id)
    {
        FileResponse stats = await GetScanResults(GetApiKey(), id);

        if (stats != null)
        {
            MessageBox.Show(
                $"{stats.data.attributes.names.ToArray().GetValue(0)}: \n Malicious: {stats.data.attributes.last_analysis_stats.malicious} \n Confirmed Timeout: {stats.data.attributes.last_analysis_stats.confirmed_timeout} \n Timeout: {stats.data.attributes.last_analysis_stats.timeout} \n Failure: {stats.data.attributes.last_analysis_stats.failure} \n Harmless: {stats.data.attributes.last_analysis_stats.harmless} \n Suspicious: {stats.data.attributes.last_analysis_stats.suspicious} \n Type unsupported: {stats.data.attributes.last_analysis_stats.type_unsupported} \n Undetected: {stats.data.attributes.last_analysis_stats.undetected}",
                "Scan Status", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Yes);
        }
        else
        {
            MessageBox.Show("Scan results could not be retrieved.", "Error", MessageBoxButton.OK, MessageBoxImage.Error,
                MessageBoxResult.Yes);
        }
    }

    /// <summary>
    /// Handles the click event for clearing the scan history.
    /// Clears the entry history and resets the main tab control to the first tab,
    /// then displays a success message to the user.
    /// </summary>
    /// <param name="sender">The object that raised the event (typically the button).</param>
    /// <param name="e">The event data associated with the button click event.</param>
    private void ClearHistoryOnClick(object sender, RoutedEventArgs e)
    {
        ClearEntryHistory();
        MainTabControl.SelectedIndex = 0;
        MessageBox.Show("Successfully cleared scan history!", "Success!", MessageBoxButton.OK,
            MessageBoxImage.Information, MessageBoxResult.Yes);
    }

    private void OpenCreditsOnClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(
            "This application is an independent third-party client for VirusTotal and is not affiliated with, endorsed by, or sponsored by VirusTotal or its parent company. All trademarks, service marks, and logos associated with VirusTotal are the property of their respective owners.\n\nThis software is provided \"as is\" without any warranties, express or implied. The developer of this application assumes no responsibility for the accuracy, legality, or reliability of the VirusTotal services used through this client.\n\nFor official VirusTotal services, please visit https://www.virustotal.com\n\n",
            "Notice", MessageBoxButton.OK, MessageBoxImage.Exclamation,
            MessageBoxResult.OK);
    }
}