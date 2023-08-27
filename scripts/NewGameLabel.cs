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
      var level1 = (PackedScene)ResourceLoader.Load(Constants.GAME_SCENE_PATH);

      GetTree().ChangeSceneToPacked(level1);
    }
  }
}
