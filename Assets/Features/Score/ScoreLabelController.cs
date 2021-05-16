using Entitas.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLabelController : MonoBehaviour
{
    public Text label;

    void Start() =>
        OnAny<GameStateScope, ScoreG>.I.Sub(en => label.text = "Score " + en.Get<ScoreG>().value);
}
