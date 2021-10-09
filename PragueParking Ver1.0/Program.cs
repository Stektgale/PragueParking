using System;

namespace PragueParking_Ver1._0
{
    class Program
    {
        static string[] parkingGarage = new string[100];

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the super useful and not at all broken parking garage software.");
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

                case "REMOVE":
                    RemoveVehicle();
                    break;

                case "MOVE":
                    MoveVehicle();
                    break;

                case "SEARCH":
                    SearchForVehicle();
                    break;

                case "HELP":
                    Help();
                    break;

                case "SHOWALL":
                    ShowAll();
                    break;

                case "EXIT":
                    Exit();
                    break;

                default:
                    Console.WriteLine("The input Was not a valid command. Input help for a help list.");
                    ProcessInput();
                    break;
            }
        }


        static void AddVehicle()
        {
            string[] temporary = new string[2];
            Console.Write("Select Parking Spot From 1-100: ");
            int i = int.Parse(Console.ReadLine());
            int j = VehicleCheck(i);
            if (j == 0)
            {
                Console.Write("Enter vehicle type. car or mc:");
                temporary[0] = Console.ReadLine().ToUpper();

                Console.Write("Enter Vehicle Registration number:");
                temporary[1] = Console.ReadLine().ToUpper();

                parkingGarage[i - 1] = string.Join('#', temporary);

                Console.WriteLine("Assigned: {0} to parking spot: {1}", (temporary[0] + " " + temporary[1]), i);
            }
            else if (j == -1)
            {
                Console.WriteLine("The parkingspace is occupied by a car. Please try something else.");
            }
            else if (j == 1)
            {
                Console.WriteLine("The parkingspace is occupied by one mc. Do you want to add another one? (Y/N)");
                string s = Console.ReadLine().ToUpper();
                switch (s)
                {
                    case "Y":
                        temporary = new string[2];
                        string[] temp2 = new string[2];
                        temporary[0] = "MC";
                        Console.Write("Enter Vehicle Registration number:");
                        temporary[1] = Console.ReadLine().ToUpper();
                        temp2[0] = string.Join('#', temporary);
                        temp2[1] = parkingGarage[i - 1];
                        parkingGarage[i - 1] = string.Join('|', temp2);
                        Console.WriteLine("Assigned: {0} to parking spot: {1}", (temporary[0] + " " + temporary[1]), i);
                        break;

                    case "N":
                        Console.WriteLine("Try something else.");
                        break;

                    default:
                        Console.WriteLine("Invalid input try something else.");
                        break;
                }
            }
            else if (j == 2)
            {
                Console.WriteLine("The Parking Space is occupied by two motorcycles. Please try something else.");
            }

            ProcessInput();
        }

        static void RemoveVehicle()
        {
            Console.Write("Select parking spot from 1-100: ");
            int i = int.Parse(Console.ReadLine());
            parkingGarage[i - 1].Remove(i - 1, 1);
        }

        static void MoveVehicle()
        {
            Console.Write("Select parking spot from 1-100 to move: ");
            int i = int.Parse(Console.ReadLine());
            Console.Write("Select parking spot from 1-100 to move current vehicle to: ");
            int j = int.Parse(Console.ReadLine());
            string tempString = parkingGarage[j - 1];
            (parkingGarage[j - 1], parkingGarage[i - 1]) = (parkingGarage[i - 1], parkingGarage[j - 1]);
            ProcessInput();
        }

        static void SearchForVehicle()
        {

        }

        static void Help()
        {

        }

        static void ShowAll()
        {
            for (int i = 0; i < parkingGarage.Length; i++)
            {
                Console.WriteLine("Parking {0}: {1}", i + 1, parkingGarage[i]);
            }
            ProcessInput();
        }

        static void Exit()
        {

        }

        static int VehicleCheck(int index)
        {
            int i = 0;
            if (!string.IsNullOrEmpty(parkingGarage[index - 1]))
            {
                string[] s1 = new string[4];
                char[] separators = { '#', '|' };
                s1 = parkingGarage[index - 1].Split(separators);

                foreach (string s in s1)
                {
                    if (s == "MC") { i++; }
                    if (s == "CAR") { i--; }
                }
            }
            return i;
        }
    }
}
