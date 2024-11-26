using System;
using System.IO;
using System.Xml;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Xsl;

namespace lab1
{
    public partial class MainPage : ContentPage
    {
        private string _selectedFilePath;
        private List<string> _availableAttributes = new List<string>();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnBrowseClicked(object sender, EventArgs e)
        {
            try
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.xml" } },
                    { DevicePlatform.Android, new[] { "application/xml" } },
                    { DevicePlatform.WinUI, new[] { ".xml" } },
                    { DevicePlatform.MacCatalyst, new[] { "public.xml" } }
                });

                var options = new PickOptions
                {
                    PickerTitle = "Спочатку оберіть XML файл.",
                    FileTypes = customFileType
                };

                var result = await FilePicker.Default.PickAsync(options);

                if (result != null)
                {
                    _selectedFilePath = result.FullPath;
                    FilePathLabel.Text = _selectedFilePath;

                    LoadAttributesFromXml(result.FullPath);
                }
                else
                {
                    FilePathLabel.Text = "Жоден файл не обраний";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Помилка", $"ПОмилка: {ex.Message}", "OK");
            }
        }

        private void LoadAttributesFromXml(string filePath)
        {
            try
            {
                _availableAttributes.Clear();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                var elements = xmlDoc.GetElementsByTagName("*");

                foreach (XmlNode element in elements)
                {
                    foreach (XmlAttribute attribute in element.Attributes)
                    {
                        if (!_availableAttributes.Contains(attribute.Name))
                        {
                            _availableAttributes.Add(attribute.Name);
                        }
                    }
                }

                AttributePicker.ItemsSource = _availableAttributes;
                AttributePicker.SelectedIndex = -1; 
            }
            catch (Exception ex)
            {
                OutputField.Text = $"Помилка завантаження файлу: {ex.Message}";
            }
        }

        private void OnSearchClicked(object sender, EventArgs e)
        {
            try
            {
                string filePath = FilePathLabel.Text;

                if (filePath == "Жоден файл не обраний" || string.IsNullOrEmpty(filePath))
                {
                    OutputField.Text = "Спочатку оберіть XML файл.";
                    return;
                }

                string selectedAttribute = AttributePicker.SelectedItem as string;

                if (string.IsNullOrEmpty(selectedAttribute))
                {
                    OutputField.Text = "Оберіть атрибут.";
                    return;
                }

                string parsingMethod = ParsingMethodPicker.SelectedItem as string;

                if (string.IsNullOrEmpty(parsingMethod))
                {
                    OutputField.Text = "Оберіть метод обробки.";
                    return;
                }

                OutputField.Text = $"Обраний метод обробки: {parsingMethod}\n\n";

                XmlParserContext context;

                if (parsingMethod == "DOM")
                {
                    context = new XmlParserContext(new DomXmlParser());
                }
                else if (parsingMethod == "SAX")
                {
                    context = new XmlParserContext(new SaxXmlParser());
                }
                else if (parsingMethod == "LINQ")
                {
                    context = new XmlParserContext(new LinqXmlParser());
                }
                else
                {
                    OutputField.Text = "Обраний не правильний метод.";
                    return;
                }

                context.ExecuteStrategy(filePath, OutputField, selectedAttribute); 
            }
            catch (Exception ex)
            {
                OutputField.Text = $"Помилка: {ex.Message}";
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            FilePathLabel.Text = "Жоден файл не обраний";
            OutputField.Text = string.Empty;
            AttributePicker.SelectedIndex = -1;
            ParsingMethodPicker.SelectedIndex = -1;
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Вихід", "Ви впевнені, що хочете вийти?", "Так", "Ні");

            if (answer)
            {
                Environment.Exit(0);
            }
        }

        private async void OnTransformToHtmlClicked(object sender, EventArgs e)
        {
            try
            {
                string filePath = FilePathLabel.Text;

                if (filePath == "Жоден файл не обраний" || string.IsNullOrEmpty(filePath))
                {
                    OutputField.Text = "Спочатку оберіть XML файл.";
                    return;
                }

                string xslFilePath = "/Users/ksenia/Downloads/test.xsl"; 
                if (!File.Exists(xslFilePath))
                {
                    OutputField.Text = "XSL файл не знайдений.";
                    return;
                }

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);

                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xslFilePath);

                StringWriter writer = new StringWriter();
                XmlTextWriter xmlWriter = new XmlTextWriter(writer) { Formatting = Formatting.Indented };

                xslt.Transform(xmlDocument, null, xmlWriter);

                string htmlOutput = writer.ToString();

                OutputField.Text = htmlOutput;
            }
            catch (Exception ex)
            {
                OutputField.Text = $"Помилка: {ex.Message}";
            }
        }
    }
}
