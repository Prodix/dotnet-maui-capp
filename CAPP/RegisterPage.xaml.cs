using CommunityToolkit.Maui.Alerts;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
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

    AccountData account = new AccountData() { isCheckOnly = true };
    HttpClient httpClient = new HttpClient();


    public RegisterPage()
	{
        InitializeComponent();
	}

    private async void OnAccountCreating(object sender, EventArgs e)
	{
        if (checkInternet())
        {
            account.username = UsernameEntry.Text;
            account.email = EmailEntry.Text;
            account.password = PasswordEntryOne.Text;
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            var request = await httpClient.PostAsJsonAsync("https://2ahcf.localtonet.com/capp/api/register/", account);
            var respone = JsonConvert.DeserializeObject<ResponseJsonMessage>(await request.Content.ReadAsStringAsync());

            if (!request.IsSuccessStatusCode)
            {
                await Toast.Make(respone.message, CommunityToolkit.Maui.Core.ToastDuration.Short).Show(cancellationTokenSource.Token);
            }
            else
            {
		        await Navigation.PushAsync(new HeightPage(account.username, account.email, account.password));
            }
        }
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

    bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    private void EmailChanged(object sender, EventArgs e)
    {
        Entry emailElement = (Entry)sender;
        if (IsValidEmail(emailElement.Text))
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
            GoNextButton.IsEnabled = true;
        }
        else
        {
            GoNextButton.IsEnabled = false;
        }
    }

    private void PasswordChangedOne(object sender, EventArgs e)
    {
        Border parentElement = (Border)((Entry)sender).Parent;
        Entry senderElement = (Entry)sender;
        IsPasswordsValid = false;

        if (senderElement.Text.Length < 8)
        {
            parentElement.StrokeThickness = 1;
            LabelOne.Text = "Пароль слишком короткий";
        }
        else
        {
            if (!Regex.Match(senderElement.Text, @"[0-9]").Success)
            {
                parentElement.StrokeThickness = 1;
                LabelOne.Text = "Пароль не содержит цифры";
            }
            else if (!Regex.Match(senderElement.Text, @"[%$&^#@*!.,/;:]").Success)
            {
                parentElement.StrokeThickness = 1;
                LabelOne.Text = "Пароль не содержит символы: %$&^#@*!.,/;:";
            }
            else if (!Regex.Match(senderElement.Text, @"[A-Z]").Success)
            {
                parentElement.StrokeThickness = 1;
                LabelOne.Text = "Пароль не содержит заглавных букв";
            }
            else if (Regex.Match(senderElement.Text, @"[а-яА-Я]").Success)
            {
                parentElement.StrokeThickness = 1;
                LabelOne.Text = "Пароль содержит кириллицу";
            }
            else if (senderElement.Text != PasswordEntryTwo.Text)
            {
                parentElement.StrokeThickness = 1;
                LabelOne.Text = "Пароли не совпадают";
            }
            else
            {
                parentElement.StrokeThickness = 0;
                PasswordTwoBorder.StrokeThickness = 0;
                LabelOne.Text = "";
                LabelTwo.Text = "";
                IsPasswordsValid = true;
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
            parentElement.StrokeThickness = 1;
            LabelTwo.Text = "Пароль слишком короткий";
        }
        else
        {
            if (!Regex.Match(senderElement.Text, @"[0-9]").Success)
            {
                parentElement.StrokeThickness = 1;
                LabelTwo.Text = "Пароль не содержит цифры";
            }
            else if (!Regex.Match(senderElement.Text, @"[%$&^#@*!.,/;:]").Success)
            {
                parentElement.StrokeThickness = 1;
                LabelTwo.Text = "Пароль не содержит символы: %$&^#@*!.,/;:";
            }
            else if (!Regex.Match(senderElement.Text, @"[A-Z]").Success)
            {
                parentElement.StrokeThickness = 1;
                LabelTwo.Text = "Пароль не содержит заглавных букв";
            }
            else if (Regex.Match(senderElement.Text, @"[а-яА-Я]").Success)
            {
                parentElement.StrokeThickness = 1;
                LabelTwo.Text = "Пароль содержит кириллицу";
            }
            else if (senderElement.Text != PasswordEntryOne.Text)
            {
                parentElement.StrokeThickness = 1;
                LabelTwo.Text = "Пароли не совпадают";
            }
            else
            {
                parentElement.StrokeThickness = 0;
                PasswordOneBorder.StrokeThickness = 0;
                LabelOne.Text = "";
                LabelTwo.Text = "";
                IsPasswordsValid = true;
            }
        }
        CheckChanged(InputCheckBox, new EventArgs());
    }

    private bool checkInternet()
    {
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        if (accessType == NetworkAccess.Internet)
        {
            return true;
        }
        else
        {
            Toast.Make("Пожалуйста включите интернет", CommunityToolkit.Maui.Core.ToastDuration.Short).Show(); 
            return false;
        }
    }
}

