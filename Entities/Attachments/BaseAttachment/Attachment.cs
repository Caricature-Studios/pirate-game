using Godot;
using System;

public partial class Attachment : Sprite2D
{
	[Export]
	public int range; // distance attachment operate at (in pixels)
	public int might; // strength of attachment
	public int cost;  // material cost to build attachment
	public double firerate;  // rate at which attachment operates
	public string owner;
	public Vector2 ownerPosition;
	public double ownerRotation;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
