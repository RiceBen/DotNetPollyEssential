# Polly Essential PlayGround

This repo is a safe place to play with .net Foundation awesome package - Polly.

## How to Install Polly

### Solution 1

Install Polly directly, this solution will cause a lot of system relative packages installed.

> Install-Package Polly -Version 7.1.1

### Solution 2

Install necessary packages first with the latest version.

1. Install Microsoft.NETCore.Platforms latest version

>ex. Install-Package Microsoft.NETCore.Platforms -Version 3.0.0

2. Install NETStandard.Library latest version

>ex. Install-Package NETStandard.Library -Version 2.0.3

3. Install Polly latest version

>ex. Install-Package Polly -Version 7.1.1

## How to run

> how to run, set default project.

## DotNetPollyEssential

This project show the easiest way to use Polly. Only use Retry policy and Wrap to build custom policy.

## PollyCircuitBreaker



## PollyWithIoC

This project show you how to integrate with IoC container (Autofac as example), that real worl will happen.
Also shows how to let caller no need to reference to Polly. 