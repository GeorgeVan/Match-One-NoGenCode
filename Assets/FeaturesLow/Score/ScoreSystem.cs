using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using GameEntityG = Entitas.Generic.Entity<GameScope>;

public sealed class ScoreSystem : ReactiveSystemApp<GameScope>, IInitializeSystem
{
    public ScoreSystem(Contexts contexts) : base(contexts) { }

    public void Initialize()
    {
        GameStateScope.Set(Cache<ScoreG>.I.Set(0));
    }

    protected override ICollector<GameEntityG> GetTrigger(IContext<GameEntityG> context)
        => context.CreateCollector(Matcher<GameEntityG>.AllOf(Matcher<GameScope, DestroyedG>.I, Matcher<GameScope, PieceG>.I).NoneOf(Matcher<GameScope, KilledBuySysG>.I));

    protected override bool Filter(GameEntityG entity) => entity.Is<DestroyedG>() && entity.Is<PieceG>() && !entity.Is<KilledBuySysG>();

    protected override void Execute(List<GameEntityG> entities)
    {
        GameStateScope.Replace(Cache<ScoreG>.I.Set(
            GameStateScope.Get<ScoreG>().value + entities.Count));
    }
}
