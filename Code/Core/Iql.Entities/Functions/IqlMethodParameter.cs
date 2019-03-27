namespace Iql.Entities.Functions
{
    public class IqlMethodParameter
    {
        public string Name { get; set; }
        public bool IsBindingParameter { get; set; }
        public ITypeDefinition Type { get; set; }

        public IqlMethodParameter(string name = null, bool isBindingParameter = false, ITypeDefinition type = null)
        {
            Name = name;
            IsBindingParameter = isBindingParameter;
            Type = type;
        }
    }
}