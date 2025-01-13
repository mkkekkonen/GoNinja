using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public partial class GamePaused : Node2D
{
	private PackedScene mainMenu;

	public override void _Ready() {
		mainMenu = ResourceLoader.Load<PackedScene>("res://scenes/menu.tscn");
	}

    public override void _Process(double delta)
    {
		if (GameState.CountdownValue != 0) {
			return;
		}

		if (GameState.Won || GameState.GameOver) {
			return;
		}

        if (Input.IsActionJustPressed("escape"))
		{
			if (!GetTree().Paused) {
				Visible = true;
				GetTree().Paused = true;
			}
			else {
				GetTree().Paused = false;
				GetTree().ChangeSceneToPacked(mainMenu);
			}
		}

		if (Visible && Input.IsActionJustPressed("confirm")) {
			Visible = false;
			GetTree().Paused = false;
		}
    }
}
