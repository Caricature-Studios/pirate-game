using Godot;
using System;

public partial class Cannonball : Area2D
{
	float speed = 400;
	public int bulletDamage;
	public float range;
	public string owner;
	bool shot = false;
	Vector2 initialPosition;
	public Vector2 velocity;
	
	public override void _Ready(){
		initialPosition = Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		velocity = velocity.Normalized() * speed;
		Position += velocity*(float)delta;
		if(initialPosition.DistanceTo(Position) > range){
			QueueFree();
		}
		
	}
	
	private void _on_body_entered(Node2D body) {
		var isBoat = body.Name.ToString().Contains("Boat");
		if(isBoat && body.Name != owner) {
			var boat = (EnemyCutter)body;
			boat.health -= (bulletDamage - boat.defense);
			QueueFree();
		}
	}
}






