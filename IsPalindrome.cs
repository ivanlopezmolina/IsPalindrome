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


//hibernate 
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Timers;

namespace TimerApp
{
    class Program
    {
        private static Timer timer;
        private static bool flag1;
        private static bool flag2;
        private static bool flag3;

        // Import the SetSuspendState function from the kernel32.dll library
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

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

            // Check if the first flag is true and hibernate the machine if it is
            if (flag1)
            {
                HibernateMachine();
            }

            // Check if the third flag is false and stop the timer tick if it is
            if (!flag3)
            {
                timer.Stop();
                Console.WriteLine("Timer tick stopped because the third flag is false.");
            }
            else
            {
                // Perform the required tasks here if the third flag is true.
                // For example, you can put the logic for other actions you want to perform.
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

        private static void HibernateMachine()
        {
            // Hibernate the machine
            SetSuspendState(true, false, false);
        }
    }
}


//press key
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;

namespace TimerApp
{
    class Program
    {
        private static Timer timer;
        private static bool flag1;
        private static bool flag2;
        private static bool flag3;

        // Import the SetSuspendState function from the kernel32.dll library
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

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

            // Check if the first flag is true and press the Scroll Lock key if it is
            if (flag1)
            {
                PressScrollLockKey();
            }

            // Check if the third flag is false and stop the timer tick if it is
            if (!flag3)
            {
                timer.Stop();
                Console.WriteLine("Timer tick stopped because the third flag is false.");
            }
            else
            {
                // Perform the required tasks here if the third flag is true.
                // For example, you can put the logic for other actions you want to perform.
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

        private static void PressScrollLockKey()
        {
            // Simulate pressing the Scroll Lock key
            SendKeys.SendWait("{SCROLLLOCK}");
        }
    }
}

// turn off
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Timers;

namespace TimerApp
{
    class Program
    {
        private static Timer timer;
        private static bool flag1;
        private static bool flag2;
        private static bool flag3;

        // Import the ExitWindowsEx function from the user32.dll library
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        // Constants for the ExitWindowsEx function
        private const uint EWX_SHUTDOWN = 0x00000001;
        private const uint EWX_REBOOT = 0x00000002;
        private const uint EWX_FORCE = 0x00000004;

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

            // Check if the first flag is true and turn off the machine if it is
            if (flag1)
            {
                TurnOffMachine();
            }

            // Check if the third flag is false and stop the timer tick if it is
            if (!flag3)
            {
                timer.Stop();
                Console.WriteLine("Timer tick stopped because the third flag is false.");
            }
            else
            {
                // Perform the required tasks here if the third flag is true.
                // For example, you can put the logic for other actions you want to perform.
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

        private static void TurnOffMachine()
        {
            // Turn off the machine
            ExitWindowsEx(EWX_SHUTDOWN | EWX_FORCE, 0);
        }
    }
}


//read file fresh

using System.IO;

string filePath = "path_to_your_file";
string fileContent;

// Read the file with SequentialScan option to force reading a fresh copy
using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, FileOptions.SequentialScan))
{
    using (StreamReader streamReader = new StreamReader(fileStream))
    {
        fileContent = streamReader.ReadToEnd();
    }
}


//conver string to datetime
string dateTimeString = "2023-07-21 12:34:56";
DateTime dateTime = DateTime.Parse(dateTimeString);

