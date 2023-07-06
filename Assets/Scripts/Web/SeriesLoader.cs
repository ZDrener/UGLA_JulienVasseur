using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.Events;
using TMPro;

public class SeriesLoader : MonoBehaviour
{
    private string _JsonFilePath = "./Assets/www/series.json";

    public static UnityEvent ON_SeriesUpdated = new UnityEvent();
    public static UnityEvent ON_ConnectionFailed = new UnityEvent();

    [SerializeField] private TMP_InputField _InputField;

    private void Start()
    {
        FetchInfo();
    }

    public void FetchInfo()
    {
        // Default is "http://ugla/"
        StartCoroutine(GetRequest(_InputField.text));
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
                    Debug.LogWarning(pages[page] + ": Error: " + webRequest.error);
                    ConnectionFailed();
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogWarning(pages[page] + ": HTTP Error: " + webRequest.error);
                    ConnectionFailed();
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    ReadJSON();
                    break;
            }            
        }
    }

    private void ConnectionFailed()
    {
        // Clear old lists
        GenreData.list.Clear();
        SeriesData.list.Clear();

        ON_ConnectionFailed.Invoke();
    }

    private void ReadJSON()
    {
        // Clear old lists
        GenreData.list.Clear();
        SeriesData.list.Clear();

        string jsonString = File.ReadAllText(_JsonFilePath);        

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
