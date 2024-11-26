using System;
using System.Xml;

namespace lab1
{
    public class SaxXmlParser : IXmlParserStrategy
    {
        public void Parse(string filePath, Editor outputField, string selectedAttribute)
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    string result = $"Using SAX parser:\n";
                    string selectedValue = null;

                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            if (reader.Name == "gurtok")
                            {
                                if (selectedAttribute == "id" && reader.HasAttributes)
                                {
                                    selectedValue = reader.GetAttribute("id");
                                }
                                else if (selectedAttribute == "data" && reader.HasAttributes)
                                {
                                    selectedValue = reader.GetAttribute("data");
                                }
                            }

                            if (reader.Name == "kerivnyk")
                            {
                                if (selectedAttribute == "pib" && reader.HasAttributes)
                                {
                                    selectedValue = reader.GetAttribute("pib");
                                }
                                else if (selectedAttribute == "fakultet" && reader.HasAttributes)
                                {
                                    selectedValue = reader.GetAttribute("fakultet");
                                }
                                else if (selectedAttribute == "kafedra" && reader.HasAttributes)
                                {
                                    selectedValue = reader.GetAttribute("kafedra");
                                }
                            }

                            if (reader.Name == "zustrich")
                            {
                                if (selectedAttribute == "den" && reader.HasAttributes)
                                {
                                    selectedValue = reader.GetAttribute("den");
                                }
                                else if (selectedAttribute == "chas" && reader.HasAttributes)
                                {
                                    selectedValue = reader.GetAttribute("chas");
                                }
                            }

                            if (reader.Name == "nazva")
                            {
                                if (selectedAttribute == "nazva")
                                {
                                    reader.Read(); // Читати текст всередині тега <nazva>
                                    selectedValue = reader.Value;
                                }
                            }

                            if (reader.Name == "oriyentaciya")
                            {
                                if (selectedAttribute == "kurs" && reader.HasAttributes)
                                {
                                    selectedValue = reader.GetAttribute("kurs");
                                }
                                else if (selectedAttribute == "tematyka" && reader.HasAttributes)
                                {
                                    selectedValue = reader.GetAttribute("tematyka");
                                }
                            }

                            if (reader.Name == "starosta")
                            {
                                if (selectedAttribute == "pib" && reader.HasAttributes)
                                {
                                    selectedValue = reader.GetAttribute("pib");
                                }
                            }
                        }

                        if (selectedValue != null)
                        {
                            result += $"{selectedAttribute}: {selectedValue}\n";
                            selectedValue = null;
                        }
                    }

                    outputField.Text += result;
                }
            }
            catch (Exception ex)
            {
                outputField.Text = $"Error: {ex.Message}";
            }
        }
    }
}
