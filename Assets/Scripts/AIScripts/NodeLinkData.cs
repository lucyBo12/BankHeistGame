[System.Serializable]
public class NodeLinkData
{
    public string BaseNodeGuid;
    public string PortName;
    public string TargetNodeGuid;

    public override string ToString()
    {
        return $"Base: {BaseNodeGuid} " +
            $"\nPort: {PortName}" +
            $"\nTarget: {TargetNodeGuid}";
    }
}