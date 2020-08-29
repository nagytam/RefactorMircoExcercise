Solution Changes
1) remove NUnit lib reference -> replace it with a NuGet package reference in test project
2) relocate test code to separate test project

Telemetry:
1) add unittests to simulate existing client behaviour
2) Initialize random generators with fix seed to allow to simulate existing client behaviour reproducible (Random constructor with 2020 parameter -> will be removed finally)
3) Telemetry control clients 1,2 and 3 are the same ??? -> 2 and 3 could be eliminated

