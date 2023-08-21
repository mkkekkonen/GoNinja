using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public partial class GameWorldManager : Node2D
{
	private readonly int ENEMY_SCORE_INCREMENT = 10;

	private TileMap tileMap;
	private Node2D enemyContainer;
	private readonly List<IEnemy> enemies = new();

	[Signal]
	public delegate void EnemyAreaEnteredEventHandler(string enemyInstanceId);

	public List<IEnemy> Enemies
	{
		get
		{
			return enemies;
		}
	}

	public override void _Ready()
	{
		tileMap = GetNode<TileMap>("../TileMap");
		enemyContainer = GetNode<Node2D>("EnemyContainer");

		GameState.Reset();

		GameWorld.Instance.GeneratePlatforms();

		GameWorldRenderer.Instance.Seed();

		GameWorldRenderer.Instance.RenderBackground(tileMap);
		GameWorldRenderer.Instance.RenderPlatforms(tileMap);

		GameWorldRenderer.Instance.SpawnEnemies(enemyContainer, tileMap, enemies);
		GameWorldRenderer.Instance.CreateTreasure(this, tileMap);
	}

	private void OnEnemyAreaEntered(string enemyGuid)
	{
		foreach (var enemy in enemies)
		{
			if (enemy != null && enemy.GetGuid() == enemyGuid)
			{
				GameState.Score += ENEMY_SCORE_INCREMENT;
				enemies.Remove(enemy);
				enemy.Destroy();
				break;
			}
		}
	}
}
