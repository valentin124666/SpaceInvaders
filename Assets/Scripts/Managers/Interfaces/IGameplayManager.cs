using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameplayManager 
{
    T GetController<T>() where T : IController;

    Enumerators.AppState CurrentState { get; }
    bool IsPause { get;}
    bool EndGame { get;}
    void EnablePause();
    void StartGameplay();
    void StopGameplay();
    void RefreshGameplay();
    void ChangeAppState(Enumerators.AppState stateTo);

}
