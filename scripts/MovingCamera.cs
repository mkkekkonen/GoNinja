using Godot;
using System;

public partial class MovingCamera : Node2D
{
	private float maxY;
	private bool moving = false;

	private Vector2 velocity = new(150, 0);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var window = GetWindow();

		maxY = window.Size.Y * .75f;

		Position = new Vector2(window.Size.X / 2, maxY);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (moving)
		{
			var ninjaCharacterBody = GetNode<CharacterBody2D>("../Ninja/CharacterBody2D");

			var newPosition = GlobalPosition;

			newPosition += velocity * (float)delta;

			newPosition.Y = Math.Min(ninjaCharacterBody.GlobalPosition.Y, maxY);

			GlobalPosition = newPosition;
		}
	}

	public void StartMoving()
	{
		moving = true;
	}
}
