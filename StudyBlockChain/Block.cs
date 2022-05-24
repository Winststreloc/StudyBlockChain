using System;

namespace BlockChain_Study
{
    [Serializable()]
    class Block
    {
        public Block(string ts, string data, string hs, int nc)
        {
            timestamp = ts; 
            this.data = data; 
            hash = hs; 
            nonce = nc; 
        } 

        public string timestamp;
        public string data;
        public string hash;
        public int nonce;


    }
}