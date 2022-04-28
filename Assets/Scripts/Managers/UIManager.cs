using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : IService, IUIManager
{
    private List<IUIElement> _uiPages;
    private List<IUIPopup> _uiPopups;

    public GameObject Canvas { get; set; }
    public IUIElement CurentPage { get; set; }

    public void Init()
    {
        Canvas = GameObject.Find("Canvas");

        _uiPages = new List<IUIElement>();
        _uiPages.Add(new SignInPage());
        _uiPages.Add(new GameplayPage());

        _uiPopups = new List<IUIPopup>();
        _uiPopups.Add(new PausePopups());
        _uiPopups.Add(new EndGamePopup());

        foreach (var page in _uiPages)
            page.Init();

        foreach (var popup in _uiPopups)
            popup.Init();

    }

    public void Update()
    {
        foreach (var page in _uiPages)
        {
            page.Update();
        }

        foreach (var popup in _uiPopups)
        {
            popup.Update();
        }
    }
    public void ResetAll()
    {
        foreach (var page in _uiPages)
            page.Reset();

        foreach (var popup in _uiPopups)
            popup.Reset();

    }
    public void Dispose()
    {

    }

    public T GetPage<T>() where T : IUIElement
    {
        IUIElement page = null;
        foreach (var _page in _uiPages)
        {
            if (_page is T)
            {
                page = _page;
                break;
            }
        }
        return (T)page;
    }
    public T GetPopup<T>() where T : IUIPopup
    {
        IUIPopup popup = null;
        foreach (var _popup in _uiPopups)
        {
            if (_popup is T)
            {
                popup = _popup;
                break;
            }
        }

        return (T)popup;
    }
    public void SetPage<T>(bool hideAll = false) where T : IUIElement
    {
        if (hideAll)
        {
            HideAllPages();
        }
        else
        {
            if (CurentPage != null)
                CurentPage.Hide();
        }

        foreach (var _page in _uiPages)
        {
            if (_page is T)
            {
                CurentPage = _page;
                break;
            }
        }

        CurentPage.Show();
    }
    public void DrawPopup<T>() where T : IUIPopup
    {
        IUIPopup popup = null;
        foreach (var _popup in _uiPopups)
        {
            if (_popup is T)
            {
                popup = _popup;
                break;
            }
        }
        popup.Show();
    }
    public void HidePopup<T>() where T : IUIPopup
    {
        foreach (var _popup in _uiPopups)
        {
            if (_popup is T)
            {
                _popup.Hide();
                break;
            }
        }
    }
    public void HideAllPages()
    {
        foreach (var _page in _uiPages)
        {
            _page.Hide();
        }
    }
    public void HideAllPopups()
    {
        foreach (var _popup in _uiPopups)
        {
            _popup.Hide();
        }
    }

}
