using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class Spawner : Node3D
{
    [Export]
    public PackedScene EnemyScene;

    public Gate Gate;
    public bool isLevelFinished;

    public Node3D[] SpawnPoints = { };
    public List<Node3D> EnemiesNodes = new List<Node3D>();
    public bool HasSpawned;
    public Area3D SpawnTriggerZone;

    public override void _Ready()
    {
        SpawnPoints = GetNode("SpawnPoints").GetChildren().OfType<Node3D>().ToArray();
        SpawnTriggerZone = GetNode<Area3D>("SpawnTriggerZone");
        SpawnTriggerZone.BodyEntered += OnSpawnTriggerZoneBodyEntered;
        Gate = GetNode<Gate>("Gate");
    }

    public void OnSpawnTriggerZoneBodyEntered(Node3D body)
    {
        if (HasSpawned)
            return;

        if (body.IsInGroup("player"))
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        foreach (var point in SpawnPoints)
        {
            SpawnEnemyAt(point);
        }

        HasSpawned = true;
    }

    public void SpawnEnemyAt(Node3D targetPoint)
    {
        var enemy = EnemyScene.Instantiate<Enemy>();
        GetTree().Root.GetNode<Node3D>("Node3D").AddChild(enemy);
        enemy.DeadState.EnemyDeath += OnEnemyDeath;
        enemy.GlobalPosition = targetPoint.GlobalPosition;
        EnemiesNodes.Add(enemy);
    }

    public void OnEnemyDeath(Enemy enemy)
    {
        EnemiesNodes.Remove(enemy);
        if (EnemiesNodes.Count == 0)
        {
            isLevelFinished = true;
            Gate.Open();
        }
    }
}
