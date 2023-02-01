public class Vector2D {
    public double x;
    public double y;
    public Vector2D(double x, double y) {
        this.x = x;
        this.y = y;
    }
    public static Vector2D operator +(Vector2D a, Vector2D b) {
        dynamic x = a.x + b.x;
        dynamic y = a.y + b.y;
        return new Vector2D(x, y);
    }

    public static Vector2D operator *(Vector2D a, double k) {
        dynamic x = k * a.x;
        dynamic y = k * a.y;
        return new Vector2D(x, y);
    }

    public static Vector2D operator -(Vector2D a, Vector2D b) {
        dynamic x = a.x - b.x;
        dynamic y = a.y - b.y;
        return new Vector2D(x, y);
    }

    public bool IsZero() {
        // TODO: add epsilon
        return x == 0 && y == 0;
    }
}