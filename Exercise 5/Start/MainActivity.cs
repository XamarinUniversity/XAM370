using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Newtonsoft.Json;
using Android.Content;

namespace EmailClient.Droid
{
    [Activity (Label = "Email Client", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : ListActivity
    {
        EmailServer emailServer;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            emailServer = new EmailServer(1000);
            GC.Collect (); // Get rid of temp email data.

            this.ListAdapter = new ArrayAdapter<EmailItem> (this, 
                Android.Resource.Layout.SimpleListItem1, emailServer.Email);
        }

        protected override void OnListItemClick(ListView l, Android.Views.View v, int position, long id)
        {
            Intent intent = new Intent(this, typeof(DetailActivity));
            intent.PutExtra("emailItem", JsonConvert.SerializeObject(emailServer.Email[position]));
            StartActivity(intent);
        }

        public override bool OnPrepareOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.gc:
                    GC.Collect();
                    return true;
                default:
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

    }
}


