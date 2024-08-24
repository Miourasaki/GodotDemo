using Godot;
using System;

public partial class OpenScript : Node
{
    [Export] double durationTime = 3;
    private double timer = 0;
    private int count = 0;
    public override void _Process(double delta)
    {
        if (count <= 2) timer += delta;
        if (timer >= durationTime)
        {
            count++; timer = 0;
            if (count == 1) this.loadFinish();
            if (count == 2) this.GetTree().ChangeSceneToFile("res://tscn/3dGame/node_3d.tscn");
            // PackedScene tscn = ResourceLoader.Load<PackedScene>("res://ControlScene/Control.tscn");
            // Node node = tscn.Instantiate();
            // this.GetTree().CurrentScene.AddChild(node);

        }
    }

    private void loadFinish()
    {
        Animation osNode = GetNode<Animation>("/root/Initialize/OpenScreen");
        osNode.EmitSignal("StopAnimation");
        Node musicNode = GetNode<Node>("/root/MusicConsole");
        musicNode.EmitSignal("StartPlay");
    }
}
