using Godot;
using System;

public partial class Blob : Node2D, IEnemy
{
  private Guid guid;

  private CharacterBody2D characterBody;
  private Sprite2D sprite;
  private AnimationPlayer animationPlayer;

  public bool Hit
  {
    get
    {
      try
      {
        return GameState.BlobsHit[guid.ToString()];
      }
      catch
      {
        return false;
      }
    }
    set
    {
      GameState.BlobsHit[guid.ToString()] = value;
    }
  }

  public override void _Ready()
  {
    guid = Guid.NewGuid();
    Hit = false;

    characterBody = GetNode<CharacterBody2D>("CharacterBody2D");
    sprite = GetNode<Sprite2D>("CharacterBody2D/Sprite2D");
    animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
  }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(double delta)
  {
    if (StopAnimationIfHit())
    {
      return;
    }

    HandleSpriteFlip();
    HandleAnimation();
  }

  public override void _PhysicsProcess(double delta)
  {
    var visibilityNotifier = GetNode<VisibleOnScreenNotifier2D>("CharacterBody2D/VisibleOnScreenNotifier2D");
    if (Hit && !visibilityNotifier.IsOnScreen())
    {
      QueueFree();
    }
  }

  public void Destroy()
  {
    if (!Hit)
    {
      sprite.Modulate = new Color(1, 0, 0, 0.5f);

      Hit = true;
      Utils.DropCharacterBody2D(characterBody);
    }
  }

  public void HitBySword(Area2D area)
  {
    if (area.Name == "SwordArea2D" && !GameState.NinjaHit)
    {
      GetParent().EmitSignal("EnemyAreaEntered", guid.ToString());
    }
  }

  public string GetGuid()
  {
    return guid.ToString();
  }

  private bool StopAnimationIfHit()
  {
    if (Hit)
    {
      animationPlayer.Play("idle");
      return true;
    }

    return false;
  }

  private void HandleSpriteFlip()
  {
    if (characterBody.Velocity.X < 0 && !sprite.FlipH)
    {
      sprite.FlipH = true;
    }
    else if (characterBody.Velocity.X > 0 && sprite.FlipH)
    {
      sprite.FlipH = false;
    }
  }

  private void HandleAnimation()
  {
    if (Mathf.Abs(characterBody.Velocity.X) > 0)
    {
      animationPlayer.Play("move");
    }
  }
}
