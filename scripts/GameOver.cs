using Godot;
using System.Linq;


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
		var fileExists = FileAccess.FileExists(Constants.SCORE_FILE_PATH);

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
		var highScores = Utils.GetHighScores();

		if (!highScores.Any()
			|| highScores.Count() < Constants.MAX_HIGH_SCORES
			|| (GameState.TotalScore > highScores.Last().Score))
		{
			GameState.HighScores = highScores;
			return true;
		}

		return false;
	}
}
