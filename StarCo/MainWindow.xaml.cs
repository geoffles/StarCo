using StarCo.Controllers;
using StarCo.Domain;
using StarCo.Domain.Factories;
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
        private TimeSpan timePerFrame = TimeSpan.FromSeconds(1);
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
            colony.Storage.AddContainer(StorageContainer.Small());
            colony.Storage.AddContainer(StorageContainer.Small());
            colony.Storage.AddContainer(StorageContainer.Medium());

            colony.AddWorker(new BasicWorker(colony));

            colony.Improvements.Add(ObjectFactory.ImprovementFactory().BuildImprovement("basicmine"));

            ViewModel = new MainWindowViewModel(new ColonyController(colony));

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(OnUpdate);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
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
