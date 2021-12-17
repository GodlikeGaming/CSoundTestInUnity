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
    klfo line 0, 3, 20
    al   lfo klfo, 5, 0
    
    aOut poscil p5, cpspch(p4)+al, 1
    ;print(al)
    ;aOut vco2 p5, cpspch(p4)
    
    kEnv madsr .1, .05, .6, .1
    
    a1 clfilt aOut*kEnv, 500, 0, 10
    
        outs a1, a1
    ; outs aOut*kEnv, aOut*kEnv

    ga1 += a1 * p6

endin


instr 99	;(highest instr number executed last)

arev reverb ga1, 1
    outs arev, arev 
  
ga1  = 0	;clear
endin

</CsInstruments>
<CsScore>
;causes Csound to run for about 7000 years...
f0 z
f 1 0 32768 10 1
i99 0 z
</CsScore>
</CsoundSynthesizer>