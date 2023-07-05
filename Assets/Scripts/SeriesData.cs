using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeriesData
{
    public static List<SeriesData> list = new List<SeriesData>();

    public int id;
    public string title;
    public GenreData genre;
    public int note;
    public int episodes;

    public static SeriesData GetSeriesByID(int pID)
    {
        foreach (SeriesData lSeries in list)
            if (lSeries.id.Equals(pID)) return lSeries;

        Debug.LogWarning("No series found with ID : " + pID);
        return null;
    }

    internal static void SortByNote()
    {
        list.Sort(delegate (SeriesData t1, SeriesData t2)
        { return (t2.note.CompareTo(t1.note)); });
    }

    internal static void SortByEpisode()
    {
        list.Sort(delegate (SeriesData t1, SeriesData t2)
        { return (t2.episodes.CompareTo(t1.episodes)); });
    }

    public static void SortByID()
    {
        list.Sort(delegate (SeriesData t1, SeriesData t2)
        { return (t1.id.CompareTo(t2.id)); });
    }
    public static void SortByTitle()
    {
        list.Sort(delegate (SeriesData t1, SeriesData t2)
        { return (t1.title.CompareTo(t2.title)); });
    }
}

public class GenreData
{
    public static List<GenreData> list = new List<GenreData>();

    public int id;
    public string Genre;

    public static GenreData GetGenreByName(string pName)
    {
        foreach (GenreData lGenre in list)
            if (lGenre.Genre.Equals(pName)) return lGenre;        

        Debug.LogWarning("No genre found with name : " + pName);
        return null;
    }
    public static GenreData GetGenreByID(int pID)
    {
        foreach (GenreData lGenre in list)
            if (lGenre.id.Equals(pID)) return lGenre;

        Debug.LogWarning("No genre found with ID : " + pID);
        return null;
    }
}

#region RAW DATA
public class SeriesDataRaw
{
    public string id;
    public string title;
    public string genre;
    public string note;
    public string episodes;
}
public class GenreDataRaw
{
    public string id;
    public string Genre;
}
#endregion 

public class RootObject
{
    public List<SeriesDataRaw> series { get; set; }
    public List<GenreDataRaw> genre { get; set; }
}
