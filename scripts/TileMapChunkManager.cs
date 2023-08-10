using Godot;
using System;

public partial class TileMapChunkManager : Node2D
{
	private readonly int CHUNK_SIZE = 24;

	private TileMap tileMap;
	private CharacterBody2D playerCharacterBody;

	private Vector2I lastChunkPosition;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameWorld.Instance.GeneratePlatforms();

		tileMap = GetNode<TileMap>("../TileMap");
		playerCharacterBody = GetNode<CharacterBody2D>("../Ninja/CharacterBody2D");

		lastChunkPosition = GetPlayerChunkPosition();

		LoadChunks();
		GameWorldRenderer.Instance.RenderPlatforms(tileMap);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var currentChunkPosition = GetPlayerChunkPosition();

		if (currentChunkPosition != lastChunkPosition)
		{
			lastChunkPosition = currentChunkPosition;

			LoadChunks();
		}
	}

	private Vector2I GetPlayerChunkPosition()
	{
		return tileMap.LocalToMap(playerCharacterBody.Position) / CHUNK_SIZE;
	}

	private void LoadChunks()
	{
		var startChunk = lastChunkPosition - Vector2I.One;
		var endChunk = lastChunkPosition + Vector2I.One;

		for (var x = startChunk.X; x <= endChunk.X; x++)
		{
			for (var y = startChunk.Y; y <= endChunk.Y; y++)
			{
				LoadChunk(x, y);
			}
		}
	}

	private void LoadChunk(int chunkX, int chunkY)
	{
		for (var x = 0; x < CHUNK_SIZE; x++)
		{
			for (var y = 0; y < CHUNK_SIZE; y++)
			{
				tileMap.SetCell(
					0,
					new Vector2I(chunkX * CHUNK_SIZE + x, chunkY * CHUNK_SIZE + y),
					0,
					new Vector2I(0, 0),
					0
				);
			}
		}
	}
}
