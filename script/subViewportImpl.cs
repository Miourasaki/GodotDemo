using Godot;
using System;

public partial class subViewportImpl : SubViewport
{
	[Export]
	public double frameRate = 12;
	private double timer = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timer += delta;
		double frameInterval = 1.0 / frameRate;
		if (timer >= frameInterval)
		{
			timer %= frameInterval;
			this.RenderTargetUpdateMode = UpdateMode.Once;

		}

	}
}
