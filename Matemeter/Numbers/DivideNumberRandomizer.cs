using System.Collections.ObjectModel;
using System.Linq;

namespace Matemater.Numbers
{
    /// <summary>
    /// Klasa losująca liczby do operacji dzielenia.
    /// </summary>
    class DivideNumberRandomizer : NumberRandomizer
    {
        /// <value>Zwraca wynik dzielenia wylosowanych liczb.</value>
        public int DivideResult => this._answer.Aggregate((a, b) => a / b);

        /// <summary>
        /// Losuje liczby tworzące poprawną odpowiedź.
        /// </summary>
        /// <param name="range">Określa zakres losowanych liczb.</param>
        public override void DrewAnswer(int range, int level = 0)
        {
            this._answer.Clear();

            ObservableCollection<int> list = new ObservableCollection<int>();
            int howMany = this._random.Next(this._min, this._max);
            int number;

            for(int i = 1; i<= howMany; i++)
            {
                number = this._random.Next(1, range);
                while (this._answer.Contains(number))
                {
                    number = this._random.Next(1, range);
                }
                list.Add(number);
            }

            this._answer.Add(list.Aggregate((a, b) => a * b));
            for(int i=1; i< list.Count(); i++)
            {
                this._answer.Add(list[i]);
            }
        }
    }
}
