using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class SeriesLoader : MonoBehaviour
{
    private string jsonFilePath = "./Assets/www/series.json";

    public List<SeriesData> seriesList = new List<SeriesData>();

    void Start()
    {
        // A correct website page.
        StartCoroutine(GetRequest("http://ugla/"));
    }

    private IEnumerator GetRequest(string uri)
    {
        // From UnityWebRequest documentation
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }

            ReadJSON();
        }
    }

    private void ReadJSON()
    {
        string jsonString = File.ReadAllText(jsonFilePath);

        List<SeriesDataRaw> lSeriesListRaw = JsonConvert.DeserializeObject<List<SeriesDataRaw>>(jsonString);
        
        SeriesData lData = new SeriesData();

        foreach (SeriesDataRaw lRaw in lSeriesListRaw)
        {
            lData.id = StringToInt(lRaw.id);
            lData.title = lRaw.title;
            lData.genre = lRaw.genre;
            lData.note = StringToInt(lRaw.note);
            lData.episodes = StringToInt(lRaw.episodes);

            seriesList.Add(lData);
        }
    }

    private int StringToInt(string pString)
    {
        int lInt;
        if (int.TryParse(pString, out lInt)) return lInt;
        else Debug.LogError("Value cannot be cast to an int !");
        return 0;
    }
}
