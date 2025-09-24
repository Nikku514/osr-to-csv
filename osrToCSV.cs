using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ReplayAPI;

namespace osrToCSV
{
    public class osrToCSV
    {
        static void Main(string[] args)
        {
            string ReplayPath = null;
            if (File.Exists("Options.cfg"))
            {
                StreamReader sr = new StreamReader("Options.cfg");
                ReplayPath = sr.ReadLine();
                sr.Close();
            }
            else //NTODO: unfuck this so that Replay.cs doesnt complain if it's not perfectly a valid .osr
            {
                Console.WriteLine("Enter replay path:");
                ReplayPath = Console.ReadLine();
                StreamWriter RP = new StreamWriter("Options.cfg");
                RP.WriteLine(ReplayPath);
                RP.Close();
            }

            Replay ry = new Replay(ReplayPath); //NTODO: somehow get this shitter to allow multiple files per run of the exe

            Console.WriteLine("Writing "+ReplayPath);
            if (File.Exists("Replays.csv"))
            {
                using (StreamWriter sw = File.AppendText("Replays.csv"))
                { 
                sw.WriteLine(ry.PlayerName + "," + ry.GameMode + "," + ry.Version + "," + ry.Mods + "," + ry.TotalScore + "," + ry.Count300 + "," + ry.Count100 + "," + ry.Count50 + "," + ry.CountGeki + "," + ry.CountKatu + "," + ry.CountMiss + "," + ry.MaxCombo + "," + ry.IsPerfect + "," + ry.MapHash);
                sw.Close();
                }
            }
            else
            {
                StreamWriter sw = new StreamWriter("Replays.csv");
                sw.WriteLine("Name,Mode,Version,Mods,Score,300s,100s,50s,Geki,Katu,Misses,Max Combo,PFC,Beatmap Hash");
                sw.WriteLine(ry.PlayerName + "," + ry.GameMode + "," + ry.Version + "," + ry.Mods + "," + ry.TotalScore + "," + ry.Count300 + "," + ry.Count100 + "," + ry.Count50 + "," + ry.CountGeki + "," + ry.CountKatu + "," + ry.CountMiss + "," + ry.MaxCombo + "," + ry.IsPerfect + "," + ry.MapHash);
                sw.Close();
            }

            Console.WriteLine("Finished writing to "+("Replays.csv"));
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }
}


