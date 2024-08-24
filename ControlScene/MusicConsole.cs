using Godot;
using System;

public partial class MusicConsole : Node
{
    [Signal] public delegate void StartPlayEventHandler();
    public void PlayStart() { audio.Play(); }


    private AudioStreamPlayer audio;
    private const string BASE_URI = "res://ControlScene/music/";

    public override void _EnterTree()
    {
        audio = new AudioStreamPlayer();
        this.AddChild(audio);
    }

    public override void _Ready()
    {
        this.Connect("StartPlay", new Callable(this, "PlayStart"));
        audio.Connect("finished", new Callable(this, "onFinished"));
        audio.VolumeDb = -15;


        AudioStream bgmEITW = ResourceLoader.Load<AudioStream>(
            BASE_URI + "EchoInTheWind.tres"
        );
        audio.Stream = bgmEITW;
    }

    public void onFinished()
    {
        audio.Play();
    }


}
