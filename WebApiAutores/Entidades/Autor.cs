﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiAutores.Validaciones;

namespace WebApiAutores.Entidades
{
    public class Autor : IValidatableObject
    {
        public int Id { get; set; } 
        [Required(ErrorMessage = "el campo {0} es requerido")]
        [StringLength(120, ErrorMessage = "el campo {0} no debe de tener mas de {1} caracteres")]
        //[PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        //[Range(18, 120)]
        //[NotMapped]
        //public int Edad { get; set; }
     
        //public int menor { get; set; }
        //public int mayor { get; set; }

        public List<Libro> Libros { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe de ser mayuscula",
                        new string[] { nameof(Nombre) });

                }
            }

            //if (menor > mayor)
            //{
            //    yield return new ValidationResult("Este valor no puede ser mas grande que el campo mayor", new string[] { nameof(menor) });
            //}
        }
    }
}
