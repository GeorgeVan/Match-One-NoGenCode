using GLib;

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
                Mini.Error(ex, "CreateContextObserver异常");
            }
        }
        Mini.Success("InitVisualDebugging完成");
#endif
    }
}
