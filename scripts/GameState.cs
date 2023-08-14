using System.Collections.Generic;

public static class GameState
{
  public static int CountdownValue { get; set; } = 5;

  public static bool NinjaHit { get; set; }
  public static bool GameOver { get; set; }

  public static Dictionary<string, bool> BlobsHit { get; set; }
  public static Dictionary<string, bool> BatsHit { get; set; }

  public static void Reset()
  {
    NinjaHit = false;
    GameOver = false;

    BlobsHit = new();
    BatsHit = new();
  }
}
