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
using AZGameToolTry1.Model;

namespace AZGameToolTry1.Controls
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        ITabNavigationService tabNavigationService;
      

        public MainWindow()
        {
            InitializeComponent();
            tabNavigationService = NinjectServiceLocator.GetStuff<ITabNavigationService>();
         //   projectDataService = NinjectServiceLocator.GetStuff<IProjectDataService>();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tabNavigationService.TabCreated += TabNavigationService_TabCreated;

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

        private void TabNavigationService_TabCreated(string s, TabNavigationEventArgs e)
        {
            switch (s)
            {
                case "ReadMeTab":
                    {
                        var readmeTab = new ContentTabItem() { DataContext = e.Parameter, Header = ((ReadMe)e.Parameter).FileName };
                        TabHostControl.Items.Add(readmeTab);

                        TabHostControl.SelectedItem = readmeTab;

                        //tabNavigationService.NavigateToTab("ReadMeTab",e.Parameter);
                    }
                    break;
                case "AboutMe":
                    {
                        var readmeTab = new ContentTabItem() { DataContext = e.Parameter, Header = "Omar Ajerray"};
                        TabHostControl.Items.Add(readmeTab);

                        TabHostControl.SelectedItem = readmeTab;

                        //tabNavigationService.NavigateToTab("ReadMeTab",e.Parameter);
                    }
                    break;
                default:
                    break;
            }
        }

        private void MetroWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            tabNavigationService.TabCreated -= TabNavigationService_TabCreated;
        }
    }
}