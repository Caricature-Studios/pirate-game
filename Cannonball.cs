using Godot;
using System;

public partial class Cannonball : Area2D
{
	float speed = 50;
	public float bulletDamage;
	public float range;
	bool shot = false;
	Vector2 initialPosition;
	public Vector2 velocity;
	public override void _Ready(){
		initialPosition = Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		if (Input.IsActionPressed("click") && shot == false){
			var clickLocation = GetViewport().GetMousePosition();
			velocity.X += clickLocation.X - Position.X;
			velocity.Y += clickLocation.Y - Position.Y;
			shot = true;
		}
		velocity = velocity.Normalized() * speed;
		Position += velocity;
		
		if(initialPosition.DistanceTo(Position) > range){
			QueueFree();
		}
		
	}
	
	private void _on_area_entered(Boat area){
		area.health -= (bulletDamage - area.defense);
		QueueFree();
	}
}



