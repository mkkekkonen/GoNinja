using Godot;
using System;

public partial class Blob : Node2D
{
  public bool Hit
  {
    get
    {
      return GameState.BlobsHit[GetInstanceId()];
    }
    set
    {
      GameState.BlobsHit[GetInstanceId()] = value;
    }
  }

  public override void _Ready()
  {
    Hit = false;
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta)
  {
    var characterBody = GetNode<CharacterBody2D>("CharacterBody2D");
    var sprite = GetNode<Sprite2D>("CharacterBody2D/Sprite2D");
    var animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

    if (Hit)
    {
      animationPlayer.Play("idle");
      return;
    }

    if (characterBody.Velocity.X < 0 && !sprite.FlipH)
    {
      sprite.FlipH = true;
    }
    else if (characterBody.Velocity.X > 0 && sprite.FlipH)
    {
      sprite.FlipH = false;
    }

    if (Mathf.Abs(characterBody.Velocity.X) > 0)
    {
      animationPlayer.Play("move");
    }
  }

  public override void _PhysicsProcess(double delta)
  {
    base._PhysicsProcess(delta);

    var visibilityNotifier = GetNode<VisibleOnScreenNotifier2D>("CharacterBody2D/VisibleOnScreenNotifier2D");
    if (Hit && !visibilityNotifier.IsOnScreen())
    {
      GameState.BlobsHit.Remove(GetInstanceId());
      QueueFree();
    }
  }

  public void OnAreaEntered(Area2D area)
  {
    if (area.Name == "SwordArea2D" && !GameState.NinjaHit)
    {
      var characterBody = GetNode<CharacterBody2D>("CharacterBody2D");

      if (!Hit)
      {
        var sprite = GetNode<Sprite2D>("CharacterBody2D/Sprite2D");
        sprite.Modulate = new Color(1, 0, 0, 0.5f);

        Hit = true;
        Utils.DropCharacterBody2D(characterBody);
      }
    }
  }
}
