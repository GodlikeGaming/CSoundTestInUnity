using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MusicTheory
{
    public static readonly int OCTAVE = 12;

    public static List<int> MajorPentatonicScale = new List<int>() { 0, 2, 4, 7, 9, 12 };
    public static List<int> MinorPentatonicScale = new List<int>() { 0, 3, 5, 7, 10, 12 };
    internal static float RoundToNearestNoteInScale(float note, List<int> scale)
    {
        var octave = Mathf.Round(note / OCTAVE);
        return scale.OrderBy(n1=> Math.Abs((n1+octave*OCTAVE) - note)).FirstOrDefault() + octave*OCTAVE;
    }
}
