using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Windows;
using RestSharp;
using virustotal_desktop.Models;

namespace virustotal_desktop.Utils;

using Newtonsoft.Json;

public class Utilities
{
    /// <summary>
    /// The folder path to store application data for the VirustotalDesktop app in the user's Application Data directory.
    /// </summary>
    private static readonly string AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\VirustotalDesktop";
    
    /// <summary>
    /// The RestClient instance used to send HTTP requests to the VirusTotal API for file analysis.
    /// </summary>
    private static readonly RestClient Client = new RestClient(new RestClientOptions("https://www.virustotal.com/api/v3/files"));
    
    /// <summary>
    /// Computes the SHA-256 hash of a file located at the specified path.
    /// </summary>
    /// <param name="filePath">The path to the file to hash.</param>
    /// <returns>The SHA-256 hash of the file as a hexadecimal string.</returns>
    public static string ComputeSha256Hash(string filePath)
    {
        using SHA256 sha256Hash = SHA256.Create();
        using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var hashBytes = sha256Hash.ComputeHash(fileStream);
        var hashStringBuilder = new StringBuilder();
        foreach (var b in hashBytes)
        {
            hashStringBuilder.Append(b.ToString("x2"));
        }
        return hashStringBuilder.ToString();
    }
    
    
    /// <summary>
    /// Clears the scan history by writing an empty string to the history file.
    /// </summary>
    public static void ClearEntryHistory()
    {
        if (!Directory.Exists(AppDataFolder))
        {
            Directory.CreateDirectory(AppDataFolder);
        }
        var writer = new StreamWriter(AppDataFolder + @"\scan_history.txt");
        writer.Write("");
        writer.Close();
    }
    
    /// <summary>
    /// Adds a new scan entry to the scan history file.
    /// </summary>
    /// <param name="entry">The scanned file's hash to add to the history.</param>
    public static void AddScanEntry(string entry)
    {
        if (!Directory.Exists(AppDataFolder))
        {
            Directory.CreateDirectory(AppDataFolder);
        }
        var writer = new StreamWriter(AppDataFolder + @"\scan_history.txt", true);
        writer.WriteLine(entry);
        writer.Close();
    }
    
    /// <summary>
    /// Writes the provided API key to a file for future use.
    /// </summary>
    /// <param name="apiKey">The API key to write to the file.</param>
    public static void WriteApiKey(string apiKey)
    {
        if (!Directory.Exists(AppDataFolder))
        {
            Directory.CreateDirectory(AppDataFolder);
        }
        var writer = new StreamWriter(AppDataFolder + @"\vt_ak_dat.dat");
        writer.Write(apiKey);
        writer.Close();
    }
    
    /// <summary>
    /// Retrieves the API key from the stored file.
    /// </summary>
    /// <returns>The API key stored in the file, or an empty string if no key is stored.</returns>
    public static string GetApiKey()
    {
        using var reader = new StreamReader(AppDataFolder + @"\vt_ak_dat.dat");
        return reader.ReadToEnd();
    }
    
    /// <summary>
    /// Retrieves the scan history as a list of strings from the stored history file.
    /// </summary>
    /// <returns>A list of scan history entries.</returns>
    public static List<string> GetScanHistory()
    {
        List<string> result = [];
        
        using var reader = new StreamReader(AppDataFolder + @"\scan_history.txt");
        while (reader.EndOfStream == false)
        {
            result.Add(reader.ReadLine());
        }
        
        return result;
    }

    /// <summary>
    /// Uploads a file to the VirusTotal API for analysis using the provided API key.
    /// </summary>
    /// <param name="apiKey">The API key used for authentication with VirusTotal.</param>
    /// <param name="filePath">The path to the file to upload.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is true if the file was successfully uploaded, otherwise false.</returns>
    public static async Task<bool> UploadFile(string apiKey, string filePath)
    {
        var request = new RestRequest("")
        {
            AlwaysMultipartFormData = true
        };
        
        request.AddHeader("accept", "application/json");
        request.AddHeader("x-apikey", apiKey);
        request.FormBoundary = "---011000010111000001101001";
        request.AddFile("file", filePath);
        
        var response = await Client.PostAsync(request);
        using JsonDocument doc = JsonDocument.Parse(response.Content);
        var id = doc.RootElement.GetProperty("data").GetProperty("id").GetString();

        if (id != null)
        {
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// Retrieves the scan results for a file from VirusTotal based on its SHA-256 hash.
    /// </summary>
    /// <param name="apiKey">The API key used for authentication with VirusTotal.</param>
    /// <param name="sha256hash">The SHA-256 hash of the file to retrieve scan results for.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the scan results for the specified file.</returns>
    public static async Task<FileResponse> GetScanResults(string apiKey, string sha256hash)
    {
        var options = new RestClientOptions($"https://www.virustotal.com/api/v3/files/{sha256hash}");
        var client = new RestClient(options);
        var request = new RestRequest("");
        request.AddHeader("accept", "application/json");
        request.AddHeader("x-apikey", apiKey);
        
        var response = await client.GetAsync(request);
        FileResponse lastAnalysisResults = JsonConvert.DeserializeObject<FileResponse>(response.Content);
        
        return lastAnalysisResults;
    }
}