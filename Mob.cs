using Godot;
using System;

public class Mob : RigidBody2D
{
    [Export]
	public int MinSpeed;
	[Export]
	public int MaxSpeed;
	
	private string[] mobTypes = {"fly", "swim", "walk"};

    public override void _Ready()
    {
        AnimatedSprite animatedSprite = (AnimatedSprite)GetNode("AnimatedSprite");
        Random rand = new Random();
		animatedSprite.Animation  = mobTypes[rand.Next(2) % mobTypes.Length];
        
    }
	
	private void _OnVisibilityNotifier2DScreenExited()
	{
		QueueFree();
	}

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
