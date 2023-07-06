using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;


public class MainMenu : MonoBehaviour
{
    public static bool groupByGenre = false;

    [SerializeField] private GameObject _SerieSelectionButton;
    [SerializeField] private GameObject _Separator;
    [SerializeField] private GameObject _VerticalLayoutGroup;
    [SerializeField] private GameObject _SearchBar;

    private List<GameObject> _CurrentButtons = new List<GameObject>();    

    private void Awake()
    {
        SeriesLoader.ON_SeriesUpdated.AddListener(UpdateMenu);
        SeriesLoader.ON_ConnectionFailed.AddListener(DeleteOldButtons);
    }

    private void UpdateMenu()
    {
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

        Search();
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

    public void Search()
    {
        string lSearchText = _SearchBar.GetComponent<TMP_InputField>().text;
        int lTxtLength = lSearchText.Length;

        if (lTxtLength == 0) foreach (GameObject lButton in _CurrentButtons) lButton.SetActive(true);

        SeriesSelectionButton lSelectionButton;
        SeriesData lData;
        foreach (GameObject lButton in _CurrentButtons)
        {
            if (lButton.TryGetComponent(out lSelectionButton))
            {
                lData = SeriesData.GetSeriesByID(lSelectionButton.seriesID);

                if(lData.title.ToLower().Contains(lSearchText.ToLower()))
                    lButton.SetActive(true);

                else if (lData.genre.Genre.ToLower().Contains(lSearchText.ToLower()))
                    lButton.SetActive(true);

                else
                    lButton.SetActive(false);
            }
        }
    }

    private void DeleteOldButtons()
    {
        GameObject lButton;
        for (int i = _CurrentButtons.Count - 1; i >= 0; i--)
        {
            lButton = _CurrentButtons[i];
            _CurrentButtons.Remove(lButton);
            Destroy(lButton);
        }      
    }
}
