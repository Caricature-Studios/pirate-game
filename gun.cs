using Godot;
using System;

public partial class gun : Area2D
{
	[Export]
	public int range { get; set;} = 500;
	[Export]
	public int might { get; set;} = 10;
	[Export]
	public int cost { get; set;} = 100;
	[Export]
	public string damageType { get; set;} = "Normal";
	[Export]
	public double fireRate { get; set; } = 1;
	public Vector2 translation;
	public string owner;
	bool cooldown = false;
	bool shooting = false;
	Vector2 globalPosition;
	float globalRotation;
	public float boatRotation;
	Vector2 targetPosition;
	Vector2 targetVelocity;
	Vector2 targetDestination;
	float targetHealth = 10;
	float targetDamage;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var cooldownTimer = GetNode<Timer>("Timer");
		cooldownTimer.WaitTime = 1/fireRate;
		cooldownTimer.Timeout += OnTimerTimeout;
		var rangeCollision = GetNode<CollisionShape2D>("Range");
		var rangeShape = (CircleShape2D)rangeCollision.Shape;
		rangeShape.Radius = range;
		Position += translation;
		GD.Print("gun created");
		GD.Print(Position);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var cooldownTimer = GetNode<Timer>("Timer");
		var bulletScene = GD.Load<PackedScene>("res://Cannonball.tscn");
		var cannonBall = bulletScene.Instantiate<Cannonball>();
		cannonBall.bulletDamage = might;
		cannonBall.range = range;
		cannonBall.owner = owner;
		cannonBall.gunRotation = Rotation;
		
		var parent = (Area2D)GetParent();
		if(parent.Name.ToString().Contains("Boat")){
			globalPosition = parent.Position;
			globalRotation = parent.Rotation + boatRotation;
		}
		
		if(shooting == true) {
			if(cooldown == false) {
				cannonBall.velocity.X += ((targetPosition.X - globalPosition.X) * (float)Math.Cos(globalRotation*-1)) - ((targetPosition.Y - globalPosition.Y) * (float)Math.Sin(globalRotation*-1));
				cannonBall.velocity.Y += ((targetPosition.X - globalPosition.X) * (float)Math.Sin(globalRotation*-1)) + ((targetPosition.Y - globalPosition.Y) * (float)Math.Cos(globalRotation*-1));
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
		
		GD.Print(globalRotation);
		
	}
	
	private void OnTimerTimeout(){
		cooldown = false;
	}
	
	private void _on_area_entered(Area2D area){
		var isBoat = area.Name.ToString().Contains("Boat");
		if(shooting == false && isBoat == true && area.Name.ToString() != owner){
			var target = (Boat)area;
			if(target.hostile == true) {
				targetDamage = might - target.defense;
				targetHealth = target.health + targetDamage;
				targetPosition = target.Position;
				targetVelocity = target.velocity;
				targetDestination = target.destination;
				shooting = true;
			}
		} else {
		}
	}
	
}




