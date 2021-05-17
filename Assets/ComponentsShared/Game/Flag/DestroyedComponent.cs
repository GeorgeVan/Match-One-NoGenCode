using Entitas;
using Entitas.Generic;

public sealed class DestroyedG :
    IComponent,
    ICompFlag,
    Scope<GameScope>,
    IEvent_Self<GameScope, DestroyedG>
{
}