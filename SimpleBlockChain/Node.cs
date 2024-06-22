namespace SimpleBlockChain;

/// <summary>The Node class simulates a network node, handling peer connections and consensus</summary>
public class Node
{
    public string Address { get; set; }
    public Blockchain Blockchain { get; set; }
    public List<Node> Peers { get; set; }

    public Node(string address)
    {
        Address = address;
        Blockchain = new Blockchain();
        Peers = new List<Node>();
    }

    public void AddPeer(Node peer)
    {
        Peers.Add(peer);
    }

    public void BroadcastBlock(Block block)
    {
        foreach (var peer in Peers)
        {
            peer.ReceiveBlock(block);
        }
    }

    public void ReceiveBlock(Block block)
    {
        Blockchain.AddBlock(block);
        Console.WriteLine($"Node {Address} received block {block.Index}.");
        Console.WriteLine();
    }

    // Syncs the blockchain with peers, adopting the longest valid chain found
    public void SyncBlockchain()
    {
        foreach (var peer in Peers)
        {
            if (peer.Blockchain.Chain.Count > Blockchain.Chain.Count)
            {
                Blockchain.ReplaceChain(peer.Blockchain.Chain);
            }
        }
    }
    
    
    /// <summary>Iterates over all peer blockchains, selecting the longest valid chain for consensus.</summary>
    public void ReachConsensus()
    {
        List<Blockchain> chains = new List<Blockchain>();
        foreach (var peer in Peers)
        {
            chains.Add(peer.Blockchain);
        }

        Blockchain longestChain = Blockchain;
        foreach (var chain in chains)
        {
            if (chain.Chain.Count > longestChain.Chain.Count && chain.IsChainValid())
            {
                longestChain = chain;
            }
        }

        Blockchain.ReplaceChain(longestChain.Chain);
    }
}