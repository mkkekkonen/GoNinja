using Godot;
using System;

public partial class LavaTileAnimation : AnimatedSprite2D
{
  private readonly string ANIMATION_NAME = "default";

  private LavaTileFramesResource _lavaTileFramesResource;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    try
    {
      _lavaTileFramesResource = GD.Load<LavaTileFramesResource>("res://resources/lava_tiles_frame_resource.tres");

      SpriteFrames = new SpriteFrames();

      var sheetTexture = (Texture2D)ResourceLoader.Load("res://img/sheet.png");

      foreach (var texture in _lavaTileFramesResource.Textures)
      {
        SpriteFrames.AddFrame(ANIMATION_NAME, texture);
      }
    }
    catch (Exception e)
    {
      GD.Print(e.Message);
    }
  }

  public override void _Process(double delta)
  {
    base._Process(delta);

    Play(ANIMATION_NAME);
  }
}
