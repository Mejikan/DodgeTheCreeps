using Godot;
using System;

public class Player : Area2D
{
	[Export]
    public int Speed = 0;
	
	private Vector2 velocity = new Vector2();
	
	private Vector2 screensize;
	
	[Signal]
	delegate void Hit();

    public override void _Ready()
    {
		Hide();
        screensize = GetViewportRect().Size;
    }
	
	public override void _Process(float delta)
	{
		velocity = new Vector2();
		
		if (Input.IsActionPressed("ui_right"))
		{
			velocity.x += 1;
		}
		if (Input.IsActionPressed("ui_left"))
		{
			velocity.x -= 1;
		}
		if (Input.IsActionPressed("ui_down"))
		{
			velocity.y += 1;
		}
		if (Input.IsActionPressed("ui_up"))
		{
			velocity.y -= 1;
		}
		
		AnimatedSprite animatedSprite = GetNode("AnimatedSprite") as AnimatedSprite;
		Particles2D trail = GetNode("Trail") as Particles2D;
		if (velocity.Length() > 0)
		{
			animatedSprite.Play();
			trail.Emitting = true;
			velocity = velocity.Normalized() * Speed;
		}
		else 
		{
			animatedSprite.Stop();
			trail.Emitting = false;
		}
		
		Position += velocity * delta;
		
		Position = new Vector2(Mathf.Clamp(Position.x, 0, screensize.x), Mathf.Clamp(Position.y, 0, screensize.y));
		
		if (velocity.x != 0)
		{
			animatedSprite.Animation = "right";
			animatedSprite.FlipV = false;
			animatedSprite.FlipH = velocity.x < 0;
		}
		if (velocity.y != 0)
		{
			animatedSprite.Animation = "up";
			animatedSprite.FlipV = velocity.y > 0;
			animatedSprite.FlipH = false;
		}
	}
	
	private void _OnPlayerBodyEntered(Godot.Object body)
	{
	    Hide();
		EmitSignal(nameof(Hit));
		CallDeferred("set_monitoring", false);
	}
	
	public void Start(Vector2 position)
	{
		Position = position;
		Show();
		Monitoring = true;
	}
}
