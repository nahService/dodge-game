using Arch.Core;
using Arch.Core.Extensions;
using Godot;
using System;

public partial class EnemyNode : Area2D
{
  [Export]
  public VisibleOnScreenNotifier2D? ScreenNotifier2D { get; set;}
  public Entity? entity { get; set; }
  public Action? OnScreenExit { get; set;}
  
  public override void _Ready() {
	ArgumentNullException.ThrowIfNull(this.ScreenNotifier2D);
	this.ScreenNotifier2D.ScreenExited += () => {

	  if (this.entity != null) {
		this.OnScreenExit.Invoke();
	  }
	};  
  }

  public override void _Process(double delta) {
	if (this.entity != null && this.entity.Value.IsAlive()){
	 ref var position = ref this.entity.Value.Get<Position>();
	 this.Position = new(position.X, position.Y);

	} 
  }
}
