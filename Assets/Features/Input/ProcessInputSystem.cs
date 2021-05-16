using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using InputEntityG = Entitas.Generic.Entity<InputScope>;

public sealed class ProcessInputSystem : ReactiveSystemG<InputScope>
{
    readonly Contexts _contexts;

    public ProcessInputSystem(Contexts contexts) : base(contexts)
    {
        _contexts = contexts;
    }

    protected override ICollector<InputEntityG> GetTrigger(IContext<InputEntityG> context)
        => context.CreateCollector(Matcher<InputScope,InputG>.I);

    protected override bool Filter(InputEntityG entity) => entity.Has<InputG>();

    protected override void Execute(List<InputEntityG> entities)
    {
        var inputEntity = entities.SingleEntity();
        var input = inputEntity.Get<InputG>();

        var e = _contexts.GameC.GetPieceWithPosition(input.value);
        if (e != null && e.Is<InteractiveG>())
        {
            e.Flag<DestroyedG>(true);
        }
    }
}
