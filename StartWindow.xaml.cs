﻿using System;
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
        public StartWindow()
        {
            InitializeComponent();





            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "kezdő.jpg");
            this.Background = new ImageBrush
            (
                new BitmapImage(new Uri(path))
            );

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
            játékAblak.jelenlegiPályaIndex = 1;
            játékAblak.Show();
            this.Close();
        }
        private void Kilepes_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
