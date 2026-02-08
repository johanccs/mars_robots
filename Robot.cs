using marsRobots;
using System;
using System.Linq;

namespace mars_robots;

public class Robot
{
    private List<string> validMoves = new(){ "L", "R", "F" };

    // InternalCoordinate serves as a running coordinate that is updated
    // as the robot steps are taken. This will also be returned to main program
    public Coordinate InternalCoordinate { get; }
    public string Name { get; }
    public Coordinate Coordinate { get; }
    public Coordinate MaxBoardSize { get; }
    public Coordinate RobotScent { get; set; } = new(0, 0);

    public string Movements { get; }


    public Robot(string name, Coordinate coordinate, string movements, Coordinate maxBoardSize)
    {
        Name = name;
        Coordinate = coordinate;
        MaxBoardSize = maxBoardSize;

        if (string.IsNullOrEmpty(movements))
            throw new ArgumentNullException($"{nameof(movements)} is required");

        Movements = movements.ToUpper();

        InternalCoordinate = new(Coordinate.X, Coordinate.Y, Coordinate.Orientation);
    }

    public void Move()
    {
        Console.WriteLine("Current coordinates");
        Console.WriteLine($"Name: { Name } >> Orientation: { Coordinate.Orientation } >> Coordinates.X: { Coordinate.X } Y : {Coordinate.Y}");
     
        if (!ValidateMovements(Movements))
            throw new Exception("Some invalid movements added");

        for(int i = 0; i <= Movements.Length - 1; i++)
        {
            // Start walking algorithm Movements FR
            TakeStep(Movements[i].ToString());
        }
    }

    private void TakeStep(string movement)
    {
        // movement F
        if (Coordinate.Orientation == "E")
        {
            MovementIfOrientationEast(movement);
        }
        else if (Coordinate.Orientation == "W")
        {
            MovementIfOrientationWest(movement);
        }

        else if(Coordinate.Orientation == "N")
        {
            MovementIfOrientationNorth(movement);
        }
        else
        {
            MovementIfOrientationSouth(movement);
        }
    }

    private void MovementIfOrientationSouth(string movement)
    {
        if (movement == "F")
        {
            InternalCoordinate.Y--;
            
            if(InternalCoordinate.Y < 0)
            {
                RobotScent.Y = 0;
                RobotScent.X = InternalCoordinate.X;
                RobotScent.Orientation = "S";

                PrintRoboScentMessage();
            }
        }
        else if (movement == "L")
        {
            InternalCoordinate.Orientation = "W";
        }
        else if (movement == "R")
        {
            InternalCoordinate.Orientation = "E";
        }
    }

    private void MovementIfOrientationNorth(string movement)
    {
        if (movement == "F")
        {
            InternalCoordinate.Y++;

            if (InternalCoordinate.Y > MaxBoardSize.Y)
            {
                RobotScent.Y = InternalCoordinate.Y;
                RobotScent.X = InternalCoordinate.X;
                RobotScent.Orientation = "N";

                PrintRoboScentMessage();
            }
        }
        else if (movement == "L")
        {
            InternalCoordinate.Orientation = "W";
        }
        else if (movement == "R")
        {
            InternalCoordinate.Orientation = "E";
        }
    }

    private void MovementIfOrientationWest(string movement)
    {
        if (movement == "F")
        {
            InternalCoordinate.X--;
            if (InternalCoordinate.X < 0)
            {
                RobotScent.Y = InternalCoordinate.Y;
                RobotScent.X = InternalCoordinate.X;
                RobotScent.Orientation = "W";

                PrintRoboScentMessage();
            }
        }
        else if (movement == "L")
        {
            InternalCoordinate.Orientation = "S";
        }
        else if (movement == "R")
        {
            InternalCoordinate.Orientation = "N";
        }
    }

    private void MovementIfOrientationEast(string movement)
    {
        if (movement == "F")
        {
            InternalCoordinate.X++;
            if (InternalCoordinate.X > MaxBoardSize.X)
            {
                RobotScent.Y = InternalCoordinate.Y;
                RobotScent.X = InternalCoordinate.X;
                RobotScent.Orientation = "E";

                PrintRoboScentMessage();
            }
        }
        else if (movement == "L")
        {
            InternalCoordinate.Orientation = "N";
        }
        else if (movement == "R")
        {
            InternalCoordinate.Orientation = "S";
        }
    }
    
    private void PrintRoboScentMessage()
    {
        Console.WriteLine($"Robot {Name} just fell off the edge at: {RobotScent}");
    }

    private bool ValidateMovements(string movements)
    {
        for (int i = 0; i <= movements.Length - 1; i++)
        {
            string movement = movements[i].ToString();
            if (!validMoves.Any(x => x == movement))
                return false;
        }

        return true;
    }
}
