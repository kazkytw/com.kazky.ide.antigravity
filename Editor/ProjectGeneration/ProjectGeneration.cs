/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Kazky. All rights reserved.
 *  Based on Unity Technologies' Visual Studio integration
 *  Licensed under the MIT License. See LICENSE.md for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace Kazky.Unity.IDE.Antigravity.ProjectGeneration
{
    /// <summary>
    /// Handles generation of .csproj and .sln files for Antigravity IntelliSense support
    /// </summary>
    public class ProjectGeneration : IGenerator
    {
        private readonly string m_ProjectDirectory;
        private readonly IFileIO m_FileIOProvider;

        public ProjectGeneration() : this(Directory.GetParent(Application.dataPath).FullName, new FileIOProvider())
        {
        }

        public ProjectGeneration(string projectDirectory, IFileIO fileIOProvider)
        {
            m_ProjectDirectory = projectDirectory;
            m_FileIOProvider = fileIOProvider;
        }

        public bool SyncIfNeeded(IEnumerable<string> affectedFiles, IEnumerable<string> importedFiles)
        {
            // Check if any affected files are C# scripts
            if (affectedFiles.Any(file => file.EndsWith(".cs", StringComparison.OrdinalIgnoreCase)))
            {
                Sync();
                return true;
            }
            return false;
        }

        public void Sync()
        {
            try
            {
                GenerateAndWriteProjectFiles();
                UnityEngine.Debug.Log("Successfully generated project files for Antigravity");
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"Failed to generate project files: {e.Message}");
            }
        }

        private void GenerateAndWriteProjectFiles()
        {
            var assemblies = CompilationPipeline.GetAssemblies();
            var projectName = Application.productName;

            // Generate solution file
            GenerateSolutionFile(projectName, assemblies);

            // Generate project files for each assembly
            foreach (var assembly in assemblies)
            {
                GenerateProjectFile(assembly);
            }
        }

        private void GenerateSolutionFile(string projectName, Assembly[] assemblies)
        {
            var solutionFile = Path.Combine(m_ProjectDirectory, $"{projectName}.sln");
            var solutionContent = GenerateSolutionContent(projectName, assemblies);
            m_FileIOProvider.WriteAllText(solutionFile, solutionContent);
        }

        private string GenerateSolutionContent(string projectName, Assembly[] assemblies)
        {
            var content = "Microsoft Visual Studio Solution File, Format Version 12.00\n";
            content += "# Visual Studio Version 16\n";
            content += "VisualStudioVersion = 16.0.0.0\n";
            content += "MinimumVisualStudioVersion = 10.0.0.1\n";

            foreach (var assembly in assemblies)
            {
                var projectGuid = GuidForProject(assembly.name);
                content += $"Project(\"{{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}}\") = \"{assembly.name}\", \"{assembly.name}.csproj\", \"{{{projectGuid}}}\"\n";
                content += "EndProject\n";
            }

            content += "Global\n";
            content += "\tGlobalSection(SolutionConfigurationPlatforms) = preSolution\n";
            content += "\t\tDebug|Any CPU = Debug|Any CPU\n";
            content += "\t\tRelease|Any CPU = Release|Any CPU\n";
            content += "\tEndGlobalSection\n";
            content += "EndGlobal\n";

            return content;
        }

        private void GenerateProjectFile(Assembly assembly)
        {
            var projectFile = Path.Combine(m_ProjectDirectory, $"{assembly.name}.csproj");
            var projectContent = GenerateProjectContent(assembly);
            m_FileIOProvider.WriteAllText(projectFile, projectContent);
        }

        private string GenerateProjectContent(Assembly assembly)
        {
            var content = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
            content += "<Project ToolsVersion=\"4.0\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">\n";
            content += "  <PropertyGroup>\n";
            content += $"    <ProjectGuid>{{{GuidForProject(assembly.name)}}}</ProjectGuid>\n";
            content += "    <OutputType>Library</OutputType>\n";
            content += "    <RootNamespace></RootNamespace>\n";
            content += $"    <AssemblyName>{assembly.name}</AssemblyName>\n";
            content += "    <TargetFrameworkVersion>v4.7.1</TargetFrameworkVersion>\n";
            content += $"    <LangVersion>{GetLanguageVersion()}</LangVersion>\n";
            content += "    <AllowUnsafeBlocks>true</AllowUnsafeBlocks>\n";
            content += "  </PropertyGroup>\n";
            
            // Add source files
            content += "  <ItemGroup>\n";
            foreach (var sourceFile in assembly.sourceFiles)
            {
                var relativePath = GetRelativePath(m_ProjectDirectory, sourceFile);
                content += $"    <Compile Include=\"{relativePath}\" />\n";
            }
            content += "  </ItemGroup>\n";

            // Add references
            content += "  <ItemGroup>\n";
            foreach (var reference in assembly.allReferences)
            {
                content += $"    <Reference Include=\"{Path.GetFileNameWithoutExtension(reference)}\">\n";
                content += $"      <HintPath>{reference}</HintPath>\n";
                content += "    </Reference>\n";
            }
            content += "  </ItemGroup>\n";

            content += "</Project>\n";
            return content;
        }

        private string GuidForProject(string projectName)
        {
            return MD5Hash("unity-antigravity-project-guid" + projectName).Substring(0, 32);
        }

        private string MD5Hash(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        private string GetRelativePath(string fromPath, string toPath)
        {
            var fromUri = new Uri(fromPath + Path.DirectorySeparatorChar);
            var toUri = new Uri(toPath);
            var relativeUri = fromUri.MakeRelativeUri(toUri);
            return Uri.UnescapeDataString(relativeUri.ToString()).Replace('/', Path.DirectorySeparatorChar);
        }

        private string GetLanguageVersion()
        {
            // Return the C# language version that Unity is using
            return "latest";
        }
    }

    /// <summary>
    /// Interface for file I/O operations (for testing purposes)
    /// </summary>
    public interface IFileIO
    {
        void WriteAllText(string path, string content);
    }

    /// <summary>
    /// Default file I/O implementation
    /// </summary>
    public class FileIOProvider : IFileIO
    {
        public void WriteAllText(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
