using System.Collections.Generic;
using System.Linq;

namespace Matemater.Players
{
    /// <summary>
    /// Klasa opisująca pojedynczego gracza. 
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Zmienna zawierająca imię gracza.
        /// </summary>
        private string _name;
        /// <summary>
        /// Lista wyników gracza.
        /// </summary>
        private List<int> _scoreHistory;

        /// <value>Zwraca imię gracza.</value>
        public string Name
        {
            get { return this._name; }
            set { _name = value; }
        }

        /// <value>Zwraca najlepszy wynik gracza.</value>
        public int BestScore
        {
            get { return this._scoreHistory.Max(); }
            set { }
        }
        
        /// <value>Zwraca listę wyników gracza.</value>
        public List<int> ScoreHistory
        {
            get { return _scoreHistory; }
            set { }
        }

        /// <summary>
        /// Konstruktor bez parametrów służący do zapisu i odczytu pliku XML.
        /// </summary>
        public Player()
        {
            this._scoreHistory = new List<int>();
            this._scoreHistory.Add(0);
        }

        /// <summary>
        /// Konstruktor klasy przypisuję imię gracza oraz inicjalizuje zmienne prywatne.
        /// </summary>
        /// <param name="name">Imię gracza.</param>
        public Player(string name)
        {
            this._name = name;
            this._scoreHistory = new List<int>();
            this._scoreHistory.Add(0);
        }

        /// <summary>
        /// Dodanie nowego wyniku do historii gracza.
        /// </summary>
        /// <param name="score">Wynik gracza.</param>
        public void AddScore(int score) 
        {
            this._scoreHistory.Add(score);
        } 
    }
}
