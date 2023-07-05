using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separator : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _Title;

    private int _ID;

    public void Init(int pID)
    {
        _ID = pID;
        _Title.text = GenreData.GetGenreByID(pID).Genre;
    }
}
