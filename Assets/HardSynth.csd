<CsoundSynthesizer>
<CsOptions>
-odac
-+rtmidi=NULL -M0
</CsOptions>
<CsInstruments>
; Initialize the global variables. 
ksmps = 32
nchnls = 2
0dbfs = 1

ga1 init 0 
;instrument will be triggered by keyboard widget
instr 1
    ; klfo line 0, 3, 20
    ; al   lfo klfo, 5, 0
    klfo line 0, 3, 20
    al   lfo klfo, 5, 0
    iwetamt = p6
    idryamt = 1 - p6
    ;aOut poscil p5, cpspch(p4), 1 ;+al, 1
    ;print(al)
    aOut vco2 p5, cpspch(p4)
    
    kEnv madsr 0.01, .05, .6, .1
    
    ;a1 clfilt aOut*kEnv, 500, 0, 10
    a1 clfilt aOut*kEnv, 500, 0, 10
    
    outs a1*idryamt, a1*idryamt


    ; outs aOut*kEnv, aOut*kEnv

    ga1 += a1 * iwetamt

endin



instr 99	;(highest instr number executed last)

; arev reverb ga1, 2
;     outs arev, arev 
  
kroomsize    init      0.85          ; room size (range 0 to 1)
kHFDamp      init      0.5           ; high freq. damping (range 0 to 1)
; create reverberated version of input signal (note stereo input and output)
aRvbL,aRvbR  freeverb  ga1, ga1,kroomsize,kHFDamp
             outs      aRvbL, aRvbR ; send audio to outputs
ga1  = 0	;clear
endin

</CsInstruments>
<CsScore>
;causes Csound to run for about 7000 years...
f0 z
f 1 0 32768 10 1 ; create sine wave table used by the vibrato
i99 0 z
</CsScore>
</CsoundSynthesizer>