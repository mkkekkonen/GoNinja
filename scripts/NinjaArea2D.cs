using Godot;
using System;

public partial class NinjaArea2D : Area2D
{
  private bool hit = false;

  public void OnAreaEntered(Area2D area)
  {
    if (area.Name == "BlobArea2D")
    {
      var characterBody = GetNode<CharacterBody2D>("../../CharacterBody2D");

      if (!hit)
      {
        hit = true;

        characterBody.Velocity += new Vector2(0, -250);

        characterBody.SetCollisionLayerValue(1, false);
        characterBody.SetCollisionMaskValue(1, false);

        characterBody.SetCollisionLayerValue(3, true);
        characterBody.SetCollisionMaskValue(3, true);
      }
    }
  }
}
