using Entitas;
using Entitas.Generic;

public sealed class BurstModeG :
    IComponent,
    ICompFlag,
    IUnique,
    Scope<InputScope>,
    IEvent_Any<InputScope, BurstModeG>,
    IEvent_AnyRemoved<InputScope, BurstModeG>
{
}
