namespace marsRobots
{
    public class Coordinate
    {
        private string[] validDirections = {"N", "S", "W","E"};

        public int X { get; set; }
        public int Y { get; set; }
        public string Orientation { get; set; }
        public Coordinate(int x, int y, string orientation = "")
        {
            if (x < 0 || y < 0)
                throw new ArgumentException("Enter valid x and y coordinates");

            X = x;
            Y = y;
            Orientation = orientation;
        }

        public Coordinate(string coordinates)
        {
            // 4 3 E
            if (string.IsNullOrEmpty(coordinates)) 
                throw new ArgumentException("Please enter a valid coordinate");

            var coords = coordinates.Split(' ');
            if (coords.Length == 2)
                Orientation = "";

            if (coords.Length == 3)
            {
                if (!validDirections.Any(x => x == coords[2]))
                    throw new ArgumentException("Please enter a valid orientation (N, S, W, E)");
             
                Orientation = coords[2].ToUpper();
            }


            X = int.Parse(coords[0]);
            Y = int.Parse(coords[1]);
        }

        public bool ValidateCoordinate(Coordinate maxCoordinate)
        {
            if( X > maxCoordinate.X || Y > maxCoordinate.Y)
                return false;

            return true;
        }
    }
}
