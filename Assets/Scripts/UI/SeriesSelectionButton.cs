using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SeriesSelectionButton : MonoBehaviour
{
    public static UnityEvent<int> ON_SeriesSelection = new UnityEvent<int>();

    [SerializeField] private TMPro.TextMeshProUGUI _Title;

    public int seriesID;

    public void Init(int pID)
    {
        seriesID = pID;
        _Title.text = SeriesData.GetSeriesByID(pID).title;
    }

    public void ShowSeriesInfo()
    {
        Debug.Log(seriesID);
        ON_SeriesSelection.Invoke(seriesID);
    }
}
