using Entitas.Generic;

/// 客户端最上层使用的单例
public static class ContextHolder
{
    public static readonly Contexts I;

    static ContextHolder()
    {
        BootConfig.ScannedAssemblies = new[] { typeof(GameScope).Assembly };

        I = Create();

        static Contexts Create()
        {
            var ret = new Contexts();
            ret.AddScopedContexts();
            ret.SafeInitVisualDebuggingForGenerics();
            ret.Scope<GameScope>().InitializePieceEntityIndices();
            return ret;
        }
    }
}
