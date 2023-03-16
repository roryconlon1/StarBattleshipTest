namespace Battleship
{
    // Imagine a game of battleships.
    //   The player has to guess the location of the opponent's 'ships' on a 10x10 grid
    //   Ships are one unit wide and 2-4 units long, they may be placed vertically or horizontally
    //   The player asks if a given co-ordinate is a hit or a miss
    //   Once all cells representing a ship are hit - that ship is sunk.
    public class Game
    {
        // ships: each string represents a ship in the form first co-ordinate, last co-ordinate
        //   e.g. "3:2,3:5" is a 4 cell ship horizontally across the 4th row from the 3rd to the 6th column
        // guesses: each string represents the co-ordinate of a guess
        //   e.g. "7:0" - misses the ship above, "3:3" hits it.
        // returns: the number of ships sunk by the set of guesses

        public static int[,] gameBoard;

        public static void SetUpGame(String[] ships)
        {
            //            2d array to make a 10 by 10 game board of all value 0, whereby can change value depending on if
            //            it contains a ship, if the ship has been hit or miss etc. and if the ship is sunk or not
            gameBoard = new int[10, 10];
            int shipsCount = 0;
            for (int i = 0; i < ships.Length; i++)
            {
                //             for each ship in the array split by the comma to get the start and end points of the ship
                //            from this, split coordinate of start/end by colon and find x and y of both
                String[] individualShip = ships[i].Split(',');
                String[] startCoordinates = individualShip[0].Split(':');
                String[] endCoordinates = individualShip[1].Split(":");
                int startXposition = int.Parse(startCoordinates[0]);
                int endXposition = int.Parse(endCoordinates[0]);
                int startYpositon = int.Parse(startCoordinates[1]);
                int endYposition = int.Parse(endCoordinates[1]);
                if (startXposition > 9 || endXposition > 9 || startYpositon > 9 || endYposition > 9)
                {
                    throw new IndexOutOfRangeException("Ship Placement out of bounds");
                }
                //              for each new ship, give it a new id by incrementing the value of how many total ships there are
                //            determine if horizontal or vertical and assign the value of those positions to the same as the ships id
                int shipId = shipsCount + 1;
                if (startXposition != endXposition && startYpositon != endYposition)
                {
                    throw new ArgumentException("Invalid Ship Placement");
                }
                //               vertical
                if (startXposition == endXposition)
                {
                    for (int j = startYpositon; j <= endYposition; j++)
                    {
                        gameBoard[startXposition, j] = shipId;
                    }
                }
                else
                {
                    //              horizontal
                    for (int l = startXposition; l <= endXposition; l++)
                    {
                        gameBoard[l, startYpositon] = shipId;
                    }
                }
                shipsCount++;
            }
        }
        //        check if an individual guess is a hit or not by checking if it is 0
        //        or if it is greater and therefore a ship, returning bool value
        public static bool HitOrMiss(String guess)
        {
            String[] guessCoordinates = guess.Split(':');
            int x = int.Parse(guessCoordinates[0]);
            int y = int.Parse(guessCoordinates[1]);
            if (x > 9 || y > 9)
            {
                throw new IndexOutOfRangeException("Guessed Position out of bounds");
            }
            return gameBoard[x, y] > 0;
        }

        //        method to check if a ship is sunk, iterate over every element and if none match
        //        a certain shipId then that ship is sunk

        public static bool IsSunk(int shipId)
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    if (gameBoard[i, j] == shipId)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // iterate over the guesses in final method and check with HitOrMiss function to see if it is a boat or not
        // if it is a boat, denote with -1

        public static int Play(String[] ships, String[] guesses)
        {
            SetUpGame(ships);
            int boatsSunk = 0;
            for (int i = 0; i < guesses.Length; i++)
            {
                if (HitOrMiss(guesses[i]))
                {
                    String[] guessCoordinates = guesses[i].Split(':');
                    int x = int.Parse(guessCoordinates[0]);
                    int y = int.Parse(guessCoordinates[1]);
                    int idTracker = gameBoard[x, y];
                    gameBoard[x, y] = -1;
                    if (IsSunk(idTracker))
                    {
                        boatsSunk++;
                    }
                }
            }
            return boatsSunk;
        }
    }
}
