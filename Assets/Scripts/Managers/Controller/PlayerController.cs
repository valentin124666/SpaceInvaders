using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : IController
{
    private PlayerShip _selfShips;

    private IGameplayManager _gameplayManager;

    private EnvironmentController _environmentController;
    private LevelController _levelController;


    public void Init()
    {
        _gameplayManager = GameClient.Get<IGameplayManager>();

        _environmentController = _gameplayManager.GetController<EnvironmentController>();
        _levelController = _gameplayManager.GetController<LevelController>();
    }
    public void Update()
    {
        if (_selfShips == null)
            return;

        _selfShips.Update();
    }
    private void Died()
    {
        _levelController.GameOver();
    }
    public void CreateNewShips()
    {
        _selfShips = _environmentController.CreateShips(Enumerators.ShipType.PlayerShip) as PlayerShip;
        _selfShips.DisposEvent += Died;
        _selfShips.CreateShip();
    }

    public void ResetAll()
    {
        if (_selfShips == null)
            return;

        _selfShips.DisposEvent -= Died;
        Dispose();
        _selfShips = null;
    }
    public void Dispose()
    {
        if (_selfShips != null)
            _selfShips.Dispose();
    }

}
