// Separe las responsabilidades en las clases. La clase Recipe ahora solo representa la receta y almacena sus pasos, 
// mientras que la clase ConsolePrinter se encarga exclusivamente de imprimir en la consola. Esto hace que cada clase tenga una única razón para cambiar.

using System;
using System.Collections.Generic;

namespace Full_GRASP_And_SOLID.Library
{
    // Nueva clase ConsolePrinter para manejar la responsabilidad de imprimir recetas en la consola.
    public class ConsolePrinter
    {
        // Método estático para imprimir una receta.
        public static void PrintRecipe(Recipe recipe)
        {
            // Verifica si la receta es nula para evitar excepciones.
            if (recipe == null)
            {
                Console.WriteLine("Receta no válida.");
                return;
            }

            Console.WriteLine($"Receta de {recipe.FinalProduct.Description}:");

            // Consigue la lista de pasos de la receta.
            List<Step> steps = recipe.GetSteps();

            // Itera sobre los pasos y los imprimimos.
            foreach (Step step in steps)
            {
                Console.WriteLine($"{step.Quantity} de '{step.Input.Description}' " +
                    $"usando '{step.Equipment.Description}' durante {step.Time}");
            }
        }
    }
}
