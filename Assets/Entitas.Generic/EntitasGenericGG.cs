using System;
using System.Reflection;
using Entitas;
using Entitas.Generic;

public partial class Contexts
{
    public ScopedContext<TScope> Scope<TScope>() where TScope : IScope => Get<TScope>();

    public ICleanupSystem CreateDestorySystem<TScope, TComponent>()
        where TScope : IScope
        where TComponent : IComponent, Scope<TScope>
        => CreateDestorySystem(Matcher<TScope, TComponent>.I);

    public ICleanupSystem CreateDestorySystem<TScope>(IMatcher<Entity<TScope>> m)
        where TScope : IScope
        => Scope<TScope>().CreateDestorySystem(m);

    static Contexts()
    {
        Lookup_ScopeManager.RegisterAll();
    }
}

namespace Entitas.Generic
{
    public partial class ScopedContext<TScope>
    {
        public void Flip<TComp>() where TComp : class, Scope<TScope>, IComponent, ICompFlag, IUnique, new()
        {
            Flag<TComp>(!Is<TComp>());
        }
    }

    public static class BootConfig
    {
        private static Assembly[]       _ScannedAssemblies;
        public static  Assembly[]       ScannedAssemblies { get => _ScannedAssemblies ?? _AllAssemblies.Value; set => _ScannedAssemblies = value; }
        private static Lazy<Assembly[]> _AllAssemblies = new Lazy<Assembly[]>(() => AppDomain.CurrentDomain.GetAssemblies());

        /// <summary>
        /// GG 因为可能要以DLL发布，所以需要暴露给上层。
        /// </summary>
        public static bool SafeAERC { get; set; } =
#if (ENTITAS_FAST_AND_UNSAFE)
            false;
#else
            true;
#endif
    }
}

namespace Entitas
{
    public abstract class ReactiveSystemG<TS> : ReactiveSystem<Generic.Entity<TS>> where TS : IScope
    {
        protected readonly Contexts          _contexts;
        protected readonly ScopedContext<TS> Scope;

        protected ReactiveSystemG(Contexts contexts) : base(contexts.Get<TS>())
        {
            _contexts = contexts;
            Scope     = contexts.Get<TS>();
        }

        protected ReactiveSystemG(ICollector<Entity<TS>> collector) : base(collector) { }
    }
}
