using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Responsive.Samples.Pages;

namespace Xamarin.Responsive.Samples.ViewModels
{
    internal class SamplesViewModel : INotifyPropertyChanged
    {
        public SamplesViewModel()
        {
            Samples = new Sample[]
            {
                new Sample<SimplePage>("Simple"),
                new Sample<MixPhoneTabletAndDesktop>("Mixed"),
                new Sample<FormPage>("Simple Form"),
                new Sample<OffsetPage>("Offsets"),
                new Sample<VisibilityPage>("Visibility"),
                new Sample<OnViewSizePage>("Set Properties on View Size")
            };

            PropertyChanged += SamplesViewModel_PropertyChanged;
        }

        public string Title => "Samples";

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<Sample> Samples { get; }

        public Sample CurrentSample { get; set; }

        private void SamplesViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(CurrentSample):
                    CurrentSample?.Open();
                    break;
            }
        }
    }

    internal abstract class Sample
    {
        public Sample(string title, Type page)
        {
            Title = title;
            Page = page;
        }

        public string Title { get; }
        public Type Page { get; }

        public void Open()
        {
            var mainPage = (MasterDetailPage)App.Current.MainPage;

            mainPage.Detail.Navigation.PushAsync((Page)Activator.CreateInstance(Page));
        }
    }

    internal class Sample<TPage> : Sample where TPage : Page
    {
        public Sample(string title) : base(title, typeof(TPage)) { }
    }
}
