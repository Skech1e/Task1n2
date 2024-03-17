using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static AllData;

public class Manager : MonoBehaviour
{
    public Transform Content;
    public Entry _entry;
    public Button LoadData, Reset;
    public List<Entry> _entries;

    [Header("DetailsPanel Section")]
    public GameObject DetailsPanel;
    public TextMeshProUGUI _name, _points, _address;


    private void Awake()
    {
    }
    private void Start()
    {

    }
    private void OnEnable()
    {
        LoadData.onClick.AddListener(() => PopulateList());
        Reset.onClick.AddListener(() => FilterData(0));
    }
    private void OnDisable()
    {
        LoadData.onClick.RemoveAllListeners();
        Reset.onClick.RemoveAllListeners();
    }

    void PopulateList()
    {
        _entries = new();
        if (Content.childCount > 0)
        {
            for (byte b = 0; b < Content.childCount; b++)
                Destroy(Content.GetChild(b).gameObject);
        }
        for (byte b = 0; b < allData.clients.Count; b++)
        {
            Entry en = Instantiate(_entry, Content);
            en.Srno.text = (b + 1).ToString();
            en.id = allData.clients[b].id;
            en.isManager = allData.clients[b].isManager;
            en._label = allData.clients[b].label;
            en._points = allData.clients[b].points;
            en.button.onClick.AddListener(() => GetDetails(en));
            _entries.Add(en);
        }
    }

    void GetDetails(Entry en)
    {
        DetailsPanel.SetActive(true);
        foreach (Client client in allData.clients)
        {
            if (client.id == en.id)
            {
                _name.text = client.name;
                _address.text = client.address;
                _points.text = client.points.ToString();
            }
        }
    }

    public void FilterData(int option)
    {
        foreach (Entry en in _entries)
        {
            switch (option)
            {
                case 0:
                    en.gameObject.SetActive(true);
                    break;
                case 1:
                        en.gameObject.SetActive(en.isManager);
                    break;
                case 2:
                        en.gameObject.SetActive(!en.isManager);
                    break;
            }
        }
    }


}
