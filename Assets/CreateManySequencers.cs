using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManySequencers : MonoBehaviour
{
    public GameObject sequencerPrefab;
    // Start is called before the first frame update
    void Start()
    {

        CreateSequencer(new Section()
        {
            Notes = new List<NoteRecord>()
            {
                new NoteRecord(60, 0.2f, 0.2, 0),
                new NoteRecord(60, 0.2f, 0.2, 1),
                new NoteRecord(60, 0.2f, 0.2, 2),
                new NoteRecord(65, 0.2f, 0.2, 3),
                new NoteRecord(65, 0.2f, 0.2, 4),
                new NoteRecord(65, 0.2f, 0.2, 5),
            },
            EndTime = 6f
        });

        CreateSequencer(new Section()
        {
            Notes = new List<NoteRecord>()
            {
                new NoteRecord(64, 0.2f, 0.2, 0.5),
                new NoteRecord(64, 0.2f, 0.2, 1.5),
                new NoteRecord(64, 0.2f, 0.2, 2.5),
                new NoteRecord(69, 0.2f, 0.2, 3.5),
                new NoteRecord(69, 0.2f, 0.2, 4.5),
                new NoteRecord(69, 0.2f, 0.2, 5.5)
            },
            EndTime = 6f
        });

        CreateSequencer(new Section()
        {
            Notes = new List<NoteRecord>()
            {
                new NoteRecord(84, 0.2f, 0.2, 0),
                new NoteRecord(86, 1/8f, 0.2, 1/8f),
                new NoteRecord(84, 1/8f, 0.2, 2/8f),
                new NoteRecord(83, 1/8f, 0.2, 3/8f),
                new NoteRecord(84, 1/8f, 0.2, 4/8f),

                new NoteRecord(79, 1/8f, 0.2, 2.25f),
                new NoteRecord(79, 1/8f, 0.2, 2.5f),
                new NoteRecord(77, 1/8f, 0.2, 2.75f),
                new NoteRecord(77, 1/8f, 0.2, 3f),
                new NoteRecord(76, 1/8f, 0.2, 3.25f),
                new NoteRecord(76, 1/8f, 0.2, 3.5f),
            },
            EndTime = 4f
        });

        CreateSequencer(new Section()
        {
            Notes = new List<NoteRecord>()
            {
                new NoteRecord(79, 0.2f, 0.2, 0),
                new NoteRecord(81, 1/8f, 0.2, 1/8f),
                new NoteRecord(79, 1/8f, 0.2, 2/8f),
                new NoteRecord(76, 1/8f, 0.2, 3/8f),
                new NoteRecord(79, 1/8f, 0.2, 4/8f),

                new NoteRecord(76, 1/8f, 0.2, 2.25f),
                new NoteRecord(76, 1/8f, 0.2, 2.5f),
                new NoteRecord(74, 1/8f, 0.2, 2.75f),
                new NoteRecord(74, 1/8f, 0.2, 3f),
                new NoteRecord(72, 1/8f, 0.2, 3.25f),
                new NoteRecord(72, 1/8f, 0.2, 3.5f),
            },
            EndTime = 4f
        });
    }

    private void CreateSequencer(Section section)
    {
        var sequencer = Instantiate(sequencerPrefab);
        
        sequencer.GetComponent<Sequencer>().SetSection(section);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
