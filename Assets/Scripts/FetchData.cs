using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using static AllData;

public class FetchData : MonoBehaviour
{
    public static FetchData fetchData { get; private set; }

    const string url = "https://qa.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";
    UnityWebRequest request;
        

    private void Awake()
    {
        if (fetchData == null)
            fetchData = this;
        else
            Destroy(this);

        request = UnityWebRequest.Get(url);
        StartCoroutine(GetData());
    }

    public IEnumerator GetData()
    {
        yield return request.SendWebRequest();

        string json = request.downloadHandler.text;
        allData = LoadClients(json);
    }

    AllData LoadClients(string json) => JsonConvert.DeserializeObject<AllData>(json);
}

[Serializable]
public class Client
{
    public bool isManager;
    public byte id;
    public string label;

    public string name, address;
    public byte points
    {
        get
        {
            switch (id)
            {
                case 1:
                    name = allData.data._1.name;
                    address = allData.data._1.address;
                    return allData.data._1.points;
                case 2:
                    name = allData.data._2.name;
                    address = allData.data._2.address;
                    return allData.data._2.points;
                case 3:
                    name = allData.data._3.name;
                    address = allData.data._3.address;
                    return allData.data._3.points;
                default:
                    return 0;
            }
        }
    }
}
[Serializable]
public class ClientData
{
    [JsonProperty("1")]
    public Info _1;
    [JsonProperty("2")]
    public Info _2;
    [JsonProperty("3")]
    public Info _3;

    [Serializable]
    public class Info
    {
        public string address;
        public string name;
        public byte points;
    }
}
[Serializable]
public class AllData
{
    public static AllData allData { get; set; }
    public List<Client> clients;
    public ClientData data;
    public string label;
}
