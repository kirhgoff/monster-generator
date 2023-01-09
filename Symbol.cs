public class Symbol 
{
	string face;

	public Symbol(string face) {
		this.face = face;
	}

	public override string ToString()
	{
		return "[" + face + "]";
	}

    public override bool Equals(Object? obj)
    {
      if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
      {
         return false;
      }
      else {
         Symbol another = (Symbol) obj;

         return another.face == this.face;
      }
    }

    public override int GetHashCode()
    {
        return this.face.GetHashCode();
    }
}
