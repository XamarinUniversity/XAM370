using System;
using System.Diagnostics;
using UIKit;
using System.Threading;

namespace PeerPromotion
{
    public partial class ViewController : UIViewController
    {
        static int Counter;

        protected ViewController (IntPtr handle) : base (handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad ()
        {
            Interlocked.Increment (ref Counter);
            Debug.WriteLine ("ViewController created, {0} instances in memory.", Counter);

            base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.
            TheSlider.ValueChanged += (s, e) => {
                ValueLabel.Text = Math.Round (TheSlider.Value).ToString();
            };
        }

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);
            Debug.WriteLine ("ViewController disposed, {0} instances left.", 
                             Interlocked.Decrement(ref Counter));
        }
    }
}

