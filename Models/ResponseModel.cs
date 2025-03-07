namespace virustotal_desktop.Models;

/// <summary>
/// Represents the top-level file response which contains the data object.
/// </summary>
public class FileResponse
{
    /// <summary>
    /// Gets or sets the data associated with the file response.
    /// </summary>
    public Data data { get; set; }
}

/// <summary>
/// Contains detailed information about the file, including its identifiers, analysis results, and metadata.
/// </summary>
public class Data
{
    /// <summary>
    /// Gets or sets the unique identifier of the file.
    /// </summary>
    public string id { get; set; }

    /// <summary>
    /// Gets or sets the type of the file response.
    /// </summary>
    public string type { get; set; }

    /// <summary>
    /// Gets or sets the links related to the file.
    /// </summary>
    public Links links { get; set; }

    /// <summary>
    /// Gets or sets the attributes associated with the file.
    /// </summary>
    public Attributes attributes { get; set; }

    /// <summary>
    /// Gets or sets the SHA-256 hash of the file.
    /// </summary>
    public string sha256 { get; set; }

    /// <summary>
    /// Gets or sets the size of the file in bytes.
    /// </summary>
    public int size { get; set; }

    /// <summary>
    /// Gets or sets the MD5 hash of the file.
    /// </summary>
    public string md5 { get; set; }

    /// <summary>
    /// Gets or sets the results of the file's last analysis.
    /// </summary>
    public LastAnalysisResults last_analysis_results { get; set; }
}

/// <summary>
/// Represents links related to the file, such as its self-reference URL.
/// </summary>
public class Links
{
    /// <summary>
    /// Gets or sets the URL linking to the file's information.
    /// </summary>
    public string self { get; set; }
}

/// <summary>
/// Contains various attributes related to the file, including tags, analysis details, and file metadata.
/// </summary>
public class Attributes
{
    /// <summary>
    /// Gets or sets a list of tags associated with the file.
    /// </summary>
    public List<string> tags { get; set; }

    /// <summary>
    /// Gets or sets information about the file bundle.
    /// </summary>
    public BundleInfo bundle_info { get; set; }

    /// <summary>
    /// Gets or sets the number of times the file has been submitted for analysis.
    /// </summary>
    public int times_submitted { get; set; }

    /// <summary>
    /// Gets or sets the number of unique sources from which the file originated.
    /// </summary>
    public int unique_sources { get; set; }

    /// <summary>
    /// Gets or sets the magic type of the file.
    /// </summary>
    public string magika { get; set; }

    /// <summary>
    /// Gets or sets the SSDEEP hash of the file.
    /// </summary>
    public string ssdeep { get; set; }

    /// <summary>
    /// Gets or sets a description of the file type.
    /// </summary>
    public string type_description { get; set; }

    /// <summary>
    /// Gets or sets the file's vhash.
    /// </summary>
    public string vhash { get; set; }

    /// <summary>
    /// Gets or sets a list of type tags related to the file.
    /// </summary>
    public List<string> type_tags { get; set; }

    /// <summary>
    /// Gets or sets a list of names associated with the file.
    /// </summary>
    public List<string> names { get; set; }

    /// <summary>
    /// Gets or sets the primary type tag for the file.
    /// </summary>
    public string type_tag { get; set; }

    /// <summary>
    /// Gets or sets the date of the last analysis.
    /// </summary>
    public long last_analysis_date { get; set; }

    /// <summary>
    /// Gets or sets the date of the last modification.
    /// </summary>
    public long last_modification_date { get; set; }

    /// <summary>
    /// Gets or sets a list of TrID analysis results for the file.
    /// </summary>
    public List<Trid> trid { get; set; }

    /// <summary>
    /// Gets or sets the file extension.
    /// </summary>
    public string type_extension { get; set; }

    /// <summary>
    /// Gets or sets the last analysis statistics for the file.
    /// </summary>
    public LastAnalysisStats last_analysis_stats { get; set; }
}

/// <summary>
/// Represents information about a file bundle.
/// </summary>
public class BundleInfo
{
    /// <summary>
    /// Gets or sets the datetime of the highest file in the bundle.
    /// </summary>
    public string highest_datetime { get; set; }

    /// <summary>
    /// Gets or sets the datetime of the lowest file in the bundle.
    /// </summary>
    public string lowest_datetime { get; set; }

    /// <summary>
    /// Gets or sets the number of children files in the bundle.
    /// </summary>
    public int num_children { get; set; }

    /// <summary>
    /// Gets or sets the extensions associated with the bundle.
    /// </summary>
    public Extensions extensions { get; set; }

    /// <summary>
    /// Gets or sets the file types associated with the bundle.
    /// </summary>
    public FileTypes file_types { get; set; }

    /// <summary>
    /// Gets or sets the type of the bundle.
    /// </summary>
    public string type { get; set; }

    /// <summary>
    /// Gets or sets the uncompressed size of the bundle.
    /// </summary>
    public int uncompressed_size { get; set; }
}

/// <summary>
/// Represents file extensions in a bundle.
/// </summary>
public class Extensions
{
    public int cfg { get; set; }
    public int json { get; set; }
    public int txt { get; set; }
    public int ini { get; set; }
    public int r2x { get; set; }
}

/// <summary>
/// Represents file types in a bundle.
/// </summary>
public class FileTypes
{
    public int unknown { get; set; }
    public int JSON { get; set; }
}

/// <summary>
/// Represents the result of a TrID file type analysis.
/// </summary>
public class Trid
{
    /// <summary>
    /// Gets or sets the detected file type.
    /// </summary>
    public string file_type { get; set; }

    /// <summary>
    /// Gets or sets the probability of the file type detection.
    /// </summary>
    public double probability { get; set; }
}

/// <summary>
/// Contains the analysis statistics for the file.
/// </summary>
public class LastAnalysisStats
{
    public int malicious { get; set; }
    public int suspicious { get; set; }
    public int undetected { get; set; }
    public int harmless { get; set; }
    public int timeout { get; set; }
    public int confirmed_timeout { get; set; }
    public int failure { get; set; }
    public int type_unsupported { get; set; }
}

/// <summary>
/// Contains the results of the file's analysis from various antivirus engines.
/// </summary>
public class LastAnalysisResults
{
    public AntivirusResult Bkav { get; set; }
    public AntivirusResult Lionic { get; set; }
    public AntivirusResult Cynet { get; set; }
    public AntivirusResult CTX { get; set; }
    public AntivirusResult CAT_QuickHeal { get; set; }
    public AntivirusResult Skyhigh { get; set; }
    public AntivirusResult ALYac { get; set; }
    public AntivirusResult Malwarebytes { get; set; }
    public AntivirusResult Zillya { get; set; }
    public AntivirusResult Sangfor { get; set; }
    public AntivirusResult Trustlook { get; set; }
    public AntivirusResult Alibaba { get; set; }
    public AntivirusResult K7GW { get; set; }
    public AntivirusResult K7AntiVirus { get; set; }
    public AntivirusResult Baidu { get; set; }
    public AntivirusResult VirIT { get; set; }
    public AntivirusResult Symantec { get; set; }
    public AntivirusResult ESET_NOD32 { get; set; }
    public AntivirusResult TrendMicro_HouseCall { get; set; }
    public AntivirusResult Avast { get; set; }
    public AntivirusResult ClamAV { get; set; }
    public AntivirusResult Kaspersky { get; set; }
    public AntivirusResult BitDefender { get; set; }
    public AntivirusResult NANO_Antivirus { get; set; }
    public AntivirusResult SUPERAntiSpyware { get; set; }
    public AntivirusResult MicroWorld_eScan { get; set; }
    public AntivirusResult Tencent { get; set; }
    public AntivirusResult Emsisoft { get; set; }
    public AntivirusResult F_Secure { get; set; }
    public AntivirusResult DrWeb { get; set; }
    public AntivirusResult VIPRE { get; set; }
    public AntivirusResult TrendMicro { get; set; }
    public AntivirusResult CMC { get; set; }
    public AntivirusResult Sophos { get; set; }
    public AntivirusResult Ikarus { get; set; }
    public AntivirusResult FireEye { get; set; }
    public AntivirusResult Jiangmin { get; set; }
    public AntivirusResult Webroot { get; set; }
    public AntivirusResult Varist { get; set; }
    public AntivirusResult Avira { get; set; }
    public AntivirusResult Fortinet { get; set; }
    public AntivirusResult Antiy_AVL { get; set; }
    public AntivirusResult Kingsoft { get; set; }
    public AntivirusResult Gridinsoft { get; set; }
    public AntivirusResult Xcitium { get; set; }
    public AntivirusResult Arcabit { get; set; }
    public AntivirusResult ViRobot { get; set; }
    public AntivirusResult Avast_Mobile { get; set; }
    public AntivirusResult Microsoft { get; set; }
    public AntivirusResult Google { get; set; }
    public AntivirusResult AhnLab_V3 { get; set; }
    public AntivirusResult Acronis { get; set; }
    public AntivirusResult McAfee { get; set; }
    public AntivirusResult TACHYON { get; set; }
    public AntivirusResult VBA32 { get; set; }
    public AntivirusResult Zoner { get; set; }
    public AntivirusResult Rising { get; set; }
    public AntivirusResult Yandex { get; set; }
    public AntivirusResult huorong { get; set; }
    public AntivirusResult MaxSecure { get; set; }
}

/// <summary>
/// Represents the result of an antivirus scan.
/// </summary>
public class AntivirusResult
{
    public string method { get; set; }
    public string engine_name { get; set; }
    public string engine_version { get; set; }
    public string engine_update { get; set; }
    public string category { get; set; }
    public string result { get; set; }
}
