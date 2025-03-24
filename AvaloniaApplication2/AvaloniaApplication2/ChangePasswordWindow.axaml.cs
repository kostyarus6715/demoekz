using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication2.Context;
using AvaloniaApplication2.Entities;
using MsBox.Avalonia;

namespace AvaloniaApplication2;

public partial class ChangePasswordWindow : Window
{
    private readonly User _user;

    public ChangePasswordWindow(User user)
    {
        InitializeComponent();
        _user = user ?? throw new ArgumentNullException(nameof(user));
    }

    private async void OnChangePasswordClick(object? sender, RoutedEventArgs e)
    {
        string newPassword = NewPasswordBox.Text;
        string confirmPassword = ConfirmPasswordBox.Text;

        if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
        {
            await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Пароль не может быть пустым").ShowAsync();
            return;
        }

        if (newPassword != confirmPassword)
        {
            await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Пароли не совпадают").ShowAsync();
            return;
        }

        using (var context = new MyDbContext())
        {
            var user = context.Users.FirstOrDefault(x => x.Id == _user.Id);
            if (user != null)
            {
                user.Password = newPassword;
                user.Firstlogin = false;
                context.SaveChanges();

                await MessageBoxManager.GetMessageBoxStandard("Успех", "Пароль успешно изменен").ShowAsync();
                this.Close();
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("Ошибка", "Пользователь не найден").ShowAsync();
            }
        }
    }
}