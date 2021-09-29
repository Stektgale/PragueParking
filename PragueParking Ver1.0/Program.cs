using System;

namespace PragueParking_Ver1._0
{
    class Program
    {
        static string[] parkingGarage = new string[100];

        static void Main(string[] args)
        {
            Console.WriteLine("Skriv pwease :3");
            ProcessInput();
        }

        static void ProcessInput()
        {
            Console.Write("Input a command: ");
            string input = Console.ReadLine();
            switch (input.ToUpper())
            {
                case "ADD":
                    AddVehicle();
                    break;

                default:
                    Console.WriteLine("The input Was not a valid command. Input the help command for help list.");
                    ProcessInput();
                    break;
            }
        }

        static void AddVehicle()
        {
            Console.Write("Select Parking Spot From 1-100: ");
            int i = int.Parse(Console.ReadLine());

            parkingGarage[i] = Console.ReadLine();
        }

        static void RemoveVehicle()
        {

        }
        static void MoveVehicle()
        {

        }

        static void SearchForVehicle()
        {

        }
    }
}
