using Godot;
using System;

public partial class NinjaArea2D : Area2D
{
  public void OnAreaEntered(Area2D area)
  {
    var isBlob = area.Name == "BlobArea2D" && !area.GetNode<BlobPhysics>("../../CharacterBody2D").Hit;
    var isBat = area.Name == "BatArea2D" && !area.GetNode<Bat>("../../Bat").Hit;

    if (isBlob || isBat)
    {
      var characterBody = GetNode<CharacterBody2D>("../../CharacterBody2D");

      if (!GameState.NinjaHit)
      {
        GameState.NinjaHit = true;
        Utils.DropCharacterBody2D(characterBody);
      }
    }
  }
}
