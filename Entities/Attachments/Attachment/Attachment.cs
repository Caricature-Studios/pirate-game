using Godot;
using System;

public partial class Attachment : Node2D
{
	[Export]
	private int range; // distance attachment operate at (in pixels)
	private int might; // strength of attachment
	private int cost;  // material cost to build attachment
	private double firerate;  // rate at which attachment operates


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
