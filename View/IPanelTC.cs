using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.View
{
    /// <summary>
    /// Interfejs definiujący publiczny kontrakt dla kontrolki użytkownika PanelTC
    /// Zgodnie z wymaganiami projektu, publiczny interfejs powinien zawierać:
    /// - Właściwość bieżącej ścieżki
    /// - Kolekcję dostępnych dysków
    /// - Kolekcję zawierającą zawartość bieżącej ścieżki
    /// </summary>
    internal interface IPanelTC
    {
        /// <summary>
        /// Bieżąca ścieżka wyświetlana w panelu
        /// </summary>
        string currentPath { get; }

        /// <summary>
        /// Kolekcja zawierająca zawartość bieżącej ścieżki
        /// </summary>
        List<string> currentPathContent { get; }

        /// <summary>
        /// Kolekcja dostępnych dysków
        /// </summary>
        List<string> drives { get; }

        /// <summary>
        /// Ustawia dostępne dyski w panelu
        /// </summary>
        /// <param name="drives">Lista dostępnych liter dysków</param>
        void SetDisks(List<string> drives);

        /// <summary>
        /// Ustawia bieżący katalog i jego zawartość
        /// </summary>
        /// <param name="currentPath">Ścieżka do bieżącego katalogu</param>
        /// <param name="directories">Lista zawartości katalogu (foldery i pliki)</param>
        void SetDirectories(string currentPath, List<string> directories);
    }
}