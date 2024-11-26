using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public interface IXmlParserStrategy
    {
        void Parse(string filePath, Editor outputField, string selectedAttribute);
    }
}
