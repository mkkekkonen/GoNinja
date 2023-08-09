using System;
using System.Collections.Generic;
using Godot;

public class World
{
  private static World instance;

  public static World Instance
  {
    get
    {
      instance ??= new World();
      return instance;
    }
  }

  private readonly int N_PLATFORMS = 75;
  private readonly int MIN_PLATFORM_WIDTH = 3;
  private readonly int MAX_PLATFORM_WIDTH = 8;

  private readonly string[] platformLabels = new string[] { "platform", "pillar" };

  private Vector2I newPlatformTopLeft = new(24, 11);

  private readonly List<AbstractPlatform> platforms = new();

  private readonly Random random = new();

  private World() { }

  public List<AbstractPlatform> Platforms
  {
    get
    {
      return platforms;
    }
  }

  public void GeneratePlatforms()
  {
    for (var i = 0; i < N_PLATFORMS; i++)
    {
      var platform = GeneratePlatform();
      platforms.Add(platform);
      IncrementNewPlatformTopLeft(platform);
    }
  }

  private int GetNewPlatformWidth()
  {
    return random.Next(MIN_PLATFORM_WIDTH, MAX_PLATFORM_WIDTH + 1);
  }

  private AbstractPlatform GeneratePlatform()
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

    return platform;
  }

  private void IncrementNewPlatformTopLeft(AbstractPlatform platform)
  {
    var gap = random.Next(1, 3);
    var yDiff = random.Next(-2, 2);

    var topLeftIncrement = new Vector2I(
      platform.Width + gap,
      yDiff
    );

    newPlatformTopLeft += topLeftIncrement;
  }
}
