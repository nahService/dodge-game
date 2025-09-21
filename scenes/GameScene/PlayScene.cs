using Godot;
using System;

public partial class PlayScene : Node
{
  public dodgeGame Game { get; set;} = new();

  [Export]
  public PlayerNode? PlayerNode2d { get; set;}

  [Export]
  public Timer? timer { get; set;}

  [Export]
  public PathFollow2D? MobSpawnLocation { get; set;}

  [Export]
  public PanelContainer? DeadLabel { get; set;}

  public override void _Ready() { 

    this.NewGame();
    initTimer();
    ArgumentNullException.ThrowIfNull(this.PlayerNode2d);
    this.PlayerNode2d.OnHit = () => {
      this.PlayerNode2d.Hide();
      this.DeadLabel?.Show();
    };
  }
  
  private void initTimer() {

    ArgumentNullException.ThrowIfNull(this.timer);
    ArgumentNullException.ThrowIfNull(this.MobSpawnLocation);

    this.timer.Timeout += () => {
      this.MobSpawnLocation.ProgressRatio = GD.Randf();

      var dir = this.MobSpawnLocation.Rotation + MathF.PI / 2;
      var velocity = new Velocity(Mathf.Cos(dir),Mathf.Sin(dir));
      var enemy_node = ResourceLoader.Load<PackedScene>("res://scenes/Enemy/enemyNode.tscn").Instantiate<EnemyNode>();

      enemy_node.Position = this.MobSpawnLocation.Position;

      var entity = this.Game.AddEnemy(new(enemy_node.Position.X, enemy_node.Position.Y), velocity);
      
      enemy_node.OnScreenExit=()=> { 
        enemy_node.QueueFree();
        this.Game.Arch_ecs.Destroy(entity); 
      };
      enemy_node.entity = entity;
      AddChild(enemy_node);
    };
  }
  public void NewGame() { 
    ArgumentNullException.ThrowIfNull(this.PlayerNode2d);
    var player = this.Game.NewGame(); 
    this.PlayerNode2d.PlayerEntity = player;
  }

  public override void _Process(double delta) {
    this.Game.MovementSystem(delta);
  }





}
