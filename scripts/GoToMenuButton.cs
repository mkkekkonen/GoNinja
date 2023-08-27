using Godot;
using System;

public partial class GoToMenuButton : Button
{
	public void GoToMenu()
	{
		GetTree().ChangeSceneToPacked(Constants.MENU_SCENE);
	}
}
