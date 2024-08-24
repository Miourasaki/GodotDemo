using Godot;
using System;

public partial class BackgroundAnim : TextureRect
{

	[Export] int frameRate = 15;
	[Export] float Speed = 10;
	private double frameTimer = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Visible = true;

		this.Size = GetWindow().Size + new Vector2(150, 150);
		this.Position = -GetWindow().Size / 2;
		originSize = GetWindow().Size;
		originPosition = this.Position;


		frameInterval = 1.0 / frameRate;
		moveInterval = 150 / (frameRate * Speed);
	}
	private double frameInterval;
	private float moveInterval;


	private Vector2 originPosition;
	private Vector2 originSize;

	private float count = 0;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (GetWindow().Size != originSize)
		{
			this.Size = GetWindow().Size + new Vector2(150, 150);
			this.Position = -GetWindow().Size / 2;
			originSize = GetWindow().Size;
			originPosition = this.Position;
			count = 0;
			this.Position = originPosition;
		}

		frameTimer += delta;
		if (frameTimer >= frameInterval)
		{
			frameTimer %= frameInterval; count += moveInterval;
			if (count >= 150)
			{
				count = 0;
				this.Position = originPosition;
			}
			this.Position -= new Vector2(moveInterval, moveInterval);

		}
	}
}
