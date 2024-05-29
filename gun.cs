using Godot;
using System;

public partial class gun : Area2D
{
	[Export]
	public int range { get; set;} = 200;
	[Export]
	public int might { get; set;} = 10;
	[Export]
	public int cost { get; set;} = 100;
	[Export]
	public string damageType { get; set;} = "Normal";
	[Export]
	public double fireRate { get; set; } = 1;
	bool cooldown = false;
	bool shooting = false;
	Vector2 targetPosition;
	Vector2 targetVelocity;
	Vector2 targetDestination;
	float targetHealth;
	float targetDamage;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var cooldownTimer = GetNode<Timer>("Timer");
		cooldownTimer.Timeout += OnTimerTimeout;
		cooldownTimer.WaitTime = 1/fireRate;
		var rangeCollision = GetNode<CollisionShape2D>("Range");
		var rangeShape = (CircleShape2D)rangeCollision.Shape;
		rangeShape.Radius = range;
		var bulletScene = GD.Load<PackedScene>("res://Cannonball.tscn");
		var cannonBall = bulletScene.Instantiate<Cannonball>();
		cannonBall.bulletDamage = might;
		cannonBall.range = range;
		
		if(shooting == true && cooldown == false) {
			AddChild(cannonBall);
			cannonBall.velocity.X += targetPosition.X - Position.X;
			cannonBall.velocity.Y += targetPosition.Y - Position.Y;
			targetHealth -= targetDamage;
			cooldown = true;
			cooldownTimer.Start();
			if(targetPosition.DistanceTo(targetDestination) > 50) {
				targetPosition += targetVelocity * (float)delta;
				GD.Print(targetPosition);
				GD.Print(targetVelocity);
			}
		}
		
		if(targetHealth <= 0){
			shooting = false;
		}
		
	}
	
	private void OnTimerTimeout(){
		cooldown = false;
	}
	
	private void _on_area_entered(Boat area){
		targetDamage = might - area.defense;
		targetHealth = area.health + targetDamage;
		targetPosition = area.Position;
		targetVelocity = area.velocity;
		targetDestination = area.destination;
		shooting = true;
	}
	
}




