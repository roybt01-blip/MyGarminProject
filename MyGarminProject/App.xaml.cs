using MyGarminProject.Views;

namespace MyGarminProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new SignIn());
        }
    }
}