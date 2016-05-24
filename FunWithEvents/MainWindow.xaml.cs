using System;
using System.Windows;

namespace FunWithEvents
{
    public partial class MainWindow : Window
    {
        Counter c = new Counter();

        public MainWindow()
        {
            InitializeComponent();
            forum.Text += "New Counter initialized with count " + c.Count;

            //attach a subscriber to the event delegate
            c.IntervalReached += c_IntervalReached;
        }

        //the rest of the class consists of three event handlers:

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            forum.Text += "\nButton1 raised its click event " + e.RoutedEvent.ToString() + " which was handled by the VS-generated handler.\n";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            c.Increment();
            forum.Text += c.Count + "  ";
        }

        private void c_IntervalReached(object sender, EventArgs e)
        {
            forum.Text += "\nThe Increment method raised a custom event " + e.ToString() + " which was handled by a custom handler.\n";
        }

    }  //end MainWindow class

    public class Counter
    {
        //declare the properties and events of the class
        public int Count { get; set; }
        public event EventHandler IntervalReached;

        public Counter()
        {
            Count = 100;
        }

        public void Increment()
        {
            Count++;
            if (Count % 3 == 0)
            {
                //raise the event through the proxy method
                OnIntervalReached(EventArgs.Empty);
            }
        }

        //this proxy method raises the event indirectly
        //providing protection from a race condition
        //and also an overridable target for derived classes to raise the event
        protected virtual void OnIntervalReached(EventArgs e)
        {
            EventHandler IRCopy = IntervalReached;
            if(IRCopy != null)
            {
                IRCopy(this, e);
            }
        }
    }  //end Counter class

}  //end FunWithEvents namespace
