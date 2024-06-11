namespace Reservation
{
    public abstract class Hall 
    {
        public string Name { get; }
        public int MaxCapacity { get; } // maximum number of people
        
        // ToDo: provide a property HasChairs
        public bool HasChairs { get; set; }



        // ToDo: provide a constructor
        protected Hall(string name, int maxCapacity)
        {
            Name = name;
            MaxCapacity = maxCapacity;
        }

    }
}
