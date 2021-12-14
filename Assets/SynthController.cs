using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

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


    public List<int> played_notes = new List<int>();
    CsoundUnity csound;
    NumberFormatInfo nfi;
    private void Start()
    {
        for (int i = 0; i < frequencies.Length; i++)
        {
            frequencies[i] /= 2;
        }

        nfi = new NumberFormatInfo();
        nfi.NumberDecimalSeparator = ".";

        csound = GetComponent<CsoundUnity>();
        if (!csound.CompiledWithoutError())
        {
            Debug.Log(csound.GetSpout());
        }
    }
    void Update()
    {


        checkForKey(KeyCode.A, 0);
        checkForKey(KeyCode.W, 1);
        checkForKey(KeyCode.S, 2);
        checkForKey(KeyCode.E, 3);
        checkForKey(KeyCode.D, 4);
        checkForKey(KeyCode.F, 5);
        checkForKey(KeyCode.T, 6);
        checkForKey(KeyCode.G, 7);
        checkForKey(KeyCode.Y, 8);
        checkForKey(KeyCode.H, 9);
        checkForKey(KeyCode.U, 10);
        checkForKey(KeyCode.J, 11);
        checkForKey(KeyCode.K, 12);
        //checkForKey(KeyCode.A, 0);
        //checkForKey(KeyCode.A, 0);

    }
    void checkForKey(KeyCode code, int note)
    {
        if (Input.GetKeyDown(code))
        {
            var str = $"i1 0 0.5 {frequencies[note].ToString(nfi)} 0.1 1";
            played_notes.Add(note);
            Debug.Log(str);
            csound.SendScoreEvent(str);
            //csound.Se
        }
        else if (Input.GetKeyUp(code))
        {
            //var str = $"i1 0 0.2 {frequencies[note].ToString(nfi)} 0.1 0";
            //played_notes.Remove(note);
            //Debug.Log(str);
            //csound.SendScoreEvent(str);
        }
    }
}
