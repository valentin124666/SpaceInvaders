using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePopup : IUIPopup
{
    private GameObject _selfPopups;
    public GameObject Self => _selfPopups;

    private IUIManager _uIManager;
    private IGameplayManager _gameplayManager;

    private Text _messageText;

    private Button _restartButton, _mainMenuButton;
    public void Init()
    {
        _uIManager = GameClient.Get<IUIManager>();
        _gameplayManager = GameClient.Get<IGameplayManager>();

        _selfPopups = MonoBehaviour.Instantiate(MainApp.Instance.GameData.GetUiComponent("EndGame"));
        _selfPopups.transform.SetParent(_uIManager.Canvas.transform, false);

        _messageText = _selfPopups.transform.Find("Message").GetComponent<Text>();

        _restartButton = _selfPopups.transform.Find("Restart").GetComponent<Button>();
        _mainMenuButton = _selfPopups.transform.Find("MainMenu").GetComponent<Button>();

        _restartButton.onClick.AddListener(()=>_gameplayManager.RefreshGameplay());
        _mainMenuButton.onClick.AddListener(()=>_gameplayManager.ChangeAppState(Enumerators.AppState.AppStart));
    }
    public void ReportResult(string message)
    {
        _messageText.text = message;
    }
    public void Hide()
    {
        _selfPopups.SetActive(false);
    }

    public void Reset()
    {
    }

    public void Show()
    {
        _selfPopups.SetActive(true);
    }

    public void Show(Action callback)
    {
        _selfPopups.SetActive(true);
    }

    public void Update()
    {
       
    }
}
