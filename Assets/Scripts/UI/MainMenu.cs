using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MainMenu : MonoBehaviour
{
    public static bool groupByGenre = false;

    [SerializeField] private GameObject _SerieSelectionButton;
    [SerializeField] private GameObject _Separator;
    [SerializeField] private GameObject _VerticalLayoutGroup;

    private List<GameObject> _CurrentButtons = new List<GameObject>();    

    private void Awake()
    {
        SeriesLoader.ON_SeriesUpdated.AddListener(UpdateMenu);
    }

    private void UpdateMenu()
    {
        Debug.Log("Launched update method");
        DeleteOldButtons();
        CreateButtons();
    }

    private void CreateButtons()
    {
        if (groupByGenre)
        {
            foreach (GenreData lGenre in GenreData.list)
            {
                CreateSeparator(lGenre);
                foreach (SeriesData seriesData in SeriesData.list)
                {
                    if (seriesData.genre.Equals(lGenre))
                        CreateSelectionButton(seriesData);
                }
            }
        }
        else
        {
            foreach (SeriesData seriesData in SeriesData.list)
            {
                CreateSelectionButton(seriesData);
            }            
        }        
    }

    private void CreateSeparator(GenreData lGenre)
    {
        GameObject lSeparator;
        Separator lSeparatorComponent;

        lSeparator = Instantiate(_Separator);
        lSeparator.transform.SetParent(_VerticalLayoutGroup.transform, false);
        _CurrentButtons.Add(lSeparator);

        lSeparatorComponent = lSeparator.GetComponentInChildren<Separator>();
        lSeparatorComponent.Init(lGenre.id);
    }

    private void CreateSelectionButton(SeriesData seriesData)
    {
        GameObject lNewButton;
        SeriesSelectionButton lSelectionButton;

        lNewButton = Instantiate(_SerieSelectionButton);
        lNewButton.transform.SetParent(_VerticalLayoutGroup.transform, false);
        _CurrentButtons.Add(lNewButton);

        lSelectionButton = lNewButton.GetComponentInChildren<SeriesSelectionButton>();
        lSelectionButton.Init(seriesData.id);
    }

    private void DeleteOldButtons()
    {
        foreach (GameObject lOldButton in _CurrentButtons) Destroy(lOldButton);
    }

}
