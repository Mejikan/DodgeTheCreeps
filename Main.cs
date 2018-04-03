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
		
		Player player = (Player)GetNode("Player");
		Node2D startPosition = (Node2D)GetNode("StartPosition");
		player.Start(startPosition.Position);
		
		Timer startTimer = (Timer)GetNode("StartTimer");
		startTime.Start();
	}
	
	private void GameOver()
	{
	    Timer scoreTimer = (Timer)GetNode("ScoreTimer");
		scoreTimer.Stop();
		Timer mobTimer = (Timer)GetNode("MobTimer");
		mobTimer.Stop();
		
	}
	
	private void _OnStartTimerTimeout()
	{
		Timer mobTimer = (Timer)GetNode("MobTimer");
		mobTimer.Start();
		
		Timer scoreTimer = (Timer)GetNode("ScoreTimer");
		scoreTimer.Start();
	}
	
	private void _OnScoreTimerTimeout()
	{
	    score += 1;
	}
	
	private void _OnMobTimerTimeout()
	{
		Path2D mobPath = (Path2D)GetNode("MobPath");
	    PathFollow2D mobSpawnLocation = (PathFollow2D)GetNode("MobPath/MobSpawnLocation");
		mobSpawnLocation.SetOffset(new Random().NextInt());
		
		Node2D mob = Mob.Instance();
		AddChild(mob);
		
		float direction = mobSpawnLocation.Rotation;
		mob.Position = mobSpawnLocation.Position;
		direction += (float)GetRandomNumber(-1 * Mathf.PI/4, Mathf.PI/4);
		mob.Rotation = direction;
		mob.SetLinearVelocity(new Vector2(new Random.NextRange(
			mob.MinSpeed, mob.MaxSpeed), 0).Rotated(direction));
	}
	
	private double GetRandomNumber(double minimum, double maximum)
	{ 
	    Random random = new Random();
	    return random.NextDouble() * (maximum - minimum) + minimum;
	}
}

