using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace VisualHostWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyAsyncProgBar.Child = CreateMediaElementOnWorkerThread();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MyAsyncProgBar.Child = CreateMediaElementOnWorkerThread();
            //Application.Current
            //    .Dispatcher
            //    .BeginInvoke(
            //        new Action(
            //            () =>
            //            {
            //                MyAction();
            //            }
            //            )
            //        );
            MyAction();
        }

        private void MyAction()
        {
            int i = 0;
            while (i < Int32.MaxValue) { i++; }
        }

        private HostVisual CreateMediaElementOnWorkerThread()
        {
            HostVisual hostVisual = new HostVisual();
            Thread thread = new Thread(new ParameterizedThreadStart(MediaWorkerThread));
            thread.ApartmentState = ApartmentState.STA;
            thread.IsBackground = true;
            thread.Start(hostVisual);
            s_event.WaitOne();

            return hostVisual;
        }

        private FrameworkElement CreateMovingElement()
        {
            var pb = new ProgressBar();
            pb.BeginInit();
            pb.IsIndeterminate = true;
            pb.Width = 200;
            pb.Height = 100;
            pb.EndInit();

            return pb;
        }

        private void MediaWorkerThread(object arg)
        {
            HostVisual hostVisual = (HostVisual)arg;
            VisualTargetPresentationSource visualTargetPS = new VisualTargetPresentationSource(hostVisual);
            s_event.Set();
            visualTargetPS.RootVisual = CreateMovingElement();
            System.Windows.Threading.Dispatcher.Run();
            
        }

        private static AutoResetEvent s_event = new AutoResetEvent(false);
    }
}
