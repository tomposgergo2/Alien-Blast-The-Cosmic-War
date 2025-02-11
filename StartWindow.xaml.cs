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
        public int Level;
        public int Money;
        public StartWindow()
        {
            InitializeComponent();

            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "kezdő.jpg");
            this.Background = new ImageBrush
            (
                new BitmapImage(new Uri(path))
            );

            LoadData();
        }

        private void KezdesGomb_Click(object sender, RoutedEventArgs e)
        {
            MainWindow játékAblak = new MainWindow();
            játékAblak.Show(); 
            this.Close(); 
        }

        private void Folytatas_Click(object sender, RoutedEventArgs e)
        {
            MainWindow játékAblak = new MainWindow();
            játékAblak.jelenlegiPályaIndex = Level;
            játékAblak.money = Money;
            játékAblak.Show();
            this.Close();
        }
        private void Kilepes_Click(object sender, RoutedEventArgs e)
        {
            SaveData(Level, Money);
            this.Close();
        }

        private void LoadData()
        {
            var datas = File.ReadAllLines("Save.txt").Skip(1).First().Trim().Split(";");
            Level = int.Parse(datas[0]);
            Money = int.Parse(datas[1]);
        }
        private void SaveData(int Level, int Money)
        {
            File.WriteAllLines("Save.txt", ["level;money", $"{Level};{Money}"]);
        }
    }
}
