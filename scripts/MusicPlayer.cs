using Godot;
using System;

public partial class MusicPlayer : AudioStreamPlayer
{
	public override void _Ready()
	{
		Play();
	}
}
