using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using UnityEngine;
using G = Entitas.Generic;
using GameEntityG = Entitas.Generic.Entity<GameScope>;
using GameStateEntityG = Entitas.Generic.Entity<GameStateScope>;

public sealed class FillSystem : ReactiveSystemG<GameScope>
{
    readonly Contexts _contexts;

    public FillSystem(Contexts contexts) : base(contexts)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntityG> GetTrigger(IContext<GameEntityG> context)
        => context.CreateCollector(Matcher<GameScope,DestroyedG>.I);

    protected override bool Filter(GameEntityG entity) => entity.Is<DestroyedG>() && entity.Is<PieceG>();

    protected override void Execute(List<GameEntityG> entities)
    {
        var board = _contexts.GameStateC.Get<BoardG>().value;
        for (int x = 0; x < board.x; x++)
        {
            var position = new Vector2Int(x, board.y);
            var nextRowPos = BoardLogic.GetNextEmptyRow(_contexts, position);
            while (nextRowPos != board.y)
            {
                _contexts.GameC.CreateRandomPiece(x, nextRowPos);
                nextRowPos = BoardLogic.GetNextEmptyRow(_contexts, position);
            }
        }
    }
}
