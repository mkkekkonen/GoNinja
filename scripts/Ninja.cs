using Godot;
using System;

public partial class Ninja : Node2D
{
  public const float Speed = 300.0f;
  public const float JumpVelocity = -450.0f;
  public const float Epsilon = 0.1f;

  public bool Attacking { get; set; } = false;

  public override void _Input(InputEvent @event)
  {
    base._Input(@event);


  }

  public override void _Process(double delta)
  {
    if (Input.IsActionJustPressed("attack"))
    {
      var animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

      animationPlayer.Play("attack");
      Attacking = true;
    }
  }

  public override void _PhysicsProcess(double delta)
  {
    var visibilityNotifier = GetNode<VisibleOnScreenNotifier2D>("CharacterBody2D/VisibleOnScreenNotifier2D");
    var gameOver = GetNode<GameOver>("../GameOver");

    if (GameState.CountdownValue == 0 && !visibilityNotifier.IsOnScreen())
    {
      gameOver.ShowGameOver();
      QueueFree();
    }
  }

  public void OnAreaEntered(Area2D area)
  {
    var isBlob = IsAreaBlob(area);
    var isBat = IsAreaBat(area);

    if (isBlob || isBat)
    {
      HandleHitByEnemy();
    }
  }

  public void EndAttack()
  {
    Attacking = false;
  }

  private void HandleHitByEnemy()
  {
    var characterBody = GetNode<CharacterBody2D>("CharacterBody2D");

    if (!GameState.NinjaHit)
    {
      var sprite = GetNode<Sprite2D>("CharacterBody2D/Sprite2D");
      sprite.Modulate = Constants.DESTROYED_COLOR;

      GameState.NinjaHit = true;

      Utils.DropCharacterBody2D(characterBody);
    }
  }

  private bool IsAreaBlob(Area2D area)
  {
    try
    {
      if (area.Name == "BlobArea2D")
      {
        var blob = area.GetParent().GetParent<Blob>();
        return blob != null ? !blob.Hit : false;
      }
    }
    catch { }

    return false;
  }

  private bool IsAreaBat(Area2D area)
  {
    try
    {
      if (area.Name == "BatArea2D")
      {
        var bat = area.GetParent<Bat>();
        return bat != null ? !bat.Hit : false;
      }
    }
    catch { }

    return false;
  }
}
