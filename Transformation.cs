public class Transformation 
{
    public double startRadius;
    public double endRadius;
    public long stepsToFullGrown;
    public long stepsToChildrenGrowthStarts;

    public double currentRadius;
    public long currentSteps;

    public Transformation(double startRadius, double endRadius, long stepsToFullGrown, long stepsToChildrenGrowthStarts) 
    {
        this.startRadius = startRadius;
        this.endRadius = endRadius;
        this.stepsToFullGrown = stepsToFullGrown;
        this.stepsToChildrenGrowthStarts = stepsToChildrenGrowthStarts;
        this.currentRadius = startRadius;
        this.currentSteps = 0;
    }
}

public class ConstantTransformation : Transformation 
{
    public ConstantTransformation(double radius) 
        : base(radius, radius, 0, 0) 
    {
    }
}