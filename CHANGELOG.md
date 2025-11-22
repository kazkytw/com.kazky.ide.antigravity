# Changelog

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2025-11-22

### Important Changes
- **Package Name**: Uses `com.kazky.ide.antigravity` namespace to comply with Unity package attribution rules
- Avoids conflicts with Unity's official `com.unity.*` packages

### Added
- Initial release of Antigravity Unity IDE integration
- Auto-discovery of Antigravity installations on Windows, macOS, and Linux
- Support for generating .csproj files for IntelliSense
- Integration with Unity's External Tools preferences
- Seamless file opening from Unity to Antigravity
- Cross-platform support
- AI-powered coding assistance integration
- Main process check to prevent initialization in background Unity processes
- Proper copyright headers in all source files

### Technical Details
- **Namespace**: `Kazky.Unity.IDE.Antigravity`
- **Features**: 
  - `AntigravityEditor.cs`: Implements IExternalCodeEditor interface
  - `AntigravityInstallation.cs`: Cross-platform installation detection
  - `ProjectGeneration.cs`: VS-compatible project file generation
  - `UnityInstallation.cs`: Main process detection helper

### Compatibility
- Unity 2019.4 or later
- Windows, macOS, and Linux support

### Features
- **AntigravityInstallation.cs**: Detects Antigravity installations across platforms
- **AntigravityEditor.cs**: Implements IExternalCodeEditor interface
- **ProjectGeneration**: Generates Visual Studio-compatible project files
- **Discovery.cs**: Smart discovery mechanism for editor installations

### Compatibility
- Unity 2019.4 or later
- Windows, macOS, and Linux support

---

## Future Roadmap

### Planned Features
- [ ] Direct communication between Unity and Antigravity
- [ ] Real-time error highlighting from Unity console
- [ ] AI-powered code suggestions based on Unity API
- [ ] Automatic script template generation
- [ ] Enhanced debugging integration
- [ ] Custom Antigravity panels within Unity Editor
