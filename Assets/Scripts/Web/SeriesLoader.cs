using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.Events;

public class SeriesLoader : MonoBehaviour
{
    private string jsonFilePath = "./Assets/www/series.json";

    public static UnityEvent ON_SeriesUpdated = new UnityEvent();

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

        // Get genres
        List<GenreDataRaw> lGenreListRaw = JsonConvert.DeserializeObject<RootObject>(jsonString).genre;

        foreach (GenreDataRaw lRaw in lGenreListRaw)
        {
            GenreData lData = new GenreData();

            lData.id = StringToInt(lRaw.id);
            lData.Genre = lRaw.Genre;

            GenreData.list.Add(lData);
        }

        // Get series
        List<SeriesDataRaw> lSeriesListRaw = JsonConvert.DeserializeObject<RootObject>(jsonString).series;

        foreach (SeriesDataRaw lRaw in lSeriesListRaw)
        {
            SeriesData lData = new SeriesData();

            lData.id = StringToInt(lRaw.id);
            lData.title = lRaw.title;
            lData.genre = GenreData.GetGenreByName(lRaw.genre);
            lData.note = StringToInt(lRaw.note);
            lData.episodes = StringToInt(lRaw.episodes);

            SeriesData.list.Add(lData);
        }

        ON_SeriesUpdated.Invoke();
    }

    private int StringToInt(string value)
    {
        int result;
        int.TryParse(value, out result);
        return result;
    }
}
