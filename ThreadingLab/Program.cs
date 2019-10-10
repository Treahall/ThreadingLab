using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingLab
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int DartsToThrow;
            int TotalDartsThrown;
            int ThreadsToRun;
            double TotalDartsInCircle = 0;
            Console.WriteLine("ESTIMATING PI USING THE MONTE CARLO METHOD\n");
            Console.Write("Enter how many darts would you like each thread throw: ");
            DartsToThrow = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter how many threads you want to run: ");
            ThreadsToRun = Convert.ToInt32(Console.ReadLine());

            TotalDartsThrown = DartsToThrow * ThreadsToRun;
            List<Thread> Threads = new List<Thread>();
            List<FindPiThread> FindPiThreads = new List<FindPiThread>();

            for (int i = 0; i < ThreadsToRun; i++)
            {
                FindPiThread TempFindPiThread = new FindPiThread(DartsToThrow);
                Thread TempThread = new Thread(new ThreadStart(TempFindPiThread.ThrowAllDarts));
                FindPiThreads.Add(TempFindPiThread);
                Threads.Add(TempThread);

                Threads[i].Start();
                Thread.Sleep(16);
            }

            foreach (var thread in Threads)
            {
                thread.Join();
            }

            foreach (var findPi in FindPiThreads)
            {
                TotalDartsInCircle += findPi.DartsOnBoard;
            }


            double pi = (4 * (TotalDartsInCircle / TotalDartsThrown));

            Console.WriteLine($"\nThe aproximation of pi is {pi}");
            Console.Read();
        }

        
    }

    class FindPiThread
    {
        int DartsToThrow;
        public int DartsOnBoard;
        Random Throw;

        public FindPiThread(int darts)
        {
            DartsToThrow = darts;
            DartsOnBoard = 0;
            Throw = new Random();
        }

        public void ThrowAllDarts()
        {
            for (int i = 0; i < DartsToThrow; i++)
            {
                double x = Throw.NextDouble();
                double y = Throw.NextDouble();
                double Hypotenuse = Math.Sqrt((x * x) + (y * y));
                if (Hypotenuse <= 1) { DartsOnBoard += 1; }
            }
        }
    }
}
