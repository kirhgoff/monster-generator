public class Organella {
    public Shape shape;
    public Symbol symbol;

    public Transformation transformation;

    public Organella(Symbol symbol, Shape shape, Transformation transformation) {
        this.shape = shape;
        this.symbol = symbol;
        this.transformation = transformation;
    }

    public static Organella CreateStable(Symbol symbol, Shape shape) {
        return new Organella(
            symbol, 
            shape, 
            new ConstantTransformation(shape.radius)
        );
    }
}