using Godot;
using System;


public partial class Animation : Control
{

    [Signal] public delegate void StopAnimationEventHandler();


    public override void _EnterTree()
    {
        this.Connect("StopAnimation", new Callable(this, "AnimationStop"));
        this.Visible = true;

        addOne = 1f / (frameRate * inTime);
        minusOne = 1f / (frameRate * outTime);
        frameInterval = 1.0 / frameRate;
    }

    [Export] float inTime = 1;
    [Export] float outTime = 0.5f;
    [Export] double durationTime = 10;
    private double timer = 0;
    [Export] int frameRate = 12;
    private double frameTimer = 0;


    [Export] Sprite2D iconNode;
    [Export] Sprite2D titleNode;
    ShaderMaterial iconMaterial;
    ShaderMaterial titleMaterial;
    public override void _Ready()
    {
        iconMaterial = (ShaderMaterial)iconNode.Material;
        titleMaterial = (ShaderMaterial)titleNode.Material;
        iconMaterial.SetShaderParameter("alpha", 0);
        titleMaterial.SetShaderParameter("alpha", 0.4);
    }

    float addOne;
    float minusOne;
    double frameInterval;
    public override void _Process(double delta)
    {
        timer += delta;
        frameTimer += delta;
        if (frameTimer >= frameInterval)
        {
            frameTimer %= frameInterval;
            float iconValue = (float)iconMaterial.GetShaderParameter("alpha");
            float titleValue = (float)titleMaterial.GetShaderParameter("alpha");
            if (iconValue < 1.0)
            {
                iconMaterial.SetShaderParameter("alpha", iconValue + addOne);
                titleMaterial.SetShaderParameter("alpha", titleValue + addOne / 2);
            }
            else
            {
                iconMaterial.SetShaderParameter("alpha", 1);
                titleMaterial.SetShaderParameter("alpha", 1);
            }

            Color color = this.Modulate;

            if (outFrame == true && color.A > 0)
            {
                color.A -= minusOne;
                this.Modulate = color;
                iconMaterial.SetShaderParameter("alpha", iconValue - minusOne);
                titleMaterial.SetShaderParameter("alpha", titleValue - minusOne / 1.2);
            }
        }


        if (durationTime >= 0 && timer >= durationTime) this.EmitSignal("StopAnimation");
    }


    public void AnimationStop()
    {
        outFrame = true;
    }

    private bool outFrame = false;
}
