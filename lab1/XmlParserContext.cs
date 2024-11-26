namespace lab1
{
    public class XmlParserContext
    {
        private IXmlParserStrategy _strategy;

        public XmlParserContext(IXmlParserStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IXmlParserStrategy strategy)
        {
            _strategy = strategy;
        }

        public void ExecuteStrategy(string filePath, Editor outputField, string selectedAttribute)
        {
            _strategy.Parse(filePath, outputField, selectedAttribute);
        }
    }
}
