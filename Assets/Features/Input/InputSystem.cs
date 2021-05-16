using System;
using Entitas;
using Entitas.Generic;
using UnityEngine;

public sealed class InputSystem : IExecuteSystem
{
    readonly Contexts _contexts;

    public InputSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        setBurstMode();
        emitInput();
    }

    void setBurstMode()
    {
        if (Input.GetKeyDown(KeyCode.B))
            _contexts.InputC.Flip<BurstModeG>();
    }

    void emitInput()
    {
        var input = _contexts.InputC.Is<BurstModeG>()
            ? Input.GetMouseButton(0)
            : Input.GetMouseButtonDown(0);

        if (input)
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var e = _contexts.InputC.CreateEntity();
            e.Add(Cache<InputG>.I.Set(
                new Vector2Int(
                    (int) Math.Round(mouseWorldPos.x),
                    (int) Math.Round(mouseWorldPos.y)
                )));
        }
    }
}
