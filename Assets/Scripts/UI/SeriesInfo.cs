using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SeriesInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _Title;
    [SerializeField] private TextMeshProUGUI _Genre;
    [SerializeField] private TextMeshProUGUI _Episodes;
    [Space]
    [SerializeField] private TextMeshProUGUI _Note;
    [SerializeField] private Image _NoteBar;
    [SerializeField] private Gradient _NoteGradient;

    private const string GENRE_PREFIX = "Genre : ";
    private const string EPISODES_PREFIX = "Episodes : " ;
    private const float MAX_NOTE = 10f;

    private void Awake()
    {
        SeriesSelectionButton.ON_SeriesSelection.AddListener(UpdateSeriesInfo);
    }

    private void UpdateSeriesInfo(int pSeriesID)
    {
        SeriesData lData = SeriesData.GetSeriesByID(pSeriesID);

        _Title.text = lData.title;
        _Genre.text = GENRE_PREFIX + lData.genre;
        _Episodes.text = EPISODES_PREFIX + lData.episodes;
        _Note.text = lData.note.ToString();
        _NoteBar.fillAmount = lData.note / MAX_NOTE;
        _NoteBar.color = _NoteGradient.Evaluate(lData.note / MAX_NOTE);
    }
}
