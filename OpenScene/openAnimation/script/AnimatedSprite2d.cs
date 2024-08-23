using Godot;
using System;

public partial class AnimatedSprite2d : AnimatedSprite2D
{

    public override void _Ready()
    {
        this.Play("idle");
    }
}
