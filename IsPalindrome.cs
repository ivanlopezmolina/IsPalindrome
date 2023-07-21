string word = "essfsze";
int halfLength = (word.Length-1) / 2;
for(int index = 0; index <= halfLength; index++)
{
    if(word[index] != word[word.Length - 1 - index]){
        Console.WriteLine(false);
        return;
    }
}
Console.WriteLine(true);




//timer tick
using System;
using System.IO;
using System.Timers;

namespace TimerApp
{
    class Program
    {
        private static Timer timer;
        private static bool flag1;
        private static bool flag2;
        private static bool flag3;

        static void Main(string[] args)
        {
            // Read the flags from the text file
            ReadFlagsFromFile();

            // Set up and start the timer
            timer = new Timer(5 * 60 * 1000); // 5 minutes in milliseconds
            timer.Elapsed += OnTimerElapsed;
            timer.Start();

            // Wait for user input to keep the application running
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            // Stop the timer when the application is about to exit
            timer.Stop();
        }

        private static void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Read the flags again on each timer tick
            ReadFlagsFromFile();

            // Check if the third flag is false and stop the timer tick if it is
            if (!flag3)
            {
                timer.Stop();
                Console.WriteLine("Timer tick stopped because the third flag is false.");
            }
            else
            {
                // Perform the required tasks here if the third flag is true.
                // For example, you can put the logic for the actions you want to perform.
                Console.WriteLine("Timer ticked!");
            }
        }

        private static void ReadFlagsFromFile()
        {
            // Read the flags from the text file
            string[] lines = File.ReadAllLines("flags.txt");

            // Update the flag variables
            flag1 = bool.Parse(lines[0]);
            flag2 = bool.Parse(lines[1]);
            flag3 = bool.Parse(lines[2]);
        }
    }
}
