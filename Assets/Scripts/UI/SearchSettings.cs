using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchSettings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _SortSettings;
    [SerializeField] private TMP_Dropdown _GroupSettings;

    public void ChangeSortSettings(int pInt)
    {
        switch (pInt)
        {
            case 0:
                SeriesData.SortByTitle();
                break;
            case 1:
                SeriesData.SortByNote();
                break;
            case 2:
                SeriesData.SortByEpisode();
                break;
            default:
                break;
        }
        SeriesLoader.ON_SeriesUpdated.Invoke();
    }

    public void ChangeGroupSettings(int pInt)
    {
        switch (pInt)
        {
            case 0:
                MainMenu.groupByGenre = false;
                break;
            case 1:
                MainMenu.groupByGenre = true;
                break;
            default:
                break;
        }
        SeriesLoader.ON_SeriesUpdated.Invoke();
    }
}
