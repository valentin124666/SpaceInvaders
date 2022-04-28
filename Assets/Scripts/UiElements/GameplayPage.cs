using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameplayPage : IUIElement
{
    private GameObject _selfPage;

    private IUIManager _uIManager;

    private Transform _healthGroup;
    private List<GameObject> _healthPrefabs;
    private Text _scoreText;

    private float _curentScore;
    private int _curentHealt;
    public void Init()
    {
        _uIManager = GameClient.Get<IUIManager>();

        _selfPage = MonoBehaviour.Instantiate(MainApp.Instance.GameData.GetUiComponent("InGameplay"));
        _selfPage.transform.SetParent(_uIManager.Canvas.transform, false);

        _healthGroup = _selfPage.transform.Find("Healths");
        _scoreText = _selfPage.transform.Find("Score/ScoreText").GetComponent<Text>();

        CreateHealth();
        Hide();
    }
    private void CreateHealth()
    {
        GameObject prefab = MainApp.Instance.GameData.GetUiElements("Health");
        _healthPrefabs = new List<GameObject>();

        for (int i = 0; i < MainApp.Instance.GameData.GetShips(Enumerators.ShipType.PlayerShip).health; i++)
        {
            GameObject health = MonoBehaviour.Instantiate(prefab);
            health.name = $"{i}";
            health.transform.SetParent(_healthGroup, false);
            _healthPrefabs.Add(health);
        }
        _curentHealt = _healthPrefabs.Count - 1;
    }
    public void AddScore(float score)
    {
        _curentScore += score;
        _scoreText.text = _curentScore.ToString();
    }
    public void RemoveHealth()
    {
        if (_curentHealt >= 0)
        {
            _healthPrefabs[_curentHealt].SetActive(false);
            _curentHealt--;
        }
    }
    public void Hide()
    {
        _selfPage.SetActive(false);
    }

    public void Show()
    {
        _selfPage.SetActive(true);
    }
    public void Reset()
    {
        foreach (var item in _healthPrefabs)
        {
            item.SetActive(true);
        }

        _curentScore = 0;
        _curentHealt = _healthPrefabs.Count - 1;

        _scoreText.text = _curentScore.ToString();
    }

    public void Update()
    {
    }
}
