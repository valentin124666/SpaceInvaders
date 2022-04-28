using UnityEngine;

public interface IUIManager
{
    GameObject Canvas { get; set; }

    IUIElement CurentPage { get; set; }
    void SetPage<T>(bool hideAll = false) where T : IUIElement;
    T GetPage<T>() where T : IUIElement;
    void DrawPopup<T>() where T : IUIPopup;
    void HidePopup<T>() where T : IUIPopup;
    T GetPopup<T>() where T : IUIPopup;

    void ResetAll();

    void HideAllPages();
    void HideAllPopups();

}
