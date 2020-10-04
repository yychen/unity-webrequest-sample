# unity-webrequest-sample
This is just a sample unity project that performs HTTP API request to a local server (in this case, Django).
It SHOULD NOT be used in production.

The main script is in [Assets/Server.cs](Assets/Server.cs). There is no dependencies required in this project.
You can find the `HttpClient` references [here](https://docs.microsoft.com/zh-tw/dotnet/api/system.net.http.httpclient?view=netstandard-2.0) 
and the usage of `HttpUtility` [here](https://docs.microsoft.com/zh-tw/dotnet/api/system.web.httputility?view=netstandard-2.0).

## django server
For the server side, there's only a [documentation](https://www.notion.so/Python-8ff168f6c8af48b79583e830f5ec63d0) describing the steps. You can follow the doc and create your own django gameserver.
