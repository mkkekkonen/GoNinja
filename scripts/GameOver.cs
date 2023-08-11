using Godot;
using System;

public partial class GameOver : Node2D
{
	private PackedScene menu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameState.GameOver = false;

		menu = (PackedScene)ResourceLoader.Load("res://scenes/menu.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		var window = GetWindow();

		if (GetWindow != null && Visible)
		{
			FollowCamera(window);
		}
	}

	public void ShowGameOver()
	{
		var timer = GetNode<Timer>("Timer");

		Visible = true;
		GameState.GameOver = true;
		timer.Start();
	}

	public void GoToMenu()
	{
		GetTree().ChangeSceneToPacked(menu);
	}

	private void FollowCamera(Window window)
	{
		var camera = window.GetCamera2D();

		if (camera != null)
		{
			Position = camera.GlobalPosition;
		}
	}
}
