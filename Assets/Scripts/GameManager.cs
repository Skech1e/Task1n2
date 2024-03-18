using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject circle;
    public RectTransform Layout;
    int posCount { get => Layout.childCount; }
    [field: SerializeField] public int spawnCount { get; private set; }
    [field: SerializeField] public List<Transform> positions { get; private set; }
    public Button restart;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void OnEnable() => restart.onClick.AddListener(() => Restart());
    private void OnDisable() => restart.onClick.RemoveAllListeners();

    private void Restart()
    {
        foreach (Transform t in positions)
            t.GetChild(0).gameObject.SetActive(false);
        positions.Clear();
        SpawnCircles();
    }


    private void Start()
    {
        SpawnCircles();
    }
    void SpawnCircles()
    {
        spawnCount = Random.Range(5, 11);
        positions = new();
        List<int> count = new();
        for (int i = 0; i < spawnCount; i++)
        {
            int a = Random.Range(0, posCount);
            while (count.Contains(a))
            {
                a = Random.Range(0, posCount);
            }
            positions.Add(Layout.GetChild(a));
            positions[i].GetChild(0).gameObject.SetActive(true);
            count.Add(a);
        }
    }

}
