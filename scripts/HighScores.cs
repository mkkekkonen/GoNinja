using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public partial class HighScores : Control
{
	private readonly int SCORES_PER_COLUMN = 5;
	private readonly int LABEL_WIDTH = 400;

	private List<Label> labels;
	private readonly LabelSettings labelSettings =
		ResourceLoader.Load<LabelSettings>(Constants.HIGH_SCORE_LABEL_SETTINGS_PATH);

	public override void _Ready()
	{
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

			if (i < SCORES_PER_COLUMN)
			{
				label.Position = new(200, 200 + (i * 80));
			}
			else
			{
				label.Position = new(200 + LABEL_WIDTH, 200 + ((i - SCORES_PER_COLUMN) * 80));
			}

			labelContainer.AddChild(label);
		}
	}
}
