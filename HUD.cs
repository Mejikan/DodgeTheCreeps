using Godot;
using System;

public class HUD : CanvasLayer
{
    [Signal]
	delegate void StartGame();
	
	public void ShowMessage(string message)
	{
		Label messageLabel = GetNode("MessageLabel") as Label;
		messageLabel.Text = message;
		messageLabel.Show();
		
		Timer messageTimer = GetNode("MessageTimer") as Timer;
		messageTimer.Start();
	}
	
	public async void GameOver()
	{
		ShowMessage("GameOver");
		
		Timer messageTimer = GetNode("MessageTimer") as Timer;
		await ToSignal(messageTimer, "timeout");
		
		Button startButton = GetNode("StartButton") as Button;
		startButton.Show();
		
		Label messageLabel = GetNode("MessageLabel") as Label;
		messageLabel.Text = "Dodge the\nCreeps!";
		messageLabel.Show();
	}
	
	public void UpdateScore(int score)
	{
		Label scoreLabel = GetNode("ScoreLabel") as Label;
		scoreLabel.Text = score.ToString();
	}
	
	private void _OnMessageTimerTimeout()
	{
	    Label messageLabel = GetNode("MessageLabel") as Label;
		messageLabel.Hide();
	}
	
	private void _OnStartButtonPressed()
	{
	    Button startButton = GetNode("StartButton") as Button;
		startButton.Hide();
		EmitSignal(nameof(StartGame));
	}

}
