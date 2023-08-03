using BenchmarkDotNet.Running;
using MemoryAllocation.Tests;

BenchmarkRunner.Run<OffsetVsCursor>();

Console.Read();