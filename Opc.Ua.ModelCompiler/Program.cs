﻿using ModelCompiler;
using McMaster.Extensions.CommandLineUtils;
using System.Reflection;
using System.Diagnostics;

try
{
    if (System.Diagnostics.Debugger.IsAttached)
    {
    }

    for (int ii = 0; ii < args.Length; ii++)
    {
        args[ii] = args[ii].Replace("\n", "\\n");
    }

    if (args.Length < 2) 
    {
        Console.WriteLine($"Version: {Assembly.GetExecutingAssembly().GetName().FullName}");
        Console.WriteLine($"FileVersion: {FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion}");
        Console.WriteLine($"Opc.Ua.Core: {typeof(Opc.Ua.NodeId).Assembly.GetName().FullName}");
    }

    ModelCompilerApplication.Run(args);
}
catch (CommandParsingException e)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"[{e.GetType().Name}] {e.Message} ({e.Command})");
    Environment.Exit(3);
}
catch (AggregateException e)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"[{e.GetType().Name}] {e.Message}");

    foreach (var ie in e.InnerExceptions)
    {
        Console.WriteLine($">>> [{ie.GetType().Name}] {ie.Message}");
    }

    Environment.Exit(3);
}
catch (Exception e)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"[{e.GetType().Name}] {e.Message}");

    Exception ie = e.InnerException;

    while (ie != null)
    {
        Console.WriteLine($">>> [{ie.GetType().Name}] {ie.Message}");
        ie = ie.InnerException;
    }

    Console.WriteLine();
    Console.WriteLine($"========================");
    Console.WriteLine($"{e.StackTrace}");
    Console.WriteLine($"========================");
    Console.WriteLine();

    Environment.Exit(3);
}