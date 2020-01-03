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

