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

    #region FloydWarshall
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
    
    #endregion FloydWarshall

    #region Kruskal
    // 최소 신장 트리 : 그래프에서 모든 노드를 포함하면서 사이클이 존재하지 않는 부분 그래프
    // 최소한의 비용으로 구성되는 신장 트리를 찾아야 할 때 
    // ex) N개의 도시가 존재하는 상황에서 두 도시 사이에 도로를 놓아 전체 도시가 서로 연결될 수 있도록 도로를 설치하는 경우
    // 크루스칼 알고리즘
    // 알고리즘
    // 1. 간선 데이터를 비용에 따라 오름차순으로 정렬
    // 2. 간선을 하나씩 확인하여 현재의 간선이 사이클을 발생시키는지 확인
    // (서로소집합을 사용하여, 노드의 부모가 같은지 확인. 같으면 사이클이 발생한 것)
    //  1) 사이클이 발생하지 않는 경우 최소 신장 트리에 포함
    //  2) 사이클이 발생하는 경우 최소 신장 트리에 포함시키지 않음
    // 3. 모든 간선에 대해 2번의 과정을 반복함
    // 시간복잡도는 O(ElogE)
    public static List<(int start, int end, int weight)> Kruskal(List<(int start, int end, int weight)> edges, int nodeCount)
    {
        // 간선 데이트를 오름차순으로 정렬
        edges = edges.OrderBy(edge => edge.weight).ToList();
        
        // 부모 노드의 초기 값는 자신의 노드
        var parents = new int[nodeCount];
        for (var i = 0; i < parents.Length; i++)
            parents[i] = i;
        
        var result = new List<(int start, int end, int weight)>();

        foreach (var (start, end, weight) in edges)
        {
            var startParent = FindParent(parents, start);
            var endParent = FindParent(parents, end);

            if (startParent == endParent)
                continue;
            
            result.Add((start,end, weight));
            Union(parents, startParent, endParent);
        }

        return result;
    }
    
    // 서로소 집합 자료구조 : 나누어진 원소들의 데이터를 처리하기 위한 자료구조
    // 합집합과 찾기 연산을 지원
    // 합치기 : 두 개의 원소가 포함된 집합을 하나의 집합으로 합치는 연산
    // 찾기 : 특정한 원소가 속한 집합이 어떤 집합인지 알려주는 연산
    // 알고리즘
    // 1. 합집합 연산을 확인하여 서로 연결된 두 노드 A,B를 확인
    //  1) A와 B의 루트노드 A', B'를 찾음
    //  2) A'를 B'의 부모 노드로 설정
    // 2. 모든 집합 연산을 처리할 때까지 1번의 과정을 반복함
    // 사이클을 판별할 때 사용할 수 있음
    // 1. 각 간선의 하나씩 확인하며 두 노드의 루트노드를 확인
    // 1) 루트 노드가 서로 다르다면 두 노드에 대하여 합집합 연산을 수행
    // 2) 루트 노드가 같다면 사이클이 발생한 것
    // 2. 그래프에 포함되어 있는 모든 간선에 대하여 1번 과정을 반복
    private static int FindParent(int[] parents, int n)
    {
        // 경로압축을 통해 부모 노드를 계속 갱신함
        if (parents[n] != n)
            parents[n] = FindParent(parents, parents[n]);

        return parents[n];
    }

    private static void Union(int[] parents, int a, int b)
    {
        a = FindParent(parents, a);
        b = FindParent(parents, b);

        if (a < b)
            parents[b] = a;
        else
            parents[a] = b;
    }
    #endregion Kruskal

    #region TopologicalSort
    // 위상정렬 : 사이클이 없는 방향 그래프의 모든 노드를 방향성에 거스리지 않도록 순서대로 나열
    // 진입 차수 : 특정한 노드로 들어오는 간선의 개수
    // 진출 차수 : 특정한 노드에서 나가는 간선의 개수
    // 큐를 이용하는 위상 정렬 알고리즘
    // 알고리즘
    // 1. 진입 차수가 0인 모든 노드를 큐에 넣음
    // 2. 큐가 빌 때까지 다음의 과정을 반복
    //  1) 큐에서 원소를 꺼내 해당 노드에서 나가는 간선을 그래프에서 제거
    //  2) 새롭게 진입 차수가 0이 된 노드를 큐 넣음
    // 모든 원소를 방문하기 전에 큐가 빈다면 사이클이 존재 
    public static List<int> TopologicalSort(int nodeCount, List<(int, int)> edges)
    {
        // 방문 결과를 넣을 수 있는 리스트
        var result = new List<int>();
        // 진입 차수를 관리하는 데이터
        var dicIndegree = new Dictionary<int, int>();
        // 시작 노드와 연결된 노드를 관리
        var dicNeighbor = new Dictionary<int, List<int>>();

        // 모든 노드의 진입 차수를 0으로 설정
        for (int i = 0; i < nodeCount; i++)
            dicIndegree.TryAdd(i, 0);
        
        foreach (var (start, end) in edges)
        {
            dicIndegree[end]++;
            
            if (!dicNeighbor.ContainsKey(start))
                dicNeighbor.Add(start, new List<int>());
            
            dicNeighbor[start].Add(end);
        }
        
        // 진입 차수가 0인 노드를 큐에 넣음
        var queue = new Queue<int>();
        foreach (var (node, indegree) in dicIndegree)
        {
            if (indegree == 0)
                queue.Enqueue(node);
        }

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            result.Add(node);

            if (!dicNeighbor.TryGetValue(node, out var list)) 
                continue;
            
            foreach (var end in list)
            {
                if (!dicIndegree.ContainsKey(end))
                    continue;
                
                dicIndegree[end]--;
                if (dicIndegree[end] == 0)
                    queue.Enqueue(end);
            }
        }

        return result;
    }
    #endregion

    #region Bellman-Ford
    // 벨만 포드 알고리즘은 음의 간선이 포함된 상태에서, 출발노드에서 다른 모든 노드까지 가는 최단 거리를 구할 수 있음
    // 알고리즘 
    // 1. 출발 노드를 설정
    // 2. 최단 거리 테이블을 초기화
    // 3. 다음의 과정은 n-1번 반복
    //  1) 전체 간선 E개를 하나씩 확인
    //  2) 각 간선을 거쳐 다른 노드로 가는 비용을 계산하여 최단 거리 테이블을 갱신
    // 음수 간선 순환이 발생하는지 체크하고 싶다면 3번의 과정을 수행, 최단 거리 테이블이 갱신 되면 음수 간선이 존재
    // 다익스트라 알고리즘이 매번 방문하지 않는 노드 중에서 최단 거리가 가장 짧은 노드를 선택하는 것과 달리
    // 벨만 포드 알고리즘은 매번 모든 간선을 확인
    public static (bool hasNegativeCycle, Dictionary<int, int>) BellmanFord(Dictionary<int, List<(int, int)>> graph, int startNode)
    {
        // 최단 거리 테이블 설정
        var dicDistance = new Dictionary<int, int>();
        foreach (var node in graph.Keys)
            dicDistance.Add(node, node == startNode ? 0 : int.MaxValue);
        
        // 그래프에서 최단 경로는 최대 (node 수 - 1) 개의 간선으로 구성됨
        for (int i = 0; i < graph.Count - 1; i++)
        {
            foreach (var node in graph.Keys)
            {
                foreach (var (end, weight) in graph[node])
                {
                    if (dicDistance[node] != int.MaxValue && dicDistance[node] + weight < dicDistance[end])
                        dicDistance[end] = dicDistance[node] + weight;
                }
            }
        }
        
        // 음수 사이클 검사
        foreach (var node in graph.Keys)
        {
            foreach (var (target, weight) in graph[node])
            {
                if (dicDistance[node] != int.MaxValue && dicDistance[node] + weight < dicDistance[target])
                    return (true, dicDistance); // 음수 사이클 존재
            }
        }

        return (false, dicDistance); // 음수 사이클 없음
    }
    #endregion Bellman-Ford
}