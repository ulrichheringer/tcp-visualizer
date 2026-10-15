# tcp-visualizer

Avança pelos estados do handshake TCP, retransmissões e encerramento.

## Desktop

```sh
dotnet run --project TcpVisualizer.Desktop/TcpVisualizer.Desktop.csproj
```

## Browser

Requer o workload wasm-tools do .NET 10.

```sh
dotnet run --project TcpVisualizer.Browser/TcpVisualizer.Browser.csproj
```

## Exemplo

Ative a perda do SYN/ACK e avance os passos para observar a retransmissão.
