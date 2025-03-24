using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia;

namespace AvaloniaApplication2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void OnLogoutClick(object? sender, RoutedEventArgs e)
    {
        var result = await MessageBoxManager
            .GetMessageBoxStandard("Выход", "Вы действительно хотите выйти?", MsBox.Avalonia.Enums.ButtonEnum.YesNo)
            .ShowAsync();

        if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
        {
            this.Close();
            var authWindow = new AuthorizeWindow();
            authWindow.Show();
        }
    }
}