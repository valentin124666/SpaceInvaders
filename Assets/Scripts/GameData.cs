using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Menu/GameData")]

public class GameData : ScriptableObject
{
    [SerializeField]
    private List<Components> _uiComponents;
    [SerializeField]
    private List<Components> _uiElements;
    [SerializeField]
    private List<Components> _gameplayElements;
    [SerializeField]
    private List<CharacteristicsShip> _shipsType;
    
    public GameObject GetUiComponent(string name)
    {
        return _uiComponents.Find(component => component.name == name).Prefab;
    }
    public GameObject GetUiElements(string name)
    {
        return _uiElements.Find(component => component.name == name).Prefab;
    }
    public GameObject GetGameplayElements(string name)
    {
        return _gameplayElements.Find(component => component.name == name).Prefab;
    }
    public CharacteristicsShip GetShips(Enumerators.ShipType type)
    {
        return _shipsType.Find(component => component.type == type);
    }
}
[Serializable]
public struct Components
{
    public string name;
    public GameObject Prefab;
}
[Serializable]
public struct CharacteristicsShip
{
    public Enumerators.ShipType type;
    public GameObject Prefab;

    public int health;
    [Range(0,1)]
    public float speed;
    public int scoreHit;
    public CharacteristicsShot characteristicsShot;

}
[Serializable]
public struct CharacteristicsShot
{
    public float delayShooting;
    public float speedBullet;
    public Enumerators.TargetShot target;
    public Material material;
}

