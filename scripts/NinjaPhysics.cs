using Godot;
using System;

public partial class NinjaPhysics : CharacterBody2D
{
  public const float Speed = 300.0f;
  public const float JumpVelocity = -450.0f;
  public const float Epsilon = 0.1f;

  // Get the gravity from the project settings to be synced with RigidBody nodes.
  public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

  public override void _Process(double delta)
  {
    base._Process(delta);

    var ninja = GetNode<Ninja>("../../Ninja");
    var sprite = GetNode<Sprite2D>("Sprite2D");

    if (Velocity.X < 0 && !sprite.FlipH)
    {
      sprite.FlipH = true;
    }
    else if (Velocity.X > 0 && sprite.FlipH)
    {
      sprite.FlipH = false;
    }

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

  public override void _PhysicsProcess(double delta)
  {
    var ninja = GetNode<Ninja>("../../Ninja");

    var velocity = Velocity;
    var isOnFloor = IsOnFloor();

    // Add the gravity.
    if (!IsOnFloor())
      velocity.Y += gravity * (float)delta;

    // Handle Jump.
    if (Input.IsActionJustPressed("ui_accept") && isOnFloor)
      velocity.Y = JumpVelocity;

    // Get the input direction and handle the movement/deceleration.
    // As good practice, you should replace UI actions with custom gameplay actions.
    Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
    if (!(isOnFloor && ninja.Attacking) && direction != Vector2.Zero)
    {
      velocity.X = direction.X * Speed;
    }
    else
    {
      velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
    }

    Velocity = velocity;
    MoveAndSlide();
  }
}
