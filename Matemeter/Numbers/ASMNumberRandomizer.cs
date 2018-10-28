using System.Linq;

namespace Matemater.Numbers
{
    /// <summary>
    /// Klasa losująca liczby, które utworzą odpowiedź dla operacji dodawania, odejmowania i mnożenia.
    /// </summary>
    class ASMNumberRandomizer : NumberRandomizer
    {
        /// <value>Zwraca wynik dodawania wylosowanej odpowiedzi.</value>
        public int AddResult => this._answer.Sum();
        /// <value>Zwraca wynik odejmowania wylosowanej odpowiedzi.</value>
        public int SubtsractResult => this._answer.Aggregate((a, b) => a - b);
        /// <value>Zwraca wynik mnożenia wylosowanej odpowiedzi.</value>
        public int MuliplicationResult => this._answer.Aggregate((a, b) => a * b);

        /// <summary>
        /// Metoda losująca 2 lub 3 liczby, które dają wynik dodawania, odejmowania lub mnożenia.
        /// </summary>
        /// <param name="range">Parametr opisujący zakres losowanych liczb.</param>
        public override void DrewAnswer(int range)
        {
            this._answer.Clear();

            int number;
            int howMany = this._random.Next(this._min, this._max);
            for(int i = 1; i <= howMany; i++)
            {
                number = this._random.Next(range);
             
                while(this._answer.Contains(number))
                {
                    number = this._random.Next(range);
                }
                this._answer.Add(number);
            }       
        }
    }
}
