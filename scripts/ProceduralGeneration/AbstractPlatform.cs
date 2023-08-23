using System.Collections.Generic;
using Godot;

public abstract class AbstractPlatform
{
  protected string label;
  protected List<Vector2I> coordinates;

  public int Width { get; set; }

  public bool Rendered { get; set; } = false;

  public Vector2I TopLeft { get; set; }

  public string Label
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
