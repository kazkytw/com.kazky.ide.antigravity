# Contributing to Antigravity Unity Integration

Thank you for your interest in contributing to this project! 🎉

## How to Contribute

### Reporting Bugs

If you find a bug, please create an issue with:
- Clear description of the problem
- Steps to reproduce
- Expected vs actual behavior
- Unity version and OS information
- Antigravity version (if applicable)

### Suggesting Features

Feature requests are welcome! Please include:
- Clear description of the feature
- Use case and benefits
- Possible implementation approach (optional)

### Code Contributions

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/your-feature-name`
3. **Make your changes**
4. **Test thoroughly** in Unity
5. **Commit with clear messages**: `git commit -m "Add: Description of changes"`
6. **Push to your fork**: `git push origin feature/your-feature-name`
7. **Open a Pull Request**

## Code Standards

### C# Style Guide

- Follow Microsoft's [C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Keep methods focused and under 50 lines when possible

**Example:**

```csharp
/// <summary>
/// Opens a file in Antigravity at the specified line and column
/// </summary>
/// <param name="filePath">Absolute path to the file</param>
/// <param name="line">Line number (0-indexed)</param>
/// <param name="column">Column number (0-indexed)</param>
/// <returns>True if file was opened successfully</returns>
public bool OpenFile(string filePath, int line = -1, int column = -1)
{
    // Implementation
}
```

### Naming Conventions

- **Classes/Interfaces**: PascalCase (e.g., `AntigravityEditor`)
- **Methods**: PascalCase (e.g., `GetInstallationPath`)
- **Fields (private)**: camelCase with `m_` prefix (e.g., `m_Discovery`)
- **Properties**: PascalCase (e.g., `InstallationPath`)
- **Constants**: PascalCase (e.g., `MaxRetryCount`)

### Testing

Before submitting:

1. Test on target Unity versions (2019.4+)
2. Test on all supported platforms if possible (Windows, macOS, Linux)
3. Verify no console errors or warnings
4. Test file opening and project generation

## Pull Request Process

1. **Update documentation** if you're changing functionality
2. **Update CHANGELOG.md** with your changes
3. **Ensure all tests pass** (when implemented)
4. **Describe your changes** clearly in the PR description
5. **Link related issues** if applicable

### PR Checklist

- [ ] Code follows the style guidelines
- [ ] Self-review completed
- [ ] Comments added for complex logic
- [ ] Documentation updated
- [ ] CHANGELOG.md updated
- [ ] Tested in Unity
- [ ] No new warnings or errors

## Development Setup

1. Clone the repository
2. Open Unity (2019.4 or later)
3. Add the package to your test project via Package Manager
4. Make changes and test directly

## Questions?

Feel free to open a discussion issue or reach out to maintainers.

## License

By contributing, you agree that your contributions will be licensed under the MIT License.

---

Thank you for contributing! 🚀
