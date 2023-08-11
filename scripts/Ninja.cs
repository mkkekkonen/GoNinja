using Godot;
using System;

public partial class Ninja : Node2D
{
  public const float Speed = 300.0f;
  public const float JumpVelocity = -450.0f;
  public const float Epsilon = 0.1f;

  public bool Attacking { get; set; } = false;

  public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

  public override void _Input(InputEvent @event)
  {
    base._Input(@event);

    var animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

    var eventText = @event.AsText();

    if (eventText == "Ctrl" && !Attacking)
    {
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
    var isBlob = area.Name == "BlobArea2D" && !area.GetNode<Blob>("../../../Blob").Hit;
    var isBat = area.Name == "BatArea2D" && !area.GetNode<Bat>("../../Bat").Hit;

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
      sprite.Modulate = new Color(1, 0, 0, 0.5f);

      GameState.NinjaHit = true;

      Utils.DropCharacterBody2D(characterBody);
    }
  }
}
