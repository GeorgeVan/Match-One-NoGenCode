using System;

namespace Entitas.Generic
{
    public sealed class OnAny_Flag<TScope, TComp> : OnAny_Base<TScope>, IEventsFeature2_OnAny_Flag<TScope, TComp>
        where TScope : IScope
        where TComp : IComponent, ICompFlag, IEvent_Any<TScope, TComp>, Scope<TScope>
    {
        public OnAny_Flag(Contexts db) : base(db) { }
        /// <summary>
        /// GG 在 <see cref="EventSystem_Any_Flag2"/> 里面设置。后者对接框架，这个对接上层。
        /// </summary>
        public static IEventsFeature2_OnAny_Flag<TScope, TComp> I;
    }

    public interface IEventsFeature2_OnAny_Flag<TScope, TComp>
        where TScope : IScope
        where TComp : IComponent, ICompFlag, IEvent_Any<TScope, TComp>, Scope<TScope>
    {
        void Sub(Action<Entity<TScope>>   action, Context<Entity<TScope>> context);
        void Unsub(Action<Entity<TScope>> action, Context<Entity<TScope>> context);
        void Invoke(Entity<TScope>        entity, Context<Entity<TScope>> context);

        void Sub(Action<Entity<TScope>>   action);
        void Unsub(Action<Entity<TScope>> action);
    }
}
