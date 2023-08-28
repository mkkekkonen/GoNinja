using Godot;
using System.Collections.Generic;
using System.Text.Json;

public static class Utils
{
  public static void DropCharacterBody2D(CharacterBody2D characterBody)
  {
    characterBody.Velocity += new Vector2(0, -250);

    characterBody.SetCollisionLayerValue(1, false);
    characterBody.SetCollisionMaskValue(1, false);

    characterBody.SetCollisionLayerValue(3, true);
    characterBody.SetCollisionMaskValue(3, true);
  }

  public static List<HighScore> GetHighScores()
  {
    try
    {
      if (FileAccess.FileExists(Constants.SCORE_FILE_PATH))
      {
        var jsonText = FileAccess.GetFileAsString(Constants.SCORE_FILE_PATH);
        return JsonSerializer.Deserialize<List<HighScore>>(jsonText);
      }
    }
    catch { }

    return new();
  }
}
