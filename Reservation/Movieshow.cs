using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Reservation
{
    public class Movieshow
    {
        public string Name { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public MovieTheaterHall ReservationHall { get; }
        public double Price { get; }
        public ExtraMovie Supplement { get; }

        // ToDo: provide a constructor with the following parameters
        //       name (string), startTime (string), endTime (string)
        //       reservationHall (Hall) and supplement (string)

        public Movieshow(string name, string startTime, string endTime, Hall reservationHall, string supplement)
        {
            Name = name;
            StartTime = ParseStringToDateTime(startTime);
            EndTime = ParseStringToDateTime(endTime);
            ReservationHall = reservationHall as MovieTheaterHall;
            Supplement = GetExtra(supplement);
            Price = CalculatePrice();
            

        }

      

         // ToDo: add each reservation from the list reservations to the right file
         //       with each reservation, the national insurance number is also written to the file
         //       e.g: E17 99080500243
        public void AddReservation(List<string> reservations, string nationalSecurityNumber, string filePath)
        {
            
            
            

            foreach(string reservation in reservations)
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                    using (StreamWriter sw = new StreamWriter(filePath, true))
                        {
                            //sw.WriteLine("test");
                            sw.WriteLine($"{reservation} {nationalSecurityNumber} this a test");
                        }
                    


            
            }

            

        }

        // Optional helper function to parse a DateTime from a string
        private DateTime ParseStringToDateTime(string dateTimeInString)
        {
            string format = "d/M/yyyy HH:mm";
            CultureInfo provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(dateTimeInString, format, provider);
        }

        private double CalculatePrice()
        {
            double price = 0;
            if (Supplement == ExtraMovie.Supplement3D)
            {
                price = ReservationConstants.PriceFilm + ReservationConstants.Supplement3DFilm;
            }
            else if (Supplement == ExtraMovie.SupplementUltraLaser)
            {
                price = ReservationConstants.PriceFilm + ReservationConstants.SupplementLaserUltraFilm;
            }
            else if (Supplement == ExtraMovie.NoSupplement)
            {
                price = ReservationConstants.PriceFilm;
            }
            return price;
        }

        private static ExtraMovie GetExtra(string supplement)
        {

            return char.Parse(supplement) switch
            {
                'D' => ExtraMovie.Supplement3D,
                'L' => ExtraMovie.SupplementUltraLaser,
                'N' => ExtraMovie.NoSupplement,
                _ => throw new ArgumentException($"Invalid PlayerFunction value: {supplement}")
            };
        }

        public override string ToString()
        {
            return $"{Name} {ReservationHall.Name}; {StartTime}; {EndTime} ; {Supplement.ToString()}";
        }
    }
}
