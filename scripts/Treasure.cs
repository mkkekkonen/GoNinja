using Godot;
using System;

public partial class Treasure : Node2D
{
	private bool open = false;

	private YouWin youWin;

	public override void _Ready()
	{
		youWin = GetTree().Root.GetNode<YouWin>("Game/YouWin");
	}

	public void FinishLevel(Area2D area)
	{
		if (!open && area.Name == "NinjaArea2D")
		{
			open = true;

			var sprite = GetNode<Sprite2D>("Sprite2D");
			var treasureChestOpenTexture = GD.Load<Texture2D>("res://img/treasure2.png");
			sprite.Texture = treasureChestOpenTexture;

			Position += new Vector2(3, -3);

			youWin.Win();
		}
	}
}
