using Godot;
using System;

public partial class YouWin : Node2D
{
	private PackedScene highScores;

	public override void _Ready()
	{
		highScores = ResourceLoader.Load<PackedScene>("res://scenes/high_scores.tscn");
	}

	public void Win()
	{
		Visible = true;

		var timer = GetNode<Timer>("Timer");
		timer.Start();
	}

	public override void _PhysicsProcess(double delta)
	{
		var window = GetWindow();

		if (GetWindow != null && Visible)
		{
			FollowCamera(window);
		}
	}

	public void MoveToHighScores()
	{
		GetTree().ChangeSceneToPacked(highScores);
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
