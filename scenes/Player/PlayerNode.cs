using Arch.Core;
using Arch.Core.Extensions;
using Godot;
using System;

public partial class PlayerNode : Area2D
{
  public Entity? PlayerEntity { get; set; }

  public static StringName MoveLeft = new("move_left");
  public static StringName MoveRight = new("move_right");
  public static StringName MoveUp = new("move_up");
  public static StringName MoveDown = new("move_down");

  public Action? OnHit { get; set;}

  public override void _Ready() {
    this.AreaEntered += (a) => {
      this.OnHit?.Invoke();
    };
  }

  public override void _Process(double delta) {
    if (this.PlayerEntity != null){
     ref var position = ref this.PlayerEntity.Value.Get<Position>();

     if (Input.IsActionPressed(MoveLeft))
     {
        position.X -= 1;  

     }
     if (Input.IsActionPressed(MoveRight))
     {
        position.X += 1;  

     }
     if (Input.IsActionPressed(MoveDown))
     {
        position.Y += 1;  

     }
     if (Input.IsActionPressed(MoveUp))
     {
        position.Y -= 1;  

     }
       
     this.Position = new(position.X, position.Y);

    } 
  }
 
}
