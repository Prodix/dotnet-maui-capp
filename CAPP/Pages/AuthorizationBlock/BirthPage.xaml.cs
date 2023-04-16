using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace CAPP;

public partial class BirthPage : ContentPage
{
    int mode;
    int heightValue;
    double weightValue;
    string username;
    string email;
    string password;
    double wishWeightValue;
    bool IsWithoutRegister;
    string gender;
    SqliteDatabase database;


	public BirthPage(int mode, int heightValue, double weightValue, string username, string email, string password, string gender, double wishWeightValue = 0.0, bool IsWithoutRegister = false)
	{
		InitializeComponent();
        database = new SqliteDatabase();
        this.IsWithoutRegister = IsWithoutRegister;
        this.mode = mode;
        this.heightValue = heightValue;
        this.weightValue = weightValue;
        this.username = username;
        this.email = email;
        this.password = password;
        this.wishWeightValue = wishWeightValue;
        this.gender = gender;
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

    private async void GoNextButton_Clicked(object sender, EventArgs e)
    {
        
        DateTime dateTime = DateTime.ParseExact($"{Calendar.CurrentDay}/{Calendar.CurrentMonth}/{Calendar.CurrentYear}", "dd/M/yyyy", null);
        Console.WriteLine(dateTime.ToString("yyyy/MM/dd"));
        if ((DateTime.Now - dateTime).Days / 365.0 < 18.01)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            string text = "Вам должно быть больше 18 лет";
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }
        else
        {
            if (IsWithoutRegister)
            {
                UserData data = new UserData();
                data.Username = username;
                data.Password = password;
                data.Email = email;
                data.WeightValue = weightValue;
                data.WishWeightValue = wishWeightValue;
                data.HeightValue = heightValue;
                data.Gender = gender;
                data.IsWithoutRegister = IsWithoutRegister;
                data.Mode = mode;
                data.BirthDate = dateTime.ToString("yyyy/MM/dd");
                await database.SaveItemAsync(data);
            }
            else if (checkInternet())
            {
                AccountData account = new AccountData() { goal = mode, username = username, birthdate = dateTime.ToString("yyyy/MM/dd"), currentweight = weightValue, email = email, gender = gender, height = heightValue, password = password, wishweight = wishWeightValue, isCheckOnly = false };
                HttpClient httpClient = new HttpClient();
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                var request = await httpClient.PostAsJsonAsync("https://2ahcf.localtonet.com/capp/api/register/", account);
                var respone = JsonConvert.DeserializeObject<ResponseJsonMessage>(await request.Content.ReadAsStringAsync());
                if (!request.IsSuccessStatusCode)
                {
                    await Toast.Make(respone.message, CommunityToolkit.Maui.Core.ToastDuration.Short).Show(cancellationTokenSource.Token);
                }
                else
                {
                    await Toast.Make(respone.message, CommunityToolkit.Maui.Core.ToastDuration.Short).Show(cancellationTokenSource.Token);
                }
            }
        }
    }
}

