using System;

public class Force 
{
    static public Force NOOP = new Force(new Vector2D(0, 0));

    public Vector2D vector;

    public Force(Vector2D vector)
    {
        this.vector = vector;
    }

    public static Force operator +(Force a, Force b)
    {
        return new Force(a.vector + b.vector);
    }

    public Shape ApplyTo(Shape shape)
    {
        // TODO: this is not correct
        return new Shape (
            shape.centerX + vector.x,
            shape.centerY + vector.y,
            shape.radius
        ) ;
    }
}