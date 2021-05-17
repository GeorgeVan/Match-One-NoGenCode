using Entitas;
using Entitas.Generic;
using Vector2Int=UnityEngine.Vector2Int;

public interface IGameConfig
{
    Vector2Int boardSize { get; }
    float blockerProbability { get; }
}

public sealed class GameConfigG : 
    IComponent, 
    ICompData,
    ICopyFrom<GameConfigG>,
    IUnique, 
    ICreateApply,
    Scope<GameConfigScope>
{
    public IGameConfig value;
    
    public void        CopyFrom(GameConfigG other) => value = other.value;

    public GameConfigG Set(IGameConfig v)
    {
        value = v;
        return this;
    }
}