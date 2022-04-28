using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : IController
{
    private List<EnemyShip> _enemyShips;
    private List<EnemyShip> _enemyShipsGroup;

    private bool[] _probabilities;

    private IGameplayManager _gameplayManager;

    private EnvironmentController _environmentController;
    private CameraController _cameraController;
    private LevelController _levelController;

    private float _shipAcceleration = 0.001f;
    private float _delaySpawnMothership = 4, _timerSpawnMothership;


    public MoveMode ShipsMoveMode;
    public void Init()
    {
        _gameplayManager = GameClient.Get<IGameplayManager>();

        _environmentController = _gameplayManager.GetController<EnvironmentController>();
        _cameraController = _gameplayManager.GetController<CameraController>();
        _levelController = _gameplayManager.GetController<LevelController>();

        _enemyShips = new List<EnemyShip>();
        _enemyShipsGroup = new List<EnemyShip>();
        _timerSpawnMothership = _delaySpawnMothership;
        ÑreatePossibility();
    }
    private void RemoveEnemyShips(EnemyShip enemy)
    {
        _enemyShips.Remove(enemy);
        if (_enemyShips.Count == 0)
        {
            _levelController.GameWin();
        }
    }
    private void CreeteMothership()
    {
        if (_gameplayManager.IsPause)
            return;

        if (_timerSpawnMothership <= 0)
        {
            if (_probabilities[Random.Range(0, 100)])
            {
                Vector3 posSpawn = new Vector3(_cameraController.MinPos.x - 2, _cameraController.MaxPos.y - 0.5f, 0);

                var ship = CreateEnemy(Enumerators.ShipType.Mothership, posSpawn);
                ship.MoveControl(MoveMode.Right);
            }

            _timerSpawnMothership = _delaySpawnMothership;
        }
        else
        {
            _timerSpawnMothership -= Time.deltaTime;
        }
    }
    private void CreateEnemyGroup()
    {
        int numberLine = MainApp.Instance.LevelData.curentLevel.NumberEnemies.NumberLine;
        int numberRow = MainApp.Instance.LevelData.curentLevel.NumberEnemies.NumberRow;

        float stepAside = 0.6f; float stetpDown = 0.5f;
        Vector3 posSpawn = new Vector3(_cameraController.Camera.transform.position.x - stepAside / 2, _cameraController.MaxPos.y - 1, 0);
        posSpawn.x += stepAside * ((float)numberRow / 2);

        for (int i = 0; i < numberLine; i++)
        {
            for (int j = 0; j < numberRow; j++)
            {
                Enumerators.ShipType shipType;

                if ((i == 1 || i == 3) && (j == 0 || j == numberRow - 1))
                {
                    shipType = Enumerators.ShipType.ShootingEnemyShip;
                }
                else
                {
                    shipType = Enumerators.ShipType.StandartEnemyShip;
                }

                var ship = CreateEnemy(shipType, posSpawn);
                _enemyShipsGroup.Add(ship);
                ship.DisposEvent += () => { _enemyShipsGroup.Remove(ship); };

                posSpawn.x -= stepAside;
            }
            posSpawn.x += stepAside * numberRow;
            posSpawn.y -= stetpDown;
        }

        MainApp.Instance.LateUpdateEvent += DirectionControlGroup;
        DirectionMoveOfShipsGroup();
    }
    private void ÑreatePossibility()
    {
        _probabilities = new bool[100];
        int percentageTruth = 100;
        List<int> selected = new List<int>();
        while (percentageTruth > 0)
        {
            int i = Random.Range(0, 100);
            if (!selected.Contains(i))
            {
                _probabilities[i] = true;
                selected.Add(i);
                percentageTruth--;
            }
        }
    }
    public void StartWar()
    {
        MainApp.Instance.FixedUpdateEvent += CreeteMothership;
        CreateEnemyGroup();
    }
    public EnemyShip CreateEnemy(Enumerators.ShipType shipType, Vector3 posSpawn)
    {
        var ship = _environmentController.CreateShips(shipType) as EnemyShip;
        ship.CreateShip();
        ship.SelfShip.position = posSpawn;

        _enemyShips.Add(ship);

        ship.DisposEvent += () => { RemoveEnemyShips(ship); };
        return ship;
    }
    public void DirectionControlGroup()
    {
        foreach (var item in _enemyShipsGroup)
        {
            if (ShipsMoveMode == MoveMode.Right && item.RightAnchor.position.x > _cameraController.Border)
            {
                ChangeDirectionGroup();
            }
            else if (ShipsMoveMode == MoveMode.Left && item.LeftAnchor.position.x < -_cameraController.Border)
            {
                ChangeDirectionGroup();
            }
        }
    }
    public void DirectionMoveOfShipsGroup()
    {
        ShipsMoveMode = ShipsMoveMode == MoveMode.Left ? MoveMode.Right : MoveMode.Left;

        foreach (var item in _enemyShipsGroup)
        {
            item.MoveControl(ShipsMoveMode);
        }
    }
    private void ChangeDirectionGroup()
    {
        DirectionMoveOfShipsGroup();
        float step = 0.3f;
        foreach (var item in _enemyShipsGroup)
        {
            item.SelfShip.Translate(Vector3.down * step);
            item.MoveAcceleration(_shipAcceleration);
        }
    }
    public void ResetAll()
    {
        _enemyShips.Clear();
        _enemyShipsGroup.Clear();
        MainApp.Instance.FixedUpdateEvent -= CreeteMothership;
        MainApp.Instance.LateUpdateEvent -= DirectionControlGroup;
    }
    public void Dispose()
    {
        _enemyShips.Clear();
        _enemyShipsGroup.Clear();
        MainApp.Instance.FixedUpdateEvent -= CreeteMothership;
        MainApp.Instance.LateUpdateEvent -= DirectionControlGroup;
    }
    public void Update()
    {
    }
}
