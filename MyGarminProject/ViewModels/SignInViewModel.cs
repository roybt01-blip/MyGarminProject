using MyGarminProject.Helper;
using MyGarminProject.Service;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace MyGarminProject.ViewModels
{
    public partial class SignInViewModel : ViewModelBase
    {
        private readonly DBMokup _db = new();

        private bool _isPasswordHidden = true;
        private string _passwordIconCode = FontHelper.CLOSED_EYE_ICON;
        private string? _signInPassword;
        private string? _signInEmail;
        private string? _signInUsername;
        private string? _signInErrorMessage;
        private string _loginMessage = string.Empty;
        private bool _hasError;
        private bool _isSignInButtonEnabled;
        private bool _signInMessageVisible;
        private Color _signInMessageColor = Colors.Transparent;

        public ICommand SignInCommand { get; }

        public SignInViewModel()
        {
            TogglePassWordVisibilityCommand = new Command(TogglePasswordVisibility);
            SignInCommand = new Command(OnSignInButtonClicked);
        }

        public ICommand TogglePassWordVisibilityCommand { get; }

        public Color SignInMessageColor
        {
            get => _signInMessageColor;
            set
            {
                if (value != _signInMessageColor)
                {
                    _signInMessageColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool SignInMessageVisible
        {
            get => _signInMessageVisible;
            set
            {
                if (_signInMessageVisible != value)
                {
                    _signInMessageVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LoginMessage
        {
            get => _loginMessage;
            set
            {
                if (_loginMessage != value)
                {
                    _loginMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsPasswordHidden
        {
            get => _isPasswordHidden;
            set
            {
                if (_isPasswordHidden != value)
                {
                    _isPasswordHidden = value;
                    OnPropertyChanged(nameof(IsPasswordHidden));

                    // When hidden (true) -> closed eye; when visible (false) -> open eye
                    PassWordIconCode = _isPasswordHidden
                        ? FontHelper.CLOSED_EYE_ICON
                        : FontHelper.OPEN_EYE_ICON;
                }
            }
        }

        public string PassWordIconCode
        {
            get => _passwordIconCode;
            set
            {
                if (_passwordIconCode != value)
                {
                    _passwordIconCode = value;
                    OnPropertyChanged(nameof(PassWordIconCode));
                }
            }
        }

        public string? SignInPassword
        {
            get => _signInPassword;
            set
            {
                if (_signInPassword != value)
                {
                    _signInPassword = value;
                    OnPropertyChanged(nameof(SignInPassword));
                    ValidateForm();
                }
            }
        }

        public string? SignInUsername
        {
            get => _signInUsername;
            set
            {
                if (_signInUsername != value)
                {
                    _signInUsername = value;
                    OnPropertyChanged(nameof(SignInUsername));
                    OnPropertyChanged(nameof(IsSignInButtonEnabled));
                    ValidateForm();
                }
            }
        }

        public string? SignInEmail
        {
            get => _signInEmail;
            set
            {
                if (_signInEmail != value)
                {
                    _signInEmail = value;
                    OnPropertyChanged(nameof(SignInEmail));
                    ValidateForm();
                }
            }
        }

        public string? SignInErrorMessage
        {
            get => _signInErrorMessage;
            set
            {
                if (_signInErrorMessage != value)
                {
                    _signInErrorMessage = value;
                    OnPropertyChanged(nameof(SignInErrorMessage));
                    HasError = !string.IsNullOrEmpty(_signInErrorMessage);
                }
            }
        }

        public bool HasError
        {
            get => _hasError;
            set
            {
                if (_hasError != value)
                {
                    _hasError = value;
                    OnPropertyChanged(nameof(HasError));
                }
            }
        }

        public bool IsSignInButtonEnabled
        {
            get => _isSignInButtonEnabled;
            set
            {
                if (_isSignInButtonEnabled != value)
                {
                    _isSignInButtonEnabled = value;
                    OnPropertyChanged(nameof(IsSignInButtonEnabled));
                }
            }
        }

        private void TogglePasswordVisibility()
        {
            IsPasswordHidden = !IsPasswordHidden;
        }

        private void ValidateForm()
        {
            //bool isUsernameValid = !string.IsNullOrWhiteSpace(SignInUsername);
            bool isEmailValid = !string.IsNullOrWhiteSpace(SignInEmail) &&
                                Regex.IsMatch(SignInEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            bool isPasswordValid = !string.IsNullOrEmpty(SignInPassword);

            IsSignInButtonEnabled = isEmailValid && isPasswordValid; //isUsernameValid
        }

        private void OnSignInButtonClicked()
        {
            SignInMessageVisible = true;

            if (!string.IsNullOrWhiteSpace(SignInEmail) &&
                !string.IsNullOrEmpty(SignInPassword) &&
                _db.IsExist(SignInEmail, SignInPassword))
            {
                LoginMessage = AppMessages.loginSuccess;
                SignInMessageColor = Colors.Green;
                // Navigate to MainPage
            }
            else
            {
                LoginMessage = AppMessages.loginFailed;
                SignInMessageColor = Colors.Red;
            }
        }
    }
}