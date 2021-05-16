using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using GameEntityG = Entitas.Generic.Entity<GameScope>;

public sealed class ScoreSystem : ReactiveSystemG<GameScope>, IInitializeSystem
{
    readonly Contexts _contexts;

    public ScoreSystem(Contexts contexts) : base(contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        _contexts.GameStateC.Set(Cache<ScoreG>.I.Set(0));
    }

    protected override ICollector<GameEntityG> GetTrigger(IContext<GameEntityG> context)
        => context.CreateCollector(Matcher<GameScope, DestroyedG>.I);

    protected override bool Filter(GameEntityG entity) => entity.Is<DestroyedG>() && entity.Is<PieceG>();

    protected override void Execute(List<GameEntityG> entities)
    {
        _contexts.GameStateC.Replace(Cache<ScoreG>.I.Set(
            _contexts.GameStateC.Get<ScoreG>().value + entities.Count));
    }
}
