using Godot;
using System;

public partial class Bat : Node2D, IEnemy
{
  private float angle = 0;
  private Guid guid;
  private Vector2 startPoint;
  private Vector2 velocity = new();

  public float MaxDistance { get; set; } = 70;
  public float AngularVelocity { get; set; } = 2;

  public bool Hit
  {
    get
    {
      return GameState.BatsHit[guid.ToString()];
    }
    set
    {
      GameState.BatsHit[guid.ToString()] = value;
    }
  }

  public override void _Ready()
  {
    guid = Guid.NewGuid();

    var animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

    animationPlayer.Play("hover");

    startPoint = Position;
    Hit = false;
  }

  public override void _PhysicsProcess(double delta)
  {
    var visibilityNotifier = GetNode<VisibleOnScreenNotifier2D>("VisibleOnScreenNotifier2D");
    if (Hit && !visibilityNotifier.IsOnScreen())
    {
      QueueFree();
      return;
    }

    if (Hit)
    {
      Fall(delta);
      return;
    }

    Hover(delta);
  }

  public void HitBySword(Area2D area)
  {
    if (area.Name == Constants.SWORD_AREA_NAME && !GameState.NinjaHit)
    {
      if (!Hit)
      {
        GetNode<Node2D>("../../../GameWorldManager")
          .EmitSignal(Constants.ENEMY_AREA_ENTERED_SIGNAL_NAME, guid.ToString());
      }
    }
  }

  public void Destroy()
  {
    if (!Hit)
    {
      var sprite = GetNode<Sprite2D>("Sprite2D");
      sprite.Modulate = Constants.DESTROYED_COLOR;

      Hit = true;
    }
  }

  public string GetGuid()
  {
    return guid.ToString();
  }

  private void Fall(double delta)
  {
    velocity += new Vector2(0, (float)(Constants.GRAVITY_CONSTANT * delta * 10));

    Position += velocity * new Vector2(0, (float)delta);
  }

  private void Hover(double delta)
  {
    // add 1 to make the multiplier positive
    // (sine function value is between -1 and 1)
    var offsetMultiplier = Mathf.Sin(angle) + 1;

    offsetMultiplier /= 2; // normalize the multiplier to be between 0 and 1
    offsetMultiplier = (float)(offsetMultiplier * (delta * 100));

    var newOffset = offsetMultiplier * MaxDistance;

    Position = startPoint - new Vector2(0, newOffset);

    angle += (float)(AngularVelocity * delta);
  }
}
