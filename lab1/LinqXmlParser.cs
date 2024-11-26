using System;
using System.Xml.Linq;

namespace lab1
{
    public class LinqXmlParser : IXmlParserStrategy
    {
        public void Parse(string filePath, Editor outputField, string selectedAttribute)
        {
            try
            {
                var doc = XDocument.Load(filePath);
                string result = $"Using LINQ parser:\n";

                foreach (var gurtok in doc.Descendants("gurtok"))
                {
                    if (selectedAttribute == "id")
                    {
                        result += $"{selectedAttribute}: {gurtok.Attribute("id")?.Value}\n";
                    }
                    else if (selectedAttribute == "data")
                    {
                        result += $"{selectedAttribute}: {gurtok.Attribute("data")?.Value}\n";
                    }

                    var kerivnyk = gurtok.Element("kerivnyk");
                    if (kerivnyk != null)
                    {
                        if (selectedAttribute == "pib")
                        {
                            result += $"{selectedAttribute}: {kerivnyk.Attribute("pib")?.Value}\n";
                        }
                        else if (selectedAttribute == "fakultet")
                        {
                            result += $"{selectedAttribute}: {kerivnyk.Attribute("fakultet")?.Value}\n";
                        }
                        else if (selectedAttribute == "kafedra")
                        {
                            result += $"{selectedAttribute}: {kerivnyk.Attribute("kafedra")?.Value}\n";
                        }
                    }

                    var zustrich = gurtok.Element("zustrich");
                    if (zustrich != null)
                    {
                        if (selectedAttribute == "den")
                        {
                            result += $"{selectedAttribute}: {zustrich.Attribute("den")?.Value}\n";
                        }
                        else if (selectedAttribute == "chas")
                        {
                            result += $"{selectedAttribute}: {zustrich.Attribute("chas")?.Value}\n";
                        }
                    }

                    var nazva = gurtok.Element("nazva_oriyentaciya")?.Element("nazva");
                    if (nazva != null && selectedAttribute == "nazva")
                    {
                        result += $"{selectedAttribute}: {nazva.Value}\n";
                    }

                    var oriyentaciya = gurtok.Element("nazva_oriyentaciya")?.Element("oriyentaciya");
                    if (oriyentaciya != null)
                    {
                        if (selectedAttribute == "kurs")
                        {
                            result += $"{selectedAttribute}: {oriyentaciya.Attribute("kurs")?.Value}\n";
                        }
                        else if (selectedAttribute == "tematyka")
                        {
                            result += $"{selectedAttribute}: {oriyentaciya.Attribute("tematyka")?.Value}\n";
                        }
                    }

                    var starosta = gurtok.Element("starosta");
                    if (starosta != null && selectedAttribute == "pib")
                    {
                        result += $"{selectedAttribute}: {starosta.Attribute("pib")?.Value}\n";
                    }
                }

                outputField.Text += result;
            }
            catch (Exception ex)
            {
                outputField.Text = $"Error: {ex.Message}";
            }
        }
    }
}
