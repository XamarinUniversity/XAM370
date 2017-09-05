using Xamarin.Forms;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System.Diagnostics;

namespace NetworkState
{
    public class ProblemPage : ContentPage
    {
        public ProblemPage ()
        {
            Title = "Problem Page";
            BackgroundColor = CrossConnectivity.Current.IsConnected ? Color.Green : Color.Red;

            Debug.WriteLine ("Created ProblemPage: {0:X}", GetHashCode ());
        }

        protected override void OnAppearing ()
        {
            base.OnAppearing ();
            CrossConnectivity.Current.ConnectivityChanged += OnConnectionChanged;
        }

        protected override void OnDisappearing ()
        {
            base.OnDisappearing ();
            CrossConnectivity.Current.ConnectivityChanged -= OnConnectionChanged;
        }

        #if DEBUG
        ~ProblemPage()
        {
            Debug.WriteLine ("Finalizing ProblemPage: {0:X}", GetHashCode ());
        }
        #endif

        void OnConnectionChanged (object sender, ConnectivityChangedEventArgs e)
        {
            BackgroundColor = e.IsConnected ? Color.Green : Color.Red;
        }

        #region Buffers
        byte [] buffer1 = new byte [7000];
        byte [] buffer2 = new byte [7000];
        byte [] buffer3 = new byte [7000];
        #endregion
    }
}


