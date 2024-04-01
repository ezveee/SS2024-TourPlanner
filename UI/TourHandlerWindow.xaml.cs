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
using System.Windows.Shapes;
using UI.Models;
using UI.ViewModel;

namespace UI
{
    /// <summary>
    /// Interaction logic for AddTourWindow.xaml
    /// </summary>
    public partial class TourHandlerWindow : Window
    {
        public TourHandlerWindow()
        {
            InitializeComponent();
            DataContext = new TourHandlerViewModel(this);
        }

		public TourHandlerWindow(TourModel tourModel)
		{
			InitializeComponent();
			DataContext = new TourEditorViewModel(this, tourModel);
		}
	}
}
