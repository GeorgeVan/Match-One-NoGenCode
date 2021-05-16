using System;
using System.Reflection;
using Entitas.Generic;
using UnityEngine;

public partial class Contexts
{
    public void SafeInitVisualDebuggingForGenerics()
    {
#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
        foreach (var context in All)
        {
            try
            {
                CreateContextObserver(context);
            }
            catch (System.Exception ex)
            {
                Debug.LogErrorFormat("CreateContextObserver异常[{0}]", ex);
            }
        }
#endif
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
    }
}

namespace Entitas
{
    public abstract class ReactiveSystemG<TS> : ReactiveSystem<Generic.Entity<TS>> where TS : IScope
    {
        protected ReactiveSystemG(Contexts contexts) : base(contexts.Get<TS>()) { }

        protected ReactiveSystemG(ICollector<Entity<TS>> collector) : base(collector) { }
    }
}
