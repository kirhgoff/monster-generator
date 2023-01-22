public class ForceBasedAlgorythm 
{
    public Entity LayOut(Entity entity, int iterations)
    {
        Entity newEntity = entity.MakeCopy();
        for (int i = 0; i < iterations; i++)
        {
            newEntity = Step(newEntity);
        }
        return newEntity;
    }

    private Entity Step(Entity entity)
    {
        var physics = new Physics();
        Entity newEntity = entity.MakeCopy();

        foreach (var organ in newEntity.GetOrganellas())
        {
            if (organ.symbol.id == "seed")
            {
                continue;
            }

            var forces = new List<Force>();
            foreach (var otherOrgan in newEntity.GetOrganellas())
            {
                if (organ.id == otherOrgan.id) 
                {
                    continue;
                }
                
                forces.Add(physics.IntersectionForce(organ, otherOrgan));
            }
            var parent = entity.GetParent(organ);
            if (parent != null)
            {
                forces.Add(physics.ChildParentForce(organ, parent));
            }

            var totalForce = forces.Aggregate((a, b) => a + b);

            var frictionForce = physics.FrictionForce(organ, totalForce);

            totalForce = totalForce + frictionForce;

            var newShape = totalForce.ApplyTo(organ.shape);
            Console.WriteLine($"{organ.symbol} Force: {totalForce.vector.x}, {totalForce.vector.y} newShape: {newShape.centerX}, {newShape.centerY}");
            organ.shape = newShape;
        }

        return newEntity;
    }
}