using System;
using System.Collections.Generic;

namespace Entitas.Generic
{
    public abstract class OnSelf_Base<TScope>
        : IUnsubAll
        where TScope : IScope
    {
        public OnSelf_Base(Contexts db)
        {
            _db = db;
            Events2.I.Add(this);
        }

        private Contexts _db;

        /// 这个也是一个字典，和缺省的GenCode类型的EventSystem不同，不会新增Component。
        /// GenCode和Event实现是将每个Entity的每种监听列表作为组件记录在那个类里面。因此如果类销毁了则一切都销毁。
        /// 而这个就得在类销毁的时候UnSub。
        /// 所以对于OnSelf，用Event1模型会节省内存。
        /// 无论用什么方案，Generic框架在启动的时候都会根据定义来自动添加相关EventListenerComponent的槽位。
        /// 只不过Event2方案不去理会这个槽位，用字典来直接实现。
        /// 对于Any来说还是很方便直观的。
        private Dictionary<KeyValuePair<Context<Entity<TScope>>, int>, Action<Entity<TScope>>>
            ActionDict = new Dictionary<KeyValuePair<Context<Entity<TScope>>, int>, Action<Entity<TScope>>>();

        public void Sub(Int32 id, Action<Entity<TScope>> action, Context<Entity<TScope>> context)
        {
            var contextIdKey = new KeyValuePair<Context<Entity<TScope>>, Int32>(context, id);
            if (!ActionDict.ContainsKey(contextIdKey))
            {
                ActionDict.Add(contextIdKey, null);
            }
            ActionDict[contextIdKey] += action;
        }

        public void Unsub(Int32 id, Action<Entity<TScope>> action, Context<Entity<TScope>> context)
        {
            var contextIdKey = new KeyValuePair<Context<Entity<TScope>>, Int32>(context, id);
            if (ActionDict.ContainsKey(contextIdKey))
            {
                ActionDict[contextIdKey] -= action;
            }
        }

        public void Invoke(Int32 id, Entity<TScope> entity, Context<Entity<TScope>> context)
        {
            var contextIdKey = new KeyValuePair<Context<Entity<TScope>>, Int32>(context, id);
            if (ActionDict.TryGetValue(contextIdKey, out var action))
            {
                action?.Invoke(entity);
            }
        }

        public void Sub(Int32 id, Action<Entity<TScope>> action)
        {
            Sub(id, action, _db.Get<TScope>());
        }

        public void Unsub(Int32 id, Action<Entity<TScope>> action)
        {
            Unsub(id, action, _db.Get<TScope>());
        }

        public void UnsubAll()
        {
            ActionDict.Clear();
        }
    }
}
