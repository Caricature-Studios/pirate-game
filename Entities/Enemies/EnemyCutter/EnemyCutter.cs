using Godot;
using System;

public partial class EnemyCutter : CharacterBody2D
{
	private NavigationAgent2D _agent;
	private Attachment _portAttachment;
	private Attachment _starboardAttachment;

	public float speed = 300.0f;
	public int defense = 2;
	[Export]
	public int health = 50;
	private Vector2 _island;

	public override void _Ready()
	{
		_agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		_portAttachment = GetNode<Attachment>("PortAttachment");
		_starboardAttachment = GetNode<Attachment>("StarboardAttachment");
		_island.X += 650;
		_island.Y += 350;
		_agent.TargetPosition = _island;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_agent.IsNavigationFinished())
			return;

		LookAt(_agent.GetNextPathPosition());
		Vector2 diff = _agent.GetNextPathPosition() - GlobalPosition;
		Vector2 dir = diff.Normalized();
		Velocity = dir * speed;
		MoveAndSlide();
	}
	
	public override void _Process(double delta) 
	{
		if (health < 0) {
			QueueFree();
		}
	}
	
	public Vector2 destination() {
		return _agent.TargetPosition;
	}
}
