using Microsoft.ML;
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
var estimator = ctx.Transforms.Text
    .FeaturizeText(
        outputColumnName: "Features",
        inputColumnName: nameof(SentimentData.Text)
    ).Append(ctx.BinaryClassification.Trainers.SdcaLogisticRegression(featureColumnName: "Features"));

// Train model
ITransformer model = default!;

var rule = new Rule("Create and Train Model");
Console
    .Live(rule)
    .Start(console =>
    {
        // training happens here
        model = estimator.Fit(splitDataView.TrainSet);
        var predictions = model.Transform(splitDataView.TestSet);

        rule.Title 