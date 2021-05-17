using Entitas;
using Entitas.Generic;

public sealed class ScoreG : 
    IComponent, 
    ICompData,
    ICopyFrom<ScoreG>,
    IUnique, 
    ICreateApply,
    Scope<GameStateScope>,
    IEvent_Any<GameStateScope, ScoreG>
{
    public int  value;
    public void CopyFrom(ScoreG other) => value = other.value;

    public ScoreG Set(int v)
    {
        value = v;
        return this;
    }
}
