using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class GameWorldRenderer
{
  private readonly Vector2I PILLAR_TOP_LEFT = new(2, 0);
  private readonly Vector2I PILLAR_TOP = new(3, 0);
  private readonly Vector2I PILLAR_TOP_RIGHT = new(4, 0);
  private readonly Vector2I PILLAR_LEFT = new(2, 1);
  private readonly Vector2I PILLAR_CENTER = new(3, 1);
  private readonly Vector2I PILLAR_RIGHT = new(4, 1);

  private readonly Vector2I PLATFORM_LEFT = new(2, 3);
  private readonly Vector2I PLATFORM_MIDDLE = new(3, 3);
  private readonly Vector2I PLATFORM_RIGHT = new(4, 3);

  private readonly Vector2I BG_TILE = new(0, 0);
  private readonly Vector2I GEMS_TILE = new(1, 0);
  private readonly Vector2I LAVA_TILE = new(1, 5);

  private int renderStartX;
  private int renderStartY;
  private int renderEndX;
  private int renderEndY;

  private static GameWorldRenderer instance;
  private Random random;

  private GameWorldRenderer()
  {
    Reset();
  }

  public static GameWorldRenderer Instance
  {
    get
    {
      instance ??= new GameWorldRenderer();
      return instance;
    }
  }

  public void Seed()
  {
    random = new Random(Guid.NewGuid().GetHashCode());
  }

  public void Reset()
  {
    renderStartX = -Constants.WINDOW_WIDTH_TILES;
    renderStartY = GameWorld.Instance.GetHighestPlatformYCoordinate() - Constants.WINDOW_HEIGHT_TILES;
    renderEndX = GameWorld.Instance.GetFarthestPlatformEndXCoordinate() + Constants.WINDOW_WIDTH_TILES;
    renderEndY = Constants.WORLD_BOTTOM_TILE_Y_COORDINATE;
  }

  public void RenderPlatforms(TileMap tileMap)
  {
    foreach (var platform in GameWorld.Instance.Platforms)
    {
      foreach (var coordinates in platform.Coordinates)
      {
        tileMap.SetCell(
          Constants.FG_TILE_LAYER_INDEX,
          coordinates,
          0,
          GetTileType(platform, coordinates),
          0
        );
      }

      if (platform.Label == Constants.PILLAR_LABEL)
      {
        RenderPillarBase(platform, tileMap);
      }

      platform.Rendered = true;
    }
  }

  public void RenderBackground(TileMap tileMap)
  {
    if (renderEndX == renderStartX)
    {
      renderEndX = GameWorld.Instance.GetFarthestPlatformEndXCoordinate() + Constants.WINDOW_WIDTH_TILES;
    }

    for (var x = renderStartX; x < renderEndX; x++)
    {
      for (var y = renderStartY; y < renderEndY; y++)
      {
        var tileAtlasCoords = random.Next(0, 75) == 1
          ? GEMS_TILE
          : BG_TILE;

        tileMap.SetCell(
          Constants.BG_TILE_LAYER_INDEX,
          new Vector2I(x, y),
          0,
          tileAtlasCoords,
          0
        );
      }
    }

    // starting place for next render
    renderStartX = renderEndX;
  }

  public void SpawnEnemies(Node parent, TileMap map, List<IEnemy> enemyList)
  {
    var notYetSpawned = GameWorld.Instance.EnemyLocations.Where(loc => !loc.Spawned);

    foreach (var enemyLocation in notYetSpawned)
    {
      var enemyScene = GetEnemyScene(enemyLocation.EnemyType);

      var enemy = enemyScene.Instantiate<Node2D>();
      enemyList.Add((IEnemy)enemy);

      enemy.GlobalPosition = map.MapToLocal(enemyLocation.Location) * Constants.SCALE;
      parent.AddChild(enemy);

      enemyLocation.Spawned = true;
    }
  }

  public void CreateTreasure(Node parent, TileMap map)
  {
    var treasureScene = GD.Load<PackedScene>("res://scenes/treasure.tscn");
    var treasure = treasureScene.Instantiate<Treasure>();
    treasure.GlobalPosition = map.MapToLocal(GameWorld.Instance.TreasureLocation) * Constants.SCALE;

    GameWorld.Instance.Treasure = treasure;
    parent.AddChild(treasure);
  }

  private Vector2I GetTileType(AbstractPlatform platform, Vector2I coordinates)
  {
    var widthVector = new Vector2I(platform.Width - 1, 0);

    if (platform.Label == Constants.PILLAR_LABEL)
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

    if (platform.Label == Constants.PLATFORM_LABEL)
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

  private void RenderPillarBase(AbstractPlatform pillar, TileMap tileMap)
  {
    var startX = pillar.TopLeft.X;
    var startY = pillar.TopLeft.Y + 1;
    var endX = pillar.Coordinates.Last().X;
    var endY = Constants.WORLD_BOTTOM_TILE_Y_COORDINATE;

    for (var x = startX; x <= endX; x++)
    {
      for (var y = startY; y <= endY; y++)
      {
        Vector2I atlasCoords = PILLAR_CENTER;

        if (x == startX)
        {
          atlasCoords = PILLAR_LEFT;
        }
        else if (x == endX)
        {
          atlasCoords = PILLAR_RIGHT;
        }

        tileMap.SetCell(
          Constants.FG_TILE_LAYER_INDEX,
          new Vector2I(x, y),
          0,
          atlasCoords,
          0
        );
      }
    }
  }

  private PackedScene GetEnemyScene(string enemyType)
  {
    return enemyType switch
    {
      "bat" => Constants.BAT_SCENE,
      _ => Constants.RED_BLOB_SCENE,
    };
  }
}
