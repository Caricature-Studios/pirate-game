using Godot;
using System;

public partial class MainBoat : CharacterBody2D
{
	private NavigationAgent2D _agent;
    private Sprite2D _boatBody;
    private Attachment _portAttachment;
    private Attachment _starboardAttachment;

    [Export]
    private bool _isSelected;

	private float _speed = 300.0f;



	public override void _Ready()
    {
        _agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
        _boatBody = GetNode<Sprite2D>("BoatBody");
        _portAttachment = GetNode<Attachment>("PortAttachment");
        _starboardAttachment = GetNode<Attachment>("StarboardAttachment");

    }

    public override void _Input(InputEvent @event)
    {
        //if (
        //    @event is InputEventMouseButton e &&
        //    !e.Pressed &&
        //    e.ButtonIndex == MouseButton.Right
        //) {
        //    var map = GetWorld2D().NavigationMap;
        //    var p = NavigationServer2D.MapGetClosestPoint(map, e.Position);
        //    _agent.TargetPosition = p;
        //}
    }
    public override void _PhysicsProcess(double delta)
    {
        if (_agent.IsNavigationFinished())
            return;

        LookAt(_agent.GetNextPathPosition());
        Vector2 diff = _agent.GetNextPathPosition() - GlobalPosition;
        Vector2 dir = diff.Normalized();
        Velocity = dir * _speed;
        MoveAndSlide();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (_isSelected)
            _boatBody.Modulate = Color.Color8(0,0,255);
        else
         _boatBody.Modulate = Color.Color8(255,255,255);
    }
}
