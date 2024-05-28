using Godot;
using System;

public partial class gun : Node2D
{
	[Export]
	public int range { get; set;} = 200;
	[Export]
	public int might { get; set;} = 10;
	[Export]
	public int cost { get; set;} = 100;
	[Export]
	public string damageType { get; set;} = "Normal";
	bool cooldown = false;
	public double fireRate { get; set; } = 5;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var cooldownTimer = GetNode<Timer>("Timer");
		cooldownTimer.Timeout += OnTimerTimeout;
		var bulletScene = GD.Load<PackedScene>("res://Cannonball.tscn");
		var cannonBall = bulletScene.Instantiate<Cannonball>();
		cannonBall.bulletDamage = might;
		if (Input.IsActionPressed("click")){
			var clickLocation = GetViewport().GetMousePosition();
			if(cooldown == false) {
				AddChild(cannonBall);
				cooldown = true;
				cooldownTimer.Start();
			}
		}
		
	}
	
	private void OnTimerTimeout(){
		cooldown = false;
	}
	
}

