using DemoDAL;
using DemoDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliConsoleDemoDAL
{
    class Program
    {
        static void Main(string[] args)
        {
            TrackRepository trackrepo = new TrackRepository(new DemoDAL.ChinookEntities());

            Track track = trackrepo.GetTrackByComposer("AC/DC").FirstOrDefault();

            foreach (var prop in track.GetType().GetProperties())
            {
                if (prop.Name == "Composer")
                    prop.SetValue(track, "NOT AC/DC");

                Console.WriteLine(prop.Name + " : " + prop.GetValue(track));
            }
            Console.ReadLine();
        }
    }
}
