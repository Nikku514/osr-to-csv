using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ReplayAPI;

namespace OSRToCSV
{
    public class OSRtoCSV
    {
        static void Main(string[] args)
        {
            string replayPath;
            if (File.Exists("Options.cfg"))
            {
                StreamReader sr = new StreamReader("Options.cfg");
                replayPath = sr.ReadLine();
                sr.Close();
            }
            else
            {
                Console.WriteLine("Enter replay path:");
                replayPath = Console.ReadLine();
                StreamWriter RP = new StreamWriter("Options.cfg");
                RP.WriteLine(replayPath);
                RP.Close();
            }
            if (!File.Exists("Replays.csv"))
            {
                StreamWriter sw = new StreamWriter("Replays.csv");
                sw.WriteLine("Name,Mode,Version,Mods,Score,300s,100s,50s,Geki,Katu,Misses,Max Combo,PFC,Date Played,Beatmap Hash");
                sw.Close(); //NTODO: Add ability to configure ^
            }

            string[] replayList = Directory.GetFiles(replayPath, "*.osr");
            foreach (string replay in replayList)
            {
                Console.WriteLine("Writing " + replay);
                Replay ry = new Replay(replay, true);
                if (File.Exists("Replays.csv"))
                {
                    using (StreamWriter sw = File.AppendText("Replays.csv"))
                    {
                        sw.WriteLine(ry.PlayerName + "," + ry.GameMode + "," + ry.Version + ",\"" + ry.Mods + "\"," + ry.TotalScore + "," + ry.Count300 + "," + ry.Count100 + "," + ry.Count50 + "," + ry.CountGeki + "," + ry.CountKatu + "," + ry.CountMiss + "," + ry.MaxCombo + "," + ry.IsPerfect + "," + ry.PlayTime + "," + ry.MapHash);
                        sw.Close(); //NTODO: Along with configurability, clean up this so it's not 2 billion characters long ^
                    }
                }
            }

            Console.WriteLine("Press C to open CSV in default program");
            Console.WriteLine("Press enter to close...");
            if (Console.ReadKey().Key == ConsoleKey.C)
                Process.Start("Replays.csv");
        }
    }
}


