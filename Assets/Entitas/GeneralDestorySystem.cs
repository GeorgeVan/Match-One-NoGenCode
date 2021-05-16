using System.Collections.Generic;
using Entitas;

/// GG根据找回的自动清理代码写的一个通用类。用起来很方便
public static class GeneralDestorySystemExt
{
    public static ISystem CreateDestorySystem<TE>(this Context<TE> context, IMatcher<TE> matcher) where TE : Entity
        => new GeneralDestorySystem<TE>(context, matcher);

    private class GeneralDestorySystem<TE> : ICleanupSystem where TE : Entity
    {
        readonly List<TE>   _Buffer = new List<TE>();
        readonly IGroup<TE> _Group;

        public GeneralDestorySystem(Context<TE> context, IMatcher<TE> matcher) =>
            _Group = context.GetGroup(matcher);

        public void Cleanup()
        {
            _Buffer.Clear();
            foreach (var e in _Group.GetEntities(_Buffer))
                e.Destroy();
        }
    }
}
