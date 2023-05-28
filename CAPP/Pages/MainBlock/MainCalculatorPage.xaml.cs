using CAPP.Controls;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;

namespace CAPP.Pages.MainBlock;

public partial class MainCalculatorPage : ContentPage
{
    ProductDatabase productDatabase;
    List<ProductData> calcList = new List<ProductData>();
    double kcal = 0;
    double fat = 0;
    double carb = 0;
    double protein = 0;
    bool firstTime = true;
    bool isSearching = false;

    public MainCalculatorPage()
	{
		InitializeComponent();
        productDatabase = new ProductDatabase();
    }
    
    private async void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (((Entry)sender).Text.Length != 0)
        {
            List<ProductData> products = await productDatabase.FindProducts(((Entry)sender).Text);
            List<RecipeData> recipes = await productDatabase.FindRecipes(((Entry)sender).Text);

            foreach (RecipeData recipe in recipes)
            {
                products.Add(new ProductData
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Fat = recipe.Fat,
                    Carb = recipe.Carb,
                    Kcal = recipe.Kcal,
                    Protein = recipe.Protein,
                    Type = "Recipe"
                });
            }

            SearchCollection.ItemsSource = products;
        }
        else
        {
            SearchCollection.ItemsSource = new List<ProductData>();
        }
    }

    private void GoToActivityPage(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("///Activity");
    }

    private void GoToStatsPage(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("///Stats");
    }

    private void WeightChanged(object sender, TextChangedEventArgs e)
    {
        kcal = 0;
        fat = 0;
        carb = 0;
        protein = 0;

        foreach (var i in calcList)
        {
            kcal += i.Kcal * i.Weight / 100;
            fat += i.Fat * i.Weight / 100;
            carb += i.Carb * i.Weight / 100;
            protein += i.Protein * i.Weight / 100;
        }

        Kcal.Text = separateNumber(Math.Round(kcal,1)) + " ккал";
        Fat.Text = truncateValue(Math.Round(fat,1));
        Carb.Text = truncateValue(Math.Round(carb,1));
        Protein.Text = truncateValue(Math.Round(protein,1));
    }

    private string getUnit(double value)
    {
        switch (value)
        {
            case > 1000:
                return " кг";
            default:
                return " г";
        }
    }

    private string separateNumber(double num)
    {
        string number = Math.Truncate(num).ToString();

        switch (num)
        {
            case > 999999:
                return number.Insert(4, " ");
            case > 99999:
                return number.Insert(3, " ");
            case > 9999:
                return number.Insert(2, " ");
            case > 999:
                return number.Insert(1, " ");
            default:
                return number;
        }

    }

    private string truncateValue(double value)
    {
        if (value > 10000)
        {
            return Math.Round(value / Math.Pow(10, Math.Truncate(value).ToString().Length - 2), 2).ToString() + getUnit(value);
        }
        else if (value > 1000)
        {
            return Math.Round(value / Math.Pow(10, Math.Truncate(value).ToString().Length - 1), 2).ToString() + getUnit(value);
        }
        else
        {
            return value.ToString() + getUnit(value);
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        isSearching = true;
        ClearButton.Text = "Закрыть";
        ButtonStack.Children.RemoveAt(0);
        ClearButton.WidthRequest = 330;
        SearchCollection.ItemsSource = null;
        Flexlayout.Clear();

        Entry entry = new Entry()
        {
            HeightRequest = 45,
            FontFamily = "",
            Placeholder = "Введите название продукта"
        };

        entry.TextChanged += Entry_TextChanged;

        Flexlayout.Add(new Border()
        {
            StrokeShape = new RoundRectangle()
            {
                CornerRadius = 10
            },
            Background = new SolidColorBrush(Color.FromArgb("#F4F2F2")),
            Content = entry,
            WidthRequest = 330,
            HeightRequest = 45
        });
        
        if (!firstTime)
        {
            SearchCollection.SelectionChanged += SearchCollection_SelectionChanged;
        }
        else
        {
            firstTime = false;
        }

        SearchCollection.ItemTemplate = new DataTemplate(() =>
        {

            Label label = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontFamily = ""
            };

            label.SetBinding(Label.TextProperty, "Name");

            return new Border
            {
                Content = label,
                StrokeShape = new RoundRectangle() { CornerRadius = 10 },
                Background = new SolidColorBrush(Color.FromArgb("#F4F2F2")),
                Padding = 5
            };

        });

    }

    private void SearchCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        isSearching = false;
        ClearButton.Text = "Очистить";
        ClearButton.WidthRequest = 160;
        ButtonStack.Children.Insert(0, AddButton);
        SearchCollection.SelectionChanged -= SearchCollection_SelectionChanged;
        calcList.Add((ProductData)e.CurrentSelection[0]);

        Flexlayout.Clear();

        Flexlayout.Add(new Label()
        {
            Text = "Добавьте продукты",
            FontFamily = "",
            FontAttributes = FontAttributes.Bold,
            FontSize = 20.0
        });

        Flexlayout.Add(new Border()
        {
            WidthRequest = 70,
            HeightRequest = 45,
            StrokeShape = new RoundRectangle() { CornerRadius = 10 },
            StrokeThickness = 0,
            Background = new SolidColorBrush(Color.FromArgb("#F83D7F")),
            Content = new Microsoft.Maui.Controls.Image()
            {
                Source = ImageSource.FromFile("plusik"),
                WidthRequest = 23,
                HeightRequest = 23
            }
        });

        SearchCollection.ItemTemplate = new DataTemplate(() =>
        {

            Label name = new Label();
            name.SetBinding(Label.TextProperty, "Name");
            name.LineBreakMode = LineBreakMode.TailTruncation;
            name.FontAttributes = FontAttributes.Bold;
            name.FontFamily = "";
            name.Margin = new Thickness(0, 0, 0, 5);

            Label kcal = new Label();
            kcal.SetBinding(Label.TextProperty, "Kcal");
            kcal.FontFamily = "";
            kcal.TextColor = Color.FromArgb("#F83D7F");
            kcal.HorizontalOptions = LayoutOptions.Center;

            Label protein = new Label();
            protein.SetBinding(Label.TextProperty, "Protein");
            protein.FontFamily = "";
            protein.TextColor = Color.FromArgb("#F83D7F");
            protein.HorizontalOptions = LayoutOptions.Center;

            Label fat = new Label();
            fat.SetBinding(Label.TextProperty, "Fat");
            fat.FontFamily = "";
            fat.TextColor = Color.FromArgb("#F83D7F");
            fat.HorizontalOptions = LayoutOptions.Center;

            Label carb = new Label();
            carb.SetBinding(Label.TextProperty, "Carb");
            carb.FontFamily = "";
            carb.TextColor = Color.FromArgb("#F83D7F");
            carb.HorizontalOptions = LayoutOptions.Center;

            Entry weight = new Entry();
            weight.SetBinding(Entry.TextProperty, "Weight");
            weight.FontFamily = "";
            weight.MaxLength = 5;
            weight.Keyboard = Keyboard.Numeric;
            weight.TextColor = Color.FromArgb("#F83D7F");
            weight.HorizontalOptions = LayoutOptions.Center;
            weight.Margin = new Thickness(0,-10,0,0);
            weight.TextChanged += WeightChanged;


            Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition(GridLength.Auto),
                    new RowDefinition(GridLength.Auto)
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(50),
                    new ColumnDefinition(50),
                    new ColumnDefinition(50),
                    new ColumnDefinition(50),
                    new ColumnDefinition(50)
                }
            };

            grid.Add(new Label
            {
                Text = "К(ккал)",
                FontFamily = "",
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            }, 0, 0);

            grid.Add(new Label
            {
                Text = "Б(г)",
                FontFamily = "",
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            }, 1, 0);

            grid.Add(new Label
            {
                Text = "Ж(г)",
                FontFamily = "",
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            }, 2, 0);

            grid.Add(new Label
            {
                Text = "У(г)",
                FontFamily = "",
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            }, 3, 0);

            grid.Add(new Label
            {
                Text = "Вес(г)",
                FontFamily = "",
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            }, 4, 0);

            grid.Add(kcal, 0, 1);

            grid.Add(protein, 1, 1);

            grid.Add(fat, 2, 1);

            grid.Add(carb, 3, 1);

            grid.Add(weight, 4, 1);

            //VerticalStackLayout vst1 = new VerticalStackLayout
            //{
            //    Children =
            //    {
            //        new Label
            //        {
            //            Text = "К",
            //            FontFamily = "",
            //            FontAttributes = FontAttributes.Bold,
            //            HorizontalOptions = LayoutOptions.Center
            //        },
            //        kcal
            //    },
            //    HorizontalOptions = LayoutOptions.Center
            //};
            //VerticalStackLayout vst2 = new VerticalStackLayout()
            //{
            //    Children =
            //    {
            //        new Label
            //        {
            //            Text = "Б",
            //            FontFamily = "",
            //            FontAttributes = FontAttributes.Bold,
            //            HorizontalOptions = LayoutOptions.Center
            //        },
            //        protein
            //    },
            //    HorizontalOptions = LayoutOptions.Center
            //};
            //VerticalStackLayout vst3 = new VerticalStackLayout()
            //{
            //    Children =
            //    {
            //        new Label
            //        {
            //            Text = "Ж",
            //            FontFamily = "",
            //            FontAttributes = FontAttributes.Bold,
            //            HorizontalOptions = LayoutOptions.Center
            //        },
            //        fat
            //    },
            //    HorizontalOptions = LayoutOptions.Center
            //};
            //VerticalStackLayout vst4 = new VerticalStackLayout()
            //{
            //    Children =
            //    {
            //        new Label
            //        {
            //            Text = "У",
            //            FontFamily = "",
            //            FontAttributes = FontAttributes.Bold,
            //            HorizontalOptions = LayoutOptions.Center
            //        },
            //        carb
            //    },
            //    HorizontalOptions = LayoutOptions.Center
            //};
            //VerticalStackLayout vst5 = new VerticalStackLayout()
            //{
            //    Children =
            //    {
            //        new Label
            //        {
            //            Text = "Вес",
            //            FontFamily = "",
            //            FontAttributes = FontAttributes.Bold,
            //            HorizontalOptions = LayoutOptions.Center
            //        },
            //        weight
            //    },
            //    HorizontalOptions = LayoutOptions.Center
            //};

            //HorizontalStackLayout hst = new HorizontalStackLayout
            //{
            //    Children = { vst1, vst2, vst3, vst4, vst5 },
            //    Spacing = 30
            //};

            VerticalStackLayout vst = new VerticalStackLayout
            {
                Children = { name, grid },
                HeightRequest = 70
            };

            return new Border
            {
                Content = vst,
                StrokeShape = new RoundRectangle() { CornerRadius = 10 },
                Background = new SolidColorBrush(Color.FromArgb("#F4F2F2")),
                Padding = 10
            };

        });

        var gestureRecognizer = new TapGestureRecognizer() { NumberOfTapsRequired = 1 };
        gestureRecognizer.Tapped += TapGestureRecognizer_Tapped;

        ((Border)Flexlayout[1]).GestureRecognizers.Add(gestureRecognizer);

        SearchCollection.ItemsSource = calcList;

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (!isSearching)
        {
            calcList.Clear();
            Kcal.Text = "0 ккал";
            Protein.Text = "0 г";
            Carb.Text = "0 г";
            Fat.Text = "0 г";
            SearchCollection.ItemsSource = null;
        }
        else
        {
            isSearching = false;
            ClearButton.Text = "Очистить";
            ClearButton.WidthRequest = 160;
            ButtonStack.Children.Insert(0, AddButton);
            SearchCollection.SelectionChanged -= SearchCollection_SelectionChanged;

            Flexlayout.Clear();

            Flexlayout.Add(new Label()
            {
                Text = "Добавьте продукты",
                FontFamily = "",
                FontAttributes = FontAttributes.Bold,
                FontSize = 20.0
            });

            Flexlayout.Add(new Border()
            {
                WidthRequest = 70,
                HeightRequest = 45,
                StrokeShape = new RoundRectangle() { CornerRadius = 10 },
                StrokeThickness = 0,
                Background = new SolidColorBrush(Color.FromArgb("#F83D7F")),
                Content = new Microsoft.Maui.Controls.Image()
                {
                    Source = ImageSource.FromFile("plusik"),
                    WidthRequest = 23,
                    HeightRequest = 23
                }
            });

            SearchCollection.ItemTemplate = new DataTemplate(() =>
            {

                Label name = new Label();
                name.SetBinding(Label.TextProperty, "Name");
                name.LineBreakMode = LineBreakMode.TailTruncation;
                name.FontAttributes = FontAttributes.Bold;
                name.FontFamily = "";
                name.Margin = new Thickness(0, 0, 0, 5);

                Label kcal = new Label();
                kcal.SetBinding(Label.TextProperty, "Kcal");
                kcal.FontFamily = "";
                kcal.TextColor = Color.FromArgb("#F83D7F");
                kcal.HorizontalOptions = LayoutOptions.Center;

                Label protein = new Label();
                protein.SetBinding(Label.TextProperty, "Protein");
                protein.FontFamily = "";
                protein.TextColor = Color.FromArgb("#F83D7F");
                protein.HorizontalOptions = LayoutOptions.Center;

                Label fat = new Label();
                fat.SetBinding(Label.TextProperty, "Fat");
                fat.FontFamily = "";
                fat.TextColor = Color.FromArgb("#F83D7F");
                fat.HorizontalOptions = LayoutOptions.Center;

                Label carb = new Label();
                carb.SetBinding(Label.TextProperty, "Carb");
                carb.FontFamily = "";
                carb.TextColor = Color.FromArgb("#F83D7F");
                carb.HorizontalOptions = LayoutOptions.Center;

                Entry weight = new Entry();
                weight.SetBinding(Entry.TextProperty, "Weight");
                weight.FontFamily = "";
                weight.TextColor = Color.FromArgb("#F83D7F");
                weight.HorizontalOptions = LayoutOptions.Center;
                weight.Margin = new Thickness(0, -10, 0, 0);
                weight.TextChanged += WeightChanged;

                VerticalStackLayout vst1 = new VerticalStackLayout
                {
                    Children =
                    {
                        new Label
                        {
                            Text = "К",
                            FontFamily = "",
                            FontAttributes = FontAttributes.Bold,
                            HorizontalOptions = LayoutOptions.Center
                        },
                        kcal
                    },
                    HorizontalOptions = LayoutOptions.Center
                };
                VerticalStackLayout vst2 = new VerticalStackLayout()
                {
                    Children =
                {
                    new Label
                    {
                        Text = "Б",
                        FontFamily = "",
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.Center
                    },
                    protein
                },
                    HorizontalOptions = LayoutOptions.Center
                };
                VerticalStackLayout vst3 = new VerticalStackLayout()
                {
                    Children =
                    {
                        new Label
                        {
                            Text = "Ж",
                            FontFamily = "",
                            FontAttributes = FontAttributes.Bold,
                            HorizontalOptions = LayoutOptions.Center
                        },
                        fat
                    },
                    HorizontalOptions = LayoutOptions.Center
                };
                VerticalStackLayout vst4 = new VerticalStackLayout()
                {
                    Children =
                {
                    new Label
                    {
                        Text = "У",
                        FontFamily = "",
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.Center
                    },
                    carb
                },
                    HorizontalOptions = LayoutOptions.Center
                };
                VerticalStackLayout vst5 = new VerticalStackLayout()
                {
                    Children =
                {
                    new Label
                    {
                        Text = "Вес",
                        FontFamily = "",
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.Center
                    },
                    weight
                },
                    HorizontalOptions = LayoutOptions.Center
                };

                HorizontalStackLayout hst = new HorizontalStackLayout
                {
                    Children = { vst1, vst2, vst3, vst4, vst5 },
                    Spacing = 30
                };

                VerticalStackLayout vst = new VerticalStackLayout
                {
                    Children = { name, hst },
                    HeightRequest = 70
                };

                return new Border
                {
                    Content = vst,
                    StrokeShape = new RoundRectangle() { CornerRadius = 10 },
                    Background = new SolidColorBrush(Color.FromArgb("#F4F2F2")),
                    Padding = 10
                };

            });

            var gestureRecognizer = new TapGestureRecognizer() { NumberOfTapsRequired = 1 };
            gestureRecognizer.Tapped += TapGestureRecognizer_Tapped;

            ((Border)Flexlayout[1]).GestureRecognizers.Add(gestureRecognizer);

            SearchCollection.ItemsSource = calcList;
        }
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        if (calcList.Exists(x => x.Type == "Recipe"))
        {
            string text = "Нельзя использовать рецепты!";
            await Toast.Make(text, ToastDuration.Short).Show();
            return;
        }
        else if (calcList.Count <= 1)
        {
            string text = "Слишком мало продуктов!";
            await Toast.Make(text, ToastDuration.Short).Show();
            return;
        }

        var popup = new RecipePopup();

        var name = await this.ShowPopupAsync(popup);

        if ( name is not null )
        {
            List<RecipeItem> recipeItems = new List<RecipeItem>();

            double weight = 0;
            double carb = 0;
            double protein = 0;
            double fat = 0;
            double kcal = 0;

            int recipeCount = await productDatabase.GetRecipeCountAsync();

            foreach ( var item in calcList )
            {
                RecipeItem recipeItem = new RecipeItem
                {
                    Weight = item.Weight,
                    Recipe_id = recipeCount + 1,
                    Product_id = item.Id
                };

                weight += item.Weight;
                carb += item.Carb * (item.Weight / 100);
                fat += item.Fat * (item.Weight / 100);
                kcal += item.Kcal * (item.Weight / 100);
                protein += item.Protein * (item.Weight / 100);

                recipeItems.Add(recipeItem);
            }

            double value = 100 / weight;

            RecipeData recipe = new RecipeData
            {
                Carb = Math.Round(carb * value, 2),
                Fat = Math.Round(fat * value, 2),
                Protein = Math.Round(protein * value, 2),
                Kcal = Math.Round(kcal * value, 2),
                Name = name.ToString(),
                User_defined = true,
                Id = recipeCount + 1
            };

            try
            {
                await productDatabase.InsertRecipeAsync(recipe);
                string text = "Рецепт успешно добавлен";
                await Toast.Make(text, ToastDuration.Short).Show();
            }
            catch (SQLite.SQLiteException)
            {
                string text = "Рецепт с таким названием существует";
                await Toast.Make(text, ToastDuration.Short).Show();
                return;
            }

            recipeItems.ForEach(async x => await productDatabase.InsertRecipeItemAsync(x));
        }

    }

    private async void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("///Recipes");
    }
}