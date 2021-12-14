<CsoundSynthesizer>
<CsOptions>
; Select audio/midi flags here according to platform
-odac     ;;;realtime audio out
;-iadc    ;;;uncomment -iadc if RT audio input is needed too
; For Non-realtime ouput leave only the line below:
; -o timedseq.wav -W ;;; for file output any platform
</CsOptions>
<CsInstruments>

sr = 44100
ksmps = 32
nchnls = 2
0dbfs  = 1

ga1 init 0

instr 1
    asig vco2 p5, p4
    ; out aOut, aOut
    a1 clfilt asig, 500, 0, 10
    outs a1, a1
    ga1 += a1  


;   ihold

;   ; If p4 equals 0, turn the note off.
;   if (p6 == 0) kgoto offnow
;     kgoto playit

; offnow:
;   ; Turn the note off now.
;   turnoff

; playit:
;   ; Play the note.
;   out aOut, aOut

endin 


; instr 99	;(highest instr number executed last)

; arev reverb ga1, 0.5
;     outs arev, arev 
  
; ga1  = 0	;clear
; endin


</CsInstruments>
<CsScore>
f0 [60*60*24*7]
i99 0 [60*60*24*7]
;i1 2 1 400 
; i 1 0 4 120
; i 1 0 4 240
; i 1 + . 480
; i 1 + . -480	;when negative it plays backwards 
</CsScore>
</CsoundSynthesizer>