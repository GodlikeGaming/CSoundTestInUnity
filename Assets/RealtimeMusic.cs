using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RealtimeMusic : MonoBehaviour
{

    CSoundManager cSoundManager;
    bool playing = false;
    // Start is called before the first frame update
    void Start()
    {
        cSoundManager = FindObjectOfType<CSoundManager>();

        var rightStickAction = new InputAction(binding: "<Gamepad>/rightStick");
        
        rightStickAction.Enable();
        rightStickAction.performed += ctx =>
        {
            var val = (Vector2) ctx.ReadValueAsObject();
            var x = val.x;
            var y = val.y;

            if (val.x > 0)
            {
                //if (playing)
                //{
                //    return;
                //}
                    playing = true;
                    var velocity = Mathf.Max(x, 0);
                    //var absY = Mathf.Max(y, 0);
                    var absY = y + 1;
                    var note = Mathf.Round(absY * 12f);
                    Debug.Log(note);
                    note = MusicTheory.RoundToNearestNoteInScale(note, MusicTheory.MinorPentatonicScale);
                    Debug.Log($"Playing with velocity: {velocity} and note: {note}");
                    cSoundManager.PlayNoteRealtime(60 + note, 0.2f, -1);
              
                
            }
            else 
            {
                Debug.Log("stopping");
                playing = false;
                cSoundManager.StopNote(48);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
