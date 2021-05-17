using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Entitas;
using Entitas.Generic;

public abstract class ReactiveSystemApp<TS> : ReactiveSystemG<TS> where TS : IScope
{
    protected readonly ScopedContext<GameScope>       GameScope;
    protected readonly ScopedContext<InputScope>      InputScope;
    protected readonly ScopedContext<GameStateScope>  GameStateScope;
    protected readonly ScopedContext<GameConfigScope> GameConfigScope;

    protected ReactiveSystemApp(Contexts contexts) : base(contexts)
    {
        GameScope       = contexts.Get<GameScope>();
        InputScope      = contexts.Get<InputScope>();
        GameStateScope  = contexts.Get<GameStateScope>();
        GameConfigScope = contexts.Get<GameConfigScope>();
    }
}
