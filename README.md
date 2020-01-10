# OpenTenma
OpenTenma is an open-source data logger for Tenma multimeters.

***WARNING: this project status is ALPHA and not ready for public use***

### Serial Protocol Notes

* Each line contains a 9 character ASCII string (followed by a line break)
* Each burst contains an old value and a new value.
* One new value will always match the old value of the next burst.

```
OUTPUT DATA FORMAT:
   209493802 <-- this is a demo string for 09.49 kOhms
   11643;80: <-- this is a demo string for 16.43 Volts
   000252802 <-- this is a demo string for 0025 Hz
   428702802 <-- this is a demo string for 28.70 MHz  
   012345678 <-- index values
   ABBBBCDEF
   ||   ||||
   ||   |||`some type of config I don't really care about
   ||   ||`some type of config I don't really care about
   ||   |`sign (positive, negative, zero)
   ||   `mode select.
   |`4 digit meter
   `decimal indicator
   A: decimal indicator.
   B: the value. A abd B combine so value = BBBB*(10^A)
       if B is 5, I think it's overload
   C: mode select
       3=resistance
       ;=voltage
       6=capacitance
       2=frequency
       ?=current (mA range)
       9=current (A range)
       4=temperature
   D: sign select
       <=negative
       8=positive
       9=zero(I think?)
```

#### Example Reads
* `209523802` is 9.52 kOhm
* `04954;80:` is 4.954 V
* `206486802` is 64.8 nF
* `428702802` is 28.70 MHz
* `00844?80:` is 8.44 mA
* `000264800` is 26.0 C

## Real Measurements
```
# temperature
000224800	22 C
000234800	23 C

# low current
00000?<0:	-0.00 mA
00000?80:   0.00 mA

# high current
000009<08	-0.00 A
000009808	0.00 A

# frequency
000002802	0 Hz

# resistance
000033802	000.3 Ohm
000183802	001.8 Ohm
015943802	159.4 Ohm
107843802	0.784 kOhm
107823802	0.782 kOhm
121733802	2.173 kOhm
155393802	5.539 kOhm
210403802	10.40 kOhm
304733802	047.3 kOhm
426223802	2.622 MOhm
560003902	OL

# capacitance
000726802	0.072 nF
011246802	1.124 nF
155366802	55.36 nF
421886802	21.88 uF
510136802	101.3 uF

# voltage
00000;<0:	-0.000 V
00000;80:	0.000 V
10628;80:	6.27 V
10626;<0:	-6.26 V
00114;80:	0.114 V
00112;80:	0.112 V
00097;80:	0.097 V
00014;80:	0.014 V
00001;80:	0.001 V
40025;808	2.5 mV
40034;808	3.4 mV
00004;808	.004 V
```