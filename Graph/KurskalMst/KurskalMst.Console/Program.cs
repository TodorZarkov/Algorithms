

namespace KurskalMst.Console
{
    internal class Program
    {
        private static Dictionary<int, int> parent;
        static void Main(string[] args)
        {
            int numberOfEdges = int.Parse(System.Console.ReadLine());
            List<(int, int, int)> edges = new List<(int, int, int)>();
            parent = new Dictionary<int, int>();
            for (int i = 0; i < numberOfEdges; i++)
            {
                int[] line = System.Console.ReadLine()
                    .Split(", ")
                    .Select(int.Parse)
                    .ToArray();
                int first = line[0];
                int second = line[1];
                int weight = line[2];
                edges.Add((first, second, weight));

                parent[first] = first;
                parent[second] = second;
            }

            List<(int, int, int)> forest = new List<(int, int, int)>();
            edges = edges.OrderBy(e => e.Item3).ToList();
            foreach (var edge in edges)
            {

                int firstNodeRoot = GetRoot(edge.Item1);
                int secondNodeRoot = GetRoot(edge.Item2);
                if (firstNodeRoot == secondNodeRoot)
                {
                    continue;
                }
                parent[firstNodeRoot] = secondNodeRoot;
                forest.Add(edge);
            }

            foreach (var edge in forest)
            {
                System.Console.WriteLine($"{edge.Item1} - {edge.Item2}");
            }
        }

        private static int GetRoot(int node)
        {
            while (parent[node] != node)
            {
                node = parent[node];
            }
            return node;
        }
    }

}
