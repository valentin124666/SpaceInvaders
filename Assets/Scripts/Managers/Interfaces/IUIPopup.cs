using System;
using UnityEngine;

public interface IUIPopup
{
    GameObject Self { get; }

    void Init();
    void Show();
    void Show(Action callback);
    void Hide();
    void Update();
    void Reset();
}