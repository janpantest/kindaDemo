1. clone project
2. install dotnet
    `dotnet add package Microsoft.Playwright.NUnit`
3. run
    `pwsh Applitools.Example.Tests/bin/Debug/net7.0/playwright.ps1 install`
4. if pwsh not available, you have to install powershell
5. run test
    `dotnet test`
    `dotnet test --filter "FullyQualifiedName~Test.Valmez.Valmez.ValmezFlow" --settings:ExampleSetting`
    `dotnet test --filter "FullyQualifiedName~Test.Brno.Brno.BrnoTest" --settings:ExampleSetting`
6. debug
    `dotnet test --filter "FullyQualifiedName~Test.Valmez.Valmez.ValmezFlow" --settings:ExampleSetting --logger "console;verbosity=detailed"`