using MyGarminProject.ViewModels;

namespace MyGarminProject.Views;

public partial class SignUp : ContentPage
{
	public SignUp()
	{
		InitializeComponent();
		BindingContext = new SignUpViewModel();
	}
}