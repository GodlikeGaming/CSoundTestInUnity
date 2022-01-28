using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder 
{
    public List<NoteRecord> sequence = new List<NoteRecord>();
    public bool isRecording = false;

    public double startTime = 0f;

    public int quantFactor = 8;

    public double BPM = 60;

    Dictionary<int, NoteRecord> playingNotes = new Dictionary<int, NoteRecord>();
    public Section HitRecord()
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

    private Section StopRecording()
    {
        isRecording = false;
        var endTime = AudioSettings.dspTime - startTime;
        endTime *= BPM / 60;
        var quantizedEndTime = (float)Math.Floor(endTime * quantFactor) / quantFactor;
        return new Section() { Notes = sequence, EndTime = Math.Round(quantizedEndTime)};
    }

    private void StartRecording()
    {
        sequence.Clear();
        startTime = AudioSettings.dspTime;
        isRecording = true;
    }

    internal void PlayNote(NoteRecord noteRecord)
    {
        if (!isRecording) return;
        var currentTime = AudioSettings.dspTime;
      

        var newNoteRecord = new NoteRecord(
            noteRecord.Note,
            noteRecord.Velocity,
            -1,
            currentTime - startTime,
            noteRecord.ReverbAmount
            );

        newNoteRecord.StartTime *= BPM / 60;
        // APPLY QUANTIZATION TO START TIME
        var quantizedStartTime = (float)Math.Floor(newNoteRecord.StartTime * quantFactor) / quantFactor;
        newNoteRecord.StartTime = quantizedStartTime;



        playingNotes.Add((int)noteRecord.Note, newNoteRecord);
        sequence.Add(newNoteRecord);
    }

    internal void StopNote(float note)
    {
        if (!isRecording) return;
        var currentTime = AudioSettings.dspTime;

        var noteRecord = playingNotes[(int)note];
        noteRecord.Duration = (currentTime - startTime) - noteRecord.StartTime;
        noteRecord.Duration *= BPM / 60;
        //APPLY QUANTIZATION TO DURATION

        var quantizedDuration = (float) Math.Round(noteRecord.Duration * quantFactor) / quantFactor;
        noteRecord.Duration = Math.Max(quantizedDuration, 1/quantFactor);
        playingNotes.Remove((int)note);
    }
}
