using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using G = Entitas.Generic;
using GameEntityG = Entitas.Generic.Entity<GameScope>;
using GameStateEntityG = Entitas.Generic.Entity<GameStateScope>;

public sealed class BoardSystem : ReactiveSystemApp<GameStateScope>, IInitializeSystem
{
    readonly IGroup<G.Entity<GameScope>> _pieces;

    public BoardSystem(Contexts contexts) : base(contexts) =>
        _pieces = GameScope.GetGroup(Matcher<GameEntityG>.AllOf(Matcher<GameScope, PieceG>.I, Matcher<GameScope, PositionG>.I));

    public void Initialize() => 
        GameStateScope.Set(Cache<BoardG>.I.Set(GameConfigScope.Get<GameConfigG>().value.boardSize));

    protected override ICollector<GameStateEntityG> GetTrigger(IContext<GameStateEntityG> context) => 
        context.CreateCollector(Matcher<GameStateScope, BoardG>.I);

    protected override bool Filter(GameStateEntityG entity) => entity.Has<BoardG>();

    protected override void Execute(List<GameStateEntityG> entities)
    {
        //复位棋盘前，先把当前的全部删掉
        foreach (var e in _pieces)
        {
            e.Flag<KilledBySysG>(true);
            e.Flag<DestroyedG>(true);
        }

        var boardSize = entities.SingleEntity().Get<BoardG>().value;
        var blockerProbability = GameConfigScope.Get<GameConfigG>().value.blockerProbability;
        for (int y = 0; y < boardSize.y; y++)
        {
            for (int x = 0; x < boardSize.x; x++)
            {
                if (Rand.game.Bool(blockerProbability))
                    GameScope.CreateBlocker(x, y);
                else
                    GameScope.CreateRandomPiece(x, y);
            }
        }
    }
}
