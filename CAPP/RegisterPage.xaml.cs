using System.Text.RegularExpressions;

namespace CAPP;

public partial class RegisterPage : ContentPage
{
    private bool IsWarningShownOne = false;
    private bool IsWarningShownTwo = false;
    private bool IsNotEqualWarningShown = false;
    private bool IsPasswordsValid = false;
    private bool IsUsernameValid = false;
    private bool IsEmailValid = false;

    public RegisterPage()
	{
        InitializeComponent();
	}

    private async void OnAccountCreating(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new HeightPage(), false);
	}

    private void UsernameChanged(object sender, EventArgs e)
    {
        if (UsernameEntry.Text.Length < 5)
        {
            UsernameLabel.Text = "Имя пользователя менее 5 символов";
            ((Border)UsernameEntry.Parent).StrokeThickness = 1;
            IsUsernameValid = false;
        }
        else
        {
            UsernameLabel.Text = "";
            ((Border)UsernameEntry.Parent).StrokeThickness = 0;
            IsUsernameValid = true;
        }
        CheckChanged(InputCheckBox, new EventArgs());
    }

    private void EmailChanged(object sender, EventArgs e)
    {
        Entry emailElement = (Entry)sender;
        if (Regex.IsMatch(emailElement.Text, @"[a-z]{1,}@[a-z]{1,}\.[a-z]{1,}"))
        {
            EmailLabel.Text = "";
            ((Border)emailElement.Parent).StrokeThickness = 0;
            IsEmailValid = true;
        }
        else
        {
            EmailLabel.Text = "Неверный email";
            ((Border)emailElement.Parent).StrokeThickness = 1;
            IsEmailValid = false;
        }
        CheckChanged(InputCheckBox, new EventArgs());
    }

    private void CheckChanged(object sender, EventArgs e)
    {
        if (((InputKit.Shared.Controls.CheckBox)sender).IsChecked && IsPasswordsValid && IsEmailValid && IsUsernameValid)
        {
            //
            // TODO: Изменить метод перехода на IsEnabled
            //
            GoNextButton.Background = new SolidColorBrush(Color.FromRgb(248, 61, 127));
            GoNextButton.Clicked += OnAccountCreating;
        }
        else
        {
            GoNextButton.Background = new SolidColorBrush(Color.FromRgb(116, 116, 116));
            try
            {
                GoNextButton.Clicked -= OnAccountCreating;
            }
            catch { }
        }
    }

	private void PasswordChangedOne(object sender, EventArgs e)
	{
		Border parentElement = (Border)((Entry)sender).Parent;
		Entry senderElement = (Entry)sender;
        IsPasswordsValid = false;

        if (senderElement.Text.Length < 8)
		{
            if (IsNotEqualWarningShown)
            {
                LabelOne.Text = "";
                LabelTwo.Text = "";
                ((Border)PasswordEntryTwo.Parent).StrokeThickness = 0;
                IsNotEqualWarningShown = false;
            }
            if (!IsWarningShownOne)
			{
                LabelOne.Text = "Пароль менее 8 символов";
                IsWarningShownOne = true;
			    parentElement.StrokeThickness = 1;
			}
		}
        else
        {
            if (IsWarningShownOne)
            {
                parentElement.StrokeThickness = 0;
                LabelOne.Text = "";
                IsWarningShownOne = false;
                if (!String.IsNullOrEmpty(PasswordEntryTwo.Text) && PasswordEntryTwo.Text.Length >= 8 && !IsNotEqualWarningShown && PasswordEntryOne.Text != PasswordEntryTwo.Text)
                {
                    LabelOne.Text = "Пароли не совпадают";
                    LabelTwo.Text = "Пароли не совпадают";
                    parentElement.StrokeThickness = 1;
                    ((Border)PasswordEntryTwo.Parent).StrokeThickness = 1;
                    IsNotEqualWarningShown = true;
                }
                else if (PasswordEntryOne.Text == PasswordEntryTwo.Text)
                {
                    IsPasswordsValid = true;
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(PasswordEntryTwo.Text) && PasswordEntryTwo.Text.Length >= 8 && !IsNotEqualWarningShown && PasswordEntryOne.Text != PasswordEntryTwo.Text)
                {
                    LabelOne.Text = "Пароли не совпадают";
                    LabelTwo.Text = "Пароли не совпадают";
                    parentElement.StrokeThickness = 1;
                    ((Border)PasswordEntryTwo.Parent).StrokeThickness = 1;
                    IsNotEqualWarningShown = true;
                }
                else if (PasswordEntryOne.Text == PasswordEntryTwo.Text)
                {
                    LabelOne.Text = "";
                    LabelTwo.Text = "";
                    ((Border)PasswordEntryTwo.Parent).StrokeThickness = 0;
                    parentElement.StrokeThickness = 0;
                    IsNotEqualWarningShown = false;
                    IsPasswordsValid = true;
                    CheckChanged(InputCheckBox, new EventArgs());
                }
            }
        }
        CheckChanged(InputCheckBox, new EventArgs());
    }

    private void PasswordChangedTwo(object sender, EventArgs e)
    {
        Border parentElement = (Border)((Entry)sender).Parent;
        Entry senderElement = (Entry)sender;
        IsPasswordsValid = false;

        if (senderElement.Text.Length < 8)
        {
            if (IsNotEqualWarningShown)
            {
                LabelOne.Text = "";
                LabelTwo.Text = "";
                ((Border)PasswordEntryOne.Parent).StrokeThickness = 0;
                parentElement.StrokeThickness = 0;
                IsNotEqualWarningShown = false;
            }
            if (!IsWarningShownTwo)
            {
                LabelTwo.Text = "Пароль менее 8 символов";
                IsWarningShownTwo = true;
                parentElement.StrokeThickness = 1;
            }
        }
        else
        {
            if (IsWarningShownTwo)
            {
                parentElement.StrokeThickness = 0;
                LabelTwo.Text = "";
                IsWarningShownTwo = false;
                if (!String.IsNullOrEmpty(PasswordEntryOne.Text) && PasswordEntryOne.Text.Length >= 8 && !IsNotEqualWarningShown && PasswordEntryOne.Text != PasswordEntryTwo.Text)
                {
                    LabelOne.Text = "Пароли не совпадают";
                    LabelTwo.Text = "Пароли не совпадают";
                    parentElement.StrokeThickness = 1;
                    ((Border)PasswordEntryOne.Parent).StrokeThickness = 1;
                    IsNotEqualWarningShown = true;
                }
                else if (PasswordEntryOne.Text == PasswordEntryTwo.Text)
                {
                    IsPasswordsValid = true;
                    CheckChanged(InputCheckBox, new EventArgs());
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(PasswordEntryOne.Text) && PasswordEntryOne.Text.Length >= 8 && !IsNotEqualWarningShown && PasswordEntryOne.Text != PasswordEntryTwo.Text)
                {
                    LabelOne.Text = "Пароли не совпадают";
                    LabelTwo.Text = "Пароли не совпадают";
                    parentElement.StrokeThickness = 1;
                    ((Border)PasswordEntryOne.Parent).StrokeThickness = 1;
                    IsNotEqualWarningShown = true;
                }
                else if (PasswordEntryOne.Text == PasswordEntryTwo.Text)
                {
                    LabelOne.Text = "";
                    LabelTwo.Text = "";
                    ((Border)PasswordEntryOne.Parent).StrokeThickness = 0;
                    parentElement.StrokeThickness = 0;
                    IsNotEqualWarningShown = false;
                    IsPasswordsValid = true;
                    CheckChanged(InputCheckBox, new EventArgs());
                }
            }
        }
        CheckChanged(InputCheckBox, new EventArgs());
    }
}

