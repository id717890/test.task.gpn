using System.Collections.Generic;

namespace Oil.Bll.Interfaces.Infrastructure
{
    public interface IMessageModelBuilder
    {
        IDictionary<string, string[]> CreateModel(string code, string message);
        IDictionary<string, string[]> CreateModel(string code, string[] messages);
    }
}
