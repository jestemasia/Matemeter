using Matemater.Game;
using Matemater.Players;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Matemater
{
    /// <summary>
    /// Logika interakcji z głównym oknem gry.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Zmienna typu Session opisująca logikę gry.
        /// </summary>
        private Session _game;
        /// <summary>
        /// Repozytorium graczy.
        /// </summary>
        private IPlayersRepository _players;
        /// <summary>
        /// Zmienna MediaPlayer do odtwarzania dźwięków.
        /// </summary>
        private MediaPlayer _soundPlayer;

        PlayerSelection _selectPlayer;

        /// <summary>
        /// Konstruktor klasy.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            _players = new XMLPlayersRepository(@"..\..\..\PlayersRepository.xml");
            _selectPlayer = new PlayerSelection(_players);
            _selectPlayer.ShowDialog();

            _game = new Session(_players, _selectPlayer.Name);
            _game.NewEquation();

            this.DataContext = _game;
            _soundPlayer = new MediaPlayer();

            lbNumbers.ItemsSource = _game.CurrentNumbers;
            lbEquation.ItemsSource = _game.AnswerEquation;
        }

        /// <summary>
        /// Lista elementów, z której są przeciągane elementy za pomocą myszki.
        /// </summary>
        ListBox dragSource = null;

        /// <summary>
        /// Funkcja wywołująca się, gdy użytkownik kliknie na listę zawierającą liczby do wyboru.
        /// </summary>
        /// <param name="sender">Obiekt, który wywołał zdarzenie.</param>
        /// <param name="e">Dodatkowe informacje o zdarzeniu.</param>
        private void NumbersList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            dragSource = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));

            if (data != null)
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Copy);
        }

        /// <summary>
        /// Funkcja pobierająca liczbę z miejsca kliknięcia.
        /// </summary>
        /// <param name="source">Lista, z której pobrano element.</param>
        /// <param name="point">Położenie myszki w chwili kliknięcia.</param>
        /// <returns>Wybrany przez gracza obiekt.</returns>
        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);

                    if (data == DependencyProperty.UnsetValue)
                        element = VisualTreeHelper.GetParent(element) as UIElement;

                    if (element == source)
                        return null;
                }

                if (data != DependencyProperty.UnsetValue)
                    return data;
            }
            return null;
        }

        /// <summary>
        /// Funkcja wywołująca się przy upuszczaniu wybranego elementu.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Dodatkowe informacje o zdarzeniu.</param>
        private void ListBox_Drop(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            object data = e.Data.GetData(typeof(int));

            object replace = GetDataFromListBox(parent, e.GetPosition(parent));
            string sign = replace.ToString();
            int index = ((IList)parent.ItemsSource).IndexOf(replace);
            int dataIndex = ((IList)dragSource.ItemsSource).IndexOf(data);

            if (index == ((IList)parent.ItemsSource).Count - 1)
                return;

            if ((sign == "=") || (sign == "+") || (sign == "-") || (sign == "*") || (sign == "\\"))
                return;

            ((IList)parent.ItemsSource).RemoveAt(index);
            ((IList)parent.ItemsSource).Insert(index, data.ToString());
            ((IList)dragSource.ItemsSource).Remove(data);

            if (sign != " " && sign != "  " && sign != "   ")
                ((IList)dragSource.ItemsSource).Insert(dataIndex, int.Parse((string)replace));

            _soundPlayer.Open(new Uri(@"../../Resources/Sounds/ClickSound.wav", UriKind.Relative));
            _soundPlayer.Play();
        }

        /// <summary>
        /// Ustawienie wyglądu kursora w liście gdzie można upuścić element.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Dodatkowe informacje o zdarzeniu.</param>
        private void Label_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effects == DragDropEffects.Copy)
            {
                e.UseDefaultCursors = false;
                Mouse.SetCursor(Cursors.Cross);
            }
            else
                e.UseDefaultCursors = true;

            e.Handled = true;
        }

        /// <summary>
        /// Funkcja wywołująca się po kliknięciu przycisku sprawdź.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Dodatkowe informacje o zdarzeniu.</param>
        private void Chceck_Click(object sender, RoutedEventArgs e)
        {
            _game.UpdateScore();
            _game.NewEquation();
        }

        /// <summary>
        /// Funkcja wywołująca się po kliknięciu przycisku nowa gra.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Dodatkowe informacje o zdarzeniu.</param>
        private void newGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = SaveGame();

            if (result == MessageBoxResult.Cancel)
                return;

            _game.Score = 0;
            _game.NewEquation();
        }

        /// <summary>
        /// Funkcja pyta użytkownika czy zapisać uzyskany wynik.
        /// </summary>
        /// <returns>Odpowiedź użytkownika.</returns>
        private MessageBoxResult SaveGame()
        {
            MessageBoxResult result = MessageBox.Show("Czy zapisać wynik?", "Zapisz", MessageBoxButton.YesNoCancel);

            if (result == MessageBoxResult.Yes)
            {
                _game.Save();
            }

            return result;
        }

        /// <summary>
        /// Zamknięcie programu.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Dodatkowe informacje o zdarzeniu.</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = SaveGame();

            if (result == MessageBoxResult.Cancel)
                e.Cancel = true;
        }
    }
}
