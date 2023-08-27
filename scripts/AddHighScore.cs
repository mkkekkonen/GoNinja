using Godot;
using System;

public partial class AddHighScore : Control
{
	private PackedScene menu;

	public override void _Ready()
	{
		menu = ResourceLoader.Load<PackedScene>(Constants.MENU_SCENE_PATH);

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
		GetTree().ChangeSceneToPacked(menu);
	}
}
