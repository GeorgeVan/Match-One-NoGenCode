using Entitas.Generic;
using UnityEngine;

/**
 *
 * GameControllerBehaviour is the entry point to Match One
 *
 * The only purpose of this class is to redirect and forward
 * the Unity lifecycle events to the GameController
 * 游戏入口，直接被主场景引用
 */
public class GameControllerBehaviour : MonoBehaviour
{
    public ScriptableGameConfig gameConfig;

    GameController _gameController;

    void Awake()  => _gameController = new GameController(ContextHolder.I, gameConfig);
    void Start()  => _gameController.Initialize();
    void Update() => _gameController.Execute();
}
