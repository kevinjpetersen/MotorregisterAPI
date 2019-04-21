using System;

namespace MotorregisterAPI.Generation
{
    class Program
    {
        static void Main(string[] args)
        {
            Generation g = new Generation();

            g.LoadSettings();
            g.Generate();

            Console.ReadLine();
        }
    }
}
