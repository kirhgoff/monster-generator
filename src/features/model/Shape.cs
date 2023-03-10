
public class Shape 
{
    public double centerX;
    public double centerY;
    public double radius;

    public Shape(double centerX, double centerY, double radius) 
    {
        this.centerX = centerX;
        this.centerY = centerY;
        this.radius = radius;
    }

    public bool Contains(double x, double y) 
    {
        if (x < centerX - radius || 
            x > centerX + radius || 
            y < centerY - radius || 
            y > centerY + radius
        ) {
            return false;
        }
        return Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2) <= Math.Pow(radius, 2);
    }

    public double Overlap(Shape another) {
        var distance = Math.Sqrt(Math.Pow(centerX - another.centerX, 2) 
                + Math.Pow(centerY - another.centerY, 2));
        if (distance > radius + another.radius) {
            return 0;
        }

        return (radius + another.radius - distance) / 2;
    }

    public double RootDistanceSquared() {
        return Math.Pow(centerX, 2) + Math.Pow(centerY, 2);
    }

    public override string ToString() {
        return $"Shape({centerX}, {centerY}, {radius})";
    }
    
    public Shape Copy() {
        return new Shape(centerX, centerY, radius);
    }
}
