using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Matemater.Numbers
{
    /// <summary>
    /// Klasa losująca liczby, które utworzą odpowiedź oraz dodatkowe losowe liczby do wyboru odpowiedzi.
    /// </summary>
    abstract class NumberRandomizer
    {
        /// <summary>
        /// Minimalna ilość losowanych liczb, które utworzą odpowiedź.
        /// </summary>
        protected readonly int _min = 2;
        /// <summary>
        /// Maksymalna liczba losowanych liczb, które utworzą odpowiedź.
        /// </summary>
        protected readonly int _max = 4;
        /// <summary>
        /// Ilość wylosowanych łącznie liczb.
        /// </summary>
        protected readonly int _numberCount = 10;
        /// <summary>
        /// Obiekt klasy Random do losowania liczb.
        /// </summary>
        protected Random _random;  
        /// <summary>
        /// Kolekcja zawierająca wszystkie wylosowane liczby.
        /// </summary>
        protected ObservableCollection<int> _numbers;
        /// <summary>
        /// Kolekcja zawierająca liczby tworzące poprawną odpowiedź.
        /// </summary>
        protected ObservableCollection<int> _answer;
        
        /// <value>Zwraca liczby tworzące poprawną odpowiedź.</value>
        public ObservableCollection<int> Answer => this._answer;
        /// <value>Zwraca wszystkie wylosowane liczby.</value>
        public ObservableCollection<int> Numbers => this._numbers;
        
        /// <summary>
        /// Konstruktor klasy, inicjalizuje zmienne klasy.
        /// </summary>
        public NumberRandomizer()
        {
            _random = new Random();
            _answer = new ObservableCollection<int>();
            _numbers = new ObservableCollection<int>();
        }

        /// <summary>
        /// Losuje dodatkowe liczby, które nie dają poprawnej odpowiedzi.
        /// </summary>
        /// <param name="range">Parametr określający zakres losowanych liczb.</param>
        private void DrawRandomNumbers(int range)
        {
            this._numbers.Clear();

            int number;
            int howMany = this._numberCount - this._answer.Count();

            for (int i=1; i <= howMany; i++)
            {
                number = this._random.Next(range);
                
                while (this._numbers.Contains(number) || this._answer.Contains(number))
                {
                    number = this._random.Next(range);
                }
                this._numbers.Add(number);
            }
        }

        /// <summary>
        /// Losuje liczby dające poprawną odpowiedź.
        /// </summary>
        /// <param name="range">Parametr określający zakres losowanych liczb.</param>
        public abstract void DrewAnswer(int range);    
       
        /// <summary>
        /// Tasuje zbiór liczb.
        /// </summary>
        private void ShuffleNumbers()
        {
            int randomIndex;

            for(int i=0; i< this._answer.Count(); i++)
            {
                randomIndex = this._random.Next(this._numbers.Count());
                this._numbers.Insert(randomIndex, this._answer[i]);
            }
        }

        /// <summary>
        /// Metoda losująca nowe liczby do gry.
        /// </summary>
        /// <param name="range">Parametr określający zakres losowanych liczb.</param>
        public void ActualizeNumbers(int range)
        {
            this.DrewAnswer(range);
            this.DrawRandomNumbers(range);
            this.ShuffleNumbers();
        }
    }
}
