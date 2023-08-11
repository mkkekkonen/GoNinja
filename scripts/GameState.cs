using System.Collections.Generic;

public static class GameState
{
  public static int CountdownValue { get; set; } = 5;

  public static bool NinjaHit { get; set; } = false;
  public static bool GameOver { get; set; } = false;

  public static Dictionary<ulong, bool> BlobsHit { get; set; } = new();
  public static Dictionary<ulong, bool> BatsHit { get; set; } = new();
}
