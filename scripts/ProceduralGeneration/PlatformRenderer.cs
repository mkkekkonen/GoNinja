using Godot;

public class PlatformRenderer
{
  private readonly Vector2I PILLAR_TOP_LEFT = new(2, 0);
  private readonly Vector2I PILLAR_TOP = new(3, 0);
  private readonly Vector2I PILLAR_TOP_RIGHT = new(4, 0);
  private readonly Vector2I PILLAR_LEFT = new(2, 1);
  private readonly Vector2I PILLAR_CENTER = new(2, 1);
  private readonly Vector2I PILLAR_RIGHT = new(3, 1);

  private readonly Vector2I PLATFORM_LEFT = new(2, 3);
  private readonly Vector2I PLATFORM_MIDDLE = new(3, 3);
  private readonly Vector2I PLATFORM_RIGHT = new(4, 3);

  private static PlatformRenderer instance;

  private PlatformRenderer() { }

  public static PlatformRenderer Instance
  {
    get
    {
      instance ??= new PlatformRenderer();
      return instance;
    }
  }

  public void RenderPlatforms(TileMap tileMap)
  {
    foreach (var platform in World.Instance.Platforms)
    {
      foreach (var coordinates in platform.Coordinates)
      {
        tileMap.SetCell(
          1,
          coordinates,
          0,
          GetTileType(platform, coordinates),
          0
        );
      }
    }
  }

  private Vector2I GetTileType(AbstractPlatform platform, Vector2I coordinates)
  {
    var widthVector = new Vector2I(platform.Width - 1, 0);

    if (platform.Label == "pillar")
    {
      if (coordinates == platform.TopLeft)
      {
        return PILLAR_TOP_LEFT;
      }
      if (coordinates == (platform.TopLeft + widthVector))
      {
        return PILLAR_TOP_RIGHT;
      }
      return PILLAR_TOP;
    }

    if (platform.Label == "platform")
    {
      if (coordinates == platform.TopLeft)
      {
        return PLATFORM_LEFT;
      }
      if (coordinates == (platform.TopLeft + widthVector))
      {
        return PLATFORM_RIGHT;
      }
      return PLATFORM_MIDDLE;
    }

    return PILLAR_CENTER;
  }
}
