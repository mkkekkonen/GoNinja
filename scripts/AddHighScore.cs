using Godot;
using System;

public partial class AddHighScore : Control
{
	public override void _Ready()
	{
		GetNode<Label>("ScoreLabel").Text = $"Your score: {GameState.TotalScore}";
	}
}
