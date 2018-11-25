using System.Collections.ObjectModel;

namespace Matemater.Players
{
    /// <summary>
    /// Interfejs repozytorium graczy. Umożliwia dodawanie graczy oraz zapisanie historii gry.
    /// </summary>
    public interface IPlayersRepository
    {
        /// <value>Zwraca listę graczy.</value>
        ObservableCollection<Player> Players { get; }

        /// <summary>
        /// Dodanie nowego gracza do repozytorium.
        /// </summary>
        /// <param name="name">Imię gracza.</param>
        void AddPlayer(string name);
        /// <summary>
        /// Zapisuje historię graczy do pliku XML.
        /// </summary>
        void Save();
        /// <summary>
        /// Zapisuje wynik gracza do historii wyników.
        /// </summary>
        /// <param name="name">Imię gracza.</param>
        /// <param name="score">Uzyskany wynik.</param>
        void SavePlayerScore(string name, int score);
    }
}