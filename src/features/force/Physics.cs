public class Physics 
{
    public Force IntersectionForce(Organella organ1, Organella organ2, double intersectSquared)
    {
        return new Force(new Vector2D(0, 0));
    }

    public Force ChildParentForce(Organella organ1, Organella organ2)
    {
        return new Force(new Vector2D(0, 0));
    }

    public Force FrictionForce(Organella organ1)
    {
        return new Force(new Vector2D(0, 0));
    }
}