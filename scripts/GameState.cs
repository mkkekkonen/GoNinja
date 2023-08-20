using System.Collections.Generic;

public static class GameState
{
  private static readonly int SCORE_MULTIPLIER = 100;

  public static int CountdownValue { get; set; } = 5;
  public static int Score { get; set; }

  public static int TotalScore
  {
    get
    {
      return (Score + (int)GameTime) * SCORE_MULTIPLIER;
    }
  }

  public static double GameTime { get; set; }

  public static bool NinjaHit { get; set; }
  public static bool GameOver { get; set; }
  public static bool Won { get; set; }

  public static Dictionary<string, bool> BlobsHit { get; set; }
  public static Dictionary<string, bool> BatsHit { get; set; }

  public static List<HighScore> HighScores { get; set; }

  public static void Reset()
  {
    Score = 0;
    GameTime = 0;

    NinjaHit = false;
    GameOver = false;
    Won = false;

    BlobsHit = new();
    BatsHit = new();
  }
}
