using Godot;
using System;

public partial class NewGameLabel : Label
{
  public override void _Input(InputEvent e)
  {
    try
    {
      if (e is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left)
      {
        var clickPosition = mouseEvent.GlobalPosition;
        var labelRect = GetGlobalRect();

        if (labelRect.HasPoint(clickPosition))
        {
          var level1 = (PackedScene)ResourceLoader.Load("res://scenes/game.tscn");

          GetTree().ChangeSceneToPacked(level1);
        }
      }
    }
    catch (Exception ex)
    {
      GD.Print(ex.Message);
    }
  }
}
