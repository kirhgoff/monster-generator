public class Symbol 
{
	public string id;

	public Symbol(string id) {
		this.id = id;
	}

	public override string ToString()
	{
		return "[" + id + "]";
	}

    public override bool Equals(Object? obj)
    {
      if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
      {
         return false;
      }
      else {
         Symbol another = (Symbol) obj;

         return another.id == this.id;
      }
    }

    public override int GetHashCode()
    {
        return this.id.GetHashCode();
    }
}
