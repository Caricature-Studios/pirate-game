using Godot;
using System;

public partial class BasicCannon : Attachment
{
	bool cooldown = false;
	bool shooting = false;
	Vector2 targetPosition;
	Vector2 targetVelocity;
	Vector2 targetDestination;
	int targetHealth;
	int targetDamage;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		range = 500;
		firerate = 1;
		might = 10;
		var cooldownTimer = GetNode<Timer>("Timer");
		cooldownTimer.WaitTime = 1/firerate;
		cooldownTimer.Timeout += OnTimerTimeout;
		var rangeCollision = GetNode<CollisionShape2D>("Range");
		var rangeShape = (CircleShape2D)rangeCollision.Shape;
		rangeShape.Radius = range;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var cooldownTimer = GetNode<Timer>("Timer");
		var bulletScene = GD.Load<PackedScene>("res://Entities/Projectiles/Cannonball/Cannonball.tscn");
		var cannonBall = bulletScene.Instantiate<Cannonball>();
		cannonBall.bulletDamage = might;
		cannonBall.range = range;
		cannonBall.owner = owner;
		
		if(shooting == true) {
			if(cooldown == false) {
				cannonBall.velocity.X += ((targetPosition.X - ownerPosition.X) * (float)Math.Cos(ownerRotation*-1)) - ((targetPosition.Y - ownerPosition.Y) * (float)Math.Sin(ownerRotation*-1));
				cannonBall.velocity.Y += ((targetPosition.X - ownerPosition.X) * (float)Math.Sin(ownerRotation*-1)) + ((targetPosition.Y - ownerPosition.Y) * (float)Math.Cos(ownerRotation*-1));
				AddChild(cannonBall);
				targetHealth -= targetDamage;
				cooldown = true;
				cooldownTimer.Start();
			}
			if(targetPosition.DistanceTo(targetDestination) > 10) {
				targetPosition += targetVelocity * (float)delta;
			}
		}
		
		if(targetHealth <= 0){
			shooting = false;
		}
	}
	
	private void _on_area_2d_body_entered(Node2D body) {
		var isBoat = body.Name.ToString().Contains("Boat");
		if(shooting == false && isBoat == true && body.Name.ToString() != owner){
			var target = (EnemyCutter)body;
			targetDamage = might - target.defense;
			targetHealth = target.health + targetDamage;
			targetPosition = target.Position;
			targetDestination = target.destination();
			targetVelocity = (targetDestination - targetPosition).Normalized() * target.speed;
			shooting = true;
		} 
	}
	
	private void OnTimerTimeout() {
		cooldown = false;
	}
}








