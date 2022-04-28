using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : IController
{

    private List<Bullet> _bulletList;
    private List<SpaceShip> _spaceShips;

    private Transform _bulletContainer;
    private Transform _shipsModelContainer;

    public void Init()
    {
        _bulletList = new List<Bullet>();
        _spaceShips = new List<SpaceShip>();
        _bulletContainer = new GameObject().transform;
        _bulletContainer.name = "[BulletContainer]";
        _shipsModelContainer = new GameObject().transform;
        _shipsModelContainer.name = "[ShipsModelContainer]";
    }
    public Bullet CreateBullet()
    {
        Bullet bullet = MonoBehaviour.Instantiate(MainApp.Instance.GameData.GetGameplayElements("Bullet")).GetComponent<Bullet>();

        bullet.transform.SetParent(_bulletContainer);
        _bulletList.Add(bullet);
        return bullet;
    }
    public void DisposeBullet(Bullet bullet)
    {
        _bulletList.Remove(bullet);
        MonoBehaviour.Destroy(bullet.gameObject);
    }
    public void Dispose()
    {
        DisposeAllBullet();
        foreach (var item in _spaceShips)
        {
            item.Dispose();
        }
        _spaceShips.Clear();
    }
    public void DisposeAllBullet()
    {
        foreach (var item in _bulletList)
        {
            if (item != null)
                MonoBehaviour.Destroy(item.gameObject);
        }
        _bulletList.Clear();
    }

    public SpaceShip CreateShips(Enumerators.ShipType type)
    {
        SpaceShip ship = null;

        switch (type)
        {
            case Enumerators.ShipType.PlayerShip:
                ship = new PlayerShip();
                break;
            case Enumerators.ShipType.StandartEnemyShip:
                ship = new StandartEnemyShip();
                break;
            case Enumerators.ShipType.ShootingEnemyShip:
                ship = new ShootingEnemyShip();
                break;
            case Enumerators.ShipType.Mothership:
                ship = new Mothership();
                break;
        }
        ship.Init();
        _spaceShips.Add(ship);

        return ship;
    }
    public GameObject CreateShipsModel(Enumerators.ShipType type)
    {
        GameObject ship = null;

        switch (type)
        {
            case Enumerators.ShipType.PlayerShip:
                ship = MonoBehaviour.Instantiate(MainApp.Instance.GameData.GetShips(Enumerators.ShipType.PlayerShip).Prefab);
                break;
            case Enumerators.ShipType.StandartEnemyShip:
                ship = MonoBehaviour.Instantiate(MainApp.Instance.GameData.GetShips(Enumerators.ShipType.StandartEnemyShip).Prefab);
                break;
            case Enumerators.ShipType.ShootingEnemyShip:
                ship = MonoBehaviour.Instantiate(MainApp.Instance.GameData.GetShips(Enumerators.ShipType.StandartEnemyShip).Prefab);
                break;
            case Enumerators.ShipType.Mothership:
                ship = MonoBehaviour.Instantiate(MainApp.Instance.GameData.GetShips(Enumerators.ShipType.Mothership).Prefab);
                break;
        }

        ship.transform.SetParent(_shipsModelContainer);

        return ship;
    }
    public SpaceShip GetShipByTransform(Transform ship)
    {
        return _spaceShips.Find(item => item.SelfShip == ship);
    }
    public void RemoveShip(SpaceShip ship)
    {
        if (ship == null)
            return;

        _spaceShips.Remove(ship);
        ship.Dispose();
    }

    public void ResetAll()
    {
        Dispose();
    }

    public void Update()
    {

    }
}
