using Godot;
using System;

public partial class main : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("scene readying");
		var boatScene = GD.Load<PackedScene>("res://Scenes/Boat.tscn");
		var boat1 = boatScene.Instantiate<Boat>();
		var boat2 = boatScene.Instantiate<Boat>();
		boat1.rightGun = true;
		boat1.hostile = false;
		boat1.Name = "Boat1";
		boat2.translation.X += 500;
		boat2.Name = "Boat2";
		AddChild(boat1);
		AddChild(boat2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
