# OpenTenma
OpenTenma is an open-source data logger for Tenma multimeters.

***WARNING: this project is ALPHA and not ready for public use***

![](dev/tenma-72-7750-angle.jpg)

![](dev/screenshot.gif)

### Multimeter Notes

* I'm developing against a Tenma 72-7750 multimeter. 
  * It's about [$60 on Amazon](https://www.amazon.com/s?k=Tenma+72-7750) in 2020.
  * It's a rebranded UNI-T UT60G
  * The [manual](http://www.farnell.com/datasheets/70028.pdf) is available
  * Hardware details are on the [Sigrok Tenma 72-7750](https://sigrok.org/wiki/Tenma_72-7750) page

### Tenma Serial Protocol Notes

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
   B: the value. A and B combine so value = BBBB*(10^A)
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
       9=zero or OL
```