using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section 
{
    public List<NoteRecord> Notes;

    public double EndTime { get; set; }

    internal void Transpose(float v)
    {
        foreach (var note in Notes)
        {
            note.Transpose(v);
        }
    }
}
