using DG.Tweening;
using UnityEngine;
using GameEntityG=Entitas.Generic.Entity<GameScope>;

/// 直接被棋子Prefab引用，Prefab在AddViewSystem被克隆并Link在Entity上
public class PieceView : View
{
    public SpriteRenderer sprite;
    public float destroyDuration;

    public override void OnSelf(PositionG component, GameEntityG entity, Contexts contexts)
    {
        var value = component.value;
        transform.DOKill();
        var isTopRow = value.y == ContextHolder.I.Scope<GameStateScope>().Get<BoardG>().value.y - 1;
        if (isTopRow)
        {
            transform.localPosition = new Vector3(value.x, value.y + 1);
        }

        transform.DOMove(new Vector3(value.x, value.y, 0f), 0.3f);
    }

    protected override void destroy()
    {
        var color = sprite.color;
        color.a = 0f;
        sprite.material.DOColor(color, destroyDuration);
        gameObject.transform
            .DOScale(Vector3.one * 1.5f, destroyDuration)
            .OnComplete(base.destroy);
    }
}
