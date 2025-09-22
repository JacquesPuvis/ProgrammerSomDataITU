### Question 1: What are the regular expressions involved, and which semantic values are they associated with?
The lexer matches digits and everything else. 
For digits it uses ['0'-'9'] as regular expression and then it matches a single digit, and the semantic value is a returned matched digit string. 
For everything else it uses "_" as regular expression, and then it matches any other character, and the semantic value is an error raised. 

### Question 2: Generate the lexer out of the specification using a command prompt. Which additional file is generated during the process? How many states are there by the automaton of the lexer?
The command: dotnet ~/fsharp/fslex.dll --unicode hello.fsl
From above command hello.fs and hello.fsl is generated during this command. 
There is generated 3 states by the automaton of the lexer. 

### Question 3: Compile and run the generated program hello.fs from question 2.
First we run "dotnet build hello.fsproj" making sure that the .fsproj files has this
reference: <Reference Include="/Users/cecilieelkjaer/fsharp/FsLexYacc.Runtime.dll" /> so that 
it has the correct path to the runtime file. 
Next we run "dotnet bin/Debug/net8.0/hello.dll", and then it asks for a digit, which
we pass onto it, and then see that it only recognizes one digit. 

### Question 4: 
Added hello2.fsl, changed the regular expression to ['0'-'9']+, and ran the 
"dotnet ~/fsharp/fslex.dll --unicode hello2.fsl", "dotnet build hello2.fsproj" and 
"dotnet bin/Debug/net8.0/hello2.dll" commands again.

### Question 5: 
Added hello3.fsl, changed the regular expression to ['+''-'] ? (['0'-'9']*['.']) ? ['0'-'9']+, 
and ran the "dotnet ~/fsharp/fslex.dll --unicode hello3.fsl", "dotnet build hello3.fsproj" 
and "dotnet bin/Debug/net8.0/hello3.dll" commands again.
Added a alias for "dotnet ~/fsharp/fslex.dll --unicode", so instead of writing it like that 
it can now be written with only "fslex hello3.fsl"

### Question 6: Consider the 3 examples of input provided at the prompt and the result. Explain why the results are expected behaviour from the lexer.
1: Below is recognized correctly because the lexer can take both several digits and decimals with the regular expression defined. \
% dotnet bin/Debug/net8.0/hello3.dll \
Hello World from FsLex! \
Please pass a digit: \
34 \
The lexer recognizes 34 

2: Below is also recognized correctly because the lexer can take both several digits and decimals given with at ".". \
% dotnet bin/Debug/net8.0/hello3.dll \
Hello World from FsLex! \
Please pass a digit:\
34.24\
The lexer recognizes 34.24

3: Below is not recognized correctly because the lexer doesn't recognize numbers 
with a comma in it. This is because the regular expression isn't written to accept this. \
% dotnet bin/Debug/net8.0/hello3.dll \
Hello World from FsLex! \
Please pass a digit: \
34,34 \
The lexer recognizes 34
