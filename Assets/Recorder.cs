using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder 
{
    public List<NoteRecord> sequence = new List<NoteRecord>();
    public bool isRecording = false;

    public float startTime = 0f;

    public int quantFactor = 8;

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
        isRecording = false;
        var time = Time.time - startTime;
        var quantizedStartTime = (float)Math.Floor(time * quantFactor) / quantFactor;
        sequence.Add(new NoteRecord(0, 0, 0, quantizedStartTime));
        return sequence;
    }

    private void StartRecording()
    {
        sequence.Clear();
        startTime = Time.time;
        isRecording = true;
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

        // APPLY QUANTIZATION TO START TIME
        var quantizedStartTime = (float)Math.Floor(newNoteRecord.StartTime * quantFactor) / quantFactor;
        newNoteRecord.StartTime = quantizedStartTime;



        playingNotes.Add((int)noteRecord.Note, newNoteRecord);
        sequence.Add(newNoteRecord);
    }

    internal void StopNote(float note)
    {
        if (!isRecording) return;
        var currentTime = Time.time;

        var noteRecord = playingNotes[(int)note];
        noteRecord.Duration = (currentTime - startTime) - noteRecord.StartTime;

        //APPLY QUANTIZATION TO DURATION

        var quantizedDuration = (float) Math.Round(noteRecord.Duration * quantFactor) / quantFactor;
        noteRecord.Duration = Math.Max(quantizedDuration, 1/quantFactor);
        playingNotes.Remove((int)note);
    }
}
