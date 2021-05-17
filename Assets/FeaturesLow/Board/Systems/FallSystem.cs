using System.Collections.Generic;
using Entitas;
using G = Entitas.Generic;
using GameEntityG = Entitas.Generic.Entity<GameScope>;
using GameStateEntityG = Entitas.Generic.Entity<GameStateScope>;
using Vector2Int=UnityEngine.Vector2Int;

public sealed class FallSystem : ReactiveSystemApp<GameScope>
{
    public FallSystem(Contexts contexts) : base(contexts) { }

    protected override ICollector<GameEntityG> GetTrigger(IContext<GameEntityG> context)
        => context.CreateCollector(G.Matcher<GameScope, DestroyedG>.I);

    protected override bool Filter(GameEntityG entity) => entity.Is<DestroyedG>() && entity.Is<PieceG>();

    protected override void Execute(List<GameEntityG> entities)
    {
        var board = GameStateScope.Get<BoardG>().value;
        for (int x = 0; x < board.x; x++)
        {
            for (int y = 1; y < board.y; y++)
            {
                var position = new Vector2Int(x, y);
                var e = Scope.GetPieceWithPosition(position);
                if (e != null && e.Is<MovableG>())
                {
                    moveDown(e, position);
                }
            }
        }
    }

    void moveDown(GameEntityG e, Vector2Int position)
    {
        var nextRowPos = BoardLogic.GetNextEmptyRow(Scope, position);
        if (nextRowPos != position.y)
            e.Replace(G.Cache<PositionG>.I.Set(new Vector2Int(position.x, nextRowPos)));
    }
}
