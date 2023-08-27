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

  private static readonly float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
  private static readonly float gravityConstant = 9.81f;

  private static readonly string scoreFilePath = "user://ninjaScores23.json";
  private static readonly string addHighScoreScenePath = "res://scenes/add_high_score.tscn";
  private static readonly string highScoresScenePath = "res://scenes/high_scores.tscn";
  private static readonly string menuScenePath = "res://scenes/menu.tscn";
  private static readonly string lavaAnimatedSpritePath = "res://scenes/lavaAnimatedSprite.tscn";
  private static readonly string gameScenePath = "res://scenes/game.tscn";
  private static readonly string redBlobPath = "res://scenes/redBlob.tscn";
  private static readonly string batPath = "res://scenes/bat.tscn";

  private static readonly string lavaTileFramesResourcePath = "res://resources/lava_tiles_frame_resource.tres";
  private static readonly string spriteSheetPath = "res://img/sheet.png";

  private static string attackKey = "Ctrl";
  private static string platformLabel = "platform";
  private static string pillarLabel = "pillar";
  private static string swordAreaName = "SwordArea2D";
  private static string enemyAreaEnteredSignalName = "EnemyAreaEntered";

  private static readonly Color destroyedColor = new Color(1, 0, 0, 0.5f);

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

  #endregion

  #region ScenePaths

  public static string SCORE_FILE_PATH
  {
    get
    {
      return scoreFilePath;
    }
  }

  public static string ADD_HIGH_SCORES_SCENE_PATH
  {
    get
    {
      return addHighScoreScenePath;
    }
  }

  public static string HIGH_SCORES_SCENE_PATH
  {
    get
    {
      return highScoresScenePath;
    }
  }

  public static string MENU_SCENE_PATH
  {
    get
    {
      return menuScenePath;
    }
  }

  public static string LAVA_ANIMATED_SPRITE_PATH
  {
    get
    {
      return lavaAnimatedSpritePath;
    }
  }

  public static string GAME_SCENE_PATH
  {
    get
    {
      return gameScenePath;
    }
  }

  public static string RED_BLOB_PATH
  {
    get
    {
      return redBlobPath;
    }
  }

  public static string BAT_PATH
  {
    get
    {
      return batPath;
    }
  }

  #endregion

  #region ResourcePaths

  public static string LAVA_TILE_FRAMES_RESOURCE_PATH
  {
    get
    {
      return lavaTileFramesResourcePath;
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

  #endregion
}
