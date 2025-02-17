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
using System.Windows.Shapes;

namespace AlienBlast
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public bool HasSave;
        public int Level = -1;
        public List<int> Collected = new List<int>();
        public int Deaths;
        public StartWindow()
        {
            InitializeComponent();

            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "kezdő.jpg");
            this.Background = new ImageBrush
            (
                new BitmapImage(new Uri(path))
            );

            HasSave = LoadData();
            if (!HasSave)
            {
                btn_continue.Foreground = Brushes.DarkCyan;
                btn_continue.Background = Brushes.Black;
                btn_continue.BorderBrush = Brushes.DarkCyan;
            }
            else
            {
                btn_continue.Click += Folytatas_Click;
            }
        }

        private void KezdesGomb_Click(object sender, RoutedEventArgs e)
        {
            SaveData(0, [], 0);
            MainWindow játékAblak = new MainWindow();
            játékAblak.Collected = new List<int>();
            játékAblak.Deaths = 0;
            játékAblak.Show(); 
            this.Close(); 
        }

        private void Folytatas_Click(object sender, RoutedEventArgs e)
        {
            MainWindow játékAblak = new MainWindow();
            játékAblak.jelenlegiPályaIndex = Level;
            játékAblak.Collected = Collected;
            játékAblak.Deaths = Deaths;
            játékAblak.Show();
            this.Close();
        }
        private void Kilepes_Click(object sender, RoutedEventArgs e)
        {
            SaveData(Level, Collected, Deaths);
            this.Close();
        }

        private bool LoadData()
        {
            var rows = File.ReadAllLines("Save.txt");
            if (rows.Count() == 0)
            {
                return false;
            }
            var datas = rows.Skip(1).First().Trim().Split(";");
            Level = int.Parse(datas[0]);
            Collected = datas[1].ToList().Select(i => i - '0').ToList();
            Deaths = int.Parse(datas[2]);
            return true;
        }
        private void SaveData(int Level, List<int>Collected, int Deaths)
        {
            if (HasSave || Level >= 0)
            {
                File.WriteAllLines("Save.txt", ["level;collected;deaths", $"{Level};{string.Join("", Collected.ToArray())};{Deaths}"]);
            }
        }
    }
}
