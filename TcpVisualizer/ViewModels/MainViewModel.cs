using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using TcpVisualizer.Simulation;

namespace TcpVisualizer.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly TcpSimulation _simulation = new();
    public ObservableCollection<string> Events { get; } = [];
    [ObservableProperty] private string _clientState = "CLOSED";
    [ObservableProperty] private string _serverState = "LISTEN";
    [ObservableProperty] private bool _loseSynAck;

    [RelayCommand]
    private void NextStep()
    {
        _simulation.DropNextSynAck = LoseSynAck;
        Events.Add(_simulation.Step());
        LoseSynAck = _simulation.DropNextSynAck;
        ClientState = _simulation.ClientState;
        ServerState = _simulation.ServerState;
    }

    [RelayCommand]
    private void Reset()
    {
        _simulation.Reset(); Events.Clear();
        ClientState = "CLOSED"; ServerState = "LISTEN"; LoseSynAck = false;
    }
}
