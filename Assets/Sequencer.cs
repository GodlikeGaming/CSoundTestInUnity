using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sequencer : MonoBehaviour
{
    public Dictionary<int, int> played_notes = new Dictionary<int, int>();
    CSoundManager cSoundManager;
    NumberFormatInfo nfi;

    public float BPM = 120;
    

    public Section section;

    private void Awake()
    {
        cSoundManager = FindObjectOfType<CSoundManager>();

        SetSection(new Section() { Notes = new List<NoteRecord>() { 
            new NoteRecord(60f, 0.2f, 0.2, 0),

            new NoteRecord(64f, 0.2f, 0.2, 1),
            new NoteRecord(67f, 0.2f, 0.2, 2),
            new NoteRecord(72f, 0.2f, 0.2, 3)
        }, EndTime = 4f });
        Debug.Log(AudioSettings.dspTime);
    }

    public void SetSection(Section _section)
    {
        section = _section;

        nextTime = AudioSettings.dspTime;
        var beatsUntillNextNote = section.Notes[0].StartTime;
        nextTime += (beatsUntillNextNote * 60 / BPM);
    }
    double nextTime;
    int i = 0;
    // Not sure if Update or on AudioRead is better
    void Update()
    {
        // check if record button is hit
        var dspTime = AudioSettings.dspTime;
        if (dspTime >= nextTime)
        {
            var bpmScale = BPM / 60;
            var currentNote = section.Notes[i];
            
            // if note is everything but the last
            double beatsUntillNextNote;
            if (i < section.Notes.Count - 1)
            {
                beatsUntillNextNote = section.Notes[i + 1].StartTime - currentNote.StartTime;
                cSoundManager.PlayNote(currentNote.Note, currentNote.Velocity, currentNote.Duration / bpmScale, currentNote.ReverbAmount);
                i++;
            }
            else
            {
                beatsUntillNextNote = section.EndTime - currentNote.StartTime;
                cSoundManager.PlayNote(currentNote.Note, currentNote.Velocity, currentNote.Duration / bpmScale, currentNote.ReverbAmount);
                // restart the sequence
                i = 0;
                // add extra wait for first notes starttime
                nextTime += (section.Notes[0].StartTime * 60 / BPM);
                Debug.Log(AudioSettings.dspTime);
            }
            nextTime += (beatsUntillNextNote * 60 / BPM);
            
        }
    }


    
}
