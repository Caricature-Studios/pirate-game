using Godot;
using System;

public partial class Cannonball : Node2D
{
	float velocityX = 0;
	float velocityY = 0;
	float speed = 50;
	public float bulletDamage;
	bool shot = false;
	public override void _Ready(){
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("click") && shot == false){
			var clickLocation = GetViewport().GetMousePosition();
			velocityX += clickLocation.X - Position.X;
			velocityY += clickLocation.Y - Position.Y;
			shot = true;
		}
		Vector2 velocity = Vector2.Zero;
		velocity.X += velocityX;
		velocity.Y += velocityY;
		velocity = velocity.Normalized() * speed;
		Position += velocity;
		
	}
	
	private void _on_area_entered(Boat area){
		area.health -= (bulletDamage - area.defense);
	}
}



