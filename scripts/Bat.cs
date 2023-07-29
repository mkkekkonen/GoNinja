using Godot;
using System;

public partial class Bat : Node2D
{
  private float angle = 0;
  private Vector2 startPoint;
  private Vector2 velocity = new();

  public float MaxDistance { get; set; } = 70;
  public float AngularVelocity { get; set; } = 2;

  public bool Hit
  {
    get
    {
      return GameState.BatsHit[GetInstanceId()];
    }
    set
    {
      GameState.BatsHit[GetInstanceId()] = value;
    }
  }

  public override void _Ready()
  {
    base._Ready();

    var animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    animationPlayer.Play("hover");

    startPoint = Position;
    Hit = false;
  }

  public override void _Process(double delta)
  {
    base._Process(delta);

    if (Hit)
    {
      var sprite = GetNode<Sprite2D>("Sprite2D");
      sprite.Modulate = new Color(1, 0, 0, 0.5f);
    }
  }

  public override void _PhysicsProcess(double delta)
  {
    if (Hit)
    {
      velocity += new Vector2(0, (float)(9.81f * delta * 10));

      Position += (velocity * new Vector2(0, (float)delta));

      return;
    }

    var offsetMultiplier = Mathf.Sin(angle) + 1; // add 1 to make the multiplier positive
                                                 // (sine function value is between -1 and 1)
    offsetMultiplier = offsetMultiplier / 2; // normalize the multiplier to be between 0 and 1
    offsetMultiplier = (float)(offsetMultiplier * (delta * 100));

    var newOffset = offsetMultiplier * MaxDistance;

    Position = (startPoint + new Vector2(0, newOffset));

    angle += (float)(AngularVelocity * delta);
  }

  public void OnAreaEntered(Area2D area)
  {
    if (area.Name == "SwordArea2D" && !GameState.NinjaHit)
    {
      if (!Hit)
      {
        Hit = true;
      }
    }
  }
}