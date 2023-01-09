using System;

public struct Shape 
{
    public string id = Guid.NewGuid().ToString();
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
}
