using System.Collections.Generic;
using Godot;

public abstract class AbstractPlatform
{
  protected static string label;
  protected List<Vector2I> coordinates;

  public Vector2I TopLeft { get; set; }
  public int Width { get; set; }
  public static string Label
  {
    get
    {
      return label;
    }
  }

  public List<Vector2I> Coordinates
  {
    get
    {
      if (coordinates == null)
      {
        var list = new List<Vector2I>();

        for (var i = 0; i < Width; i++)
        {
          list.Add(new Vector2I(
            TopLeft.X + i,
            TopLeft.Y
          ));
        }

        coordinates = list;
      }

      return coordinates;
    }
  }
}
