using Godot;
using System;

public partial class MusicConsole : Node
{
    private AudioStreamPlayer audio;
    private const string BASE_URI = "res://ControlScene/music/";

    public override void _EnterTree()
    {
        audio = new AudioStreamPlayer();
        this.AddChild(audio);
    }

    public override void _Ready()
    {
        audio.Connect("finished", new Callable(this, "onFinished"));
        audio.VolumeDb = -20;


        AudioStream bgmEITW = ResourceLoader.Load<AudioStream>(
            BASE_URI + "EchoInTheWind.tres"
        );

        audio.Stream = bgmEITW;
        audio.Play();
    }

    public void onFinished()
    {
        audio.Play();
    }


}
