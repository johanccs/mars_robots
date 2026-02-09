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

class Program
{
    private static Coordinate max_board_coordinates = new(0,0);
    private static int max_coordinateSize = 50;
 
    private static int instructionLength = 100;
    private static int nrOfRobots = 0;
    private static List<Robot>robots = new List<Robot>();
    private static string? input;

    static void Main(string[] args)
    {
        try
        {
            RunApplicationLoop();
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}. Do you want to restart [Y/N]?");
            var ans = Console.ReadLine()!;

            if (ans.ToUpper() == "Y")
            {
                RunApplicationLoop();
            }
            
        }
        finally
        {
            robots.Clear();
            nrOfRobots = 0;
            max_board_coordinates = new(0, 0);
        }
        
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
            if(nrOfRobots <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid robot count. Please try again");
                Console.ResetColor();

                Thread.Sleep(2000);

                RunApplicationLoop();
            }
        }
    }

    // Step 3
    private static void SetupMaximumBoardCoordinates()
    {
        Console.WriteLine();
        Console.WriteLine("Please enter the maximum board coordinates - for example 5 3. Please leave a space between coords");
        Console.WriteLine("-------------------------------------------------------------------------------------------------");

        input = Console.ReadLine();

        if (!ValidateCoordinateLength(input!))
        {
            Console.WriteLine($"Invalid. Coordinates should be more than 0 and less than {max_coordinateSize}. Restarting...");
            Thread.Sleep(2000);
            RunApplicationLoop();
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

            if(!ValidateCoordinateLength(coordinates!))
            {
                Console.WriteLine($"Coordinates should be less than {max_coordinateSize}. Restarting...");
                Thread.Sleep(2000);
                RunApplicationLoop();
            }

            Console.Write("Enter sequence of movements like FRLRF: ");
            var movements = Console.ReadLine()!;

            if(!ValidateMovementLength(movements))
            {
                Console.WriteLine($"Ensure that movements are valid. Should be more than 0 and less than {instructionLength} characters");
                RunApplicationLoop();
            }
            
            var robot = new Robot($"R{nrOfRobots}", new Coordinate(coordinates!), movements!, max_board_coordinates);
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
                Thread.Sleep(1000);
                PrintEmptyLines(2);
            }
        }
        else
        {
            RunApplicationLoop();
        }
    }


    // Helper methods

    private static void RunApplicationLoop()
    {
        Console.Clear();
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

        PrintFooterMessage();

    }

    private static bool ValidateUserInput(string? input)
    {
        return string.IsNullOrEmpty(input);
    }

    private static bool ValidateMovementLength(string movements)
    {
        if (ValidateUserInput(movements))
            return false;
        
        if (movements.Length > instructionLength)
            return false;

        return true;
    }

    private static bool ValidateCoordinateLength(string coordinates)
    {
        if (ValidateUserInput(coordinates))
            return false;

        var internalCoords = coordinates.Split(" ");

        if (internalCoords.Length < 2)
            return false;

        if (internalCoords[0].Length > max_coordinateSize || internalCoords[1].Length > max_coordinateSize)
            return false;

        return true;
    }

    private static void PrintNewCoordinates(Robot robot)
    {
        Console.WriteLine($"Robot name: {robot.Name}");
        Console.WriteLine($" >> Coordinate.X: {robot.Coordinate.X} : Coordinate.Y: {robot.Coordinate.Y}");
        Console.WriteLine($" >> Orientation: {robot.Coordinate.Orientation}");
    }

    private static void PrintFooterMessage()
    {
        Console.WriteLine();
        Console.WriteLine("***Robot walk completed***");
        Console.Read();
    }

    private static void PrintEmptyLines(int lines)
    {
        for (int i = 0; i < lines; i++)
        {
            Console.WriteLine();
        }
    }

}
