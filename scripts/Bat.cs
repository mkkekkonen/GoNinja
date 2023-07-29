using Godot;
using System;

public partial class Bat : Node2D
{
  private float angle = 0;
  private Vector2 startPoint;

  public float MaxDistance { get; set; } = 70;
  public float AngularVelocity { get; set; } = 2;

  public override void _Ready()
  {
    base._Ready();

    startPoint = Position;
  }

  public override void _PhysicsProcess(double delta)
  {
    var offsetMultiplier = Mathf.Sin(angle) + 1;
    offsetMultiplier = offsetMultiplier / 2;
    offsetMultiplier = (float)(offsetMultiplier * (delta * 100));

    var newOffset = offsetMultiplier * MaxDistance;

    Position = (startPoint + new Vector2(0, newOffset));

    angle += (float)(AngularVelocity * delta);
  }
}
