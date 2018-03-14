using System.Text;

namespace ExRam.Gremlinq
{
    internal struct T : IGremlinSerializable
    {
        public static readonly T Id = new T("id");

        private readonly string _name;

        private T(string name)
        {
            this._name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is T variable && this._name == variable._name;
        }

        public override int GetHashCode()
        {
            return this._name.GetHashCode();
        }

        public static bool operator ==(T obj1, T obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(T obj1, T obj2)
        {
            return !obj1.Equals(obj2);
        }

        public void Serialize(StringBuilder builder, IParameterCache parameterCache)
        {
            builder.Append(nameof(T));
            builder.Append(".");
            builder.Append(this._name);
        }
    }
}