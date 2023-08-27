using Godot;
using System;

public partial class AddHighScore : Control
{
	public override void _Ready()
	{
		GetNode<Label>("ScoreLabel").Text = $"Your score: {GameState.TotalScore}";
	}

	public void SaveHighScore()
	{
		var lineEdit = GetNode<LineEdit>("LineEdit");

		if (string.IsNullOrWhiteSpace(lineEdit.Text))
		{
			var validationLabel = GetNode<Label>("ValidationLabel");
			validationLabel.Visible = true;
		}
	}

	public void GoToMenu()
	{
		GetTree().ChangeSceneToPacked(Constants.MENU_SCENE);
	}
}
