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
		
		Player player = GetNode("Player") as Player;
		Position2D startPosition = GetNode("StartPosition") as Position2D;
		player.Start(startPosition.Position);
		
		Timer startTimer = (Timer)GetNode("StartTimer");
		startTimer.Start();
		
		HUD hud = GetNode("HUD") as HUD;
		hud.ShowMessage("Get Ready");
		hud.UpdateScore(score);
		
		AudioStreamPlayer music = GetNode("Music") as AudioStreamPlayer;
		music.Play();
	}
	
	private void GameOver()
	{
	    Timer scoreTimer = (Timer)GetNode("ScoreTimer");
		scoreTimer.Stop();
		Timer mobTimer = (Timer)GetNode("MobTimer");
		mobTimer.Stop();
		
		HUD hud = GetNode("HUD") as HUD;
		hud.GameOver();
		
		AudioStreamPlayer deathSound = GetNode("DeathSound") as AudioStreamPlayer;
		deathSound.Play();
		
		AudioStreamPlayer music = GetNode("Music") as AudioStreamPlayer;
		music.Stop();
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
		HUD hud = GetNode("HUD") as HUD;
		hud.UpdateScore(score);
	}
	
	private void _OnMobTimerTimeout()
	{
		Path2D mobPath = GetNode("MobPath") as Path2D;
	    PathFollow2D mobSpawnLocation = GetNode("MobPath/MobSpawnLocation") as PathFollow2D;
		mobSpawnLocation.SetOffset(new Random().Next());
		
		Mob mob = Mob.Instance() as Mob;
		AddChild(mob);
		
		float direction = mobSpawnLocation.Rotation;
		mob.Position = mobSpawnLocation.Position;
		direction += (float)GetRandomNumber(-1 * Mathf.PI/4, Mathf.PI/4);
		mob.Rotation = direction;
		mob.SetLinearVelocity(new Vector2(new Random().Next(
			mob.MinSpeed, mob.MaxSpeed), 0).Rotated(direction));
	}
	
	private double GetRandomNumber(double minimum, double maximum)
	{ 
	    Random random = new Random();
	    return random.NextDouble() * (maximum - minimum) + minimum;
	}
}
