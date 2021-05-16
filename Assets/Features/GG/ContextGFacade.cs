using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Entitas.Generic;

public interface GameScope : IScope { }

public interface GameStateScope : IScope { }

public interface InputScope : IScope { }

public partial class Contexts
{
    public static void BootWithCodeGenAndGenerics(params Assembly[] asms)
    {
        if (asms.Length > 0)
            BootConfig.ScannedAssemblies = new HashSet<Assembly>(asms).ToArray();
        Lookup_ScopeManager.RegisterAll();
        sharedInstance.AddScopedContexts();
        sharedInstance.SafeInitVisualDebuggingForGenerics();
        sharedInstance.InitializePieceEntityIndices();
    }

    public ScopedContext<GameScope>      GameC      => Get<GameScope>();
    public ScopedContext<GameStateScope> GameStateC => Get<GameStateScope>();
    public ScopedContext<InputScope>     InputC     => Get<InputScope>();
}
