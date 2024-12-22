using Avalonia.Controls;
using Examen_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Examen_Test
{
    public partial class MainWindow : Window
    {
        List<Product> products = new List<Product>();
        List<Manufacturer> manufacturers = new List<Manufacturer>();
        public MainWindow()
        {
            InitializeComponent();
            BoxOneCost.SelectionChanged += BoxOneCost_SelectionChanged;
            BoxTwoManufacturer.SelectionChanged += BoxTwoManufacturer_SelectionChanged;

            LoangListBoxManufacturer();
            Loang();
        }
        public void Loang() 
        {
            products = Helper.DataBase.Products.ToList();

            if (BoxOneCost.SelectedIndex == 1)
            {
                products = products.OrderBy(x => x.Cost).ToList();
            }
            else if (BoxOneCost.SelectedIndex == 2)
            {
                products = products.OrderByDescending(x => x.Cost).ToList();
            }

            if (BoxTwoManufacturer.SelectedIndex != 0) 
            {
                var NamManufacturer = (Manufacturer)BoxTwoManufacturer.SelectedItem;
                products = products.Where(x => x.ManufacturerId == NamManufacturer.ManufacturerId).ToList();
            }

            var TextVoid = (Search.Text ?? "").Split(' ');
            products = products.Where(c => TextVoid.All(neww => c.Title.Contains(neww))).ToList();

            InBase.Text = Convert.ToString(Helper.DataBase.Products.Select(a => a.ManufacturerId).Count());
            ListboxIsBasePokaz.Text = products.Count().ToString();

            ListBox_Examen_Test.ItemsSource = products;
        }
        public void LoangListBoxManufacturer()
        {
            manufacturers = Helper.DataBase.Manufacturers.ToList();
            manufacturers.Add(new Manufacturer() { Name = "Производители" });
            BoxTwoManufacturer.ItemsSource = manufacturers.OrderByDescending(z => z.Name == "Производители").ToList();
            BoxTwoManufacturer.SelectedIndex = 0;
        }

        private void Button_Click_Delete(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var DeleteTovar = ListBox_Examen_Test.SelectedItem as Product;

            //Берем связанные данные для удаления в связанной таблице
            var DeleteTovar_In_ProductSales = Helper.DataBase.ProductSales.Where(p => p.ProductId == DeleteTovar.ProductId).ToList();
            // Удаляем связанные записи
            foreach (var Sale in DeleteTovar_In_ProductSales)
            {
                Helper.DataBase.ProductSales.Remove(Sale);
            }

            Helper.DataBase.Products.Remove(DeleteTovar);
            Helper.DataBase.SaveChanges();
            Loang();
        }
        private void Button_Click_Dobavit(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            OkkoRedacta okkoRedacta = new OkkoRedacta();
            okkoRedacta.ShowDialog(this);
            okkoRedacta.Closed += (a, b) => Loang();
        }
        private void ListBox_DoubleTapped_Redact(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            var redact = ListBox_Examen_Test.SelectedItem as Product;
            OkkoRedacta okkoRedacta = new OkkoRedacta(redact);
            okkoRedacta.ShowDialog(this);
            okkoRedacta.Closed += (a, b) => Loang();
        }
        private void TextBox_TextChanged(object? sender, Avalonia.Controls.TextChangedEventArgs e) => Loang();
        private void BoxTwoManufacturer_SelectionChanged(object? sender, SelectionChangedEventArgs e) => Loang();
        private void BoxOneCost_SelectionChanged(object? sender, SelectionChangedEventArgs e) => Loang();
    }
}