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
        StartCoroutine(PlaySequence());
        csound.SendScoreEvent($"t 0 {BPM}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator PlaySequence()
    {
        var bpmScale = BPM / 60;
        var pause = 0.5f;
        while (true)
        {
            for (int j = 0; j < 2; j++) { 
                for (int i = 0; i < 2; i++)
                {
                    PlayNote(64, 0.2f, 0.2f);
                    yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500, ca); // Task.Yield.ToString(nfi)
                    PlayNote(67, 0.2f, 0.2f);
                    PlayNote(71, 0.2f, 0.2f);
                    yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500, ca); // Task.Yield.ToString(nfi)
                }
                for (int i = 0; i < 2; i++)
                {
                    PlayNote(69, 0.2f, 0.2f);
                    yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)
                    PlayNote(72, 0.2f, 0.2f);
                    PlayNote(76, 0.2f, 0.2f);
                    yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)
                }
            }

            for (int i = 0; i < 2; i++)
            { 
                PlayNote(76, 0.2f, 0.2f);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)
                PlayNote(79, 0.2f, 0.2f);
                PlayNote(83, 0.2f, 0.2f);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)

                PlayNote(74, 0.2f, 0.2f);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)
                PlayNote(78, 0.2f, 0.2f);
                PlayNote(81, 0.2f, 0.2f);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)

                PlayNote(72, 0.2f, 0.2f);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)
                PlayNote(76, 0.2f, 0.2f);
                PlayNote(79, 0.2f, 0.2f);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)

                PlayNote(71, 0.2f, 0.2f);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)
                PlayNote(75, 0.2f, 0.2f);
                PlayNote(78, 0.2f, 0.2f);
                yield return new WaitForSeconds(pause / bpmScale); //await Task.Delay(500); // Task.Yield.ToString(nfi)
            }
        }
    }

    void PlayNote(float note, float velocity, float secDuration = -1)
    {
        var bpmScale = BPM / 60;
        var octave = Math.Floor(note / 12) + 2;
        var id = played_notes[(int)note];


        var str = $"i1.{id} 0 {(secDuration / bpmScale).ToString(nfi)} {(octave + (note % 12) / 100).ToString(nfi)} {velocity.ToString(nfi)} 1";
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
