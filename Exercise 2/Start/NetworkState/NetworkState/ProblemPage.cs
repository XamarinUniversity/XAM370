using Xamarin.Forms;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;

namespace NetworkState
{
    public class ProblemPage : ContentPage
    {
        public ProblemPage ()
        {
            Title = "Problem Page";
            BackgroundColor = CrossConnectivity.Current.IsConnected ? Color.Green : Color.Red;
            CrossConnectivity.Current.ConnectivityChanged += OnConnectionChanged;
        }

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


