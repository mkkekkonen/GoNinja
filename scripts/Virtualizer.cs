using Godot;
using System;

public partial class Virtualizer : Node2D
{
	private readonly int TILE_SIZE = 48;
	private readonly int CAMERA_MARGIN = 200;
	private readonly int TILE_MAP_BACKGROUND_LAYER = 0;
	private readonly int TILE_MAP_FOREGROUND_LAYER = 1;
	private readonly float ENEMY_DELETION_X_THRESHOLD = -200;

	private Camera2D camera;
	private TileMap tileMap;
	private Node2D enemyContainer;

	public override void _Ready()
	{
		camera = GetNode<Camera2D>("../MovingCamera/Camera2D");
		tileMap = GetNode<TileMap>("../TileMap");
		enemyContainer = GetNode<Node2D>("../GameWorldManager/EnemyContainer");
	}

	public override void _Process(double delta)
	{
		RemoveTilesBehindCamera();
	}

	private void RemoveTilesBehindCamera()
	{
		var windowSize = GetWindow().Size;
		var windowXHalved = windowSize.X / 2;

		var cameraX = camera.GlobalPosition.X;
		var cameraLeft = cameraX - windowXHalved;

		var rightTileColumn = (int)(cameraLeft / TILE_SIZE) - 1;

		var usedRect = tileMap.GetUsedRect();

		for (var x = usedRect.Position.X; x <= rightTileColumn; x++)
		{
			for (var y = 0; y < usedRect.Size.Y; y++)
			{
				tileMap.SetCell(TILE_MAP_BACKGROUND_LAYER, new Vector2I(x, y));
				tileMap.SetCell(TILE_MAP_FOREGROUND_LAYER, new Vector2I(x, y));
			}
		}
	}
}
