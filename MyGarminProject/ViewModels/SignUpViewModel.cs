using MyGarminProject.Helper;
using MyGarminProject.Service;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace MyGarminProject.ViewModels
{
    public partial class SignUpViewModel : ViewModelBase
    {
        private readonly DBMokup _db = new();
        private bool _isPasswordHidden = true;
        private string _passwordIconCode = FontHelper.CLOSED_EYE_ICON;
        private string? _signUpFirstName;
        private string? _signUpLastName;
        private string? _signUpEmail;
        private string? _phoneNumber;
        private string? _signUpPassword;
        private string _loginMessage = string.Empty;
        private bool _hasError;
        private bool _isSignUpButtonEnabled;
        private bool _signUpMessageVisible;
        private Color _signUpMessageColor = Colors.Transparent;

        public ICommand SignUpCommand { get; }
        public ICommand TogglePassWordVisibilityCommand { get; }

        public SignUpViewModel()
        {
            TogglePassWordVisibilityCommand = new Command(TogglePasswordVisibility);
            SignUpCommand = new Command(OnSignUpButtonClicked);
        }

        public string? SignUpFirstName
        {
            get => _signUpFirstName;
            set
            {
                if (_signUpFirstName != value)
                {
                    _signUpFirstName = value;
                    OnPropertyChanged();
                    ValidateForm();
                }
            }
        }

        public string? SignUpLastName
        {
            get => _signUpLastName;
            set
            {
                if (_signUpLastName != value)
                {
                    _signUpLastName = value;
                    OnPropertyChanged();
                    ValidateForm();
                }
            }
        }

        public string? SignUpEmail
        {
            get => _signUpEmail;
            set
            {
                if (_signUpEmail != value)
                {
                    _signUpEmail = value;
                    OnPropertyChanged();
                    ValidateForm();
                }
            }
        }

        public string? PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                    ValidateForm();
                }
            }
        }

        public string? SignUpPassword
        {
            get => _signUpPassword;
            set
            {
                if (_signUpPassword != value)
                {
                    _signUpPassword = value;
                    OnPropertyChanged();
                    ValidateForm();
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

        public bool HasError
        {
            get => _hasError;
            set
            {
                if (_hasError != value)
                {
                    _hasError = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsSignUpButtonEnabled
        {
            get => _isSignUpButtonEnabled;
            set
            {
                if (_isSignUpButtonEnabled != value)
                {
                    _isSignUpButtonEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool SignUpMessageVisible
        {
            get => _signUpMessageVisible;
            set
            {
                if (_signUpMessageVisible != value)
                {
                    _signUpMessageVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color SignUpMessageColor
        {
            get => _signUpMessageColor;
            set
            {
                if (_signUpMessageColor != value)
                {
                    _signUpMessageColor = value;
                    OnPropertyChanged();
                }
            }
        }

        private void TogglePasswordVisibility()
        {
            IsPasswordHidden = !IsPasswordHidden;
        }

        private bool ValidateForm()
        {
            string? trimmedEmail = SignUpEmail?.Trim();
            string? trimmedPhone = PhoneNumber?.Trim();

            IsSignUpButtonEnabled =
                !string.IsNullOrWhiteSpace(SignUpFirstName) &&
                !string.IsNullOrWhiteSpace(SignUpLastName) &&
                !string.IsNullOrWhiteSpace(trimmedEmail) &&
                Regex.IsMatch(trimmedEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") &&
                !string.IsNullOrWhiteSpace(trimmedPhone) &&
                Regex.IsMatch(trimmedPhone, @"^[0-9]{7,15}$") &&
                !string.IsNullOrEmpty(SignUpPassword) &&
                SignUpPassword.Length >= 6;

            return IsSignUpButtonEnabled;
        }

        private void OnSignUpButtonClicked()
        {
            if (!ValidateForm())
            {
                SignUpMessageVisible = true;
                HasError = true;
                LoginMessage = AppMessages.signUpFailed;
                SignUpMessageColor = Colors.Red;
                return;
            }

            string email = SignUpEmail!.Trim();
            string phone = PhoneNumber!.Trim();

            // Uncomment once DBMokup methods are wired up:
            // if (_db.IsEmailExists(email))
            // {
            //     SignUpMessageVisible = true;
            //     HasError = true;
            //     LoginMessage = "Email already registered.";
            //     SignUpMessageColor = Colors.Red;
            //     return;
            // }
            //
            // bool created = _db.CreateUser(SignUpFirstName!.Trim(), SignUpLastName!.Trim(), email, phone, SignUpPassword!);
            // if (!created)
            // {
            //     SignUpMessageVisible = true;
            //     HasError = true;
            //     LoginMessage = AppMessages.signUpFailed;
            //     SignUpMessageColor = Colors.Red;
            //     return;
            // }

            SignUpMessageVisible = true;
            HasError = false;
            LoginMessage = AppMessages.signUpSuccess;
            SignUpMessageColor = Colors.Green;
        }
    }
}