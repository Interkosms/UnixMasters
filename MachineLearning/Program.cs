﻿using Microsoft.ML;
using Microsoft.ML.Data;
using Spectre.Console;
using Console = Spectre.Console.AnsiConsole;

var ctx = new MLContext();

// load data
var dataV