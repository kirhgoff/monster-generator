public class Physics 
{
    public static readonly double INTERSECTION_FORCE = 0.5;
    public static readonly double CHILD_PARENT_FORCE = 0.3;
    public static readonly double FRICTION_FORCE = 0.1;

    // Calculates the force that pushes from surrounging organs
    // To our ornagella
    public Force IntersectionForce(Organella organ, Organella otherOrgan)
    {
        var overlap = organ.shape.Overlap(otherOrgan.shape);
        if (overlap <= 0)
        {
            return new Force(new Vector2D(0, 0));
        }
        Console.WriteLine($"Intersection! {organ.symbol} vs {otherOrgan.symbol} squared: {overlap}");
        var vector = new VectorBuilder()
            .FromCenterOf(otherOrgan)
            .ToCenterOf(organ)
            .RandomIfZero();

        return new Force(vector * overlap * INTERSECTION_FORCE);
    }

    public Force ChildParentForce(Organella organ, Organella parent)
    {
        var vector = new VectorBuilder()
            .FromCenterOf(organ)
            .ToCenterOf(parent)
            .RandomIfZero();

        return new Force(vector * CHILD_PARENT_FORCE);
    }

    public Force FrictionForce(Organella organ, Force overallForce)
    {
        var vector = overallForce.vector * -1;
        return new Force(vector * FRICTION_FORCE);
    }
 }
