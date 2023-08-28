using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


public partial class GameOver : Node2D
{
	public override void _Ready()
	{
		GameState.GameOver = false;
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

	public void GoToHighScores()
	{
		var fileExists = File.Exists(Constants.SCORE_FILE_PATH);

		if (!fileExists || HasNewHighScore())
		{
			GetTree().ChangeSceneToPacked(Constants.ADD_HIGH_SCORES_SCENE);
		}
		else
		{
			GetTree().ChangeSceneToPacked(Constants.HIGH_SCORES_SCENE);
		}
	}

	private void FollowCamera(Window window)
	{
		var camera = window.GetCamera2D();

		if (camera != null)
		{
			Position = camera.GlobalPosition;
		}
	}

	private bool HasNewHighScore()
	{
		var highScores = ReadScoresFromFile();

		if (!highScores.Any() || (GameState.TotalScore > highScores.Last().Score))
		{
			GameState.HighScores = highScores;
			return true;
		}

		return false;
	}

	private List<HighScore> ReadScoresFromFile()
	{
		try
		{
			if (Godot.FileAccess.FileExists(Constants.SCORE_FILE_PATH))
			{
				var jsonText = Godot.FileAccess.GetFileAsString(Constants.SCORE_FILE_PATH);
				return JsonSerializer.Deserialize<List<HighScore>>(jsonText);
			}
		}
		catch
		{
		}

		return new();
	}
}
