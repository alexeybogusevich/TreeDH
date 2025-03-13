using System.Numerics;
using TreeDH;

Console.WriteLine("Hello, World!");

int numberOfUsers = 5;
List<BigInteger> privateKeys = new List<BigInteger>();

for (int i = 0; i < numberOfUsers; i++)
{
    privateKeys.Add(GroupDiffieHellman.GeneratePrivateKey());
}

BigInteger finalSharedKey = GroupDiffieHellman.ComputeTreeDH(privateKeys);

Console.WriteLine("Private keys:");
for (int i = 0; i < privateKeys.Count; i++)
{
    Console.WriteLine($"User {i + 1}: {privateKeys[i]}");
}

Console.WriteLine($"\nFinal shared key (g^(x1*x2*...*xn) mod P): {finalSharedKey}");