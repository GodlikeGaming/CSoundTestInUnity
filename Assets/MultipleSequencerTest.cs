using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleSequencerTest : MonoBehaviour
{
    public GameObject sequencerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            var obj = Instantiate(sequencerPrefab);
            var sequencerScript = obj.GetComponent<Sequencer>();

            var notes = new int[] { 64, 68, 71, 73, 76};
            sequencerScript.lst = new List<NoteRecord>()
            {
                new NoteRecord(notes[Random.Range(0, notes.Length)], 0.2f, 0.2f, Random.Range(0f, 15f)),

                new NoteRecord(0, 0.0f, 0.0f, 16f)
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
