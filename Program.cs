// Steps 
// Step 1. Setup board size and constraints -- max coordinate and max instructions less than 100
// Step 2. Create robots with current position and orientation
// Step 2.1 Creation method to allow any number of robots
// Step 2.2 Allow any number movement instructions
// Step 3. Start walking
// Step 3.1 Check if current coordination is less than robot_scent

// variables to use:
// 1. robot_scent
// 2. max_coordinates / min_coordinates
// 3. max_instruction_count = 100;

using mars_robots;
using marsRobots;
using System.Threading.Channels;

class Program
{
    // Fix for CS0120: Make fields static so they can be accessed from static Main
    private static Coordinate max_board_coordinates = new(0,0);
    private static Coordinate min_board_coordinates = new(0,0);
    private static Coordinate robot_scent_coordinates = new(0,0);

    private static int instructionLength = 100;
    private static int nrOfRobots = 0;
    private static List<Robot>robots = new List<Robot>();
    private static string? input;

    static void Main(string[] args)
    {
        // Step 1
        PrintMainHeader();

        // Step 2
        GetRobotCount();

        // Step 3
        SetupMaximumBoardCoordinates();

        // Step 4
        CreateRobots();

        // Step 5
        StartWalkOperation();
    }

    // Step 1
    private static void PrintMainHeader()
    {
        Console.WriteLine("Application started...");
        Console.WriteLine("----------------------");
        Console.WriteLine();
    }

    // Step 2
    private static void GetRobotCount()
    {
        Console.WriteLine("Please enter the number of robots:");
        Console.WriteLine("----------------------------------");

        input = Console.ReadLine();

        if (ValidateUserInput(input))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }
        else
        {
            nrOfRobots = int.Parse(input!);
        }
    }

    // Step 3
    private static void SetupMaximumBoardCoordinates()
    {
        Console.WriteLine();
        Console.WriteLine("Please enter the maximum board coordinates - for example 5 3. Please leave a space between coords");
        Console.WriteLine("-------------------------------------------------------------------------------------------------");

        input = Console.ReadLine();

        if (ValidateUserInput(input))
        {
            Console.WriteLine("Invalid input. Please enter a valid coordinate");
            return;
        }
        else
        {
            max_board_coordinates = new Coordinate(input!);
        }
    }

    // Step 4
    private static void CreateRobots()
    {
        Console.WriteLine();
        while (nrOfRobots > 0)
        {
            Console.Write($"Create robot {nrOfRobots}. Enter current coordinate and orientation such as 1 3 E: ");
            var coordinates = Console.ReadLine();

            Console.Write("Enter sequence of movements like FRLRF: ");
            var movements = Console.ReadLine();

            var robot = new Robot($"R{nrOfRobots}", new Coordinate(coordinates!), movements);
            robots.Add(robot);

            nrOfRobots--;
        }

        Console.WriteLine();
        Console.WriteLine("Robots created!!!");
        Console.WriteLine();
    }

    // Step 5
    private static void StartWalkOperation()
    {
        Console.WriteLine("Do you want the robots move now? Press [Y / N]");
        Console.WriteLine("----------------------------------------------");

        var ans = Console.ReadLine()!;

        if (ans.ToUpper() == ("Y"))
        {
            foreach (var robot in robots)
            {
                robot.Move();
                PrintNewCoordinates(robot);
            }
        }
        else
        {
            Console.Clear();
            PrintMainHeader();
            GetRobotCount();
        }

        Console.Read();
    }

    // Helper methods
    private static bool ValidateUserInput(string? input)
    {
        return string.IsNullOrEmpty(input);
    }

    private static void PrintNewCoordinates(Robot robot)
    {
        Console.WriteLine($"Robot name: {robot.Name}");
        Console.WriteLine($"Coordinate.X: {robot.InternalCoordinate.X} : Coordinate.Y: {robot.InternalCoordinate.Y}");
        Console.WriteLine($"Orientation: {robot.InternalCoordinate.Orientation}");
    }

}
