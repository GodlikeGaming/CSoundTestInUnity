using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder 
{
    public List<NoteRecord> sequence;
    public bool isRecording = false;

    public float startTime = 0f;

    Dictionary<int, NoteRecord> playingNotes = new Dictionary<int, NoteRecord>();
    public List<NoteRecord> HitRecord()
    {
        if (!isRecording)
        {
            StartRecording();
            return null;
        } else
        {
            return StopRecording();
        }
    }

    private List<NoteRecord> StopRecording()
    {
        return sequence;
    }

    private void StartRecording()
    {
        startTime = Time.time;
    }

    internal void PlayNote(NoteRecord noteRecord)
    {
        if (!isRecording) return;
        var currentTime = Time.time;

        var newNoteRecord = new NoteRecord(
            noteRecord.Note,
            noteRecord.Velocity,
            -1,
            currentTime - startTime,
            noteRecord.ReverbAmount
            );

        playingNotes.Add((int)noteRecord.Note, newNoteRecord);
        sequence.Add(noteRecord);
    }

    internal void StopNote(float note)
    {
        if (!isRecording) return;
        var currentTime = Time.time;

        var noteRecord = playingNotes[(int)note];
        noteRecord.Duration = noteRecord.StartTime + (currentTime - startTime);
        
    }
}
