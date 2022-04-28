using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountDestruction
{
    private GameObject _selfObj;
    private Text _text;

    public AccountDestruction(int number, Transform transformDestruction)
    {
        _selfObj = MonoBehaviour.Instantiate(MainApp.Instance.GameData.GetUiElements("Ñheck"));
        _selfObj.transform.position = transformDestruction.position + Vector3.back;
        _text = _selfObj.transform.Find("Text").GetComponent<Text>();
        _text.text = number.ToString();
        MonoBehaviour.Destroy(_selfObj, 0.5f);
    }
}
