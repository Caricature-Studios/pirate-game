using Godot;
using System;

public partial class Boat : Area2D {
	[Export]
	public float speed { get; set; } = 400;
	[Export]
	public float health { get; set; } = 50;
	[Export]
	public float defense { get; set; } = 2;
	[Export]
	public bool hostile { get; set; } = true;
	public Vector2 translation;
	public Vector2 velocity = Vector2.Zero;
	public Vector2 destination = Vector2.Zero;
	public bool leftGun = false;
	public bool rightGun = true;
	public bool selected = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("boat readying");
		var gunScene = GD.Load<PackedScene>("res://gun.tscn");
		
		if(leftGun == true) {
			GD.Print("gun added");
			var gun1 = gunScene.Instantiate<gun>();
			gun1.translation.X -= 15;
			gun1.translation.Y -= 25;
			gun1.Rotation -= (float)1.55;
			gun1.boatRotation = (float)-1.55;
			gun1.owner = Name;
			AddChild(gun1);
		}
		if(rightGun == true) {
			GD.Print("gun added");
			var gun2 = gunScene.Instantiate<gun>();
			gun2.translation.X -= 15;
			gun2.translation.Y += 25;
			gun2.Rotation += (float)1.55;
			gun2.boatRotation = (float)1.55;
			gun2.owner = Name;
			AddChild(gun2);
		}
		Position += translation;
		GD.Print(Name);
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
				Rotation = (float)Math.Atan(velocity.Y / velocity.X);
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
		GD.Print(Position);
		
		if (velocity != Vector2.Zero && Position.DistanceTo(destination) < 10) {
			velocity -= velocity;
		}
		
		if (health < 0) {
			QueueFree();
		}
	}
}









