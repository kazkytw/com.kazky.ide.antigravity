/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Kazky. All rights reserved.
 *  Based on Unity Technologies' Visual Studio integration
 *  Licensed under the MIT License. See LICENSE.md for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Diagnostics;
using UnityEngine;

namespace Kazky.Unity.IDE.Antigravity
{
    /// <summary>
    /// Helper class to detect Unity installation information
    /// </summary>
    internal static class UnityInstallation
    {
        /// <summary>
        /// Returns true if this is the main Unity editor process (not a background process)
        /// </summary>
        public static bool IsMainUnityEditorProcess
        {
            get
            {
#if UNITY_2021_1_OR_NEWER
                // Unity 2021.1+ provides a built-in way to check
                return !UnityEditor.MPE.ProcessService.level.HasValue;
#else
                // For older Unity versions, check if we're not a slave process
                var processId = Process.GetCurrentProcess().Id;
                return !Application.isBatchMode;
#endif
            }
        }
    }
}
