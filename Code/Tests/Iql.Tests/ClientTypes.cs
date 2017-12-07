namespace Iql.Tests
{
    public class ClientTypes
    {
        public ClientType ClientType1 { get; }
        public ClientType ClientType2 { get; }
        public ClientType ClientType3 { get; }
        public ClientType ClientType4 { get; }
        public ClientType ClientType5 { get; }

        public ClientTypes(ClientType clientType1, ClientType clientType2, ClientType clientType3, ClientType clientType4,
            ClientType clientType5)
        {
            ClientType1 = clientType1;
            ClientType2 = clientType2;
            ClientType3 = clientType3;
            ClientType4 = clientType4;
            ClientType5 = clientType5;
        }
    }
}