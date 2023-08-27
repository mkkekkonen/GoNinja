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
        HandleClick(mouseEvent);
      }
    }
    catch (Exception ex)
    {
      GD.Print(ex.Message);
    }
  }

  private void HandleClick(InputEventMouseButton mouseEvent)
  {
    var clickPosition = mouseEvent.GlobalPosition;
    var labelRect = GetGlobalRect();

    if (labelRect.HasPoint(clickPosition))
    {
      GetTree().ChangeSceneToPacked(Constants.GAME_SCENE);
    }
  }
}
