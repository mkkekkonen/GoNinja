using System.Collections.Generic;
using Godot;

public partial class Menu : Control
{
	private int _selectedOption = 0;

	private Dictionary<int, Label> _options = new Dictionary<int, Label>();

	public override void _Ready()
	{
		_options[0] = GetNode<Label>("Panel/NewGameLabel");
		_options[1] = GetNode<Label>("Panel/HighScoresLabel");
		_options[2] = GetNode<Label>("Panel/QuitLabel");

		_options[0].Modulate = new Color(1, 1, 1);
		_options[1].Modulate = Constants.MENU_ITEM_COLOR;
		_options[2].Modulate = Constants.MENU_ITEM_COLOR;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("move_down"))
		{
			ResetOptionColors();
			_selectedOption++;
			if (_selectedOption > 2)
			{
				_selectedOption = 0;
			}
			_options[_selectedOption].Modulate = new Color(1, 1, 1);
		}
		else if (Input.IsActionJustPressed("move_up"))
		{
			ResetOptionColors();
			_selectedOption--;
			if (_selectedOption < 0)
			{
				_selectedOption = 2;
			}
			_options[_selectedOption].Modulate = new Color(1, 1, 1);
		}
		else if (Input.IsActionJustPressed("attack") || Input.IsActionJustPressed("confirm"))
		{
			switch (_selectedOption)
			{
				case 0:
					GetTree().ChangeSceneToPacked(Constants.GAME_SCENE);
					break;
				case 1:
					GetTree().ChangeSceneToPacked(Constants.HIGH_SCORES_SCENE);
					break;
				case 2:
					GetTree().Quit();
					break;
			}
		}
	}

	private void ResetOptionColors()
	{
		_options[_selectedOption].Modulate = Constants.MENU_ITEM_COLOR;
	}
}
