using Entitas.Generic;

internal sealed class GameSystems : Feature
{
    public GameSystems(Contexts contexts)
    {
        // Input
        Add(new InputSystem(contexts));
        Add(new ProcessInputSystem(contexts));

        // Update
        Add(new BoardSystem(contexts));
        Add(new FallSystem(contexts));
        Add(new FillSystem(contexts));
        Add(new ScoreSystem(contexts));

        // View
        Add(new AddViewSystem(contexts));

        Add(new EventSystem_Any2<GameStateScope, ScoreG>(contexts));
        Add(new EventSystem_Any_Flag2<InputScope, BurstModeG>(contexts));

        Add(new EventSystem_SelfFlag<GameScope, DestroyedG>(contexts));
        Add(new EventSystem_Self<GameScope, PositionG>(contexts));

        Add(contexts.CreateDestorySystem<GameScope, DestroyedG>());
        Add(contexts.CreateDestorySystem<InputScope, InputG>());
    }
}
