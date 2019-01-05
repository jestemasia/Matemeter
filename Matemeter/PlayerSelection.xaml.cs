using Matemater.Players;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Matemater
{
    /// <summary>
    /// Logika interakcji z oknem wyboru użytkownika.
    /// </summary>
    public partial class PlayerSelection : Window
    {
        /// <summary>
        /// Repozytorium graczy.
        /// </summary>
        private IPlayersRepository _players;
        /// <summary>
        /// Imię wybranego gracza.
        /// </summary>
        private string _userName = "Noname";
        /// <value>Zwraca imię wybranego gracza.</value>
        public string UserName
        {
            get { return _userName; }
        }
        /// <summary>
        /// Konstruktor klasy.
        /// </summary>
        /// <param name="players">Repozytorium graczy.</param>
        public PlayerSelection(IPlayersRepository players)
        {
            InitializeComponent();
            _players = players;
            this.DataContext = players;
            imgLogo.Source = new BitmapImage(new Uri(@"./Resources/Images/logo.png", UriKind.Relative));
        }
        /// <summary>
        /// Dodawanie nowego gracza.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Dodatkowe informacje o zdarzeniu.</param>
        private void newUser_Click(object sender, RoutedEventArgs e)
        {
            string newName = userName.Text;
            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("Podaj imię użytkownika!");
                return;
            }
            _players.AddPlayer(newName);
        }
        /// <summary>
        /// Start gry.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Dodatkowe informacje o zdarzeniu.</param>
        private void start_Click(object sender, RoutedEventArgs e)
        {
            Player player = (Player)usersGrid.SelectedValue;
            if (player == null)
            {
                MessageBox.Show("Wybierz użytkownika!");
                return;
            }
            this.Name = player.Name;
            this.Close();
        }
        /// <summary>
        /// Wyjście z gry.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Dodatkowe informacje o zdarzeniu.</param>
        private void close_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
