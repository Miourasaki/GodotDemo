using Godot;
using System;

public partial class AnimationSize : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float XzoomNumber = GetWindow().Size.X / 1152;
		float YzoomNumber = GetWindow().Size.Y / 648;
		float zoomNumber = Math.Min(XzoomNumber, YzoomNumber);
		// this.Scale = new Vector2(zoomNumber, zoomNumber);
	}
}
