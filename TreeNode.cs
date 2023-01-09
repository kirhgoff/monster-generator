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

    public override string ToString()
    {
        return data?.ToString() ?? "null";
    }
}
