using System;
using Godot;

public class PlatformRenderer
{
  private readonly int WORLD_BOTTOM_Y = 18;
  private readonly int BG_LAYER_INDEX = 0;
  private readonly int FG_LAYER_INDEX = 1;
  private readonly int WORLD_WIDTH = 24;
  private readonly int WORLD_HEIGHT = 14;

  private readonly Vector2I PILLAR_TOP_LEFT = new(2, 0);
  private readonly Vector2I PILLAR_TOP = new(3, 0);
  private readonly Vector2I PILLAR_TOP_RIGHT = new(4, 0);
  private readonly Vector2I PILLAR_LEFT = new(2, 1);
  private readonly Vector2I PILLAR_CENTER = new(2, 1);
  private readonly Vector2I PILLAR_RIGHT = new(3, 1);

  private readonly Vector2I PLATFORM_LEFT = new(2, 3);
  private readonly Vector2I PLATFORM_MIDDLE = new(3, 3);
  private readonly Vector2I PLATFORM_RIGHT = new(4, 3);

  private readonly Vector2I BG_TILE = new(0, 0);
  private readonly Vector2I GEMS_TILE = new(1, 0);

  private static PlatformRenderer instance;
  private Random random = new();

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
          FG_LAYER_INDEX,
          coordinates,
          0,
          GetTileType(platform, coordinates),
          0
        );
      }
    }
  }

  public void RenderBackground(TileMap tileMap)
  {
    var worldWidthHalved = WORLD_WIDTH / 2;
    var worldHeightHalved = WORLD_HEIGHT / 2;

    var startX = 0 - worldWidthHalved;
    var startY = World.Instance.GetHighestPlatformYCoordinate() - worldHeightHalved;
    var endX = World.Instance.GetFarthestPlatformEndXCoordinate() + worldWidthHalved;
    var endY = WORLD_BOTTOM_Y + worldHeightHalved;

    for (var x = startX; x < endX; x++)
    {
      for (var y = startY; y < endY; y++)
      {
        var tileAtlasCoords = random.Next(0, 75) == 1
          ? GEMS_TILE
          : BG_TILE;

        tileMap.SetCell(
          BG_LAYER_INDEX,
          new Vector2I(x, y),
          0,
          tileAtlasCoords,
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
