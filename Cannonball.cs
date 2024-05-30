using Godot;
using System;

public partial class Cannonball : Area2D
{
	float speed = 400;
	public float bulletDamage;
	public float range;
	public float gunRotation;
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
	
	private void _on_area_entered(Area2D area){
		var isBoat = area.Name.ToString().Contains("Boat");
		if(isBoat && area.Name != owner) {
			var boat = (Boat)area;
			boat.health -= (bulletDamage - boat.defense);
			QueueFree();
		}
	}
}



