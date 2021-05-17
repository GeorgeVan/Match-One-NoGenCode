using Entitas.Generic;
using UnityEngine;
using UnityEngine.UI;

/// 直接被主场景引用 
public class ScoreLabelController : MonoBehaviour
{
    public Text label;

    void Start() =>
        OnAny<GameStateScope, ScoreG>.I.Sub(en => label.text = "Score " + en.Get<ScoreG>().value);
}
