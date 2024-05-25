// See https://aka.ms/new-console-template for more information
using MultiCloudAISelectionPlatform.Logic;
using System.Threading.Channels;

Console.WriteLine("Hello, World!");

MetricService.GetStaticMetrics().ToList().ForEach(metric =>  Console.WriteLine(metric.Costs));
