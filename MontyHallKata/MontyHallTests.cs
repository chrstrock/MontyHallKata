using MontyHall;
using NuGet.Frameworks;

namespace MontyHallKata;

/**
 * This test class is used to simulate 1000 games of the Monty Hall problem,
 * proving that switching doors is the best strategy.
 */
public class MontyHallTests
{
    private Game game;
    [SetUp]
    public void Setup()
    {
        game = new Game();
    }

    [Test]
    public void GameCreates3DoorsTest()
    {
        // Arrange

        // Act
        var doors = game.Doors;

        // Assert
        Assert.That(doors, Has.Length.EqualTo(3));
    }
    
    [Test]
    public void GameCreates3DoorsWithOneWinnerTest()
    {
        // Arrange

        // Act
        var doors = game.Doors;

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(doors.Length, Is.EqualTo(3));
            Assert.That(doors.Count(d => d.IsWinner), Is.EqualTo(1));
            Assert.That(doors.Count(d => d.IsSelected), Is.EqualTo(0));
            Assert.That(doors.Count(d => d.IsOpen), Is.EqualTo(0));
        });
    }
    
    [Test]
    public void GameSelectsDoorTest()
    {

        // Act
        game.SelectDoor(0);

        // Assert
        Assert.That(game.Doors[0].IsSelected, Is.True);
    }
    
    [Test]
    public void GameOpensDoorTest()
    {
        // Arrange

        // Act
        game.OpenDoor(0);

        // Assert
        Assert.That(game.Doors[0].IsOpen, Is.True);
    }

    [Test]
    public void OpenDoorNotAWinnerTest()
    {
        //Arrange
        
        //Act
        //select a door
        game.SelectDoor(0);
        game.OpenNonWinningDoor();
        Assert.Multiple(() =>
        {

            //Assert
            Assert.That(game.Doors.Count(d => d.IsOpen), Is.EqualTo(1));
            Assert.That(game.Doors.Count(d => d.IsWinner), Is.EqualTo(1));
        });
    }
    
    [Test]
    public void GameSwitchesDoorTest()
    {
        // Arrange
        
        
        game.SelectDoor(0);

        // Act
        game.SelectDoor(1);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(game.Doors[0].IsSelected, Is.False);
            Assert.That(game.Doors[1].IsSelected, Is.True);
        });
    }
    
    [Test]
    //run 1000 games, switch doors each time
    public void SwitchDoorsTest()
    {
        // Arrange
        var wins = 0;
        for (var i = 0; i < 1000; i++)
        {
            var testGame = new Game();
            //randomly select a door
            var selectedDoor = new Random().Next(0, 3);
            testGame.SelectDoor(selectedDoor);
            //open a door that is not the winner and not selected
            var openedDoor = testGame.OpenNonWinningDoor();
            //get selected door
            //get number between 0 and 2 that's not the selected or opened index
            var switchIndex = Enumerable.Range(0, 3)
                .First(index => index != selectedDoor && index != openedDoor);
            
            
            
            //switch to the other door
            testGame.SelectDoor(switchIndex);

            //if the selected door is the winner, increment the win count
            if (testGame.Doors[switchIndex].IsWinner)
            {
                wins++;
            }
        }

        // Assert that switching doors wins 2/3 of the time (+/- 50)
        Assert.That(wins, Is.EqualTo(666).Within(50));
        
    }
    
    [Test]
    //run 1000 games, don't switch doors each time
    public void DontSwitchDoorsTest()
    {
        // Arrange
        var wins = 0;
        for (var i = 0; i < 1000; i++)
        {
            var testGame = new Game();
            //randomly select a door
            var selectedDoor = new Random().Next(0, 3);
            testGame.SelectDoor(selectedDoor);
            //open a door that is not the winner and not selected
            testGame.OpenNonWinningDoor();
            //if the selected door is the winner, increment the win count
            if (testGame.Doors[selectedDoor].IsWinner)
            {
                wins++;
            }
        }

        // Assert that not switching doors wins 1/3 of the time (+/- 50)
        Assert.That(wins, Is.EqualTo(333).Within(50));
    }
}