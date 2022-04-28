using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClient : ServiceLocatorBase
{
    private static GameClient _Instance;
    public static GameClient Instance
    {
        get
        {
            if (_Instance==null)
            {
                _Instance = new GameClient();
            }
            return _Instance;
        }
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="GameClient"/> class.
    /// </summary>

    internal GameClient(): base()
    {
        AddService<IGameplayManager>(new GameplayManager());
        AddService<IUIManager>(new UIManager());
    }
    public static T Get<T>()
    {
        return Instance.GetService<T>();
    }
}
