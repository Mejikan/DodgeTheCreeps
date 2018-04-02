using Godot;
using System;

public class Main : Node
{
    [Export]
	public PackedScene Mob;
	
	private int score;

    public override void _Ready()
    {
        
        
    }

	public void NewGame()
	{
		score = 0;
		
		Node2D player = (Node2D)GetNode("Player");
		Node2D startPosition = (Node2D)GetNode("StartPosition");
		player.Start(startPosition.Position);
		
		Node2D startTimer = (Node2D)GetNode("StartTimer");
		startTime.Start();
	}
	
	private void GameOver()
	{
	    Node2D scoreTimer = (Node2D)GetNode("ScoreTimer");
		scoreTimer.Stop();
		Node2D mobTimer = (Node2D)GetNode("MobTimer");
		mobTimer.Stop();
		
	}
	
	private void _OnStartTimerTimeout()
	{
		Node2D mobTimer = (Node2D)GetNode("MobTimer");
		mobTimer.Start();
		
		Node2D scoreTimer = (Node2D)GetNode("ScoreTimer");
		scoreTimer.Start();
	}
	
	private void _OnScoreTimerTimeout()
	{
	    score += 1;
	}
	
	private void _OnMobTimerTimeout()
	{
		Node2D mobPath = (Node2D)GetNode("MobPath");
	    Node2D mobSpawnLocation = (Node2D)GetNode("MobPath/MobSpawnLocation");
		mobSpawnLocation.SetOffset(new Random().NextInt());
		
		Node2D mob = Mob.Instance();
		AddChild(mob);
		
		
		float direction = mobSpawnLocation.Rotation;
		mob.Position = mobSpawnLocation.Position;
		
		direction += (float)GetRandomNumber();
	}
	
	private double GetRandomNumber(double minimum, double maximum)
	{ 
	    Random random = new Random();
	    return random.NextDouble() * (maximum - minimum) + minimum;
	}
}

