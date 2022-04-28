using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignInPage :IUIElement
{
    private GameObject _selfPage;

    private IUIManager _uIManager;
    private IGameplayManager _gameplayManager;

    private Button _startButton;

    public void Init()
    {
        _uIManager = GameClient.Get<IUIManager>();
        _gameplayManager = GameClient.Get<IGameplayManager>();

        _selfPage = MonoBehaviour.Instantiate( MainApp.Instance.GameData.GetUiComponent("StartManu"));
        _selfPage.transform.SetParent(_uIManager.Canvas.transform,false);

        _startButton = _selfPage.transform.Find("StartBotton").GetComponent<Button>();
        _startButton.onClick.AddListener(StartGame);
        Hide();
    }

    private void StartGame()
    {
        _gameplayManager.ChangeAppState(Enumerators.AppState.InGame);
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
       
    }

    public void Update()
    {

    }
}
