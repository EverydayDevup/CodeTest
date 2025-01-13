namespace Programmers;

public class GraphRunner : Runner
{
    public override void Solution()
    {
        // 그래프 데이터 생성
        var graph = new Dictionary<int, List<int>>
        {
            { 0, new List<int> { 1, 2 } },
            { 1, new List<int> { 0, 3, 4 } },
            { 2, new List<int> { 0, 5, 6 } },
            { 3, new List<int> { 1 } },
            { 4, new List<int> { 1 } },
            { 5, new List<int> { 2 } },
            { 6, new List<int> { 2 } }
        };
        
        var dfsByStack = Graph.DfsUsingStack(graph, 0);
        Start($"{nameof(dfsByStack)}");
        Console.WriteLine($"{nameof(dfsByStack)}: {string.Join(",", dfsByStack)}");
        End($"{nameof(dfsByStack)}");
        
        var visited = new HashSet<int>();
        Graph.DfsUsingRecursion(graph, 0, visited);
        Start($"dfsByRecursion");
        Console.WriteLine($"dfsByRecursion: {string.Join(",", visited)}");
        End($"dfsByRecursion");
        
        var bfsUsingQueue = Graph.BfsUsingQueue(graph, 0);
        Start($"{nameof(bfsUsingQueue)}");
        Console.WriteLine($"{nameof(bfsUsingQueue)}: {string.Join(",", bfsUsingQueue)}");
        End($"{nameof(bfsUsingQueue)}");
        
        var directedGraph = new Dictionary<int, List<(int, int)>>()
        {
            { 1, new List<(int, int)> { (2, 2), (3, 4) } }, // 1번 노드에서 2번 노드(가중치 2), 3번 노드(가중치 4)
            { 2, new List<(int, int)> { (3, 1), (4, 7) } }, // 2번 노드에서 3번 노드(가중치 1), 4번 노드(가중치 7)
            { 3, new List<(int, int)> { (4, 3) } },         // 3번 노드에서 4번 노드(가중치 3)
            { 4, new List<(int, int)>() }                  // 4번 노드는 이웃 노드가 없음
        };

        var dijkstra = Graph.Dijkstra(directedGraph, 1);
        Start($"{nameof(dijkstra)}");
        Console.WriteLine($"{nameof(dijkstra)}: {string.Join(",", dijkstra)}");
        End($"{nameof(dijkstra)}");
    }
}