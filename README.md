# ICMP Benchmark
This tool is capable of reaching the theoretical limit of sending ICMP pings by P/Invoking the kernel socket API. It will blast a single pixel at the coordinates 1, 1 to limit the amount of work the IPv6 place server has to do. However, please note that this functionality is only supported on Linux, as we are calling Linux kernel functions.
## Usage
```console
./ICMPBenchmark [number]
if number is left blank it will default to all cores
```