using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication2.Context;
using AvaloniaApplication2.Entities;
using MsBox.Avalonia;

namespace AvaloniaApplication2;

public partial class AuthorizeWindow : Window
{
    private int CounterAuth = 0;
    
    public AuthorizeWindow()
    {
        InitializeComponent();
    }

    private async void ButtonA_OnClick(object? sender, RoutedEventArgs e)
    {
        using (var context = new MyDbContext())
        {
            var user = context.Users.FirstOrDefault(x => x.Login == LoginBox.Text);

            if (user == null)
            {
                await MessageBoxManager
                    .GetMessageBoxStandard("Ошибка", "Неверный логин или пароль")
                    .ShowAsync();
                return;
            }

            if (user.Blocked == true)
            {
                await MessageBoxManager
                    .GetMessageBoxStandard("Ошибка", "Ваша учетная запись заблокирована")
                    .ShowAsync();
                return;
            }

            if (user.Password != PasswordBox.Text)
            {
                await MessageBoxManager
                    .GetMessageBoxStandard("Ошибка", "Неверный логин или пароль")
                    .ShowAsync();
                
                CounterAuth++;
                if (CounterAuth >= 3)
                {
                    user.Blocked = true;
                    context.Users.Update(user);
                    context.SaveChanges();
                }
                return;
            }
            
            if (user.Firstlogin == true)
            {
                await MessageBoxManager
                    .GetMessageBoxStandard("Первая авторизация", "Вам необходимо сменить пароль")
                    .ShowAsync();
                
                var changePasswordWindow = new ChangePasswordWindow(user);
                changePasswordWindow.Show();
                return;
            }
            
            var aut = new MainWindow();
            aut.Show();

            await MessageBoxManager
                .GetMessageBoxStandard("Успех", "Успешная авторизация")
                .ShowAsync();

            this.Close();
        }
    }
}
