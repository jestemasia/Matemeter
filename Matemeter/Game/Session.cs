using Matemater.Numbers;
using Matemater.Players;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Matemater.Game
{
    /// <summary>
    /// Klasa opisująca zachowanie gry podczas jednej sesji gry.
    /// </summary>
    /// <remarks>
    /// Klasa implementuje interfejs INotifyPropertyChanged w celu aktualizacji danych wyświetlanych w interfejsie gry.
    /// </remarks>
    class Session : INotifyPropertyChanged
    {
        /// <summary>
        /// Zmienna typu AMSNumberRandomizer do losowania liczb w operacjach dodawania, dzielenia oraz mnożenia.
        /// </summary>
        private readonly ASMNumberRandomizer _numbers;

        /// <summary>
        /// Zmienna typy DivideNumberRandomizer do losowania odpowiedzi w operacji dzielenia.
        /// </summary>
        private readonly DivideNumberRandomizer _divideNumbers;

        /// <summary>
        /// Rezpozytorium graczy.
        /// </summary>
        private readonly IPlayersRepository _players;

        /// <summary>
        /// Zmienna typy MediaPlayer do odtwarzania dźwięków w grze.
        /// </summary>
        private MediaPlayer _soundPlayer;

        /// <value>Zwraca wylosowane liczby.</value>
        private ObservableCollection<int> _currentNumbers;

        /// <summary>
        /// Wynik uzyskany przez gracza.
        /// </summary>
        private int _score = 0;

        /// <summary>
        /// Aktualny poziom gracza.
        /// </summary>
        private int _level = 1;

        /// <summary>
        /// Imię aktualnego gracza.
        /// </summary>
        private string _name;

        /// <summary>
        /// Znak aktualnie wykonywanej operacji matematycznej.
        /// </summary>
        private string _sign = "+";

        /// <summary>
        /// Zakres losowanych liczb.
        /// </summary>
        private int _range = 10;

        /// <summary>
        /// Czas na rozwiązanie równania.
        /// </summary>
        private int _equationTime = 30;

        /// <summary>
        /// Czas pozostały na wykonanie zadania.
        /// </summary>
        private string _timeLeft;

        /// <summary>
        /// Licznik czasu.
        /// </summary>
        DispatcherTimer _timer;

        /// <summary>
        /// Sekundy przekazywany do licznika czasu.
        /// </summary>
        TimeSpan _time;

        /// <summary>
        /// Równianie do rozwiązania wyświetlane w interfejsie graficznym.
        /// </summary>
        private ObservableCollection<string> _answerEquation;

        /// <value>Zwraca równianie do rozwiązania.</value>
        public ObservableCollection<string> AnswerEquation => _answerEquation;

        /// <value>Zwraca wylosowane liczby.</value>
        public ObservableCollection<int> CurrentNumbers
        {
            get { return _currentNumbers; }
            set { this._currentNumbers = value; }
        }

        /// <value>Zwraca imię gracza.</value>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <value>Zwraca aktualny poziom w grze.</value>
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged("Level");
            }
        }

        /// <value>Zwraca aktualny wynik gracza.</value>
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }

        /// <value>Zwraca czas pozostały do ukończenia zadania.</value>
        public string TimeLeft
        {
            get { return _timeLeft; }
            set {
                _timeLeft = value;
                OnPropertyChanged("TimeLeft");
            }
        }

        /// <summary>
        /// Konstruktor klasy.
        /// </summary>
        /// <param name="players">Repozytorium graczy.</param>
        /// <param name="name">Imię aktualnego gracza.</param>
        public Session(IPlayersRepository players, string name)
        {
            this._currentNumbers = new ObservableCollection<int>();
            this._numbers = new ASMNumberRandomizer();
            this._divideNumbers = new DivideNumberRandomizer();
            _players = players;
            this._answerEquation = new ObservableCollection<string>();
            _soundPlayer = new MediaPlayer();
            this.Name = name;
  
            _time = TimeSpan.FromSeconds(_equationTime);
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate { Tick(); }, Application.Current.Dispatcher);
            _timer.Stop();
        }

        /// <summary>
        /// Funkcja uruchomiająca się przy każdym tyknięciu zegara.
        /// </summary>
        private void Tick()
        {
            _time = _time.Add(TimeSpan.FromSeconds(-1));
      
            if (_time.Seconds == 5)
                PlaySound(@"../../Resources/Sounds/TickTock.wav");

            if (_time.Seconds == -1)
            {
                _timer.Stop();
                _time = TimeSpan.FromSeconds(_equationTime);
                UpdateScore();
                NewEquation();
            }
            TimeLeft = _time.Seconds.ToString();
        }

        /// <summary>
        /// Funkcja tworząca równanie do wyświetlenia w interfejsie graficznym.
        /// </summary>
        private void CreateAnswer()
        {
            _answerEquation.Clear();

            string space = " ";
            _answerEquation.Add(space);
            ObservableCollection<int> answer;
            if (_level == 4)
                answer = _divideNumbers.Answer;
            else
                answer = _numbers.Answer;

            for (int i = 1; i < answer.Count(); i++)
            {
                space += " ";
                _answerEquation.Add(_sign);
                _answerEquation.Add(space);
            }

            _answerEquation.Add("=");
            _answerEquation.Add(FindResult().ToString());
        }

        /// <summary>
        /// Funkcja zwracająca poprawna odpowiedź wyniku działania.
        /// </summary>
        /// <returns>Funkcja zwraca poprawny wynik działania.</returns>
        private int FindResult()
        {
            switch(_level)
            {
                case 1: return _numbers.AddResult;
                case 2: return _numbers.SubtsractResult;
                case 3: return _numbers.MuliplicationResult;
                case 4: return _divideNumbers.DivideResult;
            }
            return 0;
        }

        /// <summary>
        /// Funkcja losująca nowe równianie.
        /// </summary>
        public void NewEquation()
        {
            if(Level == 3)
            {
                this._numbers.ActualizeNumbers(_range, 3);
                CurrentNumbers = _numbers.Numbers;
            }
            else if (_level == 4)
            {
                this._divideNumbers.ActualizeNumbers(_range);
                _currentNumbers.Clear();
                foreach(int i in _divideNumbers.Numbers)
                _currentNumbers.Add(i);
            }
            else
            {
                this._numbers.ActualizeNumbers(_range);
                CurrentNumbers = _numbers.Numbers;
            }

            CreateAnswer();
            _time = TimeSpan.FromSeconds(_equationTime);
            _timer.Start();   
        }

        /// <summary>
        /// Funkcja dodaje punkty w przypadku poprawnej odpowiedzi gracza oraz odejmuje punkty w przypadku błędnej odpowiedzi gracza.
        /// </summary>
        public void UpdateScore()
        {
            bool rightAnswer = CheckAnswer();
            if (rightAnswer)
            {
                Score += 10;
                PlaySound(@"../../Resources/Sounds/TrueBuzzer.wav");
            }
            else
            {
                Score -= 10;
                PlaySound(@"../../Resources/Sounds/WrongBuzzer.wav");
            }
            Thread.Sleep(2000);
            UpdateLevel();
            UpdateRange();
        }

        /// <summary>
        /// Funkcja odtwarzająca dźwięk.
        /// </summary>
        /// <param name="uri">Ścieżka dostępu do pliku dźwiękowego.</param>
        private void PlaySound(string uri)
        {
            _soundPlayer.Open(new Uri(uri, UriKind.Relative));
            _soundPlayer.Play();
        }

        /// <summary>
        /// Funkcja uaktualnia poziom gry w zależności od uzyskanych przez gracza punktów.
        /// </summary>
        private void UpdateLevel()
        {
            _soundPlayer.Open(new Uri(@"../../Resources/Sounds/LevelUp.wav", UriKind.Relative));
            if (_score == 100  || _score == 200 || _score == 300)
                _soundPlayer.Play();

            if(_score < 100) { _sign = "+"; Level = 1; }
            else if(_score < 200) { _sign = "-"; Level = 2; }
            else if(_score < 300){ _sign = "*";  Level = 3; }
            else if(_score >= 300) { _sign = "/";  Level = 4;}
        }

        /// <summary>
        /// Zmienia zakres losowanych liczb w zależności od punktów zdobytych przez gracza.
        /// </summary>
        private void UpdateRange()
        {
            if (_score == 30 || _score == 130) _range = 15;
            if (_score == 60 || _score == 160) _range = 20;
            if (_score == 90 || _score == 190) _range = 30;

            if (_score == 100 || _score == 200 || _score == 300)_range = 10;
            if (_score == 350) _range = 15;
        }

        /// <summary>
        /// Sprawdza, czy gracz poprawnie rozwiązał równianie.
        /// </summary>
        /// <returns>Zmienna typu bool wskazująca czy gracz udzielił poprawnej odpowiedzi.</returns>
        private bool CheckAnswer()
        {
            string eqaution = string.Join("", _answerEquation.Reverse().Skip(2).Reverse());
            DataTable dt = new DataTable();

            if (_answerEquation.Contains(" ") || _answerEquation.Contains("  ") || _answerEquation.Contains("   "))
                return false;
        
            if (FindResult() == Convert.ToInt32(dt.Compute(eqaution, "")))
                return true;
        
            return false;
        }

        /// <summary>
        /// Funkcja zapisuje wynik uzyskany przez gracza.
        /// </summary>
        public void Save()
        {
            _players.SavePlayerScore(Name, Score);
        }

        /// <summary>
        /// Zdarzenie zmiany wartości zmiennej.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Funkcja wywołująca się, gdy zmieni się wartość zmiennej.
        /// </summary>
        /// <param name="name">Nazwa zmiennej, której wartość uległa zmianie.</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
