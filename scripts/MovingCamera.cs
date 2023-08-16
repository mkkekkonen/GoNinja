using Godot;
using System;
using System.Linq;


public partial class MovingCamera : Node2D
{
	private int SCALE = 6;

	private float maxY;
	private bool moving = false;

	private Vector2 velocity = new(150, 0);

	private CharacterBody2D ninjaCharacterBody;
	private TileMap tileMap;

	public override void _Ready()
	{
		ninjaCharacterBody = GetNode<CharacterBody2D>("../Ninja/CharacterBody2D");
		tileMap = GetNode<TileMap>("../TileMap");

		var window = GetWindow();

		maxY = window.Size.Y * .75f;

		Position = new Vector2(window.Size.X / 2, maxY);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (moving && !GameState.GameOver)
		{
			var lastPlatform = GameWorld.Instance.Platforms.Last();
			var lastCoordinates = lastPlatform.Coordinates.Last();
			var lastLocalCoordinates = tileMap.MapToLocal(lastCoordinates) * SCALE;

			if (GlobalPosition.X > lastLocalCoordinates.X)
			{
				return;
			}

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
