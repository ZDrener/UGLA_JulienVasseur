using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SeriesSelectionButton : MonoBehaviour
{
    public static UnityEvent<int> ON_SeriesSelection = new UnityEvent<int>();

    [SerializeField] private TMPro.TextMeshProUGUI _Title;

    private int _ID;

    public void Init(int pID)
    {
        _ID = pID;
        _Title.text = SeriesData.GetSeriesByID(pID).title;
    }

    public void ShowSeriesInfo()
    {
        Debug.Log(_ID);
        ON_SeriesSelection.Invoke(_ID);
    }
}
