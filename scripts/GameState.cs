using System.Collections.Generic;

public static class GameState
{
  public static bool NinjaHit { get; set; } = false;

  public static Dictionary<ulong, bool> BlobsHit { get; set; } = new();
}