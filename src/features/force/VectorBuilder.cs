using System;

public class VectorBuilder 
{
    private static Random random = new Random();
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

    public Vector2D RandomIfZero()
    {
        if (vector!.IsZero()) {
            var x = random.NextDouble() * 2 - 1;
            var y = random.NextDouble() * 2 - 1;
            return new Vector2D(x, y);
        } else {
            return vector!;
        }
    }
}