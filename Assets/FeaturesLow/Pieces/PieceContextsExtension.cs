using Entitas;
using Vector2Int=UnityEngine.Vector2Int;
using GameEntityG = Entitas.Generic.Entity<GameScope>;
using Entitas.Generic;
using GameContexG = Entitas.Generic.ScopedContext<GameScope>;

public static class ContextsExtensions
{
    public static GameEntityG GetPieceWithPosition(this GameContexG context, Vector2Int value)
    {
        return ((PrimaryEntityIndex<GameEntityG, Vector2Int>) context.GetEntityIndex(PieceIndexName)).GetEntity(value);
    }

    public const string PieceIndexName = "Piece";

    public static void InitializePieceEntityIndices(this GameContexG context)
    {
        context.AddPrimaryEntityIndex(
            PieceIndexName,
            context.GetGroup(Matcher<GameEntityG>
                .AllOf(Matcher<GameScope, PieceG>.I, Matcher<GameScope, PositionG>.I)
                .NoneOf(Matcher<GameScope, DestroyedG>.I)
            ),
            (e, c) => (c as PositionG)?.value ?? e.Get<PositionG>().value);
    }
}
