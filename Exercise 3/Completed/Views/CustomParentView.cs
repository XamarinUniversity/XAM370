using UIKit;
using System.Diagnostics;
using System.Threading;
using CoreGraphics;

namespace UserPeers
{
    public class CustomParentView : UIView
    {
        static int Counter;

        public CustomParentView()
        {
            Debug.WriteLine("CustomParentView created, {0} live instances", 
                Interlocked.Increment(ref Counter));

            var childView = new CustomChildView {
                BackgroundColor = UIColor.Blue,
                //Parent = this
            };

            Add(childView);
        }

        public override void LayoutSubviews()
        {
            CustomChildView childView = Subviews[0] as CustomChildView;
            if (childView != null) {
                childView.Frame = this.Bounds.Inset(50, 50);
            }

            base.LayoutSubviews();
        }

        // Approach #2 to fix the problem
        //public override void RemoveFromSuperview ()
        //{
        //    base.RemoveFromSuperview ();
        //    CustomChildView childView = Subviews [0] as CustomChildView;
        //    if (childView != null) {
        //        childView.Parent = null;
        //    }
        //}

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);
            Debug.WriteLine("CustomParentView disposed, {0} live instances", 
                Interlocked.Decrement(ref Counter));
        }
    }
    
}
