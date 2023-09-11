//-------------------------------------------------------------------------
// <copyright file="Program.cs" company="Universidad Católica del Uruguay">
// Copyright (c) Programación II. Derechos reservados.
// </copyright>
//-------------------------------------------------------------------------


// (SRP): La clase ConsolePrinter tiene la responsabilidad única de imprimir en la consola, separando esta preocupación de la lógica principal del programa.
// Inyección de Dependencias: La clase Program utiliza una interfaz IPrinter en lugar de una implementación concreta.
// (OCP): El código está diseñado para ser abierto a la extensión (se puede agregar nuevas implementaciones) pero cerrado a la modificación (no es necesario cambiar la logica principal para imprimir de manera diferente).


using System;
using System.Collections.Generic;
using Full_GRASP_And_SOLID.Library;

namespace Full_GRASP_And_SOLID
{
    // Interfaz para imprimir cualquier objeto que pueda ser impreso.
    public interface IPrinter
    {
        void Print(object printable);
    }

    // Clase que implementa la interfaz IPrinter para imprimir en la consola.
    public class ConsolePrinter : IPrinter
    {
        public void Print(object printable)
        {
            Console.WriteLine(printable);
        }
    }

    public class Program
    {
        private static List<Product> productCatalog = new List<Product>();
        private static List<Equipment> equipmentCatalog = new List<Equipment>();

        public static void Main(string[] args)
        {
            PopulateCatalogs();

            Recipe recipe = new Recipe();
            recipe.FinalProduct = GetProduct("Café con leche");
            recipe.AddStep(new Step(GetProduct("Café"), 100, GetEquipment("Cafetera"), 120));
            recipe.AddStep(new Step(GetProduct("Leche"), 200, GetEquipment("Hervidor"), 60));

            //se usa la clase ConsolePrinter para imprimir la receta en la consola.
            IPrinter printer = new ConsolePrinter();
            printer.Print($"Receta para {recipe.FinalProduct.Description}:");

            // Itera a través de los pasos e imprimimos cada uno.
            foreach (Step step in recipe.GetSteps())
            {
                printer.Print($"{step.Quantity} de '{step.Input.Description}' " +
                    $"usando '{step.Equipment.Description}' durante {step.Time}");
            }

        }

        private static void PopulateCatalogs()
        {
            AddProductToCatalog("Café", 100);
            AddProductToCatalog("Leche", 200);
            AddProductToCatalog("Café con leche", 300);

            AddEquipmentToCatalog("Cafetera", 1000);
            AddEquipmentToCatalog("Hervidor", 2000);
        }

        private static void AddProductToCatalog(string description, double unitCost)
        {
            productCatalog.Add(new Product(description, unitCost));
        }

        private static void AddEquipmentToCatalog(string description, double hourlyCost)
        {
            equipmentCatalog.Add(new Equipment(description, hourlyCost));
        }

        private static Product GetProduct(string description)
        {
            return productCatalog.Find(product => product.Description == description);
        }

        private static Equipment GetEquipment(string description)
        {
            return equipmentCatalog.Find(equipment => equipment.Description == description);
        }
    }
}
