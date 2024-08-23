using Godot;
using System;

public partial class OpenScript : Node
{
    [Export] double durationTime = 3;
    private double timer = 0;
    public override void _Process(double delta)
    {
        timer += delta;
        if (timer >= durationTime)
        {
            Animation osNode = GetNode<Animation>("/root/Initialize/CanvasLayer/OpenScreen");
            osNode.EmitSignal("StopAnimation");

            // PackedScene tscn = ResourceLoader.Load<PackedScene>("res://ControlScene/Control.tscn");
            // Node node = tscn.Instantiate();
            // this.GetTree().CurrentScene.AddChild(node);

        }
    }
}
