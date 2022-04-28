using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipKeller : Killer
{
    [SerializeField]
    private Enumerators.TargetShot _targetKeller;

    void Start()
    {
        _target = _targetKeller.ToString();
    }
    protected override void PronouncedDamage(SpaceShip ship)
    {
        GameClient.Instance.GetService<IGameplayManager>().GetController<LevelController>().GameOver();
        base.PronouncedDamage(ship);
    }
}
