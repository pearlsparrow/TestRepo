using System;
using System.Threading;
/* We are using System.Diagnostics to be able to use functions such as Stopwatch & timelapse to test
*time based thread calls */
using System.Diagnostics;
namespace GPR5100.NetworkingTestBed
{
    public interface MathCalc
    {
        double CalculateDiameter();
        double CalculateArea();
    }
    class Circle : MathCalc
    {
        private double area;
        private double diameter;
        public double Radius { get; set; }
        public double CalculateArea()
        {
            area = Math.PI * Radius * Radius;
            return area;
        }
        public double CalculateDiameter()
        {
            diameter = 2 * Math.Sqrt(area / Math.PI);
            return diameter;
        }
    }
    class Cube : MathCalc
    {

        public double CalculateArea()
        {
            throw new NotImplementedException();
        }

        public double CalculateDiameter()
        {
            throw new NotImplementedException();
        }
    }

    class Triangle : MathCalc
    {

        public double CalculateArea()
        {
            throw new NotImplementedException();
        }

        public double CalculateDiameter()
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        //Main thread...
        static void Main(string[] args)
        {
            // Multithreading κανουμε και ελεγχουμε προγραμματιστικα...
            //Το προγραμμα μας ηδη εχει ενα thread που τρεχει τον δικο μας κωδικα
            //Το .Net μας επιτρεπει να δημιουργησουμε
            //Αντικειμενα της κλασης Thread, να δημιουργησουμε λοιπον threads και
            //να της πουμε ποια μεθοδο να τρεξει, καπως ετσι δημιουργουμε multiple threads
            //έχοντας πολλά threads να τρέχουν δημιουργούμε ενα multithreading architecture structure
            /////////////////////////////
            //Να διαβασω για delegates...
            /////////////////////////////

            //Create a second & a third thread ...
            Thread secondThread = new Thread(SecondThreadExample);
            Thread thirdThread = new Thread(ThirdThreadExample);
            //Name the threads...
            secondThread.Name = "[SECOND THREAD]";
            thirdThread.Name = "[THIRD THREAD]";
            Thread.CurrentThread.Name = "[MAIN THREAD]";

            //Create a circle class instance
            Circle circle = new Circle();
            circle.Radius = 37.5;

            //Start & Join The second Thread
            secondThread.Start();
            secondThread.Join();
            //Start & Join the Third Thread when the second Thread is Finished
            if (!secondThread.IsAlive)
            {
                thirdThread.Start();
                thirdThread.Join();
                
            }
            //Run the main thread loop for 5 seconds (5k miliseconds)...
            /////////////////////////////
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            TimeSpan timeSpan = stopWatch.Elapsed;
            while (stopWatch.ElapsedMilliseconds <= 5000)
            {
                Console.WriteLine($"[CIRCLE'S AREA FOR MAIN THREAD IS] : {Math.Round(circle.CalculateArea())}\n");
                Console.WriteLine($"[CIRCLE'S DIAMETER FOR MAIN THREAD IS] : {Math.Round(circle.CalculateDiameter())}\n");
                Console.WriteLine($"[{Thread.CurrentThread.Name} THREAD TIME ELAPSE] : {timeSpan.TotalMilliseconds} Secs\n");
                Thread.Yield();
            }
            //Anounce when the Threads are Finished
            Console.WriteLine($"[THREAD : ({secondThread.Name}) => FINISHED]");
            Console.WriteLine($"[THREAD : ({thirdThread.Name}) => FINISHED]");
            Console.WriteLine($"[MAIN THREAD : ({Thread.CurrentThread.Name}) => FINISHED]");
            Console.WriteLine($"[{Thread.CurrentThread.Name} FINISHED IN TIME] : {timeSpan.TotalMilliseconds} Secs");
            stopWatch.Stop();
        }
        //Thread procedure for the second Thread...
        protected static void SecondThreadExample()
        {
            //TO DO
            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();
            Circle circle2 = new Circle();
            circle2.Radius = 56.6f;
            while (stopWatch2.ElapsedMilliseconds <= 7000)
            {
                Console.WriteLine($"[CIRCLE'S AREA FOR 2ND THREAD IS] {Math.Round(circle2.CalculateArea())}\n");
                Console.WriteLine($"[CIRCLE'S DIAMETER FOR 2ND THREAD IS] {Math.Round(circle2.CalculateDiameter())}\n");
                Thread.Yield();
            }
        }
        //Thread procedure for the third Thread...
        protected static void ThirdThreadExample()
        {
            Stopwatch stopWatch3 = new Stopwatch();
            stopWatch3.Start();
            Circle circle3 = new Circle();
            circle3.Radius = 45.6f;
            while (stopWatch3.ElapsedMilliseconds <= 8000)
            {
                Console.WriteLine($"[CIRCLE'S AREA FOR 3rd THREAD IS] {Math.Round(circle3.CalculateArea())}\n");
                Console.WriteLine($"[CIRCLE'S DIAMETER FOR 3rd THREAD IS] {Math.Round(circle3.CalculateDiameter())}\n");
                Thread.Yield();
            }
        }
    }
}
//Τα threads δεν τρεχουν παντα και για παντα... Τρεχουν μεχρις οτου να τελειωσει μια συνθηκη...
//Στην συγκεκριμενη περιπτωση δεν κανουμε τα threads να τρεχουν παραλληλα (για να καταλαβουμε την λογικη των threads σαν μοναδες)
//Και η συνθηκη που σταματαει το καθε thread ειναι ο ορισμενος χρονος, μολις φτασουν στα milliseconds που εχουμε ορισει το καθε thread τελειωνει 
//την διαδικασια του...