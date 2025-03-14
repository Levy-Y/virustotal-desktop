using System.Configuration;
using System.Data;
using System.Windows;

namespace virustotal_desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Overrides the OnStartup method to accept input arguments,
    /// and pass the first element of the arguments to the MainWindow.
    /// </summary>
    /// <param name="e">Array of arguments provided on startup</param>
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        if (e.Args.Length > 0)
        {
            var filePath = e.Args[0];
            
            new MainWindow(filePath).Show();
        }
        else
        {
            new MainWindow().Show();
        }
    }
}