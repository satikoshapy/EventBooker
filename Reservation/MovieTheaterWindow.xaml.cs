using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Reservation
{
    /// <summary>
    /// Interaction logic for MovieTheaterWindow.xaml
    /// </summary>
    public partial class MovieTheaterWindow : Window
    {
        private string _folderPath;// path of the directory we have chosen at the start of the application
        private Movieshow _movieshow;  // the movieshow which is shown in this window
        private int _ticketNum = 0;
        private List<string> _reservationsList = new List<string>(); // contains the seats the user wants to reserve. 
        private bool _isNationalInsuranceNumberOke = false;
        private readonly SolidColorBrush _colorOccupied = new SolidColorBrush(Colors.Red); // background color of an occupied seat
        private readonly SolidColorBrush _colorNotOccupied = new SolidColorBrush(Colors.Green); // background color of an unoccupied seat

        //ToDo: change the signature of the constructor
        //      Don't forget to fill in the title of the window and the priceTextBox
        public MovieTheaterWindow(string path, Movieshow movieShow)
        {
            InitializeComponent();

            // Add code here
            _folderPath = path;
            _movieshow = movieShow;

            PlaceOnCanvas();
        }

        // ToDo: uncomment the code
        // This code checks the current seat reservations of the Hall (a MovieTheaterHall) 
        // and places buttons in the interface. Taken seats result in a disabled button.
        // You do NOT have to change anything in this method!
        private void PlaceOnCanvas()
        {
            MovieTheaterHall hall = _movieshow.ReservationHall as MovieTheaterHall;
            double heightOfRow = (paperCanvas.Height - hall.NumberOfRows * 2 - 2) / hall.NumberOfRows;
            double widthOfChair = (paperCanvas.Width - 2 * ReservationConstants.SeatsOnRow - 2) / ReservationConstants.SeatsOnRow;
            char letter = 'A';
            for (int rowNumber = 0; rowNumber < hall.NumberOfRows; rowNumber++)
            {
                for (int colNumber = 0; colNumber < ReservationConstants.SeatsOnRow; colNumber++)
                {
                    double x = 2 * (colNumber + 1) + colNumber * widthOfChair;
                    double y = 2 * (rowNumber + 1) + rowNumber * heightOfRow;
                    Button seatButton = new Button()
                   {
                       Content = letter + "" + (colNumber + 1),
                       Margin = new Thickness(x, y, 0, 0),
                        Width = widthOfChair,
                        Height = heightOfRow,
                    };
                    seatButton.Click += seatButton_Click;
                   if (hall.IsSeatReserved[rowNumber, colNumber])
                   {
                       seatButton.IsEnabled = false;
                   }
                    else
                   {
                       seatButton.Background = _colorNotOccupied;
                   }
                    paperCanvas.Children.Add(seatButton);
               }
                letter++;
            }
        }

        // ToDo: check if the nationalInsuranceNumber is valid.
        //       if yes => change the Backgroundcolor of the button you clicked on.
        //         first time you click on the button => add the seat to the _reservationlist
        //         second time you click on the button => remove the seat from the _reservationList
        //       Don't forget to fill in the numberOfTicketsTextbox and the totalPriceTextBox
        private void seatButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isNationalInsuranceNumberOke || ValidateNationalInsuranceNumber())
            {
                Button clickedItem = sender as Button;
                _reservationsList.Add((string) clickedItem.Content);
                clickedItem.Background = _colorOccupied;
                _ticketNum++;
                priceTextBox.Text = $"{_movieshow.Price}";
                numberOFTicketsTextBox.Text = _ticketNum.ToString();
                totalPriceTextBox.Text = $"{_movieshow.Price * _ticketNum}";
                reservationButton.IsEnabled = true;
            }
        }


        //ToDo: show a messageBox (see p 8)
        //      if the user clicks yes
        //      do the reservation => make use of the AddReservation method of the class Movieshow
        //                         => set the right element of the property IsSeatReserved of
        //                            the reservationhall of the movieshow to true
        //      close the window
        private void reservationButton_Click(object sender, RoutedEventArgs e)
        {
            string nationalNum = nationalInsuranceNumberTextBox.Text;
            _movieshow.AddReservation(_reservationsList, nationalNum, _folderPath);
            //_movieshow.ReservationHall.IsSeatReserved[] = true;
            this.Close();
        }
        // ToDo: Validate the nationalInsuranceNumber
        //        if ok =>  you can not change the nationalInsuranceNumder and
        //                  you can click on the reservationButton
        //                  set _isNationalInsuranceNumberOke to true
        //           not ok => show a messageBox
        private bool ValidateNationalInsuranceNumber()
        {

            string nationalNum = nationalInsuranceNumberTextBox.Text;
            
            try
            {
                if (Validation.Validate(nationalNum))
                {
                    nationalInsuranceNumberTextBox.IsEnabled = false;
                    reservationButton.IsEnabled = true;
                    _isNationalInsuranceNumberOke = true;
                }
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            
            return _isNationalInsuranceNumberOke;
            
        }


            

    }
}
