namespace MuseumService.Models;

public class Game
{
    public int GameId { get; set; }
    public string GameName { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public string GameType { get; set; }
    
    public ICollection<Score> Scores { get; set; }
}





