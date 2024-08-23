using Godot;
using System;

public partial class GlobalControl : Node
{

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (@event is InputEventKey key)
        {
            if (key.Keycode == Key.F11 && key.IsReleased())
            {
                if (GetWindow().Mode != Window.ModeEnum.Fullscreen) GetWindow().Mode = Window.ModeEnum.Fullscreen;
                else GetWindow().Mode = Window.ModeEnum.Windowed;
            }
        }
    }
}
