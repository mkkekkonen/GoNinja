using Godot;
using System.Collections.Generic;

public partial class Lava : Node2D
{
  private readonly int FRAMES_COUNT = 8;

  private readonly List<Node2D> _sprites = new();

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    var window = GetWindow();

    var lavaSprite = GD.Load<PackedScene>(Constants.LAVA_ANIMATED_SPRITE_PATH);

    var spriteCount = window.Size.X / Constants.SPRITE_DIMENSIONS;

    for (var i = 0; i < spriteCount; i++)
    {
      var sprite = lavaSprite.Instantiate() as AnimatedSprite2D;
      var frameIndex = i % FRAMES_COUNT;

      sprite.Frame = frameIndex;
      sprite.Name = $"LavaSprite{i}";

      var x = i * Constants.SPRITE_DIMENSIONS + Constants.SPRITE_DIMENSIONS / 2 - window.Size.X / 2;
      sprite.Position = new Vector2(x, 0);

      AddChild(sprite);
      _sprites.Add(sprite);
    }
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _PhysicsProcess(double delta)
  {
    var camera = GetParent() as MovingCamera;

    var position = camera.GlobalPosition;
    position.Y = Constants.LAVA_Y_COORDINATE;

    GlobalPosition = position;
  }
}
