using System;
using System.Collections.Generic;
using UnityEngine;

public class MainApp : MonoBehaviour
{
    public event Action LateUpdateEvent;
    public event Action FixedUpdateEvent;

    private static MainApp _Instance;
    public static MainApp Instance
    {
        get { return _Instance; }
        private set { _Instance = value; }
    }

    [SerializeField]
    private LevelData _levelData;
    public LevelData LevelData { get { return _levelData; } }

    [SerializeField]
    private GameData _gameData;
    public GameData GameData { get { return _gameData; } }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);      

    }
    void Start()
    {
        if (Instance == this)
        {
            GameClient.Instance.InitServices();

            GameClient.Get<IGameplayManager>().ChangeAppState(Enumerators.AppState.AppStart);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Instance == this)
        {
            GameClient.Instance.Update();
        }
    }

    private void LateUpdate()
    {
        if (Instance ==this)
        {
            LateUpdateEvent?.Invoke();
        }
    }
    private void FixedUpdate()
    {
        if (Instance == this)
        {
            FixedUpdateEvent?.Invoke();
        }
    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            GameClient.Instance.Dispose();
        }
    }
}
