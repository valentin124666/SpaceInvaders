using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : IService, IGameplayManager
{
    private IUIManager _uIManager;
    private List<IController> _controllers;
    public Enumerators.AppState CurrentState { get; private set; }
    public bool IsPause {get; private set;}
    public bool EndGame {get; private set;}

    public void Update()
    {
        foreach (var item in _controllers)
            item.Update();
    }

    public void Dispose()
    {
        foreach (var item in _controllers)
            item.Dispose();
    }

    public T GetController<T>() where T : IController
    {
        return (T)_controllers.Find(controller => controller is T);
    }
    
    public void Init()
    {
        _uIManager = GameClient.Get<IUIManager>();

        FillControllers();
    }
    private void FillControllers()
    {
        _controllers = new List<IController>()
        {
            new EnvironmentController(),
            new CameraController(),
            new LevelController(),
            new EnemyController(),
            new PlayerController()
        };

        foreach (var item in _controllers)
            item.Init();
    }
    
    public void EnablePause()
    {
        IsPause = true;
        GetController<EnvironmentController>().DisposeAllBullet();
        _uIManager.GetPopup<PausePopups>().Show(() => IsPause = false);        
    }
    public void RefreshGameplay()
    {
        StopGameplay();
        _uIManager.HideAllPopups();
        StartGameplay();
    }

    public void StartGameplay()
    {
        IsPause = false;
        EndGame = false;

        GetController<PlayerController>().CreateNewShips();
        GetController<EnemyController>().StartWar();
        GetController<LevelController>().CreateLevel();
        EnablePause();
    }
    public void StopGameplay()
    {
        IsPause = true;
        EndGame = true;

        foreach (var item in _controllers)
            item.ResetAll();

        _uIManager.ResetAll();

    }

    public void ChangeAppState(Enumerators.AppState stateTo)
    {
        CurrentState = stateTo;
        switch (stateTo)
        {
            case Enumerators.AppState.AppStart:
                _uIManager.HideAllPopups();
                _uIManager.SetPage<SignInPage>();
                break;
            case Enumerators.AppState.InGame:
                StartGameplay();
                //_uIManager.HideAllPopups();
                _uIManager.SetPage<GameplayPage>();
                break;
        }
    }
}
