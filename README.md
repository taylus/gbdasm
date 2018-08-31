# gbdasm
A Game Boy disassembler that produces [RGBDS](https://github.com/rednex/rgbds) compatible source. Written in .NET Core.

[![Build Status](https://travis-ci.org/taylus/gbdasm.svg?branch=master)](https://travis-ci.org/taylus/gbdasm)
[![Coverage Status](https://coveralls.io/repos/github/taylus/gbdasm/badge.svg?branch=master)](https://coveralls.io/github/taylus/gbdasm?branch=master)

## Usage
```
gbdasm -o rom.asm rom.gb
```

gbdasm's goal is to produce identical ROMs when disassembled and re-assembled:

## Re-assembly
```
rgbasm -h -L -o rom.o rom.asm
rgblink -o test.gb test.o
bgb test.gb
```

![Screenshot](screenshot.png "Screenshot")

## Comparing ROMs
```
certutil -hashfile test.gb
certutil -hashfile rom.gb
```