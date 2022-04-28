using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip
{
    public event System.Action DisposEvent;

    protected GameObject _selfShips;

    protected IUIManager _uIManager;
    protected IGameplayManager _gameplayManager;

    public Transform SelfShip
    {
        get
        {
            if (_selfShips != null)
                return _selfShips.transform;
            else
                return null;
        }
    }

    protected CameraController _cameraController;
    protected EnvironmentController _environmentController;
    protected Transform _shootPos;

    protected CharacteristicsShot _characteristicsShot;

    protected int _healthPlayer;
    public int Health { get { return _healthPlayer; } }

    protected MoveMode _moveMode;
    protected Enumerators.ShipType _shipType;

    protected float _speedShip;
    protected float _delayShooting;
    protected int _scoreHit;
    protected bool _disableMoveControl;
    public SpaceShip()
    {
        _gameplayManager = GameClient.Get<IGameplayManager>();
        _uIManager = GameClient.Get<IUIManager>();
        _cameraController = _gameplayManager.GetController<CameraController>();
        _environmentController = _gameplayManager.GetController<EnvironmentController>();
    }
    public void Init()
    {
        var characteristicsShip = MainApp.Instance.GameData.GetShips(_shipType);

        _characteristicsShot = characteristicsShip.characteristicsShot;

        _healthPlayer = characteristicsShip.health;
        _speedShip = characteristicsShip.speed;
        _delayShooting = characteristicsShip.characteristicsShot.delayShooting;

        _scoreHit = characteristicsShip.scoreHit;

        MainApp.Instance.FixedUpdateEvent += MoveShip;
    }
    protected void Shot()
    {
        Bullet bullet = _environmentController.CreateBullet();

        bullet.InitBulletShoting(_characteristicsShot);
        bullet.transform.position = _shootPos.position;
    }
    public void MoveAcceleration(float acceleration)
    {
        _speedShip += acceleration;
    }
    protected void DestroyShip()
    {
        DisposEvent?.Invoke();
        _environmentController.RemoveShip(this);
    }
    protected void MoveShip()
    {
        if (_selfShips == null || _moveMode == MoveMode.None || _gameplayManager.IsPause)
            return;

        Vector3 posShip = SelfShip.position;

        if (_moveMode == MoveMode.Left)
        {
            posShip += (Vector3.left * _speedShip);

            if (posShip.x < -_cameraController.Border && !_disableMoveControl)
            {
                posShip.x = -_cameraController.Border;
            }
        }
        else if (_moveMode == MoveMode.Right)
        {
            posShip += (Vector3.right * _speedShip);

            if (posShip.x > _cameraController.Border && !_disableMoveControl)
            {
                posShip.x = _cameraController.Border;
            }
        }

        SelfShip.position = posShip;
    }
    public virtual void CreateShip()
    {
        if (_selfShips != null)
        {
            Debug.Log("A new ship is created when there is an old one");
            return;
        }

        _selfShips = _environmentController.CreateShipsModel(_shipType);
        _shootPos = _selfShips.transform.Find("ShotPos");
    }
    public virtual void ShipDamage()
    {
        _healthPlayer--;
        if (_healthPlayer <= 0 || _gameplayManager.EndGame)
        {
            DestroyShip();
        }
        else
        {
            MonoBehaviour.Destroy(_selfShips);
            _selfShips = null;
            CreateShip();
        }
    }
    
    public virtual void Dispose()
    {
        MonoBehaviour.Destroy(_selfShips);
        MainApp.Instance.FixedUpdateEvent -= MoveShip;
    }

}
public enum MoveMode
{
    None,
    Right,
    Left
}

