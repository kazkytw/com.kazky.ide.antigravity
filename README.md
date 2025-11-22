# Antigravity Editor Package for Unity

> [!IMPORTANT]
> **Package Naming Notice**: This package uses the namespace `com.kazky.ide.antigravity` instead of `com.unity.ide.antigravity` to prevent conflicts with Unity's official package attribution rules. Using `com.unity.*` for third-party packages may trigger warnings in Unity Editor.

Code editor integration for supporting **Antigravity** as the code editor for Unity. 

## Features

- 🤖 **AI-Powered Development**: Seamless integration with Antigravity's AI coding assistant
- 📁 **Project File Generation**: Automatic generation of .csproj files for IntelliSense
- 🔍 **Auto-Discovery**: Automatically detects Antigravity installations on your system
- 🌐 **Cross-Platform**: Works on Windows, macOS, and Linux
- ⚡ **Smart Opening**: Double-click C# files in Unity to open them in Antigravity

## Installation

### Via Package Manager (Recommended)

1. Open Unity Editor
2. Go to **Window → Package Manager**
3. Click the **"+"** button in the top-left corner
4. Select **"Add package from git URL..."**
5. Enter the following URL:
   ```
   https://github.com/YOUR_USERNAME/com.kazky.ide.antigravity.git
   ```
6. Click **"Add"**

### Manual Installation

1. Clone or download this repository
2. Copy the folder to your Unity project's `Packages` directory
3. Unity will automatically detect and install the package

## Usage

After installation:

1. Go to **Edit → Preferences** (Windows/Linux) or **Unity → Preferences** (macOS)
2. Select **External Tools**
3. Set **External Script Editor** to **Antigravity**
4. Click **"Regenerate project files"** if needed

Now when you double-click any C# script in Unity, it will open in Antigravity!

## How It Works

This package integrates Antigravity with Unity by:

- Implementing Unity's `IExternalCodeEditor` interface
- Detecting Antigravity installations across different platforms
- Generating Visual Studio-compatible .csproj files for IntelliSense
- Providing seamless file opening with AI assistance

## Compatibility

- **Unity Version**: 2019.4 or later
- **Platforms**: Windows, macOS, Linux
- **Antigravity**: Compatible with all versions

## About Antigravity

Antigravity is a powerful AI coding assistant developed by Google DeepMind's Advanced Agentic Coding team. It provides intelligent code completion, refactoring, and development assistance.

## License

MIT License - See [LICENSE.md](LICENSE.md) for details.

## Contributing

Contributions are welcome! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

---

**Note**: This package is not officially affiliated with Unity Technologies or Google DeepMind. It is a community-developed integration tool.
