using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : SpaceShip
{
    private EnemyController _enemyController;
    public Transform RightAnchor { get; private set; }
    public Transform LeftAnchor { get; private set; }
    public EnemyShip() : base()
    {
        _enemyController = _gameplayManager.GetController<EnemyController>();
    }
    public override void CreateShip()
    {
        base.CreateShip();
        LeftAnchor = SelfShip.Find("LeftAnchor");
        RightAnchor = SelfShip.Find("RightAnchor");
    }
    public void MoveControl(MoveMode moveMode)
    {
        _moveMode = moveMode;
    }
    public override void ShipDamage()
    {
        _uIManager.GetPage<GameplayPage>().AddScore(_scoreHit);
        new AccountDestruction(_scoreHit,_selfShips.transform);
        base.ShipDamage();
    }
}
