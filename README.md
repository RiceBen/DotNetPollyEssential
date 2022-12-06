# Polly Essential PlayGround

This repo is a safe place to play with .net Foundation awesome package - Polly.

## How to Install Polly

### Solution 1

Install Polly directly, this solution will cause a lot of system relative packages installed.

> Install-Package Polly -Version 7.2.3

## How to run

#### if you are using VS:

> Switch StartUp project and run debug (F5)

#### if you are using VS Code or other IDE:
 
> make sure you have msbuild and add msbuild to your system environment path
  execute command: `dotnet run --project ./DotNetPollyEssential/DotNetPollyEssential.scproj`
  double click {project folder}/bin/Debug/{project name}.exe

## DotNetPollyEssential

This project shows the easiest way to use Polly. Only use Retry policy and Wrap to build custom policy.

## Polly CircuitBreaker

This project shows how to use Circuit-Breaker the easiest way.

## PollyWithIoC

This project shows you how to integrate with IoC container (Autofac as example), that real worl will happen.
Also shows how to let caller no need to reference to Polly. 

## PollyFallback

This project shows how to use Fallback policy in the easiest way.