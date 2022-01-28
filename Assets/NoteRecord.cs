using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteRecord
{
    public float Note { get; set; }
    public float Velocity { get; set; }
    /// <summary>
    /// In beats, need to be multiplied with BPM to get in seconds
    /// </summary>
    public double Duration { get; set; }

    internal void Transpose(float v)
    {
        Note += v * MusicTheory.OCTAVE;
    }

    /// <summary>
    /// In beats, needs to be multiplied with BPM to get in seconds
    /// </summary>
    public double StartTime { get; set; }

    public float ReverbAmount { get; set; }

    public NoteRecord(float note, float velocity, double duration = -1f, double startTime = 0f, float reverbAmount = 0f)
    {
        Note = note;
        Velocity = velocity;
        Duration = duration;
        StartTime = startTime;
        ReverbAmount = reverbAmount;
    }
    
}
