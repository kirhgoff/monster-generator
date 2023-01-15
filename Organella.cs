using System;

public class Organella {
    public string id;
    public Shape shape;
    public Symbol symbol;
    public Organella(Symbol symbol, Shape shape, string? id = null) {
        this.shape = shape;
        this.symbol = symbol;
        if (id == null) {
            this.id = Guid.NewGuid().ToString();
        } else {
            this.id = id;
        } 
    }

    public override string ToString() {
        return $"Organella({id}, {symbol}, {shape})";
    }
}