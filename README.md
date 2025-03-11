[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![project_license][license-shield]][license-url]

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/levy-y/virustotal-desktop" id="readme-top">
    <img src=".github/images/logo.png" alt="Logo" width="80" height="80">
  </a>

<h3 align="center">VirusTotal Desktop</h3>

  <p align="center">
    A simple desktop application that allows users to scan files using the VirusTotal API directly from their Windows machine.
    <br />
    <br />
    <a href="https://github.com/levy-y/virustotal-desktop/issues/new?labels=bug&template=bug-report---.md">Report Bug</a>
    &middot;
    <a href="https://github.com/levy-y/virustotal-desktop/issues/new?labels=enhancement&template=feature-request---.md">Request Feature</a>
  </p>
</div>

<!-- ABOUT THE PROJECT -->
## About The Project
VirusTotal Desktop allows users to easily check files for viruses and malware using the VirusTotal API without needing to open a browser.




### Built With

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

---

<!-- GETTING STARTED -->
## Getting Started

Follow these instructions to install the VirusTotal Desktop Scanner on your system, either by downloading a prebuilt release or building from source.

### Prerequisites

Ensure you have the following installed on your system before proceeding:

- **Windows 10/11** (required for WPF applications)
- **.NET SDK (latest LTS version recommended)**  
  Download and install from [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
- **Git** (if building from source)  
  Install from [https://git-scm.com/downloads](https://git-scm.com/downloads)

---

### Installation (From Releases)

1. Go to the [Releases](https://github.com/levy-y/virustotal-desktop/releases) section of the repository.
2. Download `virustotal-desktop.exe` from the latest release.
3. Run `virustotal-desktop.exe`.

---

### Building from Source

1. Clone the repository:
```sh
git clone https://github.com/levy-y/virustotal-desktop.git
cd virustotal-desktop
```

2. Build the project:

```sh
dotnet build --configuration Release
```

3. Run the `virustotal-desktop.exe` in the `\bin\Release\net8.0-windows\win-x64` folder

### API Key Setup
To use the VirusTotal API, you need to set up your API key.
2. Get a free API Key at https://www.virustotal.com/gui/join-us
3. Open the `Settings` tab -> `Api Key` -> `Edit`, and paste your key there

<p align="right">(<a href="#readme-top">back to top</a>)</p>

---

<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>


---
<!-- LICENSE -->
## License

Distributed under the `MIT` license. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>


[stars-shield]: https://img.shields.io/github/stars/levy-y/virustotal-desktop.svg?style=for-the-badge
[stars-url]: https://github.com/levy-y/virustotal-desktop/stargazers
[issues-shield]: https://img.shields.io/github/issues/levy-y/virustotal-desktop.svg?style=for-the-badge
[issues-url]: https://github.com/levy-y/virustotal-desktop/issues
[license-shield]: https://img.shields.io/github/license/levy-y/virustotal-desktop.svg?style=for-the-badge
[license-url]: https://github.com/levy-y/virustotal-desktop/blob/master/LICENSE.txt