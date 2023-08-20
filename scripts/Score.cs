using Godot;
using System;

public partial class Score : Control
{
	private Label pointsLabel;
	private Label timeLabel;

	public override void _Ready()
	{
		pointsLabel = GetNode<Label>("PointsLabel");
		timeLabel = GetNode<Label>("TimeLabel");
	}

	public override void _Process(double delta)
	{
		ProcessTime(delta);

		pointsLabel.Text = GameState.Score.ToString();
	}

	private void ProcessTime(double delta)
	{
		GameState.GameTime += delta;

		var minutes = (int)(GameState.GameTime / 60 % 60);
		var seconds = (int)(GameState.GameTime % 60);

		var formattedTime = $"{minutes}:{seconds:D2}";

		timeLabel.Text = formattedTime;
	}
}
