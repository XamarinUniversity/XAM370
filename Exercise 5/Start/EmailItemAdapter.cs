using System;
using System.Collections.Generic;
using Android.Widget;
using Android.Views;
using Android.Content;

namespace EmailClient.Droid
{
	public class EmailItemAdapter : BaseAdapter<EmailItem>
	{
        readonly IList<EmailItem> data;
		readonly Context context;

        public EmailItemAdapter(Context context, IList<EmailItem> data)
		{
			this.context = context;
            this.data = data;
		}

		public override int Count {
			get {
				return data.Count;
			}
		}

		public override long GetItemId(int position)
		{
            return position;
		}

		public override EmailItem this[int index] {
			get { 
				return data[index];
			}
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;
			if (view == null) {
                view = LayoutInflater.FromContext(context).Inflate(Android.Resource.Layout.SimpleListItem2, null);
                var text = view.FindViewById<TextView>(Android.Resource.Id.Text1);
                var dtext = view.FindViewById<TextView> (Android.Resource.Id.Text2);
                view.Tag = new ViewHolder { TextItem = text, DetailTextItem = dtext };
			}

			var item = data[position];
            ((ViewHolder)view.Tag).TextItem.Text = item.Subject;
            ((ViewHolder)view.Tag).DetailTextItem.Text = item.From;

            return view;
		}

        class ViewHolder : Java.Lang.Object
        {
            public TextView TextItem { get; set; }  
            public TextView DetailTextItem { get; set; }
        }
	}
}

