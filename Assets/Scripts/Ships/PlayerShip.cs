using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : SpaceShip
{
    public PlayerShip() : base()
    {
        _shipType = Enumerators.ShipType.PlayerShip;
    }

    public void Update()
    {
        if (_selfShips == null || _gameplayManager.IsPause)
            return;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _moveMode = MoveMode.Right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _moveMode = MoveMode.Left;
        }
        else
        {
            _moveMode = MoveMode.None;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shot();
        }
    }

    private Vector3 StartPosShips()
    {
        Vector3 startPosShips = _cameraController.MinPos;
        startPosShips.z = _selfShips.transform.position.z;
        startPosShips.x = _cameraController.Camera.transform.position.x;
        return startPosShips;
    }
    public override void ShipDamage()
    {
        base.ShipDamage();
        _uIManager.GetPage<GameplayPage>().RemoveHealth();
        if (_healthPlayer > 0 && !_gameplayManager.EndGame)
        {
            _gameplayManager.EnablePause();
        }
    }
    public override void CreateShip()
    {
        base.CreateShip();
        _selfShips.transform.position = StartPosShips();

    }
}
