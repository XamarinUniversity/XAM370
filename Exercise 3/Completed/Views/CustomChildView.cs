using UIKit;
using System.Diagnostics;
using System.Threading;

namespace UserPeers
{
    public class CustomChildView : UIView
    {
        static int Counter;

        // Final approach - don't hold the managed reference at all.
        public CustomParentView Parent {
            get
            {
                // Instead case the NATIVE version to our managed 
                // representation.. then it's just iOS which keeps this alive.
                return this.Superview as CustomParentView;
            }
        }

        public CustomChildView()
        {
            Debug.WriteLine("CustomChildView created, {0} live instances", 
                Interlocked.Increment(ref Counter));
        }

        public override void TouchesBegan(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            if (Parent != null)
                Parent.BackgroundColor = UIColor.Red;
        }

        public override void TouchesEnded(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            if (Parent != null)
                Parent.BackgroundColor = UIColor.Yellow;
        }

        public override void TouchesCancelled(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
            if (Parent != null)
                Parent.BackgroundColor = UIColor.Yellow;
        }

        protected override void Dispose (bool disposing)
        {
            base.Dispose (disposing);
            Debug.WriteLine("CustomChildView disposed, {0} live instances", 
                Interlocked.Decrement(ref Counter));
        }
    }
}
