using Oil.Bll.Interfaces.Infrastructure;
using System.Collections.Generic;

namespace Oil.Bll.Infrastructure
{
    public class MessageModelBuilder : IMessageModelBuilder
    {
        public IDictionary<string, string[]> CreateModel(string code, string message)
        {
            return new Dictionary<string, string[]> { { code, new[] { message } } };
        }

        public IDictionary<string, string[]> CreateModel(string code, string[] messages)
        {
            return new Dictionary<string, string[]> { { code, messages } };
        }
    }
}
