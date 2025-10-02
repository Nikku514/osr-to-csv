using System;
using System.IO;
using System.Diagnostics;
using ReplayAPI;

namespace OSRtoCSV
{
    public class OSRtoCSV
    {
        static void Main(string[] args)
        {
            string replayPath;
            string outputFile;
            
            if (File.Exists("Options.cfg"))
            {
                StreamReader sr = new StreamReader("Options.cfg");
                replayPath = sr.ReadLine();
                outputFile = sr.ReadLine();
                sr.Close();
            }
            else
            {
                Console.WriteLine("Enter replay path:");
                replayPath = Console.ReadLine();
                Console.WriteLine("Replay path is set to " + replayPath);
                Console.WriteLine("Enter output file name:");
                outputFile = Console.ReadLine();
                Console.WriteLine("output file is set to " + outputFile);
                StreamWriter swo = new StreamWriter("Options.cfg");
                swo.WriteLine(replayPath);
                swo.WriteLine(outputFile + ".csv");
                swo.Close();
            }

            if (!File.Exists(outputFile))
            {
                //This looks ugly to me but it works
                if (!(outputFile == ".csv" || outputFile == null || outputFile == ""))
                {
                    StreamWriter sw = new StreamWriter(outputFile);
                    sw.WriteLine("Name,Mode,Version,Mods,Score,300s,100s,50s,Geki,Katu,Misses,Max Combo,PFC,Date Played,Beatmap Hash");
                    sw.Close(); //NTODO: Add ability to configure ^
                }
                else
                {
                    outputFile = "Replays.csv";
                    StreamWriter sw = new StreamWriter(outputFile);
                    sw.WriteLine("Name,Mode,Version,Mods,Score,300s,100s,50s,Geki,Katu,Misses,Max Combo,PFC,Date Played,Beatmap Hash");
                    sw.Close(); //NTODO: Add ability to configure ^
                }
            }

            if (replayPath == null || outputFile == "")
            {
                Console.WriteLine("Replay path is empty");
                File.Move("Options.cfg", "Options." + DateTime.Now.Ticks);
                Console.WriteLine(outputFile + " renamed to " + outputFile + "." + DateTime.Now.Ticks);
                return;
            }
            // This should be better but ngl cant be assed rn
            if (outputFile == ".csv" || outputFile == null || outputFile == "")
            {
                Console.WriteLine("Output file is empty");
                File.Move("Options.cfg", "Options." + DateTime.Now.Ticks);
                Console.WriteLine(outputFile + " renamed to " + outputFile+"." + DateTime.Now.Ticks);
                return;
            }
            
            string[] replayList = Directory.GetFiles(replayPath, "*.osr");
            foreach (string replay in replayList)
            {
                Console.WriteLine("Writing " + replay);
                Replay ry = new Replay(replay, true);
                if (!File.Exists(outputFile)) continue;
                using (StreamWriter sw = File.AppendText(outputFile))
                {
                    sw.WriteLine(ry.PlayerName + "," + ry.GameMode + "," + ry.Version + ",\"" + ry.Mods + "\"," + ry.TotalScore + "," + ry.Count300 + "," + ry.Count100 + "," + ry.Count50 + "," + ry.CountGeki + "," + ry.CountKatu + "," + ry.CountMiss + "," + ry.MaxCombo + "," + ry.IsPerfect + "," + ry.PlayTime + "," + ry.MapHash);
                    sw.Close(); //NTODO: Along with configurability, clean up this so it's not 2 billion characters long ^
                }
            }

            Console.WriteLine("Press C to open CSV in default program");
            Console.WriteLine("Press enter to close...");
            if (Console.ReadKey().Key == ConsoleKey.C)
                Process.Start(outputFile);
        }
    }
}


