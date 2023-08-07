using Godot;
using System;

public partial class BlobPhysics : CharacterBody2D
{
  public const float Speed = 100.0f;

  public Vector2 Direction = new(1, 0);

  // Get the gravity from the project settings to be synced with RigidBody nodes.
  public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

  public override void _PhysicsProcess(double delta)
  {
    var blob = GetNode<Blob>("../../Blob");

    Vector2 velocity = Velocity;

    // Add the gravity.
    if (!IsOnFloor())
      velocity.Y += gravity * (float)delta;

    if (!blob.Hit)
      HandleDirection();

    if (Direction != Vector2.Zero)
    {
      velocity.X = Direction.X * Speed;
    }
    else
    {
      velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
    }

    Velocity = velocity;
    MoveAndSlide();
  }

  private void HandleDirection()
  {
    var direction = Direction;

    var rayCastLeft = GetNode<RayCast2D>("RayCast2DLeft");
    var rayCastRight = GetNode<RayCast2D>("RayCast2DRight");

    var endOfPlatform = rayCastLeft.GetCollider() == null
      || rayCastRight.GetCollider() == null;

    if (IsOnWall() || endOfPlatform)
    {
      Direction = direction * new Vector2(-1, 0);
    }
  }
}
