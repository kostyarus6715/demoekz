using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace FIOValidator;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private async void ButtonA_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetFromJsonAsync<ResponseModel>("http://localhost:4444/TransferSimulator");

                FieldA.Text = response?.FullName;
            }
        }
        catch (Exception ex)
        {
            FieldA.Text = $"Ошибка: {ex.Message}";
        }
    }

    private class ResponseModel
    {
        public string FullName { get; set; }
        public bool WithAdditionalSymbols { get; set; }
    }

    private void ButtonB_Click(object? sender, RoutedEventArgs e)
    {
        FieldB.Text = CheckValidity(FieldA.Text) ? "Валидно" : "Невалидно";
    }

    private bool CheckValidity(string text)
    {
        Regex regex = new Regex(@"[^\p{L}\d\s]");
        return !regex.IsMatch(text);
    }
}