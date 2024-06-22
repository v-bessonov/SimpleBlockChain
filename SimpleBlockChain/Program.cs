using SimpleBlockChain;

// Sets up nodes and peers.
var nodeA = new Node("NodeA");
var nodeB = new Node("NodeB");
var nodeC = new Node("NodeC");

nodeA.AddPeer(nodeB);
nodeA.AddPeer(nodeC);
nodeB.AddPeer(nodeA);
nodeB.AddPeer(nodeC);
nodeC.AddPeer(nodeA);
nodeC.AddPeer(nodeB);


// Sets up nodes and peers.
Console.WriteLine("Node A mining block 1...");
var block1 = new Block(1, DateTime.Now, nodeA.Blockchain.GetLatestBlock().Hash, "Block 1 Data");
block1.MineBlock(nodeA.Blockchain.Difficulty);
nodeA.Blockchain.AddBlock(block1);
nodeA.BroadcastBlock(block1);

Console.WriteLine("Node B mining block 2...");
var block2 = new Block(2, DateTime.Now, nodeB.Blockchain.GetLatestBlock().Hash, "Block 2 Data");
block2.MineBlock(nodeB.Blockchain.Difficulty);
nodeB.Blockchain.AddBlock(block2);
nodeB.BroadcastBlock(block2);

Console.WriteLine("Node C mining block 3...");
var block3 = new Block(3, DateTime.Now, nodeC.Blockchain.GetLatestBlock().Hash, "Block 3 Data");
block3.MineBlock(nodeC.Blockchain.Difficulty);
nodeC.Blockchain.AddBlock(block3);
nodeC.BroadcastBlock(block3);

// Syncs and reaches consensus among all nodes.
Console.WriteLine("Synchronizing blockchains...");
nodeA.SyncBlockchain();
nodeB.SyncBlockchain();
nodeC.SyncBlockchain();
Console.WriteLine();

Console.WriteLine("Reaching consensus...");
nodeA.ReachConsensus();
nodeB.ReachConsensus();
nodeC.ReachConsensus();
Console.WriteLine();


// Prints the blockchain for each node
PrintBlockchain(nodeA);
PrintBlockchain(nodeB);
PrintBlockchain(nodeC);



static void PrintBlockchain(Node node)
{
    Console.WriteLine($"{node.Address} Blockchain:");
    Console.WriteLine();
    foreach (var block in node.Blockchain.Chain)
    {
        Console.WriteLine($"Index: {block.Index}");
        Console.WriteLine($"Timestamp: {block.Timestamp}");
        Console.WriteLine($"Previous Hash: {block.PreviousHash}");
        Console.WriteLine($"Hash: {block.Hash}");
        Console.WriteLine($"Nonce: {block.Nonce}");
        Console.WriteLine($"Data: {block.Data}");
        Console.WriteLine();
    }
}