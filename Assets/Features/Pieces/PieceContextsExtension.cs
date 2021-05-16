using Entitas;
using UnityEngine;
using GameEntityG = Entitas.Generic.Entity<GameScope>;
using Entitas.Generic;
using GameContexG= Entitas.Generic.ScopedContext<GameScope>;
public partial class Contexts
{
    public const string PieceIndexName = "Piece";

    public void InitializePieceEntityIndices()
    {
        GameC.AddPrimaryEntityIndex(
            PieceIndexName,
            GameC.GetGroup(Matcher<GameEntityG>
                .AllOf(Matcher<GameScope,PieceG>.I, Matcher<GameScope,PositionG>.I)
                .NoneOf(Matcher<GameScope,DestroyedG>.I)
            ),
            (e, c) => (c as PositionG)?.value ?? e.Get<PositionG>().value);
    }
}

public static class ContextsExtensions
{
    public static GameEntityG GetPieceWithPosition(this GameContexG context, Vector2Int value)
    {
        return ((PrimaryEntityIndex<GameEntityG, Vector2Int>)context.GetEntityIndex(Contexts.PieceIndexName)).GetEntity(value);
    }
}
