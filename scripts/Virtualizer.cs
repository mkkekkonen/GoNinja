using Godot;
using System;
using System.Linq;


public partial class Virtualizer : Node2D
{
	private readonly int CAMERA_MARGIN = 200;
	private readonly int TILE_MAP_BACKGROUND_LAYER = 0;
	private readonly int TILE_MAP_FOREGROUND_LAYER = 1;

	private double elapsedTime = 0;

	private Camera2D camera;
	private TileMap tileMap;
	private Node2D enemyContainer;

	private GameWorldManager gameWorldManager;
	private AbstractPlatform lastPlatform;

	public override void _Ready()
	{
		camera = GetNode<Camera2D>("../MovingCamera/Camera2D");
		tileMap = GetNode<TileMap>("../TileMap");
		gameWorldManager = GetNode<GameWorldManager>("../GameWorldManager");
		enemyContainer = GetNode<Node2D>("../GameWorldManager/EnemyContainer");
	}

	public override void _Process(double delta)
	{
		elapsedTime += delta;

		if (elapsedTime > 2)
		{
			RemoveTilesBehindCamera();
			elapsedTime -= 2;
		}

		TryCreateNewPlatforms();
	}

	private void RemoveTilesBehindCamera()
	{
		var windowWidthHalved = GetWindow().Size.X / 2;

		var cameraX = camera.GlobalPosition.X;
		var cameraLeft = cameraX - windowWidthHalved;

		var rightTileColumn = (int)(cameraLeft / Constants.SPRITE_DIMENSIONS) - 1;

		var usedRect = tileMap.GetUsedRect();

		for (var x = usedRect.Position.X; x <= rightTileColumn; x++)
		{
			for (var y = 0; y < usedRect.Size.Y; y++)
			{
				tileMap.SetCell(TILE_MAP_BACKGROUND_LAYER, new Vector2I(x, y));
				tileMap.SetCell(TILE_MAP_FOREGROUND_LAYER, new Vector2I(x, y));
			}
		}

		foreach (Node2D enemy in enemyContainer.GetChildren())
		{
			if (enemy.GlobalPosition.X < (cameraLeft - CAMERA_MARGIN))
			{
				enemy.QueueFree();
			}
		}
	}

	private void TryCreateNewPlatforms()
	{
		lastPlatform = GameWorld.Instance.Platforms.LastOrDefault();

		if (lastPlatform != null)
		{
			var windowWidthHalved = GetWindow().Size.X / 2;
			var cameraRightBoundary = camera.GlobalPosition.X + windowWidthHalved + CAMERA_MARGIN;
			var cameraRightColumnX = (int)(cameraRightBoundary / Constants.SPRITE_DIMENSIONS);

			if (lastPlatform.Coordinates.Last().X == cameraRightColumnX)
			{
				gameWorldManager.GenerateAndRender();
			}
		}
	}
}
