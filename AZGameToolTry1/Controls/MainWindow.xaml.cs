using AZGameToolTry1.LService;
using MahApps.Metro.Controls;
using Markdig;
using MaterialDesignThemes.Wpf;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using AZGameToolTry1.ViewModel;

namespace AZGameToolTry1.Controls
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

    

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //var pipeline = new MarkdownPipelineBuilder()
            //    .UsePragmaLines()
            //    .UseDiagrams()
            //    .UseAdvancedExtensions()
            //    .UseYamlFrontMatter()
            //    .Build();

            //var htmlStraight = Markdig.Markdown.ToHtml(File.ReadAllText("README.md"), pipeline);

            //var doc = Markdig.Markdown.Parse(File.ReadAllText("README.md"), pipeline);

            //StringBuilder sb = new StringBuilder();
            //XmlWriter writer = XmlWriter.Create(sb);
            //writer.WriteStartDocument();
            //writer.WriteStartElement("html");
            //writer.WriteStartElement("head");
            //writer.WriteStartElement("style");
            //writer.WriteRaw("ul { list-style-type: none; }");
            //writer.WriteEndElement();
            //writer.WriteEndElement();
            //writer.WriteStartElement("body");
            //writer.WriteRaw(htmlStraight);
            //writer.WriteEndElement();
            //writer.WriteEndElement();
            //writer.Close();
        }

     
    }
}