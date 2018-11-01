﻿using System;
using System.Collections.Generic;
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
using ViewModels;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for MakeQuizView.xaml
    /// </summary>
    public partial class MakeQuizView : UserControl
    {
        public MakeQuizView()
        {
            InitializeComponent();
        }

        private void btn_makeNewQuiz_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new MakeNewQuizViewModel();
        }
    }
}
