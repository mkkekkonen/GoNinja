using Godot;
using System;

public partial class NinjaPhysics : CharacterBody2D
{
  private Ninja ninja;

  private readonly Transform2D flipX = new Transform2D(-1, 0, 0, 1, 0, 0);

  public const float Speed = 300.0f;
  public const float JumpVelocity = -450.0f;
  public const float Epsilon = 0.1f;

  public override void _Ready()
  {
    ninja = GetNode<Ninja>("../../Ninja");
  }

  public override void _Process(double delta)
  {
    HandleSpriteFlip();
    HandleAnimation();
  }

  public override void _PhysicsProcess(double delta)
  {
    var velocity = Velocity;

    velocity = Fall(delta, velocity);

    if (GameState.CountdownValue == 0)
    {
      velocity = HandleJump(velocity);
      velocity = HandleMove(velocity);
    }

    Velocity = velocity;
    MoveAndSlide();
  }

  private void HandleSpriteFlip()
  {
    if (Velocity.X < 0 && Transform.Scale.Y > 0)
    {
      Transform *= flipX;
    }
    else if (Velocity.X > 0 && Transform.Scale.Y < 0)
    {
      Transform *= flipX;
    }
  }

  private void HandleAnimation()
  {
    var animationPlayer = GetNode<AnimationPlayer>("../AnimationPlayer");
    var velocityAbs = Math.Abs(Velocity.X);

    if (ninja.Attacking)
    {
      // no-op
    }
    else if (!IsOnFloor())
    {
      animationPlayer.Play("jump");
    }
    else if (velocityAbs >= Epsilon)
    {
      animationPlayer.Play("walk");
    }
    else if (velocityAbs < Epsilon)
    {
      animationPlayer.Play("idle");
    }
  }

  private Vector2 Fall(double delta, Vector2 velocity)
  {
    if (!IsOnFloor())
      velocity.Y += Constants.GRAVITY * (float)delta;

    return velocity;
  }

  private Vector2 HandleJump(Vector2 velocity)
  {
    if (Input.IsActionJustPressed("jump") && IsOnFloor())
      velocity.Y = JumpVelocity;

    return velocity;
  }

  private Vector2 HandleMove(Vector2 velocity)
  {
    Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
    if (!(IsOnFloor() && ninja.Attacking) && direction != Vector2.Zero)
    {
      velocity.X = direction.X * Speed;
    }
    else
    {
      velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
    }

    return velocity;
  }
}
