namespace MontyHall;

public class Game
{
    public Door[] Doors { get; set; } = new Door[3];

    //create a new game, assign a random door to be the winner
    public Game()
    {
        var random = new Random();
        var winner = random.Next(0, 3);
        for (var i = 0; i < 3; i++)
        {
            Doors[i] = new Door
            {
                IsWinner = i == winner
            };
        }
    }

    public void SelectDoor(int i)
    {
        //only 1 door can be selected
        foreach (var door in Doors)
        {
            door.IsSelected = false;
        }
        Doors[i].IsSelected = true;
    }

    public void OpenDoor(int i)
    {
        Doors[i].IsOpen = true;
    }

    //open a door that is not the winner and not selected
    public int OpenNonWinningDoor()
    {
        var random = new Random();
        var i = random.Next(0, 3);
        while (Doors[i].IsWinner || Doors[i].IsSelected)
        {
            i = random.Next(0, 3);
        }
        Doors[i].IsOpen = true;
        return i;
    }
}