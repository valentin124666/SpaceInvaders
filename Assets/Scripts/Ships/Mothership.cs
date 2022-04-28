using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : ShootingEnemyShip
{
    public Mothership() : base()
    {
        _shipType = Enumerators.ShipType.Mothership;
        _timerShooting = _delayShooting;
        _disableMoveControl = true;
    }
    private void ControlPosition()
    {
        if (_selfShips == null)
            return;

        if (_moveMode == MoveMode.Left && RightAnchor.position.x < _cameraController.MinPos.x)
        {
            DestroyShip();
        }
        else if (_moveMode == MoveMode.Right && LeftAnchor.position.x > _cameraController.MaxPos.x)
        {
            DestroyShip();
        }
        //Debug.Log(LeftAnchor.position.x);
        //Debug.Log(_cameraController.MaxPos.x);
    }
    public override void CreateShip()
    {
        base.CreateShip();
        MainApp.Instance.LateUpdateEvent += ControlPosition;
    }
    public override void Dispose()
    {
        base.Dispose();
        MainApp.Instance.LateUpdateEvent -= ControlPosition;

    }
}
