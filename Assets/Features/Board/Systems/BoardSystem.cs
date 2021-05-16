using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using G = Entitas.Generic;
using GameEntityG = Entitas.Generic.Entity<GameScope>;
using GameStateEntityG = Entitas.Generic.Entity<GameStateScope>;

public sealed class BoardSystem : ReactiveSystemG<GameStateScope>, IInitializeSystem
{
    readonly Contexts _contexts;

    readonly IGroup<G.Entity<GameScope>> _pieces;

    public BoardSystem(Contexts contexts) : base(contexts)
    {
        _contexts = contexts;
        _pieces   = contexts.GameC.GetGroup(Matcher<GameEntityG>.AllOf(Matcher<GameScope, PieceG>.I, Matcher<GameScope, PositionG>.I));
    }

    public void Initialize()
    {
        var entity = _contexts.GameStateC.CreateEntity();
        var boardSize = _contexts.config.gameConfig.value.boardSize;
        var blockerProbability = _contexts.config.gameConfig.value.blockerProbability;
        entity.Add(Cache<BoardG>.I.Set(boardSize));
        //entity.Apply(entity.Create<BoardG>().Set(boardSize)); //这个是一样的效果。

        for (int y = 0; y < boardSize.y; y++)
        {
            for (int x = 0; x < boardSize.x; x++)
            {
                if (Rand.game.Bool(blockerProbability))
                {
                    _contexts.GameC.CreateBlocker(x, y);
                }
                else
                {
                    _contexts.GameC.CreateRandomPiece(x, y);
                }
            }
        }
    }

    protected override ICollector<GameStateEntityG> GetTrigger(IContext<GameStateEntityG> context)
        => context.CreateCollector(Matcher<GameStateScope, BoardG>.I);

    protected override bool Filter(GameStateEntityG entity) => entity.Has<BoardG>();

    protected override void Execute(List<GameStateEntityG> entities)
    {
        var board = entities.SingleEntity().Get<BoardG>().value;
        foreach (var e in _pieces)
        {
            if (e.Get<PositionG>().value.x >= board.x || e.Get<PositionG>().value.y >= board.y)
            {
                e.Flag<DestroyedG>(true);
            }
        }
    }
}
