using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Serialization;

namespace Matemater.Players
{
    /// <summary>
    /// Repozytorium użytkowników.
    /// </summary>
    public class XMLPlayersRepository : IPlayersRepository
    {
        /// <summary>
        /// Ścieżka pliku zawierającego wszystkich graczy w grze.
        /// </summary>
        private readonly string _FilePath;
        /// <summary>
        /// Kolekcja zawierająca wszystkich graczy.
        /// </summary>
        private readonly ObservableCollection<Player> _players;
        
        /// <value>Zwraca wszystkich graczy.</value>
        public ObservableCollection<Player> Players
        {
            get { return this._players;  }
        }

        /// <summary>
        /// Konstruktor klasy wczytuje historię użytkowników z pliku XML.
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku z historią gry.</param>
        public XMLPlayersRepository(string filePath)
        {
            _FilePath = filePath;
            if (File.Exists(_FilePath))
            {
                XmlSerializer xmlser = new XmlSerializer(typeof(ObservableCollection<Player>));
                StreamReader rfile = new StreamReader(_FilePath);
                this._players= (ObservableCollection<Player>)xmlser.Deserialize(rfile);
                rfile.Close();
            }
            else
            {
                this._players = new ObservableCollection<Player>();
            }
        }

        /// <summary>
        /// Zapisuje historię graczy do pliku XML.
        /// </summary>
        public void Save()
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(ObservableCollection<Player>));
            StreamWriter wfile = new StreamWriter(_FilePath);
            xmlser.Serialize(wfile, this.Players);
            wfile.Close();
        }

        /// <summary>
        /// Dodanie nowego gracza do repozytorium.
        /// </summary>
        /// <param name="name">Imię gracza.</param>
        public void AddPlayer(string name)
        {
            if(_players.Any(p => p.Name == name))
            {
                MessageBox.Show("Gracz o takim imieniu już istnieje!");
                return;
            }

            Player newPlayer = new Player(name);
            this._players.Add(newPlayer);
            this.Save();
        }

        /// <summary>
        /// Zapisuje wynik gracza do historii wyników gracza.
        /// </summary>
        /// <param name="name">Imię gracza.</param>
        /// <param name="score">Uzyskany wynik.</param>
        public void SavePlayerScore(string name, int score)
        {
            Player player = _players.First(p => p.Name == name);
            player.AddScore(score);
            this.Save();
        }
    }
}
