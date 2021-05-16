using Entitas.Generic;
using UnityEngine;
using UnityEngine.UI;
using InputEntityG = Entitas.Generic.Entity<InputScope>;

public class BurstModeLabelController : MonoBehaviour
{
    public Text label;

    string _text;

    void Awake()
    {
        _text = label.text;
    }

    void Start()
    {
        var contexts = Contexts.sharedInstance;
        OnAny_Flag<InputScope, BurstModeG>.I.Sub(e => onAnyBurstMode(e.Is<BurstModeG>()));
        onAnyBurstMode(contexts.InputC.Is<BurstModeG>());
        //OnAny_Flag只有一个回调函数，所以需要在回调里面判断是TRUE还是FALSE。
    }

    void onAnyBurstMode(bool burst)
    {
        label.text = _text + (burst ? ": on" : ": off");
    }
}
