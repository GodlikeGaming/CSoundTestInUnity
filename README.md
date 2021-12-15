# CSoundTestInUnity

Testing CSound with Unity, play sounds with keyboard or midi keyboard.


## To change sounds

The object Synth has a component called CSoundUnity, here you can change which .csd (CSound) file it uses as its instrument.

## unity version 

tested on unity version 2020.3.2f1

## common fix
if no sound is playing check that the AudioSource component on Synth is set to "Play on Awake"
