using Godot;

public static class Constants
{
  private static readonly int spriteDimensions = 48;
  private static readonly int tileDimensions = 8;
  private static readonly int lavaYCoordinate = 800;
  private static readonly int countdownSeconds = 5;
  private static readonly int cameraSpeedInSeconds = 150;
  private static readonly int platformBatchSize = 10;
  private static readonly int worldBottomTileYCoordinate = 18;
  private static readonly int bgTileLayerIndex = 0;
  private static readonly int fgTileLayerIndex = 1;
  private static readonly int windowWidthTiles = 24;
  private static readonly int windowHeightTiles = 14;
  private static readonly int scale = 6;
  private static readonly int maxHighScores = 5;

  private static readonly float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
  private static readonly float gravityConstant = 9.81f;
  private static readonly float viewportX = 1152;
  private static readonly float viewportY = 648;

  private static readonly PackedScene addHighScoreScene = ResourceLoader.Load<PackedScene>("res://scenes/add_high_score.tscn");
  private static readonly PackedScene highScoresScene = ResourceLoader.Load<PackedScene>("res://scenes/high_scores.tscn");
  private static readonly PackedScene menuScene = ResourceLoader.Load<PackedScene>("res://scenes/menu.tscn");
  private static readonly PackedScene lavaAnimatedSpriteScene = ResourceLoader.Load<PackedScene>("res://scenes/lavaAnimatedSprite.tscn");
  private static readonly PackedScene gameScene = ResourceLoader.Load<PackedScene>("res://scenes/game.tscn");
  private static readonly PackedScene redBlobScene = ResourceLoader.Load<PackedScene>("res://scenes/redBlob.tscn");
  private static readonly PackedScene batScene = ResourceLoader.Load<PackedScene>("res://scenes/bat.tscn");

  private static readonly string scoreFilePath = "user://scores.dat";
  private static readonly string lavaTileFramesResourcePath = "res://resources/lava_tiles_frame_resource.tres";
  private static readonly string highScoreLabelSettingsPath = "res://resources/high_score_label_settings.tres";
  private static readonly string spriteSheetPath = "res://img/sheet.png";

  private static string attackKey = "Ctrl";
  private static string platformLabel = "platform";
  private static string pillarLabel = "pillar";
  private static string swordAreaName = "SwordArea2D";
  private static string enemyAreaEnteredSignalName = "EnemyAreaEntered";

  private static readonly Color destroyedColor = new Color(1, 0, 0, 0.5f);
  private static readonly Color menuItemColor = new Color("#a68ed2");

  #region NumericValues

  public static int SPRITE_DIMENSIONS
  {
    get
    {
      return spriteDimensions;
    }
  }

  public static int TILE_DIMENSIONS
  {
    get
    {
      return tileDimensions;
    }
  }

  public static int LAVA_Y_COORDINATE
  {
    get
    {
      return lavaYCoordinate;
    }
  }

  public static int COUNTDOWN_SECONDS
  {
    get
    {
      return countdownSeconds;
    }
  }

  public static int CAMERA_SPEED_IN_SECONDS
  {
    get
    {
      return cameraSpeedInSeconds;
    }
  }

  public static int PLATFORM_BATCH_SIZE
  {
    get
    {
      return platformBatchSize;
    }
  }

  public static int WORLD_BOTTOM_TILE_Y_COORDINATE
  {
    get
    {
      return worldBottomTileYCoordinate;
    }
  }

  public static int BG_TILE_LAYER_INDEX
  {
    get
    {
      return bgTileLayerIndex;
    }
  }

  public static int FG_TILE_LAYER_INDEX
  {
    get
    {
      return fgTileLayerIndex;
    }
  }

  public static int WINDOW_WIDTH_TILES
  {
    get
    {
      return windowWidthTiles;
    }
  }

  public static int WINDOW_HEIGHT_TILES
  {
    get
    {
      return windowHeightTiles;
    }
  }

  public static int SCALE
  {
    get
    {
      return scale;
    }
  }

  public static int MAX_HIGH_SCORES
  {
    get
    {
      return maxHighScores;
    }
  }

  public static float GRAVITY
  {
    get
    {
      return gravity;
    }
  }

  public static float GRAVITY_CONSTANT
  {
    get
    {
      return gravityConstant;
    }
  }

  public static float VIEWPORT_X
  {
    get
    {
      return viewportX;
    }
  }

  public static float VIEWPORT_Y
  {
    get
    {
      return viewportY;
    }
  }

  #endregion

  #region ScenePaths

  public static PackedScene ADD_HIGH_SCORES_SCENE
  {
    get
    {
      return addHighScoreScene;
    }
  }

  public static PackedScene HIGH_SCORES_SCENE
  {
    get
    {
      return highScoresScene;
    }
  }

  public static PackedScene MENU_SCENE
  {
    get
    {
      return menuScene;
    }
  }

  public static PackedScene LAVA_ANIMATED_SPRITE
  {
    get
    {
      return lavaAnimatedSpriteScene;
    }
  }

  public static PackedScene GAME_SCENE
  {
    get
    {
      return gameScene;
    }
  }

  public static PackedScene RED_BLOB_SCENE
  {
    get
    {
      return redBlobScene;
    }
  }

  public static PackedScene BAT_SCENE
  {
    get
    {
      return batScene;
    }
  }

  #endregion

  #region ResourcePaths

  public static string SCORE_FILE_PATH
  {
    get
    {
      return scoreFilePath;
    }
  }

  public static string LAVA_TILE_FRAMES_RESOURCE_PATH
  {
    get
    {
      return lavaTileFramesResourcePath;
    }
  }

  public static string HIGH_SCORE_LABEL_SETTINGS_PATH
  {
    get
    {
      return highScoreLabelSettingsPath;
    }
  }

  public static string SPRITE_SHEET_PATH
  {
    get
    {
      return spriteSheetPath;
    }
  }

  #endregion

  #region OtherStrings

  public static string ATTACK_KEY
  {
    get
    {
      return attackKey;
    }
  }

  public static string PLATFORM_LABEL
  {
    get
    {
      return platformLabel;
    }
  }

  public static string PILLAR_LABEL
  {
    get
    {
      return pillarLabel;
    }
  }

  public static string SWORD_AREA_NAME
  {
    get
    {
      return swordAreaName;
    }
  }

  public static string ENEMY_AREA_ENTERED_SIGNAL_NAME
  {
    get
    {
      return enemyAreaEnteredSignalName;
    }
  }

  #endregion

  #region Objects

  public static Color DESTROYED_COLOR
  {
    get
    {
      return destroyedColor;
    }
  }

  public static Color MENU_ITEM_COLOR
  {
    get
    {
      return menuItemColor;
    }
  }

  #endregion
}
