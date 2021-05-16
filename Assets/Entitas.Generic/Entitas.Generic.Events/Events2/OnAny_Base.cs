using System;
using System.Collections.Generic;

namespace Entitas.Generic
{
    /// <summary>
    /// 内部其实就是一个Action字典
    /// </summary>
    public abstract class OnAny_Base<TScope>
        : IUnsubAll
        where TScope : IScope
    {
        /// <summary>
        /// 框架支持[单类型多例]。这个可以算作是缺省上下文
        /// </summary>
        public OnAny_Base(Contexts db)
        {
            _db = db;
            Events2.I.Add(this); //这个为了可以快速UnSubAll
        }

        private Contexts _db;

        /// <summary>
        /// 每个OnAny组合都会创建一个[Context-Action《Entity》]字典。框架支持同类型Context的多实例。
        /// </summary>
        private Dictionary<Context<Entity<TScope>>, Action<Entity<TScope>>>
            ActionDict = new Dictionary<Context<Entity<TScope>>, Action<Entity<TScope>>>();

        /// 如果不是缺省的Context，则需要用这个来Sub 
        public void Sub(Action<Entity<TScope>> action, Context<Entity<TScope>> context)
        {
            if (!ActionDict.ContainsKey(context))
            {
                ActionDict.Add(context, null);
            }
            ActionDict[context] += action;
        }

        /// 如果不是缺省的Context，则需要用这个来UnSub 
        public void Unsub(Action<Entity<TScope>> action, Context<Entity<TScope>> context)
        {
            if (ActionDict.ContainsKey(context))
            {
                ActionDict[context] -= action;
            }
        }

        public void Invoke(Entity<TScope> entity, Context<Entity<TScope>> context)
        {
            if (ActionDict.ContainsKey(context))
            {
                ActionDict[context]?.Invoke(entity);
            }
        }

        /// 缺省Context的Sub
        public void Sub(Action<Entity<TScope>> action)
        {
            Sub(action, _db.Get<TScope>());
        }

        /// 缺省Context的UnSub
        public void Unsub(Action<Entity<TScope>> action)
        {
            Unsub(action, _db.Get<TScope>());
        }

        public void UnsubAll()
        {
            ActionDict.Clear();
        }
    }
}
