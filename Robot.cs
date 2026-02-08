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
    public string Movements { get; }


    public Robot(string name, Coordinate coordinate, string movements)
    {
        Name = name;
        Coordinate = coordinate;

        if (string.IsNullOrEmpty(movements))
            throw new ArgumentNullException($"{nameof(movements)} is required");

        Movements = movements.ToUpper();

        InternalCoordinate = new(Coordinate.X, Coordinate.Y, Coordinate.Orientation);
    }

    public void Move()
    {
        //Console.WriteLine("Name: " + Name);
        //Console.WriteLine("Coordinate Orientation: " + Coordinate.Orientation);
        //Console.WriteLine("Coordinates: X - " + Coordinate.X + " : Y - " + Coordinate.Y);
        //Console.WriteLine("Movements: " + Movements);

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
            if (movement == "F")
            {
                InternalCoordinate.X--;
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
    }

    private void MovementIfOrientationEast(string movement)
    {
        if (movement == "F")
        {
            InternalCoordinate.X++;
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
