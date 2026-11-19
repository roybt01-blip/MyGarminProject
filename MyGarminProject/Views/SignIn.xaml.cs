using MyGarminProject.Helper;
using MyGarminProject.ViewModels;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace MyGarminProject.Views;

public partial class SignIn : ContentPage
{
	public SignIn()
	{
		InitializeComponent();
		BindingContext = new SignInViewModel();
    }
}