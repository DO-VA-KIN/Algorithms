using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace lab12Generator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            System.Diagnostics.Process.Start(Environment.CurrentDirectory + @"\net7.0\Lab12_Buff.exe");

            uint[] uints;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(uint[]));
            using (FileStream fs = new FileStream("res.xml", FileMode.Open))
            { uints = xmlSerializer.Deserialize(fs) as uint[]; }

            Array.Sort(uints);
            Dictionary<uint, uint> dict = new Dictionary<uint, uint>(101);
            foreach (uint v in uints)
            {
                if(dict.ContainsKey(v)) { dict[v]++; }
                else { dict.Add(v, 1); }
            }

            uint[] eValues = new uint[11];
            int i = 0;
            int j = 0;
            foreach (var it in dict)
            {
                eValues[j] += it.Value;
                i++;
                if(i == 10)
                {
                    i = 0;
                    j++;
                }
            }


            //List<KeyValuePair<string, uint>> l = new List<KeyValuePair<string, uint>>(uints.Length);
            //for (int i = 0; i < uints.Length; i++)
            //    l.Add(new KeyValuePair<string, uint>(i.ToString() + "значение", uints[i]));

            ((AreaSeries)DVCHistogram.Series[0]).ItemsSource = dict;
        }

    }
}
