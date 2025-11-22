/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Kazky. All rights reserved.
 *  Based on Unity Technologies' Visual Studio integration
 *  Licensed under the MIT License. See LICENSE.md for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;

namespace Kazky.Unity.IDE.Antigravity
{
    /// <summary>
    /// Interface for project file generation
    /// </summary>
    public interface IGenerator
    {
        /// <summary>
        /// Synchronizes project files if needed based on affected files
        /// </summary>
        bool SyncIfNeeded(IEnumerable<string> affectedFiles, IEnumerable<string> importedFiles);
        
        /// <summary>
        /// Forces synchronization of all project files
        /// </summary>
        void Sync();
    }
}
