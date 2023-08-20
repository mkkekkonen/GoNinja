using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;


public partial class YouWin : Node2D
{
	private readonly string SCORE_FILE_PATH = "user://ninjaScores23.json";

	private PackedScene addHighScoreScene;
	private PackedScene highScores;

	public override void _Ready()
	{
		addHighScoreScene = ResourceLoader.Load<PackedScene>("res://scenes/add_high_score.tscn");
		highScores = ResourceLoader.Load<PackedScene>("res://scenes/high_scores.tscn");
	}

	public void Win()
	{
		Visible = true;

		GameState.Won = true;

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
		var fileExists = File.Exists(SCORE_FILE_PATH);

		if (!fileExists || HasNewHighScore())
		{
			GetTree().ChangeSceneToPacked(addHighScoreScene);
		}
		else
		{
			GetTree().ChangeSceneToPacked(highScores);
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
			var jsonText = File.ReadAllText(SCORE_FILE_PATH);
			return JsonSerializer.Deserialize<List<HighScore>>(jsonText);
		}
		catch
		{
			return new();
		}
	}
}
