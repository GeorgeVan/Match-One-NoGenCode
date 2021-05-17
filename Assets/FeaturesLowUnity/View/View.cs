using Entitas;
using Entitas.Generic;
using Entitas.Unity;
using UnityEngine;
using GameEntityG = Entitas.Generic.Entity<GameScope>;

public class View : MonoBehaviour, IView, IOnSelf<GameScope, PositionG>, IOnSelf<GameScope, DestroyedG>
{
    public virtual void Link(IEntity entity)
    {
        gameObject.Link(entity);
        var e = (GameEntityG) entity;
        e.Add_OnSelf<GameScope, PositionG>(this);
        e.Add_OnSelf<GameScope, DestroyedG>(this);

        var pos = e.Get<PositionG>().value;
        transform.localPosition = new Vector3(pos.x, pos.y);
    }

    public virtual void OnSelf(PositionG component, Entity<GameScope> entity, Contexts contexts)
    {
        transform.localPosition = new Vector3(component.value.x, component.value.y);
    }

    public virtual void OnSelf(DestroyedG component, Entity<GameScope> entity, Contexts contexts)
    {
        destroy();
    }

    protected virtual void destroy()
    {
        gameObject.Unlink();
        Destroy(gameObject);
    }
}
