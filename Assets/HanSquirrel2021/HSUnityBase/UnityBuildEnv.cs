using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("GLib")]

namespace GLib.Inner
{
    internal static class ConsoleSyncObjHolder
    {
        public static readonly object The = new object();
    }
    
    internal static class PlatformImpl
    {
        public static void ConsoleOut(string msg)
        {
            Debug.Log(msg);
        }
        
        public static bool RunInMSConsole => false;
        public static readonly RuntimePlatform? TargetPlatform =
#if UNITY_STANDALONE_WIN
            RuntimePlatform.WindowsPlayer;
#elif UNITY_ANDROID
            RuntimePlatform.Android;
#elif UNITY_IOS
            RuntimePlatform.IPhonePlayer;
#endif

        public static readonly bool UsingIL2CPP =
#if ENABLE_IL2CPP
            true;
#else
            false;
#endif
        public static readonly bool IsEditor =
#if UNITY_EDITOR
            true;
#else
            false;
#endif
    }
}

namespace HanSquirrel.ResourceManager
{
    public static class PrefabExts
    {
        public static string GetPrefabPathIf(this GameObject go)
        {
#if UNITY_EDITOR
            return UnityEditor.AssetDatabase.GetAssetPath(go);
#else
                return null;
#endif
        }
    }
}
