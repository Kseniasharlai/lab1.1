using System;
using System.Xml;

namespace lab1
{
    public class DomXmlParser : IXmlParserStrategy
    {
        public void Parse(string filePath, Editor outputField, string selectedAttribute)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);

                string result = $"Using DOM parser:\n";

                XmlNodeList gurtokNodes = doc.GetElementsByTagName("gurtok");
                foreach (XmlNode gurtok in gurtokNodes)
                {
                    if (selectedAttribute == "id")
                    {
                        result += $"{selectedAttribute}: {gurtok.Attributes["id"]?.Value}\n";
                    }
                    else if (selectedAttribute == "data")
                    {
                        result += $"{selectedAttribute}: {gurtok.Attributes["data"]?.Value}\n";
                    }

                    XmlNode kerivnyk = gurtok["kerivnyk"];
                    if (kerivnyk != null)
                    {
                        if (selectedAttribute == "pib")
                        {
                            result += $"{selectedAttribute}: {kerivnyk.Attributes["pib"]?.Value}\n";
                        }
                        else if (selectedAttribute == "fakultet")
                        {
                            result += $"{selectedAttribute}: {kerivnyk.Attributes["fakultet"]?.Value}\n";
                        }
                        else if (selectedAttribute == "kafedra")
                        {
                            result += $"{selectedAttribute}: {kerivnyk.Attributes["kafedra"]?.Value}\n";
                        }
                    }

                    XmlNode zustrich = gurtok["zustrich"];
                    if (zustrich != null)
                    {
                        if (selectedAttribute == "den")
                        {
                            result += $"{selectedAttribute}: {zustrich.Attributes["den"]?.Value}\n";
                        }
                        else if (selectedAttribute == "chas")
                        {
                            result += $"{selectedAttribute}: {zustrich.Attributes["chas"]?.Value}\n";
                        }
                    }

                    XmlNode nazva = gurtok.SelectSingleNode("nazva_oriyentaciya/nazva");
                    if (nazva != null && selectedAttribute == "nazva")
                    {
                        result += $"{selectedAttribute}: {nazva.InnerText}\n";
                    }

                    XmlNode oriyentaciya = gurtok.SelectSingleNode("nazva_oriyentaciya/oriyentaciya");
                    if (oriyentaciya != null)
                    {
                        if (selectedAttribute == "kurs")
                        {
                            result += $"{selectedAttribute}: {oriyentaciya.Attributes["kurs"]?.Value}\n";
                        }
                        else if (selectedAttribute == "tematyka")
                        {
                            result += $"{selectedAttribute}: {oriyentaciya.Attributes["tematyka"]?.Value}\n";
                        }
                    }

                    XmlNode starosta = gurtok["starosta"];
                    if (starosta != null && selectedAttribute == "pib")
                    {
                        result += $"{selectedAttribute}: {starosta.Attributes["pib"]?.Value}\n";
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
