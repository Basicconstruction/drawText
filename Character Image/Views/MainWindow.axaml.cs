using System;
using System.Drawing;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Character_Image.Models;
using Character_Image.Utils.ImageUtils.micro;
using Character_Image.ViewModels;
using SixLabors.ImageSharp;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using Color = SixLabors.ImageSharp.Color;

namespace Character_Image.Views;

public partial class MainWindow : Window
{
    private string resources = "C:\\Users\\betha\\RiderProjects\\Character Image\\Character Image\\Resources\\";
    public MainWindowViewModel Data => (MainWindowViewModel)DataContext!;
    public MainWindow()
    {
        InitializeComponent();
        DisplayImage.Source = new Bitmap($"{resources}girl.png");
    }
    

    private async void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var originalPath = $"{resources}girl-copy-light.png";
        var originalImage = System.Drawing.Image.FromFile(originalPath);
        var scale = ImageScale.ResizeImage(originalImage, 10240, 10240);
        scale.Save($"{resources}scale.png");
        var result = ImageTransform.Gray($"{resources}scale.png");
        await result.SaveAsPngAsync($"{resources}girlgray.png");
        Data.DisplayImage = new Bitmap($"{resources}girlgray.png");
        originalImage.Dispose();
    }

    private void Button_OnClick2(object? sender, RoutedEventArgs e)
    {
        var path = $"{resources}girlgray.png";
        var originalImage = System.Drawing.Image.FromFile(path);
        var images = ImageScale.Part(originalImage, 20, 20);
        var i = 0;
        foreach (var image in images)
        {
            i++;
            image.Save($"{resources}\\tmp\\part{i}.png");
            image.Dispose();
        }
        originalImage.Dispose();

        // var part = ImageScale.CropImage(originalImage, 0, 0, 512, 512);
        // part.Save($"{resources}part1.png");
        // Data.DisplayImage = new Bitmap($"{resources}part1.png");
        // originalImage.Dispose();
        // Console.WriteLine(Data.DisplayImage);
    }

    private void Button_OnClick3(object? sender, RoutedEventArgs e)
    {
        Data.DisplayImage = new Bitmap($"{resources}girl-copy-light.png");
    }

    private void Button_OnClick4(object? sender, RoutedEventArgs e)
    {
        Data.DisplayImage = new Bitmap($"{resources}girlgray.png");
    }

    private void Button_OnClick5(object? sender, RoutedEventArgs e)
    {
        Data.DisplayImage = new Bitmap($"{resources}part1.png");
    }


    private void DrawString_OnClick(object? sender, RoutedEventArgs e)
    {
        var path = $"{resources}part1.png";
        var c = System.Drawing.Color.FromArgb(255,120,120,120);
        var image = System.Drawing.Image.FromFile(path);
        using var graphics = Graphics.FromImage(image);
        graphics.DrawString(Data.DrawText,
            new Font(new FontFamily("微软雅黑"),Data.FontSize,System.Drawing.FontStyle.Regular),
            new SolidBrush(c),0f,0f
        );
        image.Save($"{resources}part1-draw-test.png");
        Data.DisplayImage = new Bitmap($"{resources}part1-draw-test.png");
    }

    private void Drawer_OnClick(object? sender, RoutedEventArgs e)
    {
        var config = new GenerateConfig()
        {
            Zoom = 10,
            X = 20,
            Y = 20,
            FontSize = Data.FontSize,
            DrawText = Data.DrawText
        };
        Console.WriteLine(config);
        var drawer = new QuickCharacterDrawer();
        // var onePath = $"{resources}part37.png";
        // var result = drawer.DrawOne(onePath,config);
        // result.Save($"{resources}draw-result.png");
        // Data.DisplayImage = new Bitmap($"{resources}draw-result.png");
        // Console.WriteLine("结束");
        // var onePath = $"{resources}girlgray.png";
        var onePath = $"{resources}resized.jpg";
        var result = drawer.DrawOne(onePath,config);
        result.Save($"{resources}draw-result.png");
        Data.DisplayImage = new Bitmap($"{resources}draw-result.png");
        Console.WriteLine("结束");
    }
}