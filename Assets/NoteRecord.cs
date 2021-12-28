using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteRecord
{
    public float Note { get; set; }
    public float Velocity { get; set; }
    public float Duration { get; set; }
    public float StartTime { get; set; }

    public float ReverbAmount { get; set; }

    public NoteRecord(float note, float velocity, float duration = -1f, float startTime = 0f, float reverbAmount = 0f)
    {
        Note = note;
        Velocity = velocity;
        Duration = duration;
        StartTime = startTime;
        ReverbAmount = reverbAmount;
    }
    
}
