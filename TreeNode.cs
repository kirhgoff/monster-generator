public interface TreeNodeVisitor<T> {
    public void Visit(T node, T parent);
}

public class TreeNode<T> 
{
    public T data { get; set; }
    public List<TreeNode<T>> children { get; set; }

    public TreeNode(T data)
    {
        this.data = data;
        this.children = new List<TreeNode<T>>();
    }

    public void AddChild(TreeNode<T> child)
    {
        children.Add(child);
    }

    public void Traverse(TreeNodeVisitor<T> visitor)
    {
        this.Visit(visitor, null);
    }

    private void Visit(TreeNodeVisitor<T> visitor, TreeNode<T> root)
    {
        visitor.Visit(this.data, root.data);
        foreach (var child in children)
        {
            child.Visit(visitor, this);
        }
    }

    public override string ToString()
    {
        return data?.ToString() ?? "null";
    }
}
