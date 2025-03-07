using System.Windows;

using static virustotal_desktop.Utils.Utilities;

namespace virustotal_desktop;

public partial class ApiKeyPrompt : Window
{
    /// <summary>
    /// Initializes a new instance of the ApiKeyPrompt class.
    /// </summary>
    public ApiKeyPrompt()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Handles the click event for editing the API key. It writes the entered API key to a storage location,
    /// displays a success message with the location of the stored API key, and then closes the current window.
    /// </summary>
    /// <param name="sender">The object that raised the event (typically a button).</param>
    /// <param name="e">The event data associated with the button click event.</param>
    private void EditApiKeyOnClick(object sender, RoutedEventArgs e)
    {
        WriteApiKey(ApiKeyInput.Text);
        MessageBox.Show($"Successfully set API key! \n Location: {Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}", "Success!", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
        Close();
    }
}