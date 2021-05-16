using Entitas.Generic;
using UnityEngine;
using GameContextG = Entitas.Generic.ScopedContext<GameScope>;
using G=Entitas.Generic;

public static class PieceContextExtension
{
    public static G.Entity<GameScope> CreateRandomPiece(this GameContextG context, int x, int y)
    {
        var entity = context.CreateEntity();
        entity.Flag<PieceG>(true);
        entity.Flag<MovableG>(true);
        entity.Flag<InteractiveG>(true);
        entity.Add(Cache<PositionG>.I.Set(new Vector2Int(x, y)));
        entity.Add(Cache<AssetG>.I.Set("Piece" + Rand.game.Int(6)));
        return entity;
    }

    public static G.Entity<GameScope> CreateBlocker(this GameContextG context, int x, int y)
    {
        var entity = context.CreateEntity();
        entity.Flag<PieceG>(true);
        entity.Add(Cache<PositionG>.I.Set(new Vector2Int(x, y)));
        entity.Add(Cache<AssetG>.I.Set("Blocker"));
        return entity;
    }
}
