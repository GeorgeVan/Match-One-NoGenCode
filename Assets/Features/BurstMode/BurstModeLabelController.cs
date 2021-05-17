using Entitas.Generic;
using UnityEngine;
using UnityEngine.UI;
using InputEntityG = Entitas.Generic.Entity<InputScope>;

public class BurstModeLabelController : MonoBehaviour
{
    public Text label;

    string _text;

    void Awake() => _text = label.text;

    void Start()
    {
        OnAny_Flag<InputScope, BurstModeG>.I.Sub(e => refreshLabel(e.Is<BurstModeG>()));
        refreshLabel(ContextHolder.I.Scope<InputScope>().Is<BurstModeG>());
        //OnAny_Flag只有一个回调函数，所以需要在回调里面判断是TRUE还是FALSE。
    }

    void refreshLabel(bool burst) =>
        label.text = _text + (burst ? ": on" : ": off");
}
