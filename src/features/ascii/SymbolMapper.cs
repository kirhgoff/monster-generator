public class SymbolMapper {
    public Dictionary<string, char> symbolToName = new Dictionary<string, char>();

    public SymbolMapper(Dictionary<string, char> symbolToName) {
        this.symbolToName = symbolToName;
    }
    public char GetSymbol(string symbol) {
        return symbolToName[symbol];
    }
}