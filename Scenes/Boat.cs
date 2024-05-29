using Godot;
using System;

public partial class Boat : Area2D {
	[Export]
	public float speed { get; set; } = 400;
	[Export]
	public float health { get; set; } = 50;
	[Export]
	public float defense { get; set; } = 2;
	Vector2 velocity = Vector2.Zero;
	Vector2 destination = Vector2.Zero;
	bool selected = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var ScreenSize = GetViewportRect().Size;
		var boatSprite = GetNode<AnimatedSprite2D>("Boat_Sprite");
		boatSprite.Play();
		if (Input.IsActionPressed("click")){
			var clickLocation = GetViewport().GetMousePosition();
			if(selected == false && clickLocation.X < Position.X + 100 && clickLocation.X > Position.X - 100 && clickLocation.Y < Position.Y + 100 && clickLocation.Y > Position.Y - 100){
				selected = true;
				boatSprite.Animation = "Selected";
			}
			if(selected == true && (Math.Abs(clickLocation.X - Position.X) > 100 || Math.Abs(clickLocation.Y - Position.Y) > 100)){
				destination = clickLocation;
				velocity.X += (destination.X - Position.X);
				velocity.Y += (destination.Y - Position.Y);
				Rotation = (float)Math.Atan(velocity.Y / velocity.X) + (float)1.55;
				if (velocity.X < 0){
					Rotation += (float)3.14;
				}
				selected = false;
				boatSprite.Animation = "Unselected";
			}
		}
		
		velocity = velocity.Normalized() * speed;
		Position += velocity * (float)delta;	
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
		
		if (velocity != Vector2.Zero && Position.X < destination.X + 50 && Position.X > destination.X - 50 && Position.Y < destination.Y + 50 && Position.Y > destination.Y - 50) {
			velocity -= velocity;
		}
		
		if (health < 0) {
			QueueFree();
		}
	}
}









