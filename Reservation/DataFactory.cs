using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Reservation
{
    public static class DataFactory
    {
        // ToDo: Uncomment all commented code in this file
        public static List<Hall> CreateHalls()
        {
            List<Hall> list = new List<Hall>();
            string[] names = { "zaal1", "zaal2", "zaal3", "zaal4" };
            int[] maxCapacity = { 300, 160, 500, 160 };
            string[] type = { "filmtheaterzaal", "filmtheaterzaal", "evenementzaal", "filmtheaterzaal" };
            for (int i = 0; i < names.Length; i++)
            {
                if (type[i] == "evenementzaal")
                {
                    list.Add(new EventHall(names[i], maxCapacity[i]));
                }
                else
               {
                   list.Add(new MovieTheaterHall(names[i], maxCapacity[i]));
                }
           }
           return list;
        }

        public static Dictionary<string, Hall> CreateDictionary()
        {
            List<Hall> halls = CreateHalls();
            Dictionary<string, Hall> lookup = new Dictionary<string, Hall>();
            foreach (Hall hall in halls)
            {
                lookup.Add(hall.Name, hall);
            }
            return lookup;
        }

        public static List<Movieshow> CreateMovieShows(out string folderPath)
        {
            folderPath = null; // the path of the selectedfolder
            List<Movieshow> eventList = new List<Movieshow>();
            // Remove comment below
            Dictionary<string, Hall> lookup = CreateDictionary();

            // ToDo: Make use of a FolderBrowserDialog to select the right folder
            //       Get all the filenames in this folder. (All files are files for reservations
            //       for movies. You don't need to check this).
            //
            //       Loop over the files and read the first line
            //       Using the first line:
            //          Search the hall in the dictionary and make a NEW OBJECT,
            //          which IS A COPY of this hall!!!
            //          e.g.: Hall hall = new MovieTheaterHall( <use info from the lookup> )
            //          Create a new MovieShow (using the hall and info from the file)
            //          and add this to the eventList.
            //
            //          NOTE: this first line is ALWAYS correct, you do not need to check anything.
            //
            //       Make use of the method ReadReservations(see below) to read the rest of the 
            //       file and set the property IsSeatReserved of the reserved chairs on true
            var folderSelectionDialog = new FolderBrowserDialog();
            if (folderSelectionDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                // shortcut: hardcode path ("c:\\...";) and if test in comment (on exam to avoid selecting path each time, but reset at the end)
                folderPath = folderSelectionDialog.SelectedPath; 

                var arrayOfFiles = Directory.GetFiles(folderPath);

                

                foreach (var file in arrayOfFiles)
                {

                    string movieName = Path.GetFileNameWithoutExtension(file);
                    using (StreamReader sr = new StreamReader(file))
                    {
                        string line = sr.ReadLine();
                        string[] splitLine = line.Split(";");
                        if (lookup.ContainsKey(splitLine[0].Trim()))
                            {
                                Hall hall = new MovieTheaterHall(lookup[splitLine[0]].Name, lookup[splitLine[0]].MaxCapacity);
                                Movieshow movieshow = new Movieshow(movieName, splitLine[1], splitLine[2], hall, splitLine[3]);
                                eventList.Add(movieshow);
                                ReadReservations(movieshow, sr);
                            }
                        

                        
                    }


                }
            }




            return eventList;
        }

        // ToDo: Read the whole file. 
        //       Set the IsSeatReserved property of the reservationHall of the
        //       movieshow for the chairs in the file to true.
        //       For example: seat A2 => ......IsSeatReserved[0][1] = true
        //       Or example: seat C11 => ......IsSeatReserved[2][10] = true
        public static void ReadReservations(Movieshow movieshow, StreamReader reader)
        {
            using (reader)
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] splitLine = line.Split(" ");
                    if (splitLine[0] != "")
                    {
                        string rowString = splitLine[0].Substring(0, 1);
                        string colString = splitLine[0].Substring(1);
                        int row = 0;
                        if (rowString == "A")
                        {
                            row = 0;
                        }
                        else if (rowString == "B")
                        {
                            row = 1;
                        }
                        else if (rowString == "C")
                        {
                            row = 2;
                        }
                        movieshow.ReservationHall.IsSeatReserved[row, Convert.ToInt32(colString) - 1] = true;
                    }
                    
                    
                }
            }

        }
    }
}
