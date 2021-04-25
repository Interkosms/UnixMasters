﻿using Microsoft.ML;
using Microsoft.ML.Data;
using Spectre.Console;
using Console = Spectre.Console.AnsiConsole;

var ctx = new MLContext();

// load data
var dataView = ctx.Data
    .LoadFromTextFile<SentimentData>("yelp_labelled.txt");

// split data into testing set
var splitDataView = ctx.Data
    .TrainTestSplit(dataView, testFraction: 0.2);

// Build model
var estim