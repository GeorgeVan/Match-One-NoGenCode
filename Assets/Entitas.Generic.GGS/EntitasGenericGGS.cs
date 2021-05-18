using System;
using System.Reflection;
using Entitas.Generic;
using UnityEngine;

public static class ContextsInitVisualExt1
{
    [RuntimeInitializeOnLoadMethod]
    public static void ConfigAERC()
    {
        BootConfig.SafeAERC =
#if (ENTITAS_FAST_AND_UNSAFE)
            false;
#else
            true;
#endif
    }

    public static void SafeInitVisualDebuggingForGenerics(this Contexts contexts)
    {
#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
        foreach (var context in contexts.All)
        {
            try
            {
                createContextObserver(context);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogErrorFormat("CreateContextObserver异常[{0}]", ex);
            }
        }
        
        static void createContextObserver(Entitas.IContext context)
        {
            if (UnityEngine.Application.isPlaying)
            {
                var observer = new Entitas.VisualDebugging.Unity.ContextObserver(context);
                UnityEngine.Object.DontDestroyOnLoad(observer.gameObject);
            }
        }
#endif
    }
}
