public class VectorBuilder 
{
    private Vector2D? vector;

    public VectorBuilder FromCenterOf(Organella organ)
    {
        vector = new Vector2D(organ.shape.centerX, organ.shape.centerY);
        return this;
    }

    public VectorBuilder ToCenterOf(Organella organ)
    {
        this.vector = new Vector2D(organ.shape.centerX, organ.shape.centerX)  - this.vector!;
        return this;
    }

    public Vector2D DownIfZero()
    {
        return this.vector!.IsZero() ? new Vector2D(0, -1) : vector!;
    }

    public Vector2D UpIfZero()
    {
        return this.vector!.IsZero() ? new Vector2D(0, 1) : vector!;
    }
}