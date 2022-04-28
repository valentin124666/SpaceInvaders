using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePopups : IUIPopup
{
    private Action _callback;

    private GameObject _selfPopups;
    public GameObject Self => _selfPopups;

    private IUIManager _uIManager;

    private Text _counterText;

    private int _curentNumber;
    private float _delayNumber—hange = 0.5f, _timerNumber—hange;

    public void Init()
    {
        _uIManager = GameClient.Get<IUIManager>();

        _selfPopups = MonoBehaviour.Instantiate(MainApp.Instance.GameData.GetUiComponent("PausePopups"));
        _selfPopups.transform.SetParent(_uIManager.Canvas.transform, false);

        _counterText = _selfPopups.transform.Find("—ounter").GetComponent<Text>();

        _timerNumber—hange = _delayNumber—hange;
        Refresh();
        Hide();

        MainApp.Instance.FixedUpdateEvent += Countdown;
    }

    private void Countdown()
    {
        if (!_selfPopups.activeSelf)
            return;

        if (_timerNumber—hange <= 0)
        {
            _curentNumber--;
            _counterText.text = _curentNumber.ToString();

            _timerNumber—hange = _delayNumber—hange;

            if (_curentNumber <= 0)
            {
                Refresh();
                Hide();
                _callback?.Invoke();
            }
        }
        else
        {
            _timerNumber—hange -= Time.deltaTime;
        }
    }
    private void Refresh()
    {
        _curentNumber = 3;
        _counterText.text = _curentNumber.ToString();
    }
    public void Hide()
    {
        _selfPopups.SetActive(false);
    }

    public void Show()
    {
        _selfPopups.SetActive(true);
    }

    public void Show(Action callback)
    {
        _callback = callback;
        _selfPopups.SetActive(true);
    }
    public void Reset()
    {
        _callback = null;
        Refresh();
        Hide();
    }
    public void Update()
    {

    }
}
