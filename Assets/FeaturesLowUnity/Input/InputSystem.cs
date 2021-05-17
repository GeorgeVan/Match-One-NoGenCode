using System;
using Entitas;
using Entitas.Generic;
using UnityEngine;

internal sealed class InputSystem : IExecuteSystem
{
    readonly ScopedContext<InputScope> _context;

    public InputSystem(Contexts contexts)
    {
        _context = contexts.Scope<InputScope>();
    }

    public void Execute()
    {
        setBurstMode();
        emitInput();
    }

    void setBurstMode()
    {
        if (Input.GetKeyDown(KeyCode.B))
            _context.Flip<BurstModeG>();
    }

    void emitInput()
    {
        var input = _context.Is<BurstModeG>()
            ? Input.GetMouseButton(0)
            : Input.GetMouseButtonDown(0);

        if (input)
        {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var e = _context.CreateEntity();
            e.Add(Cache<InputG>.I.Set(
                new Vector2Int(
                    (int) Math.Round(mouseWorldPos.x),
                    (int) Math.Round(mouseWorldPos.y)
                )));
        }
    }
}
