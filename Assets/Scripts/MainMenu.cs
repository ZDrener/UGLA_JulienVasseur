using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject serieSelectionButton;
    [SerializeField] private GameObject verticalLayoutGroup;

    private List<GameObject> currentSelectionButtons = new List<GameObject>();

    private void Awake()
    {
        SeriesLoader.ON_SeriesUpdated.AddListener(UpdateMenu);
    }

    private void UpdateMenu()
    {
        Debug.Log("Launched update method");

        // Destroy all previous buttons
        foreach (GameObject lOldButton in currentSelectionButtons)
        {
            Destroy(lOldButton);
        }

        // Create new buttons
        GameObject lNewButton;
        foreach (SeriesData seriesData in SeriesLoader.seriesList)
        {
            lNewButton = Instantiate(serieSelectionButton);
            lNewButton.transform.SetParent(verticalLayoutGroup.transform, false);
            lNewButton.GetComponentInChildren<TextMeshProUGUI>().text = seriesData.title;
        }        
    }
}
