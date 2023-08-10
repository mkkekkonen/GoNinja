using Godot;
using System;

public partial class TileMapManager : Node2D
{
	private TileMap tileMap;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		tileMap = GetNode<TileMap>("../TileMap");

		World.Instance.GeneratePlatforms();
		WorldRenderer.Instance.RenderBackground(tileMap);
		WorldRenderer.Instance.RenderPlatforms(tileMap);
		WorldRenderer.Instance.RenderLava(tileMap);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
