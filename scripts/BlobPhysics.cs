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

    velocity = Fall(velocity, delta);

    if (!blob.Hit)
      HandleDirection();

    if (GameState.CountdownValue == 0)
    {
      velocity = HandleMove(velocity);
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

  private Vector2 Fall(Vector2 velocity, double delta)
  {
    if (!IsOnFloor())
      velocity.Y += gravity * (float)delta;

    return velocity;
  }

  private Vector2 HandleMove(Vector2 velocity)
  {
    if (Direction != Vector2.Zero)
    {
      velocity.X = Direction.X * Speed;
    }
    else
    {
      velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
    }

    return velocity;
  }
}
