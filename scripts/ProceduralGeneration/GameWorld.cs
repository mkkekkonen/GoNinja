using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class GameWorld
{
  private static GameWorld instance;

  public static GameWorld Instance
  {
    get
    {
      instance ??= new GameWorld();
      return instance;
    }
  }

  // private readonly int N_PLATFORMS = 30;
  private readonly int N_PLATFORMS = 7;
  private readonly int MIN_PLATFORM_WIDTH = 3;
  private readonly int MAX_PLATFORM_WIDTH = 8;
  private readonly int MAX_PLATFORM_Y_COORDINATE = 16;

  private readonly string[] platformLabels = new string[] { "platform", "pillar" };

  private Vector2I newPlatformTopLeft;
  private Vector2I treasureLocation;

  private List<AbstractPlatform> platforms;
  private List<(Vector2I, string)> enemyLocations;

  private Random random;

  private GameWorld() { }

  public List<AbstractPlatform> Platforms
  {
    get
    {
      return platforms;
    }
  }

  public List<(Vector2I, string)> EnemyLocations
  {
    get
    {
      return enemyLocations;
    }
  }

  public Vector2I TreasureLocation
  {
    get
    {
      return treasureLocation;
    }
  }

  public Treasure Treasure { get; set; }

  public void GeneratePlatforms()
  {
    random = new Random(Guid.NewGuid().GetHashCode());
    platforms = new();
    enemyLocations = new();
    newPlatformTopLeft = new(25, 11);

    for (var i = 0; i < N_PLATFORMS; i++)
    {
      var platform = GeneratePlatform(i);
      platforms.Add(platform);
      IncrementNewPlatformTopLeft(platform);
    }
  }

  public int GetHighestPlatformYCoordinate()
  {
    var yCoordinate = platforms.First().TopLeft.Y;

    foreach (var platform in platforms)
    {
      yCoordinate = Math.Min(yCoordinate, platform.TopLeft.Y);
    }

    return yCoordinate;
  }

  public int GetFarthestPlatformEndXCoordinate()
  {
    return platforms.Last().Coordinates.Last().X;
  }

  private int GetNewPlatformWidth()
  {
    return random.Next(MIN_PLATFORM_WIDTH, MAX_PLATFORM_WIDTH + 1);
  }

  private AbstractPlatform GeneratePlatform(int index)
  {
    var typeIndex = random.Next(platformLabels.Length);
    var platformType = platformLabels[typeIndex];
    AbstractPlatform platform = null;

    switch (platformType)
    {
      case "platform":
        platform = new Platform();
        break;
      case "pillar":
        platform = new Pillar();
        break;
      default:
        break;
    }

    platform.Width = GetNewPlatformWidth();
    platform.TopLeft = newPlatformTopLeft;

    if (index < N_PLATFORMS - 1)
    {
      TryCreateNewEnemyStartPosition(platform);
    }
    else
    {
      treasureLocation = GetRandomCoordinatesAbovePlatform(platform);
    }

    return platform;
  }

  private void IncrementNewPlatformTopLeft(AbstractPlatform platform)
  {
    var gap = random.Next(2, 4);
    var yDiff = random.Next(-2, 3);

    var topLeftIncrement = new Vector2I(
      platform.Width + gap,
      yDiff
    );

    newPlatformTopLeft += topLeftIncrement;

    newPlatformTopLeft.Y = Math.Min(newPlatformTopLeft.Y, MAX_PLATFORM_Y_COORDINATE);
  }

  private void TryCreateNewEnemyStartPosition(AbstractPlatform platform)
  {
    var spawnEnemy = random.Next(4) == 0;

    if (spawnEnemy)
    {
      var enemyCoordinates = GetRandomCoordinatesAbovePlatform(platform);

      var enemyType = random.Next(2) == 0 ? "blob" : "bat";

      enemyLocations.Add((enemyCoordinates, enemyType));
    }
  }

  private Vector2I GetRandomCoordinatesAbovePlatform(AbstractPlatform platform)
  {
    var coordinateIndex = random.Next(platform.Coordinates.Count());
    var platformCoordinates = platform.Coordinates[coordinateIndex];

    return new Vector2I(
      platformCoordinates.X,
      platformCoordinates.Y - 1
    );
  }
}
