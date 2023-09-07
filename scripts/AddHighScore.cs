using Godot;
using System.Linq;
using System.Text.Json;

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
			return;
		}

		GameState.HighScores.Add(new()
		{
			PlayerName = lineEdit.Text,
			Score = GameState.TotalScore,
		});

		GameState.HighScores = GameState.HighScores
			.OrderByDescending(score => score.Score)
			.Take(Constants.MAX_HIGH_SCORES)
			.ToList();

		var fileContent = JsonSerializer.Serialize(GameState.HighScores);

		using var file = FileAccess.Open(Constants.SCORE_FILE_PATH, FileAccess.ModeFlags.Write);
		file.StoreString(fileContent);

		GetTree().ChangeSceneToPacked(Constants.HIGH_SCORES_SCENE);
	}

	public void GoToMenu()
	{
		GetTree().ChangeSceneToPacked(Constants.MENU_SCENE);
	}
}
