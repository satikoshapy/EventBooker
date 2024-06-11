namespace Reservation
{
    // ToDo MovieTheaterHall is a subclass of Hall. There are seats.
    public class MovieTheaterHall:Hall
    {
        

        public int NumberOfRows { get; } // number of rows of the hall
        public bool[,] IsSeatReserved { get; } // remember to initialise this in the constructor!

        public MovieTheaterHall(string name, int maxCapacity) : base(name, maxCapacity)
        {
            HasChairs = true;
            NumberOfRows = maxCapacity / ReservationConstants.SeatsOnRow;
            IsSeatReserved = new bool[NumberOfRows, ReservationConstants.SeatsOnRow];
        }
    }
}
