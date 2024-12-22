using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Examen_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Examen_Test;

public partial class OkkoRedacta : Window
{
    Product ProducT;
    List<Active> actives = new List<Active>();
    List<Manufacturer> manufacture = new List<Manufacturer>();
    public OkkoRedacta()
    {
        InitializeComponent();
        ProducT = new Product();
        LoangComboBox();
    }
    public OkkoRedacta(Product product)
    {
        InitializeComponent();
        ProducT = product;
        OkoRedacta.DataContext = ProducT;

        LoangComboBox();
    }
    public void LoangComboBox()
    {
        if (ProducT.ProductId != 0)
        {
            actives = Helper.DataBase.Actives.ToList();
            BoxActives.ItemsSource = actives.OrderByDescending(x => x.ActiveId == ProducT.IsActive);
            BoxActives.SelectedIndex = 0;

            manufacture = Helper.DataBase.Manufacturers.ToList();
            BoxManufacturer.ItemsSource = manufacture.OrderByDescending(x => x.ManufacturerId == ProducT.ManufacturerId);
            BoxManufacturer.SelectedIndex = 0;
        }
        else
        {
            actives = Helper.DataBase.Actives.ToList();
            actives.Add(new Active() { Name = "Выбрать Активность" });
            BoxActives.ItemsSource = actives.OrderByDescending(x => x.Name == "Выбрать Активность");
            BoxActives.SelectedIndex = 0;

            manufacture = Helper.DataBase.Manufacturers.ToList();
            manufacture.Add(new Manufacturer() { Name = "Выбрать Производителя" });
            BoxManufacturer.ItemsSource = manufacture.OrderByDescending(x => x.Name == "Выбрать Производителя");
            BoxManufacturer.SelectedIndex = 0;
        }
    }
    private void Button_Click_Save(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (ProducT.ProductId != 0)
        {
            // Обновляем активность если мы не меняли ее ранее
            if (BoxActives.SelectedItem is Active fghfgh)
            {
                ProducT.IsActive = fghfgh.ActiveId; // Используем фактический ActiveId
            }
            // Обновляем Производителя если мы не меняли его ранее
            if (BoxManufacturer.SelectedItem is Manufacturer manufactur) 
            {
                ProducT.ManufacturerId = manufactur.ManufacturerId;
            }
            Helper.DataBase.Products.Update(ProducT);
        }
        else 
        {
            ProducT.Description = description_pole.Text;
            ProducT.Title = Title_Pole.Text;
            ProducT.Cost = Convert.ToInt32(Cost_Pole.Text);
            ProducT.IsActive = BoxActives.SelectedIndex;
            ProducT.ManufacturerId = BoxManufacturer.SelectedIndex;
            Helper.DataBase.Products.Add(ProducT);
        }
        Helper.DataBase.SaveChanges();
        Close();
    }
    private void Button_Click_Otmena(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close();
    }
}