using Microsoft.UI.Xaml.Controls;
using Opc.Ua;
using Opc.UaFx;
using Opc.UaFx.Client;
using OPCUA.Demo.App.ViewModels;
using Windows.Media.Protection.PlayReady;

namespace OPCUA.Demo.App.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }

    private void WriteData(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        using var client = new OpcClient(ServerAddress.Text);
        client.Connect();
        double.TryParse(ValueToWrite.Text, out var value);
        string nodeId = TagToWrite.Text;
        var result = client.WriteNode(nodeId, value);
        WriteOutput.Text = result?.ToString() ?? string.Empty;
        client.Disconnect();
    }

    private void ReadData(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        using var client = new OpcClient(ServerAddress.Text);
        client.Connect();
        ValueToRead.Text = client.ReadNode(TagToRead.Text).ToString();
        client.Disconnect();
    }

    private void ReadTags(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        using var client = new OpcClient(ServerAddress.Text);
        client.Connect();
        var node = client.BrowseNode(OpcObjectTypes.ObjectsFolder);
        Browse(node);
        client.Disconnect();
    }

    private void Browse(OpcNodeInfo node, int level = 0)
    {
        Output.Text += string.Format("{0}{1}({2})\n",
                new string('.', level * 4),
                node.Attribute(OpcAttribute.DisplayName).Value,
                node.NodeId);

        level++;

        foreach (var childNode in node.Children())
            Browse(childNode, level);
    }
}
