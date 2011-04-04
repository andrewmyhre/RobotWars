The requirements didn't really specify the format of the game so I kept it very simple, two robots are initialised with user input and then the game begins a loop of requesting instructions for each robot by turn.

The dependency between Robot and Grid doesn't sit too well with me, if I was to refactor this I would add a controller which has a Grid and a number of robots attached to it, which would then arbitrate the progress of the game. But this implementation satisfies the requirements adequately so I'm happy to submit it.

