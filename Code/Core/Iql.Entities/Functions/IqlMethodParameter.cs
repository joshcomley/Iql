namespace Iql.Entities.Functions
{
    public class IqlMethodParameter
    {
        public string Name { get; set; }
        public bool IsBindingParameter { get; set; }
        public ITypeDefinition Type { get; set; }

        public IqlMethodParameter(string name, bool isBindingParameter, ITypeDefinition type)
        {
            Name = name;
            IsBindingParameter = isBindingParameter;
            Type = type;
        }
    }
}