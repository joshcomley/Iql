using Iql.Entities;
using System;
using System.Collections.Generic;

namespace Iql.Server.Serialization
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