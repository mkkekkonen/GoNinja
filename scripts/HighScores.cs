using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class HighScores : Control
{
	private readonly int LABEL_WIDTH = 400;

	private List<Label> labels;
	private readonly LabelSettings labelSettings =
		ResourceLoader.Load<LabelSettings>(Constants.HIGH_SCORE_LABEL_SETTINGS_PATH);

	public override void _Ready()
	{
		if (GameState.HighScores.Count() == 0)
		{
			GameState.HighScores = Utils.GetHighScores();
		}

		labels = new();

		var labelContainer = GetNode<Control>("HighScoreLabelContainer");

		for (var i = 0; i < GameState.HighScores.Count(); i++)
		{
			var highScore = GameState.HighScores[i];

			var label = new Label
			{
				Text = $"{i + 1}: {highScore.PlayerName}: {highScore.Score}",
				Size = new(LABEL_WIDTH, 0),
				LabelSettings = labelSettings,
			};

			labels.Add(label);

			label.Position = new(200, 200 + (i * 80));

			labelContainer.AddChild(label);
		}
	}
}
