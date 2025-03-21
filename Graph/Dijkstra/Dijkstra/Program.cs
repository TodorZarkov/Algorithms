
namespace Dijkstra
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberOfEdges = int.Parse(Console.ReadLine());
            Dictionary<(int, int), double> edgeWeight = new Dictionary<(int, int), double>();
            Dictionary<int, List<int>> nodeWithChildren = new Dictionary<int, List<int>>();
            Dictionary<int, double> dist = new Dictionary<int, double>();
            Dictionary<int, int> prev = new Dictionary<int, int>();

            for (int i = 0; i < numberOfEdges; i++)
            {
                int[] line = Console.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                int start = line[0];
                int end = line[1];
                double weight = line[2];
                edgeWeight[(start, end)] = weight;
                if (!nodeWithChildren.ContainsKey(start))
                {
                    nodeWithChildren[start] = new List<int>();
                }
                nodeWithChildren[start].Add(end);

                dist[start] = double.PositiveInfinity;
                dist[end] = double.PositiveInfinity;
                prev[start] = -1;
                prev[end] = -1;
            }

            PriorityQueue<int, double> nodesQueue = new();

            int startNode = int.Parse(Console.ReadLine());
            dist[startNode] = 0;
            int endNode = int.Parse(Console.ReadLine());

            nodesQueue.Enqueue(startNode, 0);
            while (nodesQueue.Count > 0)
            {
                //dequeue the vertex nearest to the prev vertex
                nodesQueue.TryDequeue(out int currentNode, out double currentWeight);
                //enqueue all children of the current
                List<int> children = new List<int>();
                if (nodeWithChildren.ContainsKey(currentNode))
                {
                    children = nodeWithChildren[currentNode];
                }
                foreach (int child in children)
                {
                    double newDist = Math.Min(dist[child], dist[currentNode] + edgeWeight[(currentNode, child)]);
                    //improve the distance through child edges
                    if (newDist < dist[child])
                    {
                        nodesQueue.Enqueue(child, newDist);
                        dist[child] = newDist;
                        prev[child] = currentNode;
                    }
                }
            }

            if (dist[endNode] == double.PositiveInfinity)
            {
                Console.WriteLine("There is no such path.");
                return;
            }
            Console.WriteLine(dist[endNode]);
            Stack<int> path = new Stack<int>();
            int curNode = endNode;
            while (prev[curNode] != -1)
            {
                path.Push(curNode);
                curNode = prev[curNode];
            }
            path.Push(startNode);
            Console.WriteLine(string.Join(" ", path));
        }

    }
}
