using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Threading;
using Newtonsoft.Json;

namespace EmailClient.Droid
{
    [Activity(Label = "Email details")]            
    public class DetailActivity : Activity
    {
        EmailItem detail;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.EmailDetail);

            var item = Intent.GetStringExtra("emailItem");
            if (!string.IsNullOrEmpty(item))
            {
                detail = JsonConvert.DeserializeObject<EmailItem>(item);

                FindViewById<TextView>(Resource.Id.date).Text = detail.Date.ToLongDateString();
                FindViewById<TextView>(Resource.Id.from).Text = detail.From;
                FindViewById<TextView>(Resource.Id.to).Text = detail.To;
                FindViewById<TextView>(Resource.Id.subject).Text = detail.Subject;
                FindViewById<TextView>(Resource.Id.body).Text = detail.Body;
            }
        }
    }
}

