using Godot;
using System;

public partial class BlobArea2D : Area2D
{
  public void OnAreaEntered(Area2D area)
  {
    if (area.Name == "SwordArea2D" && !GameState.NinjaHit)
    {
      var characterBody = GetNode<CharacterBody2D>("../../CharacterBody2D");
      var blobPhysics = (BlobPhysics)characterBody;

      if (!blobPhysics.Hit)
      {
        blobPhysics.Hit = true;

        Utils.DropCharacterBody2D(characterBody);
      }
    }
  }
}
