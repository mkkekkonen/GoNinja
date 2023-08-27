using Godot;
using System;

public partial class GoToMenuButton : Button
{
	private PackedScene menu;

	public override void _Ready()
	{
		menu = ResourceLoader.Load<PackedScene>(Constants.MENU_SCENE_PATH);
	}

	public void GoToMenu()
	{
		GetTree().ChangeSceneToPacked(menu);
	}
}
