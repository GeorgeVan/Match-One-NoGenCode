using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using UnityEngine;
using GameEntityG = Entitas.Generic.Entity<GameScope>;

public sealed class AddViewSystem : ReactiveSystemG<GameScope>
{
    readonly Transform _parent;

    public AddViewSystem(Contexts contexts) : base(contexts)
    {
        _parent = new GameObject("Views").transform;
    }

    protected override ICollector<GameEntityG> GetTrigger(IContext<GameEntityG> context)
        => context.CreateCollector(Matcher<GameScope, AssetG>.I);

    protected override bool Filter(GameEntityG entity) => entity.Has<AssetG>() && !entity.Has<ViewG>();

    protected override void Execute(List<GameEntityG> entities)
    {
        foreach (var e in entities)
            e.Add<ViewG>(Cache<ViewG>.I.Set(instantiateView(e)));
    }

    IView instantiateView(GameEntityG entity)
    {
        var prefab = Resources.Load<GameObject>(entity.Get<AssetG>().value);
        var view = Object.Instantiate(prefab, _parent).GetComponent<IView>();
        view.Link(entity);
        return view;
    }
}
