using Godot;
using System;

public partial class Countdown : Node2D
{
	private readonly int SECONDS = 5;

	private Label label;
	private Timer timer;
	private ColorRect overlay;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		label = GetNode<Label>("Label");
		timer = GetNode<Timer>("Timer");
		overlay = GetNode<ColorRect>("OverlayColorRect");
	}

	public override void _Process(double delta)
	{
		var window = GetWindow();

		if (window != null)
		{
			FollowCamera(window);
		}
	}

	public void StartCountdown()
	{
		label.Visible = true;
		overlay.Visible = true;
		GameState.CountdownValue = SECONDS;
		label.Text = GameState.CountdownValue.ToString();

		timer.Start();
	}

	public void OnCountdownTimeout()
	{
		GameState.CountdownValue--;

		label.Text = GameState.CountdownValue.ToString();

		if (GameState.CountdownValue == 0)
		{
			EndCountdown();
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

	private void EndCountdown()
	{
		label.Visible = false;
		overlay.Visible = false;
		timer.Stop();
	}
}
