using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    protected string _target;
    protected EnvironmentController _environmentController;

    private void Awake()
    {
        _environmentController = GameClient.Get<IGameplayManager>().GetController<EnvironmentController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _target)
        {
            var ship = _environmentController.GetShipByTransform(other.transform);
            if (ship != null)
            {
                PronouncedDamage(ship);
            }
        }
    }
    protected virtual void PronouncedDamage(SpaceShip ship)
    {
        ship.ShipDamage();
    }
}
