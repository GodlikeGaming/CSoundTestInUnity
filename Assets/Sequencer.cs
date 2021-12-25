using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Sequencer : MonoBehaviour
{
    public Dictionary<int, int> played_notes = new Dictionary<int, int>();
    CsoundUnity csound;
    NumberFormatInfo nfi;

    public float BPM = 120;
   [Serializable]
    public class NoteRecord
    {
        public float Note { get; set; }
        public float Velocity { get; set; }
        public float Duration { get; set; }
        public float StartTime { get; set; }

        public NoteRecord(float note, float velocity, float duration, float startTime)
        {
            Note = note;
            Velocity = velocity;
            Duration = duration;
            StartTime = startTime;
        }
    }

    public List<NoteRecord> lst = new List<NoteRecord>()
    {
        new NoteRecord(64, 0.2f, 0.2f, 0f),
        new NoteRecord(68, 0.2f, 0.2f, 0f),
        new NoteRecord(71, 0.2f, 0.2f, 0f),
        new NoteRecord(64, 0.2f, 0.2f, 1f),
        new NoteRecord(68, 0.2f, 0.2f, 1f),
        new NoteRecord(71, 0.2f, 0.2f, 1f),
        new NoteRecord(69, 0.2f, 0.2f, 2f),
        new NoteRecord(73, 0.2f, 0.2f, 2f),
        new NoteRecord(76, 0.2f, 0.2f, 2f),
        new NoteRecord(69, 0.2f, 0.2f, 3f),
        new NoteRecord(73, 0.2f, 0.2f, 3f),
        new NoteRecord(76, 0.2f, 0.2f, 3f),
        new NoteRecord(0, 0.0f, 0.0f, 4f)
    };

    private void Start()
    {
        var UUID = 0;
        for (int i = 0; i < 127; i++)
        {
            played_notes[i] = (UUID++);
        }
        csound = GetComponent<CsoundUnity>();

        nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";





        //StartCoroutine(PlaySequenceFromList(lst));
        StartCoroutine(PlaySequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlaySequenceFromList(List<NoteRecord> sequence)
    {
        while (!csound.IsInitialized)
        {
            yield return null; //waiting for initialization
        }

        var bpmScale = BPM / 60;
        var currentTime = 0f;
        var reverbWetAmount = 0f;

        while (false) { 
            for (int i = 0; i < sequence.Count; i++)
            {
                var item = sequence[i];
                var note = item.Note;
                var velocity = item.Velocity;
                var secDuration = item.Duration;
                var startTime = item.StartTime;
                currentTime = startTime;
                if (i == sequence.Count - 1)
                {
                    PlayNote(note, velocity, secDuration, reverbWetAmount);
                    yield return new WaitForSeconds(secDuration / bpmScale);
                } else
                {
                    var nextStartTime = sequence[i + 1].StartTime;
                    Debug.Log($"untill next tone: {nextStartTime - currentTime}");
                    PlayNote(note, velocity, secDuration, reverbWetAmount);
                    yield return new WaitForSeconds((nextStartTime - currentTime)/ bpmScale);
                }
            }
        }

    }
    IEnumerator PlaySequence()
    {
        while (!csound.IsInitialized)
        {
            yield return null; //waiting for initialization
        }

        var bpmScale = BPM / 60;
        var pause = 0.5f;
        var reverbWetAmount = 0f;
        while (true)
        {
            for (int j = 0; j < 2; j++) { 
                for (int i = 0; i < 2; i++)
                {
                    PlayNote(64, 0.2f, 0.2f, reverbWetAmount);
                    yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500, ca); // Task.Yield.ToString(nfi)
                    PlayNote(67, 0.2f, 0.2f, reverbWetAmount);
                    PlayNote(71, 0.2f, 0.2f, reverbWetAmount);
                    yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500, ca); // Task.Yield.ToString(nfi)
                }
                for (int i = 0; i < 2; i++)
                {
                    PlayNote(69, 0.2f, 0.2f, reverbWetAmount);
                    yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)
                    PlayNote(72, 0.2f, 0.2f, reverbWetAmount);
                    PlayNote(76, 0.2f, 0.2f, reverbWetAmount);
                    yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)
                }
            }

            for (int i = 0; i < 2; i++)
            { 
                PlayNote(76, 0.2f, 0.2f, reverbWetAmount);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)
                PlayNote(79, 0.2f, 0.2f, reverbWetAmount);
                PlayNote(83, 0.2f, 0.2f, reverbWetAmount);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)

                PlayNote(74, 0.2f, 0.2f, reverbWetAmount);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)
                PlayNote(78, 0.2f, 0.2f, reverbWetAmount);
                PlayNote(81, 0.2f, 0.2f, reverbWetAmount);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)

                PlayNote(72, 0.2f, 0.2f, reverbWetAmount);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)
                PlayNote(76, 0.2f, 0.2f, reverbWetAmount);
                PlayNote(79, 0.2f, 0.2f, reverbWetAmount);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)

                PlayNote(71, 0.2f, 0.2f, reverbWetAmount);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)
                PlayNote(75, 0.2f, 0.2f, reverbWetAmount);
                PlayNote(78, 0.2f, 0.2f, reverbWetAmount);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)
            }
        }
    }

    void PlayNote(float note, float velocity, float secDuration = -1, float reverbWetAmount = 0f)
    {
        var bpmScale = BPM / 60;
        var octave = Math.Floor(note / 12) + 2;
        var id = played_notes[(int)note];


        var str = $"i1.{id} 0 {(secDuration / bpmScale).ToString(nfi)} {(octave + (note % 12) / 100).ToString(nfi)} {velocity.ToString(nfi)} {reverbWetAmount.ToString(nfi)}";
        Debug.Log(str);
        csound.SendScoreEvent(str);
    }

    void StopNote(float note)
    {
        var octave = Math.Floor(note / 12) + 2;
        var id = played_notes[(int)note];
        var str = $"i-1.{id} 0 1 {(8.0f + (note % 12) / 100).ToString(nfi)} 0.1 0";
        Debug.Log(str);
        csound.SendScoreEvent(str);
    }
}
