namespace Programmers;

// 트리는 가계도와 같은 계층적인 구조를 표현할 때 사용할 수 있는 자료 구조
// 루트 노드 : 부모 노드가 없는 최상 위 노드
// 단말 노드 : 자식이 없는 노드
// 크기 : 트리에 포함된 모든 노드의 개수
// 깊이 : 루트 노드부터의 거리
// 높이 : 깊이 중 최대 값
// 차수 : 각 노드의 간선 개수
// 트리의 크기가 N일 때 전체 간선의 개수는 N-1

// 이진 탐색 트리
// 이진 탐색이 동작할 수 있도록 고안된 효율적인 탐색이 가능한 자료 구조
// 이진 탐색 트리의 특징 : 왼쪽 < 부모 < 오른쪽
// 트리 순회 방법
// 전위 순위 : 루트를 먼저 방문 vist -> left -> right
// 중위 순위 : 왼쪽 자식을 방문한 뒤에 루트를 방문 left -> visit -> right
// 후위 순위 : 왼쪽, 오른쪽 자식을 방문한 뒤에 루트를 방문
// 이진탐색트리의 균형이 맞지 않으면 검색효율이 떨어짐
// 균형을 위한 트리
// AVL 트리
// 레드블랙트리 (RB)
// 데이터에 따라 좋은 트리가 다름
// 데이터가 적을 경우 
// 삽입 - RB, 탐색 - AVL, 삭제 - RB
// 데이터가 많을 경우
// 삽입 - AVL, 탐색 - AVL, 삭제 - RB
public class TreeNode<T>
{
    public T Value { get; set; }
    public TreeNode<T>? Left { get; set; }
    public TreeNode<T>? Right { get; set; }
    
    public TreeNode(T value)
    {
        Value = value;
        Left = null;
        Right = null;
    }

    public override string ToString()
    {
        return $"{Value}";
    }
}

public class BinarySearchTree<T> where T : IComparable<T>
{
    private TreeNode<T>? _root = null;

    public void Add(T value)
    {
        _root = AddRecursive(_root, value);
    }

    private static TreeNode<T> AddRecursive(TreeNode<T>? node, T value)
    {
        if (node == null)
            return new TreeNode<T>(value);
        
        if (value.CompareTo(node.Value) < 0)
            node.Left = AddRecursive(node.Left, value);
        else 
            node.Right = AddRecursive(node.Right, value);

        return node;
    }

    public TreeNode<T>? Search(T value)
    {
        var node = _root;

        while (node != null)
        {
            if (node.Value.CompareTo(value) == 0)
                return node;
            
            if (node.Value.CompareTo(value) < 0)
                node = node.Left;
            else
                node = node.Right;
        }

        return null;
    }

    // 전위 탐색
    public List<TreeNode<T>> PreOrderSearch()
    {
        var list = new List<TreeNode<T>>();
        PreOrderSearch(_root, list);

        return list;
    }

    private static void PreOrderSearch(TreeNode<T>? node, List<TreeNode<T>> results)
    {
        if (node == null)
            return;
        
        results.Add(node);
        PreOrderSearch(node.Left, results);
        PreOrderSearch(node.Right, results);
    }
    
    // 중위 탐색, 정렬된 list가 나옴
    public List<TreeNode<T>> InOrderSearch()
    {
        var list = new List<TreeNode<T>>();
        InOrderSearch(_root, list);

        return list;
    }
    
    private static void InOrderSearch(TreeNode<T>? node, List<TreeNode<T>> results)
    {
        if (node == null)
            return;
        
        InOrderSearch(node.Left, results);
        results.Add(node);
        InOrderSearch(node.Right, results);
    }
    
    public List<TreeNode<T>> PostOrderSearch()
    {
        var list = new List<TreeNode<T>>();
        PostOrderSearch(_root, list);

        return list;
    }
    
    private static void PostOrderSearch(TreeNode<T>? node, List<TreeNode<T>> results)
    {
        if (node == null)
            return;
        
        PostOrderSearch(node.Left, results);
        PostOrderSearch(node.Right, results);
        results.Add(node);
    }
}