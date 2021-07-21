using BooksDB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BooksDB.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssistantPage : ContentPage
    {
        AssistantViewModel _viewModel;
        public AssistantPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AssistantViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}