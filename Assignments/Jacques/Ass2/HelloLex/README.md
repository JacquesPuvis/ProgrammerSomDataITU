QUESTION 1

The REGEX accepts a single digit from 0-9 and fails with any other input
The lexer returns the digit as a string.


QUESTION 2

A hello.fs file is generated

There are 3 states

QUESTION 3

Q1. Please pass a digit:
34
The lexer recognizes 34

A: hello3.dll can take both several digits and decimals, therefore it also recognized 34

Q2. Please pass a digit:
34.24
The lexer recognizes 34.24

A: hello3.dll can take both several digits and decimals, therefore it also recognizes 34.24

Q3. Please pass a digit:
34,34
The lexer recognizes 34

A: hello3.dll can take both several digits and decimals BUT it expects a '.' and not a ','. Therefore it only recognizes 34.