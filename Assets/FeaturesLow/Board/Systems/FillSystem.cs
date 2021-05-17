using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using Vector2Int=UnityEngine.Vector2Int;
using G = Entitas.Generic;
using GameEntityG = Entitas.Generic.Entity<GameScope>;
using GameStateEntityG = Entitas.Generic.Entity<GameStateScope>;

public sealed class FillSystem : ReactiveSystemApp<GameScope>
{
    public FillSystem(Contexts contexts) : base(contexts) { }

    protected override ICollector<GameEntityG> GetTrigger(IContext<GameEntityG> context)
        => context.CreateCollector(Matcher<GameScope,DestroyedG>.I);

    protected override bool Filter(GameEntityG entity) => entity.Is<DestroyedG>() && entity.Is<PieceG>();

    protected override void Execute(List<GameEntityG> entities)
    {
        var board = GameStateScope.Get<BoardG>().value;
        for (int x = 0; x < board.x; x++)
        {
            var position = new Vector2Int(x, board.y);
            var nextRowPos = BoardLogic.GetNextEmptyRow(GameScope, position);
            while (nextRowPos != board.y)
            {
                GameScope.CreateRandomPiece(x, nextRowPos);
                nextRowPos = BoardLogic.GetNextEmptyRow(GameScope, position);
            }
        }
    }
}
