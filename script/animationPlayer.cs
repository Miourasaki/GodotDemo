using Godot;
using System;

public partial class animationPlayer : AnimationPlayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Play("RESET");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
