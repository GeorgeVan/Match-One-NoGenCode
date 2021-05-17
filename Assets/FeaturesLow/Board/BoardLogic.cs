using Entitas.Generic;
using Vector2Int=UnityEngine.Vector2Int;

public static class BoardLogic
{
    public static int GetNextEmptyRow(ScopedContext<GameScope> gameScope, Vector2Int position)
    {
        position.y -= 1;
        while (position.y >= 0 && gameScope.GetPieceWithPosition(position) == null)
        {
            position.y -= 1;
        }

        return position.y + 1;
    }
}
