using System;
using System.Text;
using static System.Math;

public static class AsciiRenderer 
{

    private static int screenWidth = 80;
    private static int screenHeight = 30;

    public static string Render(List<Organella> organellas, SymbolMapper mapper) 
    {
        double realLeft = organellas.Min(s => s.shape.centerX - s.shape.radius);
        double realRight = organellas.Max(s => s.shape.centerX + s.shape.radius);
        double realTop = organellas.Min(s => s.shape.centerY - s.shape.radius);
        double realBottom = organellas.Max(s => s.shape.centerY + s.shape.radius);
        // Console.WriteLine("Real size: [{0}, {1}], [{2}, {3}]", realLeft, realTop, realRight, realBottom);

        double cellWidth = (realRight - realLeft) / (double) screenWidth;
        double cellHeight = (realBottom - realTop) / (double) screenHeight;
        // Console.WriteLine("Cell size: {0}, {1}", cellWidth, cellHeight);

        char[,] canvas = new char[screenWidth, screenHeight];
        for (int x = 0; x < screenWidth; x++) {
            for (int y = 0; y < screenHeight; y++) {
                canvas[x, y] = ' ';
            }
        }

        foreach (Organella organella in organellas) {
            Shape shape = organella.shape;
            int screenStartX = (int)((shape.centerX - shape.radius - realLeft) / cellWidth);
            int screenEndX = (int)((shape.centerX + shape.radius - realLeft) / cellWidth);
            int screenStartY = (int)((shape.centerY - shape.radius - realTop) / cellHeight);
            int screenEndY = (int)((shape.centerY + shape.radius - realTop) / cellHeight);
            // Console.WriteLine("Shape screen size: [{0}, {1}], [{2}, {3}]", screenStartX, screenStartY, screenEndX, screenEndY);

            for (int screenX = screenStartX; screenX < screenEndX; screenX ++) {
                for (int screenY = screenStartY; screenY < screenEndY; screenY++) {
                    double x = realLeft + screenX * cellWidth;
                    double y = realTop + screenY * cellHeight;
                    //Console.WriteLine("Shape screen: [{0}, {1}], real: {2}, {3}", screenX, screenY, x, y);

                    if (shape.Contains(x, y)) {
                        //Console.WriteLine("Shape screen: [{0}, {1}], real: {2}, {3}", screenX, screenY, x, y);
                        canvas[screenX, screenY] = mapper.GetSymbol(organella.symbol.id);
                    }
                }
            }
        }

        return RenderCanvas(canvas);
    }

    static string RenderCanvas(char[,] canvas) 
    {
        StringBuilder sb = new StringBuilder();

        for (int y = 0; y < canvas.GetLength(1); y++) {
            for (int x = 0; x < canvas.GetLength(0); x++) {
                sb.Append(canvas[x, y]);
            }
            sb.Append("\n");
        }
        return sb.ToString();
    }
}