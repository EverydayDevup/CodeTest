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
        
        var nodeCount = 6; // 노드 수
        var edges = new List<(int, int, int)> // (시작 노드, 끝 노드, 가중치)
        {
            (0, 1, 4), // A-B, 가중치 4
            (0, 2, 4), // A-C, 가중치 4
            (1, 2, 2), // B-C, 가중치 2
            (1, 3, 5), // B-D, 가중치 5
            (2, 3, 8), // C-D, 가중치 8
            (2, 4, 10), // C-E, 가중치 10
            (3, 4, 2), // D-E, 가중치 2
            (3, 5, 6), // D-F, 가중치 6
            (4, 5, 3)  // E-F, 가중치 3
        };
        
        var kruskal = Graph.Kruskal(edges, nodeCount);
        Start($"{nameof(kruskal)}");
        Console.WriteLine($"{nameof(kruskal)}: {string.Join(",", kruskal)}");
        End($"{nameof(kruskal)}");

        var topologicalEdges = new List<(int, int)>
        {
            (0, 2), // A → C
            (1, 2), // B → C
            (1, 3), // B → D
            (2, 4), // C → E
            (3, 5), // D → F
            (4, 5) // E → F
        };
        
        var topological = Graph.TopologicalSort(nodeCount, topologicalEdges);
        Start($"{nameof(topological)}");
        Console.WriteLine($"{nameof(topological)}: {string.Join(",", topological)}");
        End($"{nameof(topological)}");
    }
}