using Godot;
using System;

public partial class MainBoat : CharacterBody2D
{
	private NavigationAgent2D _agent;
	private Attachment _portAttachment;
	private Attachment _starboardAttachment;

	private float _speed = 300.0f;
	public int defense = 2;
	public int health = 50;

	public override void _Ready()
	{
		_agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		_portAttachment = GetNode<Attachment>("PortAttachment");
		_starboardAttachment = GetNode<Attachment>("StarboardAttachment");
		_portAttachment.owner = Name;
		_starboardAttachment.owner = Name;
	}

	public override void _Input(InputEvent @event)
	{
		if (
			@event is InputEventMouseButton e &&
			!e.Pressed &&
			e.ButtonIndex == MouseButton.Right
		) {
			var map = GetWorld2D().NavigationMap;
			var p = NavigationServer2D.MapGetClosestPoint(map, e.Position);
			_agent.TargetPosition = p;
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		_portAttachment = GetNode<Attachment>("PortAttachment");
		_starboardAttachment = GetNode<Attachment>("StarboardAttachment");
		if (_agent.IsNavigationFinished())
			return;

		LookAt(_agent.GetNextPathPosition());
		Vector2 diff = _agent.GetNextPathPosition() - GlobalPosition;
		Vector2 dir = diff.Normalized();
		Velocity = dir * _speed;
		MoveAndSlide();
		_portAttachment.ownerPosition.X = Position.X;
		_starboardAttachment.ownerPosition.X = Position.X;
		_portAttachment.ownerPosition.Y = Position.Y;
		_starboardAttachment.ownerPosition.Y = Position.Y;
		_portAttachment.ownerRotation = Rotation - 1.5;
		_starboardAttachment.ownerRotation = Rotation + 1.5;
	}
	
	public Vector2 destination() {
		return (Vector2)_agent.TargetPosition;
	}
}
