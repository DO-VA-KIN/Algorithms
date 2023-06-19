using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Threading;

using AlgorithmL1.Classes;

namespace AlgorithmL1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Config.Timer.Tick += Timer_Tick;
            Config.Timer.Start();
            Config.Labels = new Label[] { L1000, L2000, L4000, L8000, L16000, L32000, L64000, L128000 };
            Config.ListBoxes = new ListBox[] { LB1000, LB2000, LB4000, LB8000, LB16000, LB32000, LB64000, LB128000 };
            for (int i = 0; i < Config.Backs.Length; i++)
            {
                Config.Backs[i] = new BackgroundWorker
                {
                    WorkerSupportsCancellation = false,
                    WorkerReportsProgress = false
                };
            }
            BtnGenerate_Click(null, null);
            CB1.SelectedIndex = 0;

            for (int i = 0; i < Config.Methods.Length; i++)
            {
                Method m = new Method()
                {
                    LeftTime = new TimeSpan[Config.Range.Length],
                    UsedMemory = new long[Config.Range.Length]
                };
                Config.Results.Add(Config.Methods[i], m);
            }

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Process p = Process.GetCurrentProcess();
            long mem = p.WorkingSet64;
            string s = "Используется памяти: " + (mem / Math.Pow(2, 20)).ToString() + " МБ\n";
            s += "Процессорное время: " + p.UserProcessorTime.ToString();
            LUsedResources.Content = s;
        }


        private void MISortMethod_Click(object sender, RoutedEventArgs e)
        {
            MenuItem i = sender as MenuItem;

            foreach (MenuItem item in MISortMethod.Items)
            {
                if (item == i)
                {
                    item.IsChecked = true;
                    Config.CurrentSortMethod = Convert.ToInt16(item.Tag);
                }
                else item.IsChecked = false;
            }
        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            foreach (var list in Config.ListBoxes)
                list.Items.Clear();

            for (int i = 0; i < Config.Range.Length; i++)
                Config.Doubles[i] = new Algo().Gen(Config.Range[i]);

            for (int i = 0; i < Config.ListBoxes.Length; i++)
            {
                for (int j = 0; j < Config.Doubles[i].Length; j++)
                    Config.ListBoxes[i].Items.Add(Config.Doubles[i][j]);                
            }
        }

        private void BtnSort_Click(object sender, RoutedEventArgs e)
        {
            PBProgress.Value = 0;
            BtnGenerate.IsEnabled = false;
            BtnSort.IsEnabled = false;
            MMenu1.IsEnabled = false;
            foreach (TabItem item in TCControl1.Items)
            {
                if (TCControl1.SelectedItem != item)
                    item.IsEnabled = false;
            }

            for (short i = 0; i < Config.Backs.Length; i++)
            {
                Background b = new Background(Config.Backs[i], i, ref Config.Doubles[i], Config.CurrentSortMethod);
                b.CompleteSort += B_CompleteSort;
                b.Start();
            }
        }

        private void B_CompleteSort(Background B)
        {
            B.CompleteSort -= B_CompleteSort;
            Config.Labels[B.NumBack].Content = B.LeftTime;
            PBProgress.Value++;

            Config.ListBoxes[B.NumBack].Items.Clear();
            for (int j = 0; j < Config.Doubles[B.NumBack].Length; j++)
                Config.ListBoxes[B.NumBack].Items.Add(Config.Doubles[B.NumBack][j]);

            Config.Results[Config.Methods[B.NumAlgo]].LeftTime[B.NumBack] = B.LeftTime;
            //Config.Results[Config.Methods[B.NumAlgo]].UsedMemory[B.NumBack] = B.UsedMemory;

            foreach (var back in Config.Backs)
                if (back.IsBusy) return;

            BtnGenerate.IsEnabled = true;
            BtnSort.IsEnabled = true;
            MMenu1.IsEnabled = true;
            foreach (TabItem item in TCControl1.Items)
                item.IsEnabled = true;
        }



        private void BtnRequest_Click(object sender, RoutedEventArgs e)
        {
            if (CB1.SelectedIndex == -1 || CB2.SelectedIndex == -1 || CB3.SelectedIndex == -1)
            { MessageBox.Show("Установите значения", "Ошибка"); return; }

            if(Config.Results.Count == 0)
            { MessageBox.Show("Проведите вычисления.\nКоллекция пуста.", "Ошибка"); return; }

            ((LineSeries)DVCHistogram.Series[0]).ItemsSource = null;
            ((AreaSeries)DVCHistogram.Series[1]).ItemsSource = null;
            ((ColumnSeries)DVCHistogram.Series[2]).ItemsSource = null;
            ((PieSeries)DVCHistogram.Series[3]).ItemsSource = null;
            ((LabeledPieSeries)DVCHistogram.Series[4]).ItemsSource = null;

            switch (CB1.SelectedIndex)
            {
                case 0://метод
                    switch (CB3.SelectedIndex)
                    {
                        case 0://время
                            Method m = Config.Results[Config.Methods[CB2.SelectedIndex]];
                            List<KeyValuePair<string, double>> l = new List<KeyValuePair<string, double>>(Config.Range.Length);
                            for (int i = 0; i < Config.Range.Length; i++)
                                l.Add(new KeyValuePair<string, double>(Config.Range[i].ToString(), m.LeftTime[i].TotalSeconds));
                            
                            switch (CBSeries.SelectedIndex)
                            {
                                case 0:
                                    ((LineSeries)DVCHistogram.Series[0]).ItemsSource = l;
                                    break;
                                case 1:
                                    ((AreaSeries)DVCHistogram.Series[1]).ItemsSource = l;
                                    break;
                                case 2:
                                    ((ColumnSeries)DVCHistogram.Series[2]).ItemsSource = l;
                                    break;
                                case 3:
                                    ((PieSeries)DVCHistogram.Series[3]).ItemsSource = l;
                                    break;
                                case 4:
                                    ((LabeledPieSeries)DVCHistogram.Series[4]).ItemsSource = l;
                                    break;
                            }                            
                            break;
                        case 1://память

                            break;
                    }
                    break;

                case 1://размер
                    switch (CB3.SelectedIndex)
                    {
                        case 0://время
                            List<KeyValuePair<string, double>> l = new List<KeyValuePair<string, double>>(Config.Methods.Length);
                            for (int i = 0; i < Config.Methods.Length; i++)
                                l.Add(new KeyValuePair<string, double>(Config.Methods[i].ToString(),
                                    Config.Results[Config.Methods[i].ToString()].LeftTime[CB2.SelectedIndex].TotalSeconds));
                            switch (CBSeries.SelectedIndex)
                            {
                                case 0:
                                    ((LineSeries)DVCHistogram.Series[0]).ItemsSource = l;
                                    break;
                                case 1:
                                    ((AreaSeries)DVCHistogram.Series[1]).ItemsSource = l;
                                    break;
                                case 2:
                                    ((ColumnSeries)DVCHistogram.Series[2]).ItemsSource = l;
                                    break;
                                case 3:
                                    ((PieSeries)DVCHistogram.Series[3]).ItemsSource = l;
                                    break;
                                case 4:
                                    ((LabeledPieSeries)DVCHistogram.Series[4]).ItemsSource = l;
                                    break;
                            }
                            break;
                        case 1://память

                            break;
                    }
                    break;
            }


        }

        private void CB1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CB1.SelectedIndex)
            {
                case 0:
                    CB2.Items.Clear();
                    for (int i = 0; i < 5; i++)
                        CB2.Items.Add(Config.Methods[i]);
                    CB2.SelectedIndex = 0;
                    break;
                case 1:
                    CB2.Items.Clear();
                    for (int i = 0; i < 8; i++)
                        CB2.Items.Add(Config.Range[i]);
                    CB2.SelectedIndex = 0;

                    break;
                default:
                    MessageBox.Show("Вызвано 'CB1_SelectionChanged'", "Изменил в xaml - измени и в cs");
                    break;
            }
        }
    }








   

}

