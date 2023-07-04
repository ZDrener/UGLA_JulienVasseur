using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeriesData : MonoBehaviour
{
    public static List<SeriesData> list = new List<SeriesData>();
    public int id;
    public string title;
    public string genre;
    public int note;
    public int episodes;
}
