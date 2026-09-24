# 🌟 Pulse

[![Build & Release](https://github.com/RobbyB97/pulse/actions/workflows/release.yml/badge.svg)](https://github.com/RobbyB97/pulse/actions/workflows/release.yml)

> **Automatically change your wallpaper** by downloading stunning images from the internet — all driven by your own search terms.

Forked from ~~[CodePlex Archive](https://archive.codeplex.com/?p=pulse)~~ *(link broken)*

---

## ✨ Features

| Feature | Description |
|---|---|
| 🖥️ **Resolution-Aware** | Automatically picks wallpapers that match your screen resolution |
| 🚫 **Ban Wallpapers** | Ban any wallpaper you dislike — Pulse immediately fetches a fresh replacement |
| ⚡ **Smart Caching** | Search results and wallpapers are cached for fast load times |
| 📦 **Pre-Fetch** | Bulk-download all wallpapers returned by a search in one click |
| 🔄 **Auto-Change on Startup** | Wallpaper changes automatically every time Pulse launches |

---

## 🔌 Providers

Pulse uses a plugin-based provider system. **Input providers** fetch image URLs; **output providers** apply wallpapers/effects.

### 📥 Input Providers

| Provider | Source | Status | Notes |
|---|---|---|---|
| 🖼️ **Local Directory** | Local filesystem | ✅ Working | Scans a folder for images by extension |
| 🌌 **Wallhaven** | [wallhaven.cc](https://wallhaven.cc) | ✅ Working | Official API v1: keywords, categories, purity, resolution & colors |
| 📡 **Media RSS** | Any MediaRSS feed URL | ✅ Working | Works with DeviantArt and other RSS feeds |
| 🚀 **NASA APOD** | [apod.nasa.gov](https://apod.nasa.gov/apod/archivepix.html) | ✅ Working | Scrapes the Astronomy Picture of the Day archive |
| 🌍 **National Geographic** | ngm.nationalgeographic.com | ❌ Dead | Old endpoint returns 403 — needs URL update |
| 🔍 **Google Images** | images.google.com | ❌ Blocked | Google bot-detection blocks scraping |

### 📤 Output Providers

| Provider | Purpose | Status |
|---|---|---|
| 🖥️ **Wallpaper Setter** | Sets the Windows desktop wallpaper | ✅ Working |
| 🪟 **Logon Background** | Changes the Windows 7 login screen background | ⚠️ Win 7 only |
| 💎 **Aero Glass Changer** | Changes the Aero glass accent colour | ⚠️ Win 7/8 only |
| 📦 **Piler** | Saves/organises downloaded images | ✅ Working |


![Pulse UI](images/UI.png)

---

## 📁 Project Structure

The solution (`Pulse.sln`) is organized into core components, plugin providers, and test suites:

```
pulse/
├── PulseForm/                 # Main Windows Forms application (compiles to Pulse.exe)
│   ├── Tray icon lifecycle, options dialog, wallpaper scheduling
├── Pulse.Base/                # Core engine library (Pulse.Base.dll)
│   ├── Provider interfaces (IInputProvider, IOutputProvider)
│   ├── Dynamic plugin discovery & loading (ProviderManager)
│   ├── Wallpaper rotation engine (PulseRunner), DownloadManager, Settings
├── Pulse.Forms.UI/            # WinForms UI controls & monitors (Pulse.Forms.UI.dll)
│   ├── DownloadMonitor & DownloadQueue dialogs, provider settings controls
├── Providers/                 # Plugin providers (compiled into bin/<Config>/Providers/)
│   ├── Wallhaven/             # [Input] Wallhaven API v1 wallpaper search provider
│   ├── LocalDirectory/        # [Input] Scans local folders for wallpapers
│   ├── MediaRSS/              # [Input] Scrapes MediaRSS feeds (e.g., DeviantArt)
│   ├── NASAAPOD/              # [Input] Scrapes NASA Astronomy Picture of the Day
│   ├── GoogleImages/          # [Input] Google Images scraper (blocked by bot checks)
│   ├── NationalGeographicWallpapers/ # [Input] Legacy NatGeo photo scraper
│   ├── WallpaperSetter/       # [Output] Sets desktop wallpaper via Windows API
│   ├── Piler/                 # [Output] Automatically archives downloaded pictures
│   ├── LogonBackground/      # [Output] Windows 7 logon background changer
│   ├── AeroGlassChanger/      # [Output] Windows 7/8 Aero Glass tint synchronizer
│   └── WinAPI/                # Shared native Windows API P/Invoke interop library
├── Pulse.Tests/               # Unit and integration test suite (MSTest)
├── PulseMassDownloader/       # Standalone bulk wallpaper downloader utility
└── References/                # Third-party pre-compiled dependencies
```

---

## 🔨 Building & Running

### 📋 Prerequisites

- **Operating System:** Windows 7 / 8 / 10 / 11
- **Target Framework:** [.NET Framework 4.8 Developer Pack](https://dotnet.microsoft.com/download/dotnet-framework/net48)
- **Build Tools:**
  - [Visual Studio](https://visualstudio.microsoft.com/) 2019 / 2022 (with the **.NET desktop development** workload), OR
  - [Visual Studio Build Tools](https://visualstudio.microsoft.com/downloads/#build-tools-for-visual-studio-2022) with MSBuild
  - [NuGet CLI](https://www.nuget.org/downloads) (or Visual Studio package restore)

### 🛠️ Building from Source

#### Option 1: Visual Studio
1. Clone the repository:
   ```cmd
   git clone https://github.com/RobbyB97/pulse.git
   cd pulse
   ```
2. Open `Pulse.sln` in Visual Studio.
3. Select your configuration: **Release** (or **Debug**) and **Mixed Platforms**.
4. Restore packages: Right-click the solution in Solution Explorer > **Restore NuGet Packages**.
5. Build: Press `Ctrl+Shift+B` or select **Build > Build Solution**.

#### Option 2: Command Line (MSBuild & NuGet)
1. Restore NuGet dependencies:
   ```cmd
   nuget restore Pulse.sln
   ```
2. Build the solution using MSBuild:
   ```cmd
   msbuild Pulse.sln /p:Configuration=Release /p:"Platform=Mixed Platforms" /m
   ```

> [!NOTE]
> Build outputs are placed directly in the `bin\` directory:
> - Application: `bin\Release\Pulse.exe`
> - Plugins: `bin\Release\Providers\*.dll` (automatically output to the `Providers\` subfolder)

### 🏃 Running Pulse

1. Navigate to the build output directory:
   ```cmd
   cd bin\Release
   ```
2. Run `Pulse.exe`:
   ```cmd
   .\Pulse.exe
   ```
3. Pulse runs in the background as a **System Tray** application:
   - Look for the Pulse icon in the Windows notification area (bottom-right taskbar).
   - **Double-click** the tray icon to advance to the next wallpaper.
   - **Right-click** the tray icon to access:
     - **Next / Previous Picture:** Cycle through wallpapers.
     - **Ban Picture:** Discard the current wallpaper and fetch a fresh replacement immediately.
     - **Pre-Fetch:** Bulk-download all wallpapers returned by the active search.
     - **Settings / Options:** Configure search queries, provider selection, rotation intervals, and resolution matching.
     - **Download Monitor:** Inspect the active image download queue and status.

### 🧪 Running Tests

Pulse includes unit and integration tests using MSTest.

- **In Visual Studio:** Open **Test > Test Explorer** and click **Run All Tests In View** (`Ctrl+R, A`).
- **Via Command Line:**
  ```cmd
  vstest.console.exe Pulse.Tests\bin\Release\Pulse.Tests.dll /TestAdapterPath:packages\MSTest.TestAdapter.3.6.0\build\net462
  ```

---

## 📥 Download

> ⚠️ Pre-built binary releases are not currently available. Follow the [Building from Source](#%EF%B8%8F-building-from-source) instructions above to compile and run Pulse.

---

## 📰 Articles & Press

- [Lifehacker — *Pulse Creates Wallpaper Slideshows Based on Keywords*](https://lifehacker.com/5799438/pulse-creates-wallpaper-slideshows-based-on-keywords)
- [Redmond Pie — *Pulse for Windows 7: Changes Your Login Screen Background and Desktop Wallpaper Periodically*](https://www.redmondpie.com/pulse-for-windows-7-changes-your-login-screen-background-and-desktop-wallpaper-periodically/)
- [TV 2 Nyheder (Danish) — *Aldrig mere kedeligt Windows*](http://nyheder.tv2.dk/article/aldrig-mere-kedeligt-windows)
- [Chip.com.tr (Turkish) — *Masaüstü Resimlerini Otomatik Değiştirin*](http://www.chip.com.tr/konu/masaustu-resimlerini-otomatik-degistirin_27164.html)
- ~~[GuidingTech — *Automatically Download Wallpapers from NatGeo, Google Images & Wallbase*](https://www.guidingtech.com/10320/automatically-download-wallpapers-natgeo-google-images-wallbase/)~~ *(link broken — 404)*

---

## 🛠️ Built With

- **C# / .NET Framework 4.8** — Windows Desktop Application
- **Windows Forms (WinForms)** — User interface and system tray management
- **Windows API (P/Invoke)** — Desktop wallpaper manipulation (`SystemParametersInfo` / `ActiveDesktop`), Aero Glass, and logon integration
- **CsQuery** — HTML parsing and scraping for web-based providers
- **MSTest v3** — Test framework and test adapter

---

*Originally published on CodePlex. Forked and maintained here.*
