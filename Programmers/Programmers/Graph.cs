namespace Programmers;

public static class Graph
{
    #region DFS
    // 깊이 우선 탐색, 그래프의 깊은 부분을 우선적으로 탐색
    // 스택 또는 재귀함수로 구현
    // 알고리즘
    // 1. 탐색 시작 노드를 스택에 넣고 방문처리 (어떤 노드부터 방문할지는 문제에 따라 결정됨)
    // 2. 스택의 최상단노드에 방문하지 않은 인접한 노드가 하나라도 있다면 그 노드를 스택에 넣고 방문 처리
    //    방문하지 않는 인접 노드가 없으면 스택에서 최상단 노드를 꺼냄
    // 3. 더 이상 2번의 과정을 수행하지 않을 때까지 반복
    public static HashSet<int> DfsUsingStack(Dictionary<int, List<int>> graph, int startNode)
    {
        var visited = new HashSet<int>();
        var stack = new Stack<int>();
        
        stack.Push(startNode);

        while (stack.Count > 0)
        {
            // 최상단의 노드를 꺼냄
            var node = stack.Pop();
            
            // 이미 방문한적이 있다면 건너띔
            if (!visited.Add(node))
                continue;

            // 가장 노드가 작은 노드부터 방문하기 위함 
            // graph[node].Sort((a, b) => b.CompareTo(a));
            foreach (var target in graph[node])
            {
                // 방문했던 노드는 스택에 추가하지 않음
                if (visited.Contains(target))
                    continue;
                
                stack.Push(target);
            }
        }

        return visited;
    }
    
    public static void DfsUsingRecursion(Dictionary<int, List<int>> graph, int node, HashSet<int> visited)
    {
        visited.Add(node);
        
        foreach (var target in graph[node])
        {
            // 방문했던 노드는 검색하지 않음
            if (visited.Contains(target))
                continue;
                
            DfsUsingRecursion(graph, target, visited);
        }
    }
    #endregion DFS

    #region BFS
    // 너비 우선 탐색, 그래프에서 가까운 노드부터 우선적으로 탐색하는 알고리즘
    // 탐색 시작 노드를 큐에 삽입하고 방문 처리
    // 큐에서 노드를 꺼낸 뒤에 해당 노드의 인접 노드 중에서 방문하지 않은 모든 노드를 큐에 삽입하고 방문 처리
    // 더 이상 위의 과정을 수행할 수 없을 때 까지 반복처리
    public static HashSet<int> BfsUsingQueue(Dictionary<int, List<int>> graph, int startNode)
    {
        var queue = new Queue<int>();
        var visited = new HashSet<int>();
        
        queue.Enqueue(startNode);
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            visited.Add(node);
            
            foreach (var target in graph[node])
            {
                if (visited.Contains(target))
                    continue;
                
                queue.Enqueue(target);
            }
        }

        return visited;
    }
    #endregion BFS

    #region Dijkstra
    // Dijkstra 알고리즘 : 특정한 노드에서 출발하여 다른 모든 노드로 가는 최단 경로를 계산
    // 음의 간선이 없을 때 정상적으로 동작, 매 상황에서 가장 비용이 적은 노드를 선택
    // 알고리즘
    // 1. 출발 노드를 설정
    // 2. 최단 거리 테이블을 초기화
    // 3. 방문하지 않는 노드 중에서 최단 거리가 가장 짧은 노드를 선택 (최소 힙 자료구조를 이용)
    // 4. 해당 노드를 거쳐 다른 노드로 가는 비용을 계산하여 최단 거리 테이블을 갱신
    // 5. 3~4의 과정을 반복
    // 시간 복잡도 O(ElogV)
    public static Dictionary<int, int> Dijkstra(Dictionary<int, List<(int, int)>> graph, int startNode)
    {
        var dicDistance = new Dictionary<int, int>();
        foreach (var node in graph.Keys)
        {
            dicDistance.Add(node, node == startNode ? 0 : int.MaxValue);
        }
        
        // 최소힙 : 가중치가 작은 노드부터 처리
        var priorityQueue = new SortedSet<(int node, int weight)>(Comparer<(int node, int distance)>.Create((a, b) =>
        {
            var result = a.distance.CompareTo(b.distance);
            if (result == 0)
                result = a.node.CompareTo(b.node);

            return result;
        }));
        
        priorityQueue.Add((startNode, 0));
        
        while (priorityQueue.Count > 0)
        {
            var target = priorityQueue.First();
            priorityQueue.Remove(target);
            
            var (node, distance) = target;
            
            if (dicDistance[node] < distance)
                continue;

            // 인접한 노드를 불러와서, 현재 노드 + 가중치가 현재 해당 노드의 가중치보다 작은지 비교해서 최소 값을 구함
            foreach (var (neighbor, weight) in graph[node])
            {
                var cost = distance + weight;
                if (cost >= dicDistance[neighbor])
                    continue;
                
                dicDistance[neighbor] = cost;
                priorityQueue.Add((neighbor, cost));
            }
        }

        return dicDistance;
    }
    #endregion Dijkstra 
    
    // 플로이드워셜알고리즘
    // 모든 노드에서 다른 모든 노드까지의 최단 경로를 모두 계산
    // 거쳐가는 노드를 기준으로 알고리즘을 수행
    // 그래프의 가중치가 음수인 경우에도 처리할 수 있음
    // Dab = min(Dab, Dak + Dkb)
    // 1번 노드를 거쳐서 가는 경우 D23 = min(D23, D21, D13)
    //                        D24 = min(D24, D21, D14)
    // 
    public static int[,] FloydWarshall(int[,] graph, int n)
    {
        // n의 노드 수만큼 최단 경로 행렬을 초기화
        var dist = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                dist[i, j] = graph[i, j];
        }

        const int maxValue = int.MaxValue / 2;

        // 거쳐가는 노드 K
        for (int k = 0; k < n; k++)
        {
            for (int a = 0; a < n; a++) // 출발 노드 a
            {
                for (int b = 0; b < n; b++) // 도착 노드 b
                {
                    // 오버플로우를 막기 위해 최대 값 설정은 int.MaxValue를 그대로 사용하지 않음
                    if (dist[a, k] != maxValue  && dist[k, b] != maxValue)
                        dist[a, b] = Math.Min(dist[a, b], dist[a, k] + dist[k, b]);
                }
            }
        }

        return dist;
    }
}