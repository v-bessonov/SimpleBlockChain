using SimpleBlockChain;

var myCoin = new Blockchain();

Console.WriteLine("Mining block 1...");
myCoin.AddBlock(new Block(1, DateTime.Now, myCoin.GetLatestBlock().Hash, "Block 1 Data"));

Console.WriteLine("Mining block 2...");
myCoin.AddBlock(new Block(2, DateTime.Now, myCoin.GetLatestBlock().Hash, "Block 2 Data"));

foreach (var block in myCoin.Chain)
{
    Console.WriteLine($"Index: {block.Index}");
    Console.WriteLine($"Timestamp: {block.Timestamp}");
    Console.WriteLine($"Previous Hash: {block.PreviousHash}");
    Console.WriteLine($"Hash: {block.Hash}");
    Console.WriteLine($"Nonce: {block.Nonce}");
    Console.WriteLine($"Data: {block.Data}");
    Console.WriteLine();
}