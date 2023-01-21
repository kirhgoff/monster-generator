using System;

public class Force 
{
    static public Force NOOP = new Force(new Vector2D(0, 0));

    public Vector2D vector;

    public Force(Vector2D vector)
    {
        this.vector = vector;
    }

    public static Force CalculateForce(Entity entity, Organella organ1, Organella organ2)
    {
        var overallForce = NOOP;
        var physics = new Physics();

        // Intersection forces
        var overlapSquared = organ1.shape.OverlapSquared(organ2.shape);
        if (overlapSquared > 0)
        {
            overallForce += physics.IntersectionForce(organ1, organ2, overlapSquared);
        }

        // Child parent forces
        var parent = entity.GetParent(organ1);
        if (parent != null)
        {
            overallForce += physics.ChildParentForce(organ1, organ2);
        }

        // The friction
        overallForce += physics.FrictionForce(organ1);

        return overallForce;
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