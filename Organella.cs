using System;

public class Organella {
    public string id = Guid.NewGuid().ToString();
    public Shape shape;
    public Symbol symbol;
    public Organella(Symbol symbol, Shape shape) {
        this.shape = shape;
        this.symbol = symbol;
    }

    public override string ToString() {
        return $"Organella({id}, {symbol}, {shape})";
    }
}