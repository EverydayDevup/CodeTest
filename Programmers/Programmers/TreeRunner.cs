namespace Programmers;

public class TreeRunner : Runner
{
    public override void Solution()
    {
        var bst = new BinarySearchTree<int>();
        bst.Add(50);
        bst.Add(30);
        bst.Add(70);
        bst.Add(20);
        bst.Add(40);
        bst.Add(60);
        bst.Add(80);

        Start($"{nameof(bst.PreOrderSearch)}");
        Console.WriteLine($"{nameof(bst.PreOrderSearch)}: {string.Join(",", bst.PreOrderSearch())}");
        End($"{nameof(bst.PreOrderSearch)}");
        
        Start($"{nameof(bst.InOrderSearch)}");
        Console.WriteLine($"{nameof(bst.InOrderSearch)}: {string.Join(",", bst.InOrderSearch())}");
        End($"{nameof(bst.InOrderSearch)}");
        
        Start($"{nameof(bst.PostOrderSearch)}");
        Console.WriteLine($"{nameof(bst.PostOrderSearch)}: {string.Join(",", bst.PostOrderSearch())}");
        End($"{nameof(bst.PostOrderSearch)}");
    }
}