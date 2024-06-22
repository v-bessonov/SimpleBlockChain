namespace SimpleBlockChain;

/// <summary>The Blockchain class manages the chain of blocks.</summary>
public class Blockchain
{
    public IList<Block> Chain { get; set; }
    
    /// <summary>Difficulty level of the proof-of-work algorithm.</summary>
    public int Difficulty { get; set; }

    public Blockchain()
    {
        Chain = new List<Block> { CreateGenesisBlock() };
        Difficulty = 2;
    }

    /// <summary>Creates the first block in the chain.</summary>
    private Block CreateGenesisBlock()
    {
        return new Block(0, DateTime.Now, "0", "Genesis Block");
    }

    /// <summary>Retrieves the most recent block in the chain.</summary>
    public Block GetLatestBlock()
    {
        return Chain[Chain.Count - 1];
    }

    /// <summary>Adds a new block to the chain after mining it.</summary>
    /// <param name="newBlock"></param>
    public void AddBlock(Block newBlock)
    {
        newBlock.PreviousHash = GetLatestBlock().Hash;
        
        if (string.IsNullOrWhiteSpace(newBlock.Hash))
        {
            newBlock.MineBlock(Difficulty); 
        }
        
        Chain.Add(newBlock);
    }
    
    /// <summary>Validates the blockchain by checking hashes and links between blocks.</summary>
    /// <returns></returns>
    public bool IsChainValid()
    {
        for (int i = 1; i < Chain.Count; i++)
        {
            Block currentBlock = Chain[i];
            Block previousBlock = Chain[i - 1];

            if (currentBlock.Hash != currentBlock.CalculateHash())
            {
                return false;
            }

            if (currentBlock.PreviousHash != previousBlock.Hash)
            {
                return false;
            }
        }
        return true;
    }
    
    /// <summary>Replaces the current chain with a new, longer, and valid chain</summary>
    public void ReplaceChain(IList<Block> newChain)
    {
        if (newChain.Count > Chain.Count && IsChainValid(newChain))
        {
            Chain = new List<Block>(newChain);
        }
    }
    
    /// <summary>Validates the entire chain to ensure integrity.</summary>
    private bool IsChainValid(IList<Block> chain)
    {
        for (int i = 1; i < chain.Count; i++)
        {
            Block currentBlock = chain[i];
            Block previousBlock = chain[i - 1];

            if (currentBlock.Hash != currentBlock.CalculateHash())
            {
                return false;
            }

            if (currentBlock.PreviousHash != previousBlock.Hash)
            {
                return false;
            }
        }
        return true;
    }
}