namespace Battleship.Test
{
    public class ExampleTest
    {
        [Fact]
        public void checkShips()
        {
            var ships = new[] { "0:0,0:2", "2:2,2:3", "4:4,6:4", "6:1,9:1" };
            Battleship.Game.SetUpGame(ships);
            Assert.Equal(1, Battleship.Game.gameBoard[0, 0]);
            Assert.Equal(1, Battleship.Game.gameBoard[0, 1]);
            Assert.Equal(1, Battleship.Game.gameBoard[0, 2]);
            Assert.Equal(2, Battleship.Game.gameBoard[2, 2]);
            Assert.Equal(2, Battleship.Game.gameBoard[2, 3]);
            Assert.Equal(3, Battleship.Game.gameBoard[4, 4]);
            Assert.Equal(3, Battleship.Game.gameBoard[5, 4]);
            Assert.Equal(3, Battleship.Game.gameBoard[6, 4]);
            Assert.Equal(4, Battleship.Game.gameBoard[9, 1]);
            Assert.Equal(4, Battleship.Game.gameBoard[8, 1]);
            Assert.Equal(4, Battleship.Game.gameBoard[7, 1]);
            Assert.Equal(4, Battleship.Game.gameBoard[6, 1]);
        }

        [Fact]
        public void checkWater()
        {
            var ships = new[] { "0:0,0:2", "2:2,2:3", "4:4,6:4", "6:1,9:1" };
            Battleship.Game.SetUpGame(ships);
            Assert.Equal(0, Battleship.Game.gameBoard[1, 1]);
            Assert.Equal(0, Battleship.Game.gameBoard[3, 7]);
            Assert.Equal(0, Battleship.Game.gameBoard[9, 9]);
        }

        [Fact]
        public void canGetHit()
        {
            var ships = new[] { "0:0,0:2", "2:2,2:3", "4:4,6:4", "6:1,9:1" };
            Battleship.Game.SetUpGame(ships);
            Assert.True(Battleship.Game.HitOrMiss("6:4"));
            Assert.True(Battleship.Game.HitOrMiss("9:1"));
            Assert.True(Battleship.Game.HitOrMiss("0:0"));
        }

        [Fact]
        public void canReturnNotHit()
        {
            var ships = new[] { "0:0,0:2", "2:2,2:3", "4:4,6:4", "6:1,9:1" };
            Battleship.Game.SetUpGame(ships);
            Assert.False(Battleship.Game.HitOrMiss("3:7"));
            Assert.False(Battleship.Game.HitOrMiss("1:1"));
            Assert.False(Battleship.Game.HitOrMiss("9:9"));
        }

        [Fact]
        public void canBeSunk()
        {
            var ships = new[] { "0:0,0:2", "2:2,2:3", "4:4,6:4", "6:1,9:1" };
            Battleship.Game.SetUpGame(ships);
            Assert.True(Battleship.Game.IsSunk(5));
            Assert.False(Battleship.Game.IsSunk(2));
        }

        [Fact]
        public void fullGameTest()
        {
            var ships = new[] { "0:0,0:2", "2:2,2:3", "4:4,6:4", "6:1,9:1" };
            var guesses1 = new[] { "1:1", "2:2", "1:4", "2:3", "8:9", "4:4", "5:4", "6:4" };
            var guesses2 = new[] { "1:1", "1:2", "1:4", "2:3", "8:9", "4:4", "5:4", "6:4" };
            Battleship.Game.SetUpGame(ships);
            Battleship.Game.Play(ships, guesses1).Should().Be(2);
            Battleship.Game.Play(ships, guesses2).Should().Be(1);
        }
    }
}