/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Kazky. All rights reserved.
 *  Based on Unity Technologies' Visual Studio integration
 *  Licensed under the MIT License. See LICENSE.md for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.IO;
using System.Linq;
using UnityEngine;
using Unity.CodeEditor;

namespace Kazky.Unity.IDE.Antigravity
{
    /// <summary>
    /// Antigravity installation detection and discovery
    /// </summary>
    public class AntigravityInstallation : IDiscovery
    {
        public CodeEditor.Installation[] PathCallback()
        {
            return GetAntigravityInstallations();
        }

        private static CodeEditor.Installation[] GetAntigravityInstallations()
        {
#if UNITY_EDITOR_WIN
            return GetWindowsInstallations();
#elif UNITY_EDITOR_OSX
            return GetMacOSInstallations();
#elif UNITY_EDITOR_LINUX
            return GetLinuxInstallations();
#else
            return new CodeEditor.Installation[0];
#endif
        }

#if UNITY_EDITOR_WIN
        private static CodeEditor.Installation[] GetWindowsInstallations()
        {
            var installations = new System.Collections.Generic.List<CodeEditor.Installation>();

            // Check common installation paths for Antigravity on Windows
            var possiblePaths = new[]
            {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "Antigravity", "Antigravity.exe"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Antigravity", "Antigravity.exe"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Antigravity", "Antigravity.exe"),
                // Add more potential paths as needed
            };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    installations.Add(new CodeEditor.Installation
                    {
                        Name = "Antigravity",
                        Path = path
                    });
                    break; // Use first found installation
                }
            }

            return installations.ToArray();
        }
#endif

#if UNITY_EDITOR_OSX
        private static CodeEditor.Installation[] GetMacOSInstallations()
        {
            var installations = new System.Collections.Generic.List<CodeEditor.Installation>();

            // Check common installation paths for Antigravity on macOS
            var possiblePaths = new[]
            {
                "/Applications/Antigravity.app",
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Applications", "Antigravity.app"),
            };

            foreach (var path in possiblePaths)
            {
                if (Directory.Exists(path))
                {
                    installations.Add(new CodeEditor.Installation
                    {
                        Name = "Antigravity",
                        Path = path
                    });
                    break;
                }
            }

            return installations.ToArray();
        }
#endif

#if UNITY_EDITOR_LINUX
        private static CodeEditor.Installation[] GetLinuxInstallations()
        {
            var installations = new System.Collections.Generic.List<CodeEditor.Installation>();

            // Check common installation paths for Antigravity on Linux
            var possiblePaths = new[]
            {
                "/usr/bin/antigravity",
                "/usr/local/bin/antigravity",
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".local", "bin", "antigravity"),
            };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    installations.Add(new CodeEditor.Installation
                    {
                        Name = "Antigravity",
                        Path = path
                    });
                    break;
                }
            }

            return installations.ToArray();
        }
#endif

        public static string GetAntigravityPath()
        {
            var installations = GetAntigravityInstallations();
            return installations.Length > 0 ? installations[0].Path : string.Empty;
        }

        public static bool IsAntigravityInstalled()
        {
            return !string.IsNullOrEmpty(GetAntigravityPath());
        }
    }
}
