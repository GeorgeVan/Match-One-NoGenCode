using Entitas;
using Entitas.Generic;
using Vector2Int=UnityEngine.Vector2Int;

public sealed class InputG
    : IComponent,
        ICompData,
        ICopyFrom<InputG>,
        ICreateApply,
        Scope<InputScope>
{
    public Vector2Int value;

    public void CopyFrom(InputG other)
    {
        value = other.value;
    }

    public InputG Set(Vector2Int v)
    {
        value = v;
        return this;
    }
}