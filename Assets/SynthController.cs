using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class SynthController : MonoBehaviour
{
    float[] frequencies = new float[]
    {
        440f,
        466.16f,
        493.88f,
        523.25f,
        554.37f,
        587.33f,
        622.25f,
        659.25f,
        698.46f,
        739.99f,
        783.99f,
        830.61f,
        880f
    };


    //public List<int> played_notes = new List<int>();
    public float velocity = 0.5f;
    public float reverbWetAmount = 0.2f;
    public Dictionary<int, int> played_notes = new Dictionary<int, int>();
    CsoundUnity csound;
    NumberFormatInfo nfi;

    List<InputAction> keyActions = new List<InputAction>();
    private void Start()
    {
        var UUID = 0;
        for (int i = 0; i < 127; i++)
        {
            played_notes[i] = (UUID++);
        }

        for (int i = 0; i < frequencies.Length; i++)
        {
            frequencies[i] /= 8;
        }

        nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";

        csound = GetComponent<CsoundUnity>();

        
        if (!csound.CompiledWithoutError())
        {
            Debug.Log(csound.GetSpout());
        }

        for (int i = 21; i < 127; i++)
        {
            var stringOfNote = i / 100 >= 1 ? $"{i}" : $"0{i}";
            var myAction = new InputAction(binding: $"MidiDevice*/note{stringOfNote}");
            myAction.Enable();
            myAction.started += ctx =>
            {
                // play note
                var note = int.Parse(ctx.control.name.Substring(4, 3));
                //var velocity = (float)ctx.ReadValueAsObject();

                Debug.Log($"start {note} with v: {velocity}");
                PlayNote(note, velocity, -1f, reverbWetAmount);
            };
            myAction.canceled += ctx =>
            {
                // stop note
                var note = int.Parse(ctx.control.name.Substring(4, 3));
                var velocity = (float)ctx.ReadValueAsObject();

                Debug.Log($"stop {note} with v: {velocity}");
                StopNote(note);
            };
            keyActions.Add(myAction);
        }
    }
    void Update()
    {


        checkForKey(Key.A, 0);
        checkForKey(Key.W, 1);
        checkForKey(Key.S, 2);
        checkForKey(Key.E, 3);
        checkForKey(Key.D, 4);
        checkForKey(Key.F, 5);
        checkForKey(Key.T, 6);
        checkForKey(Key.G, 7);
        checkForKey(Key.Y, 8);
        checkForKey(Key.H, 9);
        checkForKey(Key.U, 10);
        checkForKey(Key.J, 11);
        checkForKey(Key.K, 12);
        //checkForKey(KeyCode.A, 0);
        //checkForKey(KeyCode.A, 0);

        //for (int i = 0; i < 127;i++)
        //{
        //    if (MidiJack.MidiMaster.GetKeyDown(MidiJack.MidiChannel.All, i))
        //    {
        //        PlayNote(i);
        //    }  
        //    if (MidiJack.MidiMaster.GetKeyUp(MidiJack.MidiChannel.All, i))
        //    {
        //        StopNote(i);
        //    }
        //}
       // myAction.Enable();
    }
    void checkForKey(Key code, float note)
    {
        note += 12 * 6;
        if (Keyboard.current[code].wasPressedThisFrame)
        {
            PlayNote(note, velocity, -1, reverbWetAmount);
            //csound.Se
        }
        if (Keyboard.current[code].wasReleasedThisFrame)
        {
            StopNote(note);
        }
    }

    void PlayNote(float note, float velocity, float secDuration = -1, float reverbWetAmount = 0f)
    {
        var octave = Math.Floor(note / 12) + 2;
        var id = played_notes[(int)note];


        var str = $"i1.{id} 0 {(secDuration).ToString(nfi)} {(octave + (note % 12) / 100).ToString(nfi)} {velocity.ToString(nfi)} {reverbWetAmount.ToString(nfi)}";
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
