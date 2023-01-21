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
        Entity newEntity = entity.MakeCopy();
        foreach (var organ in newEntity.GetOrganellas())
        {
            var forces = new List<Force>();
            foreach (var otherOrgan in newEntity.GetOrganellas())
            {
                if (organ.id == otherOrgan.id) 
                {
                    continue;
                }
                
                var force = Force.CalculateForce(newEntity, organ, otherOrgan);
                forces.Add(force);
            }
            // TODO: there will be someting different here
            var totalForce = forces.Aggregate((a, b) => a + b);
            var newShape = totalForce.ApplyTo(organ.shape);
            organ.shape = newShape;
        }

        return newEntity;
    }
}