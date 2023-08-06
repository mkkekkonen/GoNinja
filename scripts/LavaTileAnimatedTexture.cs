using Godot;

public partial class LavaTileAnimatedTexture : AnimatedTexture
{
  public LavaTileAnimatedTexture()
  {
    var _lavaTileFramesResource = GD.Load<LavaTileFramesResource>("res://resources/lava_tiles_frame_resource.tres");

    Frames = 8;

    for (var i = 0; i < Frames; i++)
    {
      SetFrameTexture(i, _lavaTileFramesResource.Textures[i]);
    }
  }
}
