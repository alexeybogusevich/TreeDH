using System.Numerics;

namespace TreeDH
{
    class GroupDiffieHellman
    {
        private static readonly BigInteger P = 23;
        private static readonly BigInteger G = 5;

        public static BigInteger ModExp(BigInteger baseValue, BigInteger exponent, BigInteger modulus) => BigInteger.ModPow(baseValue, exponent, modulus);

        public static BigInteger GeneratePrivateKey() => new Random().Next(1, (int)P - 1);

        public static BigInteger ComputePublicKey(BigInteger privateKey) => ModExp(G, privateKey, P);

        public static BigInteger ComputeSharedKey(BigInteger receivedKey, BigInteger privateKey) => ModExp(receivedKey, privateKey, P);

        public static BigInteger ComputeTreeDH(List<BigInteger> privateKeys)
        {
            var currentLevel = new List<BigInteger>();

            foreach (var privateKey in privateKeys)
            {
                currentLevel.Add(ComputePublicKey(privateKey));
            }

            while (currentLevel.Count > 1)
            {
                var nextLevel = new List<BigInteger>();

                for (int i = 0; i < currentLevel.Count; i += 2)
                {
                    if (i + 1 < currentLevel.Count)
                    {
                        BigInteger sharedKey = ComputeSharedKey(currentLevel[i + 1], privateKeys[i]);
                        nextLevel.Add(sharedKey);
                    }
                    else
                    {
                        nextLevel.Add(currentLevel[i]);
                    }
                }

                currentLevel = nextLevel;
            }

            return currentLevel[0];
        }
    }
}