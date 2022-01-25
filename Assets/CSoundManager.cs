using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CSoundManager : MonoBehaviour
{
    CsoundUnity csound;
    NumberFormatInfo nfi;

    bool debugNotesPlayed = false;
    public Dictionary<int, int> played_notes = new Dictionary<int, int>();

    // Start is called before the first frame update
    void Start()
    {
        var UUID = 0;
        for (int i = 0; i < 127; i++)
        {
            played_notes[i] = (UUID++);
        }
        csound = GetComponent<CsoundUnity>();

        nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";
    }



    public void PlayNote(float note, float velocity, double secDuration = -1, float reverbWetAmount = 0f, float startTime = 0f)
    {
        var octave = Math.Floor(note / 12) + 2;
        var id = played_notes[(int)note];


        var str = $"i1.{id} {startTime.ToString(nfi)} {(secDuration).ToString(nfi)} {(octave + (note % 12) / 100).ToString(nfi)} {velocity.ToString(nfi)} {reverbWetAmount.ToString(nfi)}";
        if (debugNotesPlayed) Debug.Log(str);
        csound.SendScoreEvent(str);
    }

    public void PlayNote(NoteRecord noteRecord)
    {
        float note = noteRecord.Note;
        float velocity = noteRecord.Velocity;
        float secDuration = -1f;
        float reverbWetAmount = noteRecord.ReverbAmount;

        var octave = Math.Floor(note / 12) + 2;
        var id = played_notes[(int)note];


        var str = $"i1.{id} 0 {(secDuration).ToString(nfi)} {(octave + (note % 12) / 100).ToString(nfi)} {velocity.ToString(nfi)} {reverbWetAmount.ToString(nfi)}";
        Debug.Log(str);
        csound.SendScoreEvent(str);
    }


    public void StopNote(float note)
    {
        var octave = Math.Floor(note / 12) + 2;
        var id = played_notes[(int)note];

        var str = $"i-1.{id} 0 1 {(8.0f + (note % 12) / 100).ToString(nfi)} 0.1 0";
        if (debugNotesPlayed) Debug.Log(str);
        csound.SendScoreEvent(str);
    }

    public void PlayNoteRealtime(float note, float velocity, float duration)
    {
        float secDuration = -1f;

        var octave = Math.Floor(note / 12) + 2;


        var str = $"i1.{180} 0 {(secDuration).ToString(nfi)} {(octave + (note % 12) / 100).ToString(nfi)} {velocity.ToString(nfi)}";
        Debug.Log(str);
        csound.SendScoreEvent(str);
    }
}
