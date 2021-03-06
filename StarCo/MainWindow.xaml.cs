﻿using StarCo.Controllers;
using StarCo.Domain;
using StarCo.Domain.Factories;
using StarCo.Domain.Improvements;
using StarCo.Domain.Workers;
using StarCo.ViewModels;
using System;
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

namespace StarCo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimeSpan timePerFrame = TimeSpan.FromSeconds(0.3);
        private int currentFrame = 0;

        public MainWindowViewModel ViewModel
        {
            get { return DataContext as MainWindowViewModel; }
            set { DataContext = value; }
        }


        public MainWindow()
        {
            InitializeComponent();

            var colony = new Colony();
            StorageContainer.Small().Link(colony);
            StorageContainer.Small().Link(colony);
            StorageContainer.Medium().Link(colony);
            new Habitat().Link(colony);


            //new BasicWorker(colony).Link(colony);
            var abstractWorker = new AbstractWorker(new Domain.State.AbstractWorkerState
            {
                ResourceKey = "basicworker",
                ProductionOptions = ObjectFactory.ProductionLookup().GetProductionOptionsFor("basicworker").ToList(),
                Sprite = ObjectFactory.AssetName(ObjectFactory.ProductionLookup().GetGlyphKeyFor("basicworker")),
                SubCategoryKey = "basicworker"
            }, 10);

            abstractWorker.Link(colony);

            var mine = ObjectFactory.ImprovementFactory().BuildImprovement("basicmine");
            mine.Link(colony);

            colony.GetInventory("stone").Add(50);

            //colony = new Persister().Load("Save.xml");

            ViewModel = new MainWindowViewModel(new ColonyController(colony));

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer(System.Windows.Threading.DispatcherPriority.Send);
            dispatcherTimer.Tick += new EventHandler(OnUpdate);
            dispatcherTimer.Interval = timePerFrame;
            dispatcherTimer.Start();
        }

        private void OnUpdate(object sender, object e)
        {
            currentFrame = (currentFrame + 32) % 64;

            if (ViewModel.ColonyItems == null)
            {
                return;
            }

            foreach (var item in ViewModel.ColonyItems)
            {
                item.SpriteXOffset = -currentFrame;
            }
        }
    }
}
