using Godot;
using System.Collections.Generic;

public partial class LavaTileFramesResource : Resource
{
  private readonly int FRAMES_COUNT = 8;

  private readonly IReadOnlyList<(int, int)> _coords = new List<(int, int)> {
    (2, 11),
    (11, 11),
    (2, 19),
    (11, 19),
    (2, 29),
    (11, 29),
    (2, 38),
    (11, 38),
  };

  private Image spriteSheetImage;

  public Texture2D[] Textures { get; private set; }

  public LavaTileFramesResource()
  {
    var sheetTexture = (Texture2D)ResourceLoader.Load(Constants.SPRITE_SHEET_PATH);

    if (sheetTexture != null)
    {
      InitTextures(sheetTexture);
    }
  }

  private void InitTextures(Texture2D sheetTexture)
  {
    spriteSheetImage = sheetTexture.GetImage();

    Textures = new Texture2D[FRAMES_COUNT];

    for (var i = 0; i < FRAMES_COUNT; i++)
    {
      (var x, var y) = _coords[i];
      Textures[i] = ExtractTexture(x, y);
    }
  }

  private Texture2D ExtractTexture(int x, int y)
  {
    var frameRect = new Rect2I(x, y, Constants.TILE_DIMENSIONS, Constants.TILE_DIMENSIONS);

    var clippedImg = Image.Create(
      Constants.TILE_DIMENSIONS,
      Constants.TILE_DIMENSIONS,
      true,
      Image.Format.Rgba8
    );
    clippedImg.BlitRect(spriteSheetImage, frameRect, Vector2I.Zero);

    var frameTexture = ImageTexture.CreateFromImage(clippedImg);
    return frameTexture;
  }
}
