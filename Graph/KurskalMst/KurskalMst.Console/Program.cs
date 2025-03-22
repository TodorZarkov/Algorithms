
namespace KurskalMst.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberOfEdges = int.Parse(System.Console.ReadLine());
            PriorityQueue<(int, int), double> edges = new PriorityQueue<(int, int), double>();
            for (int i = 0; i < numberOfEdges; i++)
            {
                int[] line = System.Console.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                int first = line[0];
                int second = line[1];
                double weight = line[2];
                edges.Enqueue((first, second), weight);
            }

            HashSet<int> uniqueNodes = new HashSet<int>();
            List<(int, int)> forest = new List<(int, int)>();
            while (edges.Count > 0)
            {
                var edge = edges.Dequeue();
                int uniqueCountBefore = uniqueNodes.Count();
                uniqueNodes.Add(edge.Item1);//it doesn't catch the directed graph case
                uniqueNodes.Add(edge.Item2);
                if (uniqueNodes.Count() - uniqueCountBefore > 0)
                {
                    forest.Add((edge.Item1, edge.Item2));
                }
            }

            foreach (var edge in forest)
            {
                System.Console.WriteLine($"{edge.Item1} - {edge.Item2}");
            }
        }
    }

}
