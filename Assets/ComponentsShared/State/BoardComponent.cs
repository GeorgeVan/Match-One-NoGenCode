using Entitas;
using Entitas.Generic;
using Vector2Int=UnityEngine.Vector2Int;

public sealed class BoardG : 
    IComponent, 
    ICompData,
    ICopyFrom<BoardG>,
    IUnique, 
    ICreateApply,
    Scope<GameStateScope>
{
    public Vector2Int value;
    public void       CopyFrom(BoardG other) => value = other.value;

    public BoardG Set(Vector2Int v)
    {
        value = v;
        return this;
    }
}
