using marsRobots;

namespace mars_robots;

public class Robot
{
    private List<string> validMoves = new(){ "L", "R", "F" };

    // InternalCoordinate serves as a running coordinate that is updated
    // as the robot steps are taken. This will also be returned to main program
    //public Coordinate InternalCoordinate { get; }
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
            throw new ArgumentNullException("Movement is required");

        Movements = movements.ToUpper();
    }

    public void Move()
    {
        Console.WriteLine("Current coordinates");
        Console.WriteLine($"Name: { Name } >> Orientation: { Coordinate.Orientation } >> Coordinates.X: { Coordinate.X } Y : {Coordinate.Y}");
     
        if (!ValidateMovements(Movements))
            throw new Exception("Argument contains invalid movements");

        for(int i = 0; i <= Movements.Length - 1; i++)
        {
            // Start walking algorithm Movements FR
            if (!TakeStep(Movements[i].ToString()))
                return;
        }
    }

    private bool TakeStep(string movement)
    {
        // movement F
        if (Coordinate.Orientation == Orientation.East)
        {
            var result = MovementIfOrientationEast(movement);

            return result;
        }
        else if (Coordinate.Orientation == Orientation.West)
        {
            bool result = MovementIfOrientationWest(movement);

            return result;
        }

        else if(Coordinate.Orientation == Orientation.North)
        {
            bool result = MovementIfOrientationNorth(movement);
            
            return result;
        }
        else
        {
            var result = MovementIfOrientationSouth(movement);

            return result;
        }
    }

    private bool MovementIfOrientationSouth(string movement)
    {
        if (movement == ValidMoves.Forward)
        {
            Coordinate.Y--;
            
            if(Coordinate.Y < 0)
            {
                RobotScent.Y = 0;
                RobotScent.X = Coordinate.X;
                RobotScent.Orientation = Orientation.South;

                PrintRoboScentMessage();

                // Should not continue to monitor movement
                return false;
            }
        }
        else if (movement == ValidMoves.Left)
        {
            Coordinate.Orientation = Orientation.West;
        }
        else if (movement == ValidMoves.Right)
        {
            Coordinate.Orientation = Orientation.East;
        }

        return true;
    }

    private bool MovementIfOrientationNorth(string movement)
    {
        if (movement == ValidMoves.Forward)
        {
            Coordinate.Y++;

            if (Coordinate.Y > MaxBoardSize.Y)
            {
                RobotScent.Y = Coordinate.Y;
                RobotScent.X = Coordinate.X;
                RobotScent.Orientation = Orientation.North;

                PrintRoboScentMessage();

                // Should not continue to monitor movement
                return false;
            }
        }
        else if (movement == ValidMoves.Left)
        {
            Coordinate.Orientation = Orientation.West;
        }
        else if (movement == ValidMoves.Right)
        {
            Coordinate.Orientation = Orientation.East;
        }

        return true;
    }

    private bool MovementIfOrientationWest(string movement)
    {
        if (movement == ValidMoves.Right)
        {
            Coordinate.X--;
            if (Coordinate.X < 0)
            {
                RobotScent.Y = Coordinate.Y;
                RobotScent.X = Coordinate.X;
                RobotScent.Orientation = Orientation.West;

                PrintRoboScentMessage();

                // Should not continue to monitor movement
                return false;
            }
        }
        else if (movement == ValidMoves.Left)
        {
            Coordinate.Orientation = Orientation.South;
        }
        else if (movement == ValidMoves.Right)
        {
            Coordinate.Orientation = Orientation.North;
        }

        return true;
    }

    private bool MovementIfOrientationEast(string movement)
    {
        if (movement == ValidMoves.Forward)
        {
            Coordinate.X++;
            if (Coordinate.X > MaxBoardSize.X)
            {
                RobotScent.Y = Coordinate.Y;
                RobotScent.X = Coordinate.X;
                RobotScent.Orientation = Orientation.East;

                PrintRoboScentMessage();

                // Should not continue to monitor movement
                return false;
            }
        }
        else if (movement == ValidMoves.Left)
        {
            Coordinate.Orientation = Orientation.North;
        }
        else if (movement == ValidMoves.Right)
        {
            Coordinate.Orientation = Orientation.South;
        }

        return true;
    }
    
    private void PrintRoboScentMessage()
    {
        Console.WriteLine("LOST");
        Console.WriteLine($"Robot scent coordinates X: { RobotScent.X } Y: { RobotScent.Y }");
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
