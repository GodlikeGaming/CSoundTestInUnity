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
    CsoundUnity csound;
    NumberFormatInfo nfi;

    public float BPM = 120;
    

    public List<NoteRecord> lst;

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


        lst =  new List<NoteRecord>()
    {
        new NoteRecord(60, 0.1f, 0.2f, 0f),
        new NoteRecord(60, 0.1f, 0.2f, 1f),
        new NoteRecord(60, 0.1f, 0.2f, 2f),
        new NoteRecord(60, 0.1f, 0.2f, 3f),
        new NoteRecord(00, 0f, 0, 4f),
    };

        for (int i = 0; i < 100; i++)
        {
            PlayNote(60, 0.1f, 0.2f, 0f, i);
        }
       // StartCoroutine(PlaySequenceFromList(lst));
        //StartCoroutine(PlaySequence());
    }

    // Update is called once per frame
    void Update()
    {
        // check if record button is hit
    }

    IEnumerator PlaySequenceFromList(List<NoteRecord> sequence)
    {
        if (sequence == null) {
            yield break;
        }
        while (!csound.IsInitialized)
        {
            yield return null; //waiting for initialization
        }

        var bpmScale = BPM / 60;
        var currentTime = 0f;
        var reverbWetAmount = 0.1f;

        Debug.Log(sequence.Count);
        while (true) {
            yield return new WaitForSeconds(sequence.First().StartTime);
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
                    var waitUntillNext = secDuration / bpmScale;

                    var totalWait = waitUntillNext;
                    Debug.Log($"untill next tone: {totalWait}");
                    yield return new WaitForSeconds(totalWait);
                } else
                {
                    var nextStartTime = sequence[i + 1].StartTime;
                    
                    PlayNote(note, velocity, secDuration, reverbWetAmount);
                    var waitUntillNext = (nextStartTime - currentTime) / bpmScale;
                    
                    var totalWait = waitUntillNext;
                    
                    Debug.Log($"untill next tone: {totalWait}");
                    yield return new WaitForSeconds(totalWait);
                }
            }
        }
        Debug.Log("sequence stopped");
    }

    internal void RepeatSequence(List<NoteRecord> sequence)
    {
        //StartCoroutine(PlaySequenceFromList(sequence));
        
        var sb = new StringBuilder();
        sb.Append("m foo\n");
        foreach (var note in sequence)
        {
            //PlayNote(note.Note, note.Velocity, note.Duration, note.ReverbAmount, note.StartTime);
            var bpmScale = BPM / 60;
            var octave = Math.Floor(note.Note / 12) + 2;
            var id = played_notes[(int)note.Note];


            var str = $"i1.{id} {note.StartTime.ToString(nfi)} {(note.Duration/ bpmScale).ToString(nfi)} {(octave + (note.Note % 12) / 100).ToString(nfi)} {note.Velocity.ToString(nfi)} {note.ReverbAmount.ToString(nfi)}";
            sb.Append($"{str}\n");
        }
        sb.Append("s\n");
        sb.Append("r 10 foo");
        Debug.Log(sb);
        
        csound.SendScoreEvent(sb.ToString());

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

    void PlayNote(float note, float velocity, float secDuration = -1, float reverbWetAmount = 0f, float startTime = 0f)
    {
        var bpmScale = BPM / 60;
        var octave = Math.Floor(note / 12) + 2;
        var id = played_notes[(int)note];


        var str = $"i1.{id} {startTime.ToString(nfi)} {(secDuration / bpmScale).ToString(nfi)} {(octave + (note % 12) / 100).ToString(nfi)} {velocity.ToString(nfi)} {reverbWetAmount.ToString(nfi)}";
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
