using Godot;
using System;

public partial class RigidBody2d : RigidBody2D
{


    AnimatedSprite2D texture;
    public override void _Ready()
    {
        this.LockRotation = true;

        texture = this.GetChild<AnimatedSprite2D>(0);
        texture.Animation = "idle";
        texture.Play();
    }
    public override void _PhysicsProcess(double delta)
    {
        this.LinearVelocity = new Vector2(Input.GetAxis("left", "right") * 600, this.LinearVelocity.Y);
        if (Input.IsActionJustPressed("jump")) this.LinearVelocity = new Vector2(this.LinearVelocity.X, -800);
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionPressed("left") || Input.IsActionPressed("right")) texture.Animation = "run";
        else texture.Animation = "idle";

        if (Input.IsActionJustPressed("left")) leftStatus = true;
        else if (Input.IsActionJustPressed("right")) leftStatus = false;
        if (leftStatus) this.Scale = new Vector2(-1, 1);
        else this.Scale = new Vector2(1, 1);

    }

    bool leftStatus = false;
}
