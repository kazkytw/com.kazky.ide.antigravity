/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Kazky. All rights reserved.
 *  Based on Unity Technologies' Visual Studio integration
 *  Licensed under the MIT License. See LICENSE.md for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Kazky.Unity.IDE.Antigravity
{
    /// <summary>
    /// Interface for discovering code editor installations
    /// </summary>
    public interface IDiscovery
    {
        Unity.CodeEditor.CodeEditor.Installation[] PathCallback();
    }
}
