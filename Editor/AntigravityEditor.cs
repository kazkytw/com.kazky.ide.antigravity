/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Kazky. All rights reserved.
 *  Based on Unity Technologies' Visual Studio integration
 *  Licensed under the MIT License. See LICENSE.md for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Unity.CodeEditor;

namespace Kazky.Unity.IDE.Antigravity
{
    /// <summary>
    /// Main editor integration class for Antigravity
    /// Implements Unity's IExternalCodeEditor interface
    /// </summary>
    [InitializeOnLoad]
    public class AntigravityEditor : IExternalCodeEditor
    {
        private static readonly GUIContent k_ResetArgument = EditorGUIUtility.TrTextContent("Reset argument");
        
        private IDiscovery m_Discovery;
        private IGenerator m_ProjectGeneration;

        public CodeEditor.Installation[] Installations => m_Discovery.PathCallback();

        public bool TryGetInstallationForPath(string editorPath, out CodeEditor.Installation installation)
        {
            var lowerCasePath = editorPath.ToLower();
            var installations = Installations;
            
            installation = installations.FirstOrDefault(install =>
                install.Path.ToLower() == lowerCasePath);
            
            return installation.Path != null;
        }

        static AntigravityEditor()
        {
            if (!UnityInstallation.IsMainUnityEditorProcess)
                return;

            var editor = new AntigravityEditor(new AntigravityInstallation(), new ProjectGeneration.ProjectGeneration());
            CodeEditor.Register(editor);
        }

        public AntigravityEditor(IDiscovery discovery, IGenerator projectGeneration)
        {
            m_Discovery = discovery;
            m_ProjectGeneration = projectGeneration;
        }

        public void OnGUI()
        {
            var antigravityPath = AntigravityInstallation.GetAntigravityPath();
            
            if (string.IsNullOrEmpty(antigravityPath))
            {
                EditorGUILayout.HelpBox("Antigravity installation not detected. Please install Antigravity or manually set the path.", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.HelpBox($"Antigravity detected at: {antigravityPath}", MessageType.Info);
            }

            // Additional settings can be added here
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Antigravity Integration Settings", EditorStyles.boldLabel);
            
            if (GUILayout.Button("Regenerate Project Files"))
            {
                m_ProjectGeneration.Sync();
                UnityEngine.Debug.Log("Project files regenerated for Antigravity");
            }
        }

        public void SyncAll()
        {
            m_ProjectGeneration.Sync();
        }

        public void SyncIfNeeded(string[] addedFiles, string[] deletedFiles, string[] movedFiles, string[] movedFromFiles, string[] importedFiles)
        {
            m_ProjectGeneration.SyncIfNeeded(addedFiles.Union(deletedFiles).Union(movedFiles).Union(movedFromFiles), importedFiles);
        }

        public bool OpenProject(string filePath = "", int line = -1, int column = -1)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                filePath = Directory.GetCurrentDirectory();
            }

            var antigravityPath = AntigravityInstallation.GetAntigravityPath();
            
            if (string.IsNullOrEmpty(antigravityPath))
            {
                UnityEngine.Debug.LogWarning("Antigravity is not installed or cannot be found.");
                return false;
            }

            return OpenFile(antigravityPath, filePath, line, column);
        }

        private bool OpenFile(string editorPath, string filePath, int line, int column)
        {
            try
            {
                var arguments = $"\"{filePath}\"";
                
                // Add line and column information if provided
                if (line >= 0)
                {
                    arguments += $":{line}";
                    if (column >= 0)
                    {
                        arguments += $":{column}";
                    }
                }

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = editorPath,
                        Arguments = arguments,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                return true;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"Failed to open file in Antigravity: {e.Message}");
                return false;
            }
        }

        public bool Initialize(string editorInstallationPath)
        {
            return AntigravityInstallation.IsAntigravityInstalled();
        }
    }
}
