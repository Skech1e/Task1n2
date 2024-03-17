using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Entry : MonoBehaviour
{
    [HideInInspector]
    public byte id;
    [HideInInspector]
    public bool isManager;
    public string _label
    {
        get => _label;
        set => Label.text = value;
    }
    public byte _points
    {
        get => _points;
        set => Points.text = value.ToString();
    }

    public TextMeshProUGUI Srno, Label, Points;
    public Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }
}
