using Godot;
using System;

public partial class Player : CharacterBody3D
{
	[Export] private float sensitivity = 0.1f; // 鼠标灵敏度
	[Export] private float minVerticalAngle = -80f; // 垂直角度最小值
	[Export] private float maxVerticalAngle = 80f; // 垂直角度最大值
	private float _yaw = 0f; // 水平旋转角度
	private float head_yaw = 0f; // 水平旋转角度
	[Export] private float head_anim = 15f; // 水平旋转角度
	private float _pitch = 0f; // 垂直旋转角度

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion mouseMotionEvent)
		{
			var _mouseDelta = mouseMotionEvent.Relative;
			_yaw -= _mouseDelta.X * sensitivity;
			if (head_yaw >= -head_anim && head_yaw <= head_anim)
			{
				head_yaw -= _mouseDelta.X * sensitivity;
			}
			else
			{
				if (head_yaw <= -head_anim) head_yaw = -head_anim;
				if (head_yaw >= head_anim) head_yaw = head_anim;
			}
			// else if ((head_yaw < -10 && _mouseDelta.X < 0) ||)
			// {
			// 	head_yaw -= _mouseDelta.X * sensitivity;
			// }
			_pitch -= _mouseDelta.Y * sensitivity * 2 / 3;
			_pitch = Mathf.Clamp(_pitch, minVerticalAngle, maxVerticalAngle);

			float yaw = _yaw % 360;
			if (yaw <= 1 && yaw >= -1) _yaw = yaw;

			// 应用旋转到相机
			this.Rotation = new Vector3(0, Mathf.DegToRad(yaw), 0);
			headNode.Rotation = new Vector3(Mathf.DegToRad(_pitch), Mathf.DegToRad(head_yaw), 0);
			moniHead.Rotation = new Vector3(Mathf.DegToRad(_pitch), Mathf.DegToRad(head_yaw), 0);
			// camera.Rotation = new Vector3(Mathf.DegToRad(_pitch * 2 / 3), 0, 0);
		}
	}
	public override void _Process(double delta)
	{
		// headNode.Rotation = new Vector3(Mathf.DegToRad(_pitch), Mathf.DegToRad(_yaw), 0);
	}

	AnimationPlayer animControl;
	Node3D moduleNode;

	Camera3D camera;
	Node3D headNode;
	Node3D moniHead;
	public override void _Ready()
	{
		moduleNode = this.GetNode<Node3D>("Node");
		moniHead = this.GetNode<Node3D>("MoniHead");
		headNode = this.GetNode<Node3D>("Node/Root/AllBody/UpBody/UpperBody7/AllHead/Head2");
		camera = this.GetChild<Camera3D>(1);
		this.RemoveChild(camera);
		moniHead.AddChild(camera);

		originModuleRotation = moduleNode.Rotation;

		animControl = this.GetNode<AnimationPlayer>("AnimationPlayer");
		animControl.CurrentAnimation = "idle";
		animControl.Play();
	}


	public const float Speed = 2.0f;
	public const float JumpVelocity = 10.5f;

	bool isSprint = false;
	bool isSneak = false;


	private Vector3 originModuleRotation;
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity & Handle Jump.
		if (!IsOnFloor()) velocity += GetGravity() * (float)delta * 4;
		if (Input.IsActionJustPressed("jump") && IsOnFloor()) velocity.Y = JumpVelocity;


		isSprint = Input.IsKeyPressed(Key.Shift);
		isSneak = Input.IsKeyPressed(Key.Ctrl);
		float speed;
		if (isSneak) speed = Speed * 0.5f;
		else if (isSprint) speed = Speed * 3;
		else speed = Speed;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.


		if (Input.IsActionPressed("left"))
		{
			moduleNode.Rotation = originModuleRotation + new Vector3(0, Mathf.DegToRad(25), 0);
		}
		else if (Input.IsActionPressed("right"))
		{
			moduleNode.Rotation = originModuleRotation - new Vector3(0, Mathf.DegToRad(25), 0);
		}
		else moduleNode.Rotation = originModuleRotation;

		Vector2 inputDir = Input.GetVector("left", "right", "up", "down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X / 2, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * speed;
			velocity.Z = direction.Z * speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, speed);
		}

		if (!IsOnFloor()) AnimationChange("jump");
		else if (isSneak) AnimationChange("sneak");
		else if (direction != Vector3.Zero)
		{
			if (isSprint) AnimationChange("run");
			else AnimationChange("walk");
		}
		else AnimationChange("idle");

		Velocity = velocity;
		MoveAndSlide();
	}

	private void AnimationChange(string animationName)
	{
		if (animControl.CurrentAnimation != animationName)
		{
			animControl.CurrentAnimation = animationName;
		}
	}
}
