using System;

namespace PragueParking_Ver1._0
{
    class Program
    {
        static string[] parkingGarage = new string[100];

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Gale Parking v1.0.");
            MainMenu();
        }

        static void MainMenu()
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
                    Console.WriteLine("Input registration number.");
                    int i = SearchForVehicle(Console.ReadLine());
                    if (i == -1)
                    {
                        Console.WriteLine("Could not find the vehicle you are looking for. Try using SHOWALL instead.");
                    }
                    else
                    {
                        Console.WriteLine("Vehicle found on spot: {0}", i);
                    }
                    MainMenu();
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
                    MainMenu();
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
                do
                {
                    Console.Write("Enter vehicle type. car or mc:");
                    temporary[0] = Console.ReadLine().ToUpper();
                    if (temporary[0] == "MC" || temporary[0] == "CAR")
                    {
                        continue;
                    }
                    Console.WriteLine("Invalid vehicle type. Try again.");
                }
                while ((temporary[0] != "MC") && (temporary[0] != "CAR"));

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
                Console.Write("The parkingspace is occupied by one mc. Do you want to add another one? (Y/N): ");
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
            MainMenu();
        }

        static void RemoveVehicle()
        {
            Console.Write("Select parking spot from 1-100: ");
            int i = int.Parse(Console.ReadLine());
            int j = VehicleCheck(i);
            switch (j)
            {
                case -1:
                    string[] spotSplit = new string[2];
                    spotSplit = parkingGarage[i - 1].Split('#');
                    Console.WriteLine("Removed {0} {1} from spot {2}", spotSplit[0], spotSplit[1], i);
                    parkingGarage[i - 1] = null;
                    break;

                case 0:
                    Console.WriteLine("The Spot doesn't contain any vehicles.");
                    break;

                case 1:
                    spotSplit = new string[2];
                    spotSplit = parkingGarage[i - 1].Split('#');
                    Console.WriteLine("Removed {0} {1} from spot {2}", spotSplit[0], spotSplit[1], i);
                    parkingGarage[i - 1] = null;
                    break;

                case 2:
                    string[] mcSplit = new string[2];
                    spotSplit = new string[4];
                    spotSplit = parkingGarage[i - 1].Split('|', '#');
                    mcSplit = parkingGarage[i - 1].Split('|');
                    Console.Write("Two motorcycles {0} {1} and {2} {3} were found on this spot. Which one do you want to remove? (1/2): ",
                        spotSplit[0], spotSplit[1], spotSplit[2], spotSplit[3]);
                    int n = int.Parse(Console.ReadLine());
                    if (n == 1)
                    {
                        Console.WriteLine("Removed {0} {1} from spot {2}", spotSplit[0], spotSplit[1], i);
                        mcSplit[0] = null;
                        parkingGarage[i - 1] = mcSplit[1];
                    }
                    else if (n == 2)
                    {
                        Console.WriteLine("Removed {0} {1} from spot {2}", spotSplit[2], spotSplit[3], i);
                        mcSplit[1] = null;
                        parkingGarage[i - 1] = mcSplit[0];
                    }
                    else { Console.WriteLine("Failed to remove vehicle due to invalid input!"); }
                    break;
            }
            MainMenu();
        }

        static void MoveVehicle()
        {
            string[] spotSplit = new string[2];
            int secondSpot = 0;
            Console.Write("Select parking spot from 1-100 to move: ");
            int firstSpot = int.Parse(Console.ReadLine());
            int vehicleValue = VehicleCheck(firstSpot);
            switch (vehicleValue)
            {
                case -1:
                    spotSplit = parkingGarage[firstSpot - 1].Split('#');
                    Console.WriteLine("You have selected {0} {1} to move now.", spotSplit[0], spotSplit[1]);
                    Console.Write("Select parking spot from 1-100 to move current vehicle to: ");
                    secondSpot = int.Parse(Console.ReadLine());
                    (parkingGarage[secondSpot - 1], parkingGarage[firstSpot - 1]) = (parkingGarage[firstSpot - 1], parkingGarage[secondSpot - 1]);
                    Console.WriteLine("Moved vehicle to new spot. If there were any vehicles on that spot then they have switched places.");
                    break;

                case 0:
                    Console.WriteLine("The parking spot is empty. Try something else.");
                    break;

                case 1:
                    spotSplit = parkingGarage[firstSpot - 1].Split('#');
                    Console.WriteLine("You have selected {0} {1} to move.", spotSplit[0], spotSplit[1]);
                    Console.Write("Now select parking spot from 1-100 to move current vehicle to: ");
                    secondSpot = int.Parse(Console.ReadLine());
                    int secondVehicleValue = VehicleCheck(secondSpot);
                    switch (secondVehicleValue)
                    {
                        case 0:
                            (parkingGarage[secondSpot - 1], parkingGarage[firstSpot - 1]) = (parkingGarage[firstSpot - 1], parkingGarage[secondSpot - 1]);
                            Console.WriteLine("Moved vehicle to new spot.");
                            break;

                        case 2:
                            string[] secondSpotSplit = new string[4];
                            spotSplit = new string[2];
                            spotSplit = parkingGarage[firstSpot - 1].Split('#');
                            secondSpotSplit = parkingGarage[secondSpot - 1].Split('#', '|');
                            Console.Write("Two vehicles {0} {1} and {2} {3} found on that spot. Which one do you want to switch places with.(1/2): ",
                                secondSpotSplit[0], secondSpotSplit[1], secondSpotSplit[2], secondSpotSplit[3]);
                            string s = Console.ReadLine();
                            if (s == "1")
                            {
                                (spotSplit[0], spotSplit[1], secondSpotSplit[0], secondSpotSplit[1]) = (secondSpotSplit[0], secondSpotSplit[1], spotSplit[0], spotSplit[1]);
                                string[] secondSpotStitch = new string[2];
                                parkingGarage[firstSpot - 1] = string.Join('#', spotSplit);
                                secondSpotStitch[0] = string.Join('#', secondSpotSplit[0], secondSpotSplit[1]);
                                secondSpotStitch[1] = string.Join('#', secondSpotSplit[2], secondSpotSplit[3]);
                                parkingGarage[secondSpot - 1] = string.Join('|', secondSpotStitch);
                                Console.WriteLine("Swapped vehicles.");
                            }
                            else if (s == "2")
                            {
                                (spotSplit[0], spotSplit[1], secondSpotSplit[2], secondSpotSplit[3]) = (secondSpotSplit[2], secondSpotSplit[3], spotSplit[0], spotSplit[1]);
                                string[] secondSpotStitch = new string[2];
                                parkingGarage[firstSpot - 1] = string.Join('#', spotSplit);
                                secondSpotStitch[0] = string.Join('#', secondSpotSplit[0], secondSpotSplit[1]);
                                secondSpotStitch[1] = string.Join('#', secondSpotSplit[2], secondSpotSplit[3]);
                                parkingGarage[secondSpot - 1] = string.Join('|', secondSpotStitch);
                                Console.WriteLine("Swapped vehicles.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Try something else.");
                            }

                            break;

                        default:
                            (parkingGarage[secondSpot - 1], parkingGarage[firstSpot - 1]) = (parkingGarage[firstSpot - 1], parkingGarage[secondSpot - 1]);
                            Console.WriteLine("Switched spots between the vehicles.");
                            break;
                    }
                    break;

                case 2:
                    spotSplit = new string[4];
                    spotSplit = parkingGarage[firstSpot - 1].Split('#', '|');
                    Console.Write("There are two vehicles {0} {1} and {2} {3} on this spot. Chose one to move.(1/2):",
                        spotSplit[0], spotSplit[1], spotSplit[2], spotSplit[3]);
                    string input = Console.ReadLine();
                    if (input == "1")
                    {
                        Console.WriteLine("You have selected {0} {1} to move.", spotSplit[0], spotSplit[1]);
                        Console.Write("Now select parking spot from 1-100 to move current vehicle to: ");
                        secondSpot = int.Parse(Console.ReadLine());
                        vehicleValue = VehicleCheck(secondSpot);
                        switch (vehicleValue)
                        {
                            case -1:
                                Console.WriteLine("Action failed. A car won't fit on the current parking spot.");
                                break;

                            case 0:
                                parkingGarage[secondSpot - 1] = string.Join('#', spotSplit[0], spotSplit[1]);
                                parkingGarage[firstSpot - 1] = string.Join('#', spotSplit[2], spotSplit[3]);
                                spotSplit[0] = null;
                                spotSplit[1] = null;
                                Console.WriteLine("Moved vehicle to new Spot.");
                                break;

                            case 1:
                                spotSplit = new string[2];
                                spotSplit = parkingGarage[firstSpot - 1].Split('|');
                                (spotSplit[0], parkingGarage[secondSpot - 1]) = (parkingGarage[secondSpot - 1], spotSplit[0]);
                                parkingGarage[firstSpot - 1] = string.Join('|', spotSplit);
                                Console.WriteLine("Vehicles switched spots");
                                break;

                            case 2:
                                string[] secondSpotSplit = new string[4];
                                string[] secondSpotStitch = new string[2];
                                string[] spotStitch = new string[2];
                                spotSplit = new string[4];
                                spotSplit = parkingGarage[firstSpot - 1].Split('#', '|');
                                secondSpotSplit = parkingGarage[secondSpot - 1].Split('#', '|');
                                Console.WriteLine("Two vehicles {0} {1} and {2} {3} found on that spot. Which one do you want to switch places with.(1/2): ",
                                    secondSpotSplit[0], secondSpotSplit[1], secondSpotSplit[2], secondSpotSplit[3]);
                                string s = Console.ReadLine();
                                if (s == "1")
                                {
                                    (spotSplit[0], spotSplit[1], secondSpotSplit[0], secondSpotSplit[1]) = (secondSpotSplit[0], secondSpotSplit[1], spotSplit[0], spotSplit[1]);
                                    spotStitch[0] = string.Join('#', spotSplit[0], spotSplit[1]);
                                    spotStitch[1] = string.Join('#', spotSplit[2], spotSplit[3]);
                                    secondSpotStitch[0] = string.Join('#', secondSpotSplit[0], secondSpotSplit[1]);
                                    secondSpotStitch[1] = string.Join('#', secondSpotSplit[2], secondSpotSplit[3]);
                                    parkingGarage[firstSpot - 1] = string.Join('|', spotStitch);
                                    parkingGarage[secondSpot - 1] = string.Join('|', secondSpotStitch);
                                    Console.WriteLine("Vehicles switched spots.");
                                }
                                else if (s == "2")
                                {
                                    (spotSplit[0], spotSplit[1], secondSpotSplit[2], secondSpotSplit[3]) = (secondSpotSplit[2], secondSpotSplit[3], spotSplit[0], spotSplit[1]);
                                    spotStitch[0] = string.Join('#', spotSplit[0], spotSplit[1]);
                                    spotStitch[1] = string.Join('#', spotSplit[2], spotSplit[3]);
                                    secondSpotStitch[0] = string.Join('#', secondSpotSplit[0], secondSpotSplit[1]);
                                    secondSpotStitch[1] = string.Join('#', secondSpotSplit[2], secondSpotSplit[3]);
                                    parkingGarage[firstSpot - 1] = string.Join('|', spotStitch);
                                    parkingGarage[secondSpot - 1] = string.Join('|', secondSpotStitch);
                                    Console.WriteLine("Vehicles switched spots.");
                                }
                                break;
                        }
                    }
                    else if (input == "2")
                    {
                        Console.WriteLine("You have selected {0} {1} to move.", spotSplit[2], spotSplit[3]);
                        Console.Write("Now select parking spot from 1-100 to move current vehicle to: ");
                        secondSpot = int.Parse(Console.ReadLine());
                        vehicleValue = VehicleCheck(secondSpot);
                        switch (vehicleValue)
                        {
                            case -1:
                                Console.WriteLine("Action failed. A car won't fit on the current parking spot.");
                                break;

                            case 0:
                                parkingGarage[secondSpot - 1] = string.Join('#', spotSplit[2], spotSplit[3]);
                                parkingGarage[firstSpot - 1] = string.Join('#', spotSplit[0], spotSplit[1]);
                                spotSplit[2] = null;
                                spotSplit[3] = null;
                                Console.WriteLine("Moved vehicle to new Spot.");
                                break;

                            case 1:
                                spotSplit = new string[2];
                                spotSplit = parkingGarage[firstSpot - 1].Split('|');
                                (spotSplit[1], parkingGarage[secondSpot - 1]) = (parkingGarage[secondSpot - 1], spotSplit[1]);
                                parkingGarage[firstSpot - 1] = string.Join('|', spotSplit);
                                Console.WriteLine("Vehicles switched spots");
                                break;

                            case 2:
                                string[] secondSpotSplit = new string[4];
                                string[] secondSpotStitch = new string[2];
                                string[] spotStitch = new string[2];
                                spotSplit = new string[4];
                                spotSplit = parkingGarage[firstSpot - 1].Split('#', '|');
                                secondSpotSplit = parkingGarage[secondSpot - 1].Split('#', '|');
                                Console.WriteLine("Two vehicles {0} {1} and {2} {3} found on that spot. Which one do you want to switch places with.(1/2): ",
                                    secondSpotSplit[0], secondSpotSplit[1], secondSpotSplit[2], secondSpotSplit[3]);
                                string s = Console.ReadLine();
                                if (s == "1")
                                {
                                    (spotSplit[2], spotSplit[3], secondSpotSplit[0], secondSpotSplit[1]) = (secondSpotSplit[0], secondSpotSplit[1], spotSplit[2], spotSplit[3]);
                                    spotStitch[0] = string.Join('#', spotSplit[0], spotSplit[1]);
                                    spotStitch[1] = string.Join('#', spotSplit[2], spotSplit[3]);
                                    secondSpotStitch[0] = string.Join('#', secondSpotSplit[0], secondSpotSplit[1]);
                                    secondSpotStitch[1] = string.Join('#', secondSpotSplit[2], secondSpotSplit[3]);
                                    parkingGarage[firstSpot - 1] = string.Join('|', spotStitch);
                                    parkingGarage[secondSpot - 1] = string.Join('|', secondSpotStitch);
                                    Console.WriteLine("Vehicles switched spots.");
                                }
                                else if (s == "2")
                                {
                                    (spotSplit[2], spotSplit[3], secondSpotSplit[2], secondSpotSplit[3]) = (secondSpotSplit[2], secondSpotSplit[3], spotSplit[2], spotSplit[3]);
                                    spotStitch[0] = string.Join('#', spotSplit[0], spotSplit[1]);
                                    spotStitch[1] = string.Join('#', spotSplit[2], spotSplit[3]);
                                    secondSpotStitch[0] = string.Join('#', secondSpotSplit[0], secondSpotSplit[1]);
                                    secondSpotStitch[1] = string.Join('#', secondSpotSplit[2], secondSpotSplit[3]);
                                    parkingGarage[firstSpot - 1] = string.Join('|', spotStitch);
                                    parkingGarage[secondSpot - 1] = string.Join('|', secondSpotStitch);
                                    Console.WriteLine("Vehicles switched spots.");
                                }
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Try something else.");
                    }
                    break;

                default:
                    break;
            }

            MainMenu();
        }

        static int SearchForVehicle(string s)
        {
            for (int i = 1; i < 100; i++)
            {
                string[] split = new string[4];

                if (parkingGarage[i - 1] != null)
                {
                    split = parkingGarage[i - 1].Split('#', '|');
                }

                foreach (string item in split)
                {
                    if (item == s.ToUpper())
                    {
                        return i;
                    }
                    continue;
                }
            }
            return -1;
        }

        static void Help()
        {
            Console.WriteLine("\nHelplist:");
            Console.WriteLine("\nADD: Add vehicles in to the program." +
                "\nREMOVE: Remove vehicles from the system." +
                "\nMOVE: Move vehicles between parking spots. Can even switch two vehicles parking spots." +
                "\nSEARCH: Search after a licenseplate and find the parking spot of the vehicle." +
                "\nSHOWALL: Displays a list of all parking spots and tha vehicles parked there." +
                "\nEXIT: Can be used to close program." +
                "\nHELP: Bring back this list");
            MainMenu();
        }

        static void ShowAll()
        {
            for (int i = 0; i < parkingGarage.Length; i++)
            {
                Console.WriteLine("Parking {0}: {1}", i + 1, parkingGarage[i]);
            }
            MainMenu();
        }

        static void Exit()
        {

        }

        /// <summary>
        /// Returns a number: -1 occupied by car, 0 free spot, 1 occupied by one mc, 2 occupied by two mcs
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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
