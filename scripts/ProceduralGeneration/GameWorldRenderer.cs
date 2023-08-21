using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class GameWorldRenderer
{
  private readonly int WORLD_BOTTOM_Y = 18;
  private readonly int BG_LAYER_INDEX = 0;
  private readonly int FG_LAYER_INDEX = 1;
  private readonly int LAVA_LAYER_INDEX = 2;
  private readonly int WINDOW_WIDTH_TILES = 24;
  private readonly int WINDOW_HEIGHT_TILES = 14;
  private readonly int SCALE = 6;

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

  private readonly int renderStartX;
  private readonly int renderStartY;
  private readonly int renderEndX;
  private readonly int renderEndY;

  private readonly PackedScene blobScene = GD.Load<PackedScene>("res://scenes/redBlob.tscn");
  private readonly PackedScene batScene = GD.Load<PackedScene>("res://scenes/bat.tscn");

  private static GameWorldRenderer instance;
  private Random random;

  private GameWorldRenderer()
  {
    renderStartX = -WINDOW_WIDTH_TILES;
    renderStartY = GameWorld.Instance.GetHighestPlatformYCoordinate() - WINDOW_HEIGHT_TILES;
    renderEndX = GameWorld.Instance.GetFarthestPlatformEndXCoordinate() + WINDOW_WIDTH_TILES;
    renderEndY = WORLD_BOTTOM_Y;
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

  public void RenderPlatforms(TileMap tileMap)
  {
    foreach (var platform in GameWorld.Instance.Platforms)
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

      if (platform.Label == "pillar")
      {
        RenderPillarBase(platform, tileMap);
      }
    }
  }

  public void RenderBackground(TileMap tileMap)
  {

    for (var x = renderStartX; x < renderEndX; x++)
    {
      for (var y = renderStartY; y < renderEndY; y++)
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

  public void SpawnEnemies(Node parent, TileMap map, List<IEnemy> enemyList)
  {
    foreach ((var coords, var enemyType) in GameWorld.Instance.EnemyLocations)
    {
      var enemyScene = GetEnemyScene(enemyType);

      var enemy = enemyScene.Instantiate<Node2D>();
      enemyList.Add((IEnemy)enemy);

      enemy.GlobalPosition = map.MapToLocal(coords) * SCALE;
      parent.AddChild(enemy);
    }
  }

  public void CreateTreasure(Node parent, TileMap map)
  {
    var treasureScene = GD.Load<PackedScene>("res://scenes/treasure.tscn");
    var treasure = treasureScene.Instantiate<Treasure>();
    treasure.GlobalPosition = map.MapToLocal(GameWorld.Instance.TreasureLocation) * SCALE;

    GameWorld.Instance.Treasure = treasure;
    parent.AddChild(treasure);
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

  private void RenderPillarBase(AbstractPlatform pillar, TileMap tileMap)
  {
    var startX = pillar.TopLeft.X;
    var startY = pillar.TopLeft.Y + 1;
    var endX = pillar.Coordinates.Last().X;
    var endY = WORLD_BOTTOM_Y;

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
          FG_LAYER_INDEX,
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
      "bat" => batScene,
      _ => blobScene,
    };
  }
}
