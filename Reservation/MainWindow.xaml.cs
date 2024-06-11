using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace Reservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Movieshow> _moviesShows; // movies shown in the eventListBox
        private string _folderPath; // path of the directory we have choosen at the start of the application

        //ToDo: Make a list of all movies. Make use of the CreateMovieShows method from the class DataFactory
        //      Ensure that the right info is shown in the eventListBox
        public MainWindow()
        {
            InitializeComponent();
            
            Title = "Geplande filmvoorstellingen";
            _moviesShows = new List<Movieshow>();
            List<Movieshow> movies = DataFactory.CreateMovieShows(out _folderPath) as List<Movieshow>;
            foreach(Movieshow movieshow in movies)
            {
                _moviesShows.Add(movieshow);
                eventListBox.Items.Add(movieshow.Name);
            }
            

            // HELP:
            // If you are not able to fix the DataFactory method 
            // you can comment out code below to have 1 hard-coded 
            // MovieShow (with a fake MovieTheaterHall)
            // and a fake path. You can still continue with the assignment! 

            //eventListBox.Items.Add(
            //    new Movieshow("Test movie", "31/12/2024 20:00", "31/12/2024 22:00", new MovieTheaterHall("Zaal 1", 160), "S"));
            //_folderPath = Directory.GetCurrentDirectory();
        }

        private void eventListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
            string fullFilePath = _folderPath + '/' + (string)eventListBox.SelectedItem + ".txt";
            Movieshow selectedMovie = _moviesShows.FirstOrDefault(movie => movie.Name.Equals(eventListBox.SelectedItem));
            
            var detailWindow = new MovieTheaterWindow(fullFilePath, selectedMovie);
            detailWindow.ShowDialog();
        }

        //ToDo: Doubleclick on a film in the list => MovieTheaterWindow should open.
        //      The film you clicked on and the _folderPath should be the arguments of the constructor
        //      This movieTheaterWindow should be shown as a dialog

    }
}
