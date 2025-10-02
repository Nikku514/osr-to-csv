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
            
            //Temp implementation 
            string csvHeaders = ("Name,Mode,Version,Mods,Score,300s,100s,50s,Geki,Katu,Misses,Max Combo,PFC,Date Played,Beatmap Hash");
            
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
                swo.WriteLine($"{outputFile}.csv");
                swo.Close();
            }

            if (!File.Exists(outputFile))
            {
                //This looks ugly to me but it works
                if (!(outputFile == ".csv" || string.IsNullOrEmpty(outputFile)))
                {
                    StreamWriter sw = new StreamWriter(outputFile);
                    sw.WriteLine(csvHeaders);
                    sw.Close(); //NTODO: Add ability to configure csvHeaders
                }
                else
                {
                    outputFile = "Replays.csv";
                    StreamWriter sw = new StreamWriter(outputFile);
                    sw.WriteLine(csvHeaders);
                    sw.Close(); //NTODO: Add ability to configure csvHeaders
                }
            }

            if (string.IsNullOrEmpty(replayPath) || string.IsNullOrEmpty(outputFile) || outputFile == ".csv")
            {
                Console.WriteLine("Replay path is empty");
                File.Move("Options.cfg", "Options." + DateTime.Now.Ticks);
                Console.WriteLine("Options.cfg renamed to Options." + DateTime.Now.Ticks);
                return;
            }
            
            string[] replayList = Directory.GetFiles(replayPath, "*.osr");
            foreach (string replay in replayList)
            {
                Console.WriteLine("Writing " + replay);
                Replay ry = new Replay(replay, true);
                
                //Temp implementation
                string csvReplayData = (ry.PlayerName + "," + ry.GameMode + "," + ry.Version + ",\"" + ry.Mods + "\"," +
                                        ry.TotalScore + "," + ry.Count300 + "," + ry.Count100 + "," + ry.Count50 + "," +
                                        ry.CountGeki + "," + ry.CountKatu + "," + ry.CountMiss + "," + ry.MaxCombo + "," +
                                        ry.IsPerfect + "," + ry.PlayTime + "," + ry.MapHash);
                
                if (!File.Exists(outputFile)) continue;
                using (StreamWriter sw = File.AppendText(outputFile))
                {
                    sw.WriteLineAsync(csvReplayData);
                    sw.Close(); //NTODO: csvReplayData configurability 
                }
            }

            Console.WriteLine($"Wrote to {outputFile}");
            Console.WriteLine("Press C to open CSV in default program");
            Console.WriteLine("Press enter to close...");
            if (Console.ReadKey().Key == ConsoleKey.C)
                Process.Start(outputFile);
        }
    }
}


