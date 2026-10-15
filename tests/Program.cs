using TcpVisualizer.Simulation;

var sim = new TcpSimulation { DropNextSynAck = true };
sim.Step();
if (!sim.Step().Contains("perdido", StringComparison.OrdinalIgnoreCase)) throw new Exception("Packet loss was not shown.");
if (!sim.Step().Contains("retransmite", StringComparison.OrdinalIgnoreCase)) throw new Exception("Retransmission was not shown.");
sim.Step(); sim.Step(); sim.Step(); sim.Step(); sim.Step(); sim.Step();
if (!sim.Finished || sim.ClientState != "CLOSED" || sim.ServerState != "CLOSED") throw new Exception("TCP close sequence did not finish.");
Console.WriteLine("TCP state checks passed.");
