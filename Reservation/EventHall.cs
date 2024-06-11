namespace Reservation
{
    // ToDo EventHall is a subclass of Hall. There are no seats.
    public class EventHall : Hall
    {
        public EventHall(string name, int maxCapacity) : base(name, maxCapacity)
        {
            HasChairs = false;
        }
    }
}
