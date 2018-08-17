using System;
using System.Collections.Generic;
using Iql.Entities;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    public class MediaKeyGroup : IMediaKeyGroup
    {
        public IMediaKey MediaKey { get; }
        public string Separator { get; set; }
        public List<IMediaKeyPart> Parts { get; set; }
        public string[] Evaluate(object entity)
        {
            throw new NotImplementedException();
        }

        public string EvaluateToString(object entity)
        {
            throw new NotImplementedException();
        }
    }
}