using Arch.Core;

public record struct Position(float X, float Y);
public record struct Velocity(float X, float Y);

public class dodgeGame{
  public World Arch_ecs { get; set; } = World.Create();


  public Entity NewGame() {
    this.Arch_ecs.Clear();
    
    var player = this.Arch_ecs.Create(new Position(0, 0));

    return player;
  }

  public void MovementSystem(double delta_time) {
    var query = new QueryDescription().WithAll<Position, Velocity>();
    this.Arch_ecs.Query(in query, (Entity e, ref Position pos, ref Velocity vel )=> {
        pos.X += vel.X;
        pos.Y += vel.Y;
        });

  }

  public Entity AddEnemy(Position pos, Velocity vel) {
    var enemy = this.Arch_ecs.Create(pos,vel);
    return enemy;
  }
}
