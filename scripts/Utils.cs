using Godot;
using System;

public static class Utils
{
  public static void DropCharacterBody2D(CharacterBody2D characterBody)
  {
    characterBody.Velocity += new Vector2(0, -250);

    characterBody.SetCollisionLayerValue(1, false);
    characterBody.SetCollisionMaskValue(1, false);

    characterBody.SetCollisionLayerValue(3, true);
    characterBody.SetCollisionMaskValue(3, true);
  }
}
