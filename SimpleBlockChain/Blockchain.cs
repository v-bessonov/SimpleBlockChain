namespace SimpleBlockChain;

public class Blockchain
{
    public IList<Block> Chain { get; set; }
    public int Difficulty { get; set; }

    public Blockchain()
    {
        Chain = new List<Block> { CreateGenesisBlock() };
        Difficulty = 2;
    }

    private Block CreateGenesisBlock()
    {
        return new Block(0, DateTime.Now, "0", "Genesis Block");
    }

    public Block GetLatestBlock()
    {
        return Chain[Chain.Count - 1];
    }

    public void AddBlock(Block newBlock)
    {
        newBlock.PreviousHash = GetLatestBlock().Hash;
        newBlock.MineBlock(Difficulty);
        Chain.Add(newBlock);
    }
}