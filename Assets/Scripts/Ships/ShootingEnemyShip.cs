using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyShip : EnemyShip
{
    protected float _timerShooting;
    public ShootingEnemyShip() : base()
    {
        _shipType = Enumerators.ShipType.ShootingEnemyShip;
        _timerShooting = _delayShooting;
        MainApp.Instance.FixedUpdateEvent += Shooting;
    }
    private void Shooting()
    {
        if (_gameplayManager.IsPause)
            return;

        if (_timerShooting <=0)
        {
            Shot();
            _timerShooting = _delayShooting;
        }
        else
        {
            _timerShooting -= Time.deltaTime;
        }
    }
    public override void Dispose()
    {
        base.Dispose();
        MainApp.Instance.FixedUpdateEvent -= Shooting;
    }
}
