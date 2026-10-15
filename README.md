# tcp-visualizer

Step through a TCP connection from the three-way handshake to connection close. Follow packet flow and connection states, and trigger packet loss to see a retransmission.

## Run

Requires the .NET 10 SDK. Browser builds also need the WebAssembly workload: `dotnet workload install wasm-tools`.

### Desktop

~~~sh
dotnet run --project TcpVisualizer.Desktop/TcpVisualizer.Desktop.csproj
~~~

### Browser

~~~sh
dotnet run --project TcpVisualizer.Browser/TcpVisualizer.Browser.csproj
~~~

## Example

Enable SYN-ACK loss and advance the simulation to see the packet retransmit.
