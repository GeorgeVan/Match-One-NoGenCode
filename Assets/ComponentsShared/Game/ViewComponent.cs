using Entitas;
using Entitas.Generic;

public sealed class ViewG
    : IComponent,
        ICompData,
        ICopyFrom<ViewG>,
        ICreateApply,
        Scope<GameScope>
{
    public IView value;

    public void CopyFrom(ViewG other)
    {
        value = other.value;
    }

    public ViewG Set(IView v)
    {
        value = v;
        return this;
    }
}
