using Entitas;
using Entitas.Generic;
using Vector2Int=UnityEngine.Vector2Int;

public sealed class PositionG
    : IComponent,
        ICompData,
        ICopyFrom<PositionG>,
        ICreateApply,
        Scope<GameScope>,
        IEvent_Self<GameScope, PositionG>,
        IEvent_SelfRemoved<GameScope, PositionG>
{
    public Vector2Int value;

    public void CopyFrom(PositionG other)
    {
        value = other.value;
    }

    public PositionG Set(Vector2Int v)
    {
        value = v;
        return this;
    }
}
