using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.View
{
    /// <summary>
    /// Interface defining the public contract for PanelTC user control
    /// According to project requirements, the public interface should contain:
    /// - Current path property
    /// - Collection of available drives
    /// - Collection containing current path contents
    /// </summary>
    internal interface IPanelTC
    {
        /// <summary>
        /// Current path being displayed in the panel
        /// </summary>
        string currentPath { get; }

        /// <summary>
        /// Collection containing the contents of the current path
        /// </summary>
        List<string> currentPathContent { get; }

        /// <summary>
        /// Collection of available drives
        /// </summary>
        List<string> drives { get; }

        /// <summary>
        /// Sets the available drives in the panel
        /// </summary>
        /// <param name="drives">List of available drive letters</param>
        void SetDisks(List<string> drives);

        /// <summary>
        /// Sets the current directory and its contents
        /// </summary>
        /// <param name="currentPath">Path to the current directory</param>
        /// <param name="directories">List of directory contents (directories and files)</param>
        void SetDirectories(string currentPath, List<string> directories);
    }
}