namespace TcpVisualizer.Simulation;

public sealed class TcpSimulation
{
    public string ClientState { get; private set; } = "CLOSED";
    public string ServerState { get; private set; } = "LISTEN";
    public bool Finished { get; private set; }
    public bool DropNextSynAck { get; set; }
    public bool RetransmissionPending { get; private set; }
    public int StepIndex { get; private set; }

    public string Step()
    {
        if (Finished) return "Conexão encerrada.";
        if (RetransmissionPending)
        {
            RetransmissionPending = false;
            return "Timeout: cliente retransmite SYN/ACK recebido novamente.";
        }

        var message = StepIndex++ switch
        {
            0 => Set("SYN_SENT", "CLOSED", "Cliente → servidor: SYN, seq=100"),
            1 when DropNextSynAck => LoseSynAck(),
            1 => Set("SYN_SENT", "SYN_RECEIVED", "Servidor → cliente: SYN/ACK, seq=500, ack=101"),
            2 => Set("ESTABLISHED", "ESTABLISHED", "Cliente → servidor: ACK, ack=501 · handshake concluído"),
            3 => Set("FIN_WAIT_1", "ESTABLISHED", "Cliente → servidor: FIN, seq=101 · início do encerramento"),
            4 => Set("FIN_WAIT_2", "CLOSE_WAIT", "Servidor → cliente: ACK · cliente aguarda FIN do servidor"),
            5 => Set("FIN_WAIT_2", "LAST_ACK", "Servidor → cliente: FIN, seq=501"),
            6 => Finish(),
            _ => Finish()
        };
        return message;
    }

    private string LoseSynAck()
    {
        DropNextSynAck = false;
        ServerState = "SYN_RECEIVED";
        StepIndex--;
        RetransmissionPending = true;
        return "Servidor envia SYN/ACK; pacote perdido. Estado do servidor: SYN_RECEIVED.";
    }

    private string Finish()
    {
        ClientState = "CLOSED"; ServerState = "CLOSED"; Finished = true;
        return "Cliente → servidor: ACK final · conexão CLOSED nos dois lados.";
    }

    private string Set(string client, string server, string message)
    {
        ClientState = client; ServerState = server; return message;
    }

    public void Reset()
    {
        ClientState = "CLOSED"; ServerState = "LISTEN"; Finished = false;
        DropNextSynAck = false; RetransmissionPending = false; StepIndex = 0;
    }
}
