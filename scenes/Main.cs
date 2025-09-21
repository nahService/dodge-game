using Godot;
using System;

public partial class Main : Node
{
  [Export]
  public Button? buttonExit {
	get; set;
  }

  [Export]
  public Button? buttonNewGame {
	get; set;
  }

  public override void _Ready() {

	  ArgumentNullException.ThrowIfNull(this.buttonExit);
    ArgumentNullException.ThrowIfNull(this.buttonNewGame);

	  this.buttonExit.Pressed += () => { GetTree().Quit(); };

    this.buttonNewGame.Pressed += () => {
      var play_Scene = ResourceLoader.Load<PackedScene>("res://scenes/GameScene/playScene.tscn").Instantiate<PlayScene>();

      AddChild(play_Scene);
    };

  }
}
