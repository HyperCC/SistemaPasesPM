using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Auxiliares
{
    /// <summary>
    /// Clase generadora de password
    /// </summary>
    public static class PasswordGenerator
    {
        private const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
        private const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NUMBERS = "1234567890";
        private const string SPECIALS = @"!@£$%^&*()#€";

        /// <summary>
        /// Generador de claves
        /// </summary>
        /// <param name="useLowercase">uso de minusculas</param>
        /// <param name="useUppercase">uso de mayusculas</param>
        /// <param name="useNumbers">uso de numeros</param>
        /// <param name="useSpecial">uso de caracteres especiales</param>
        /// <param name="passwordSize">largo en caracteres del password</param>
        /// <returns>password generado</returns>
        public static string GeneratePassword(bool useLowercase,
            bool useUppercase,
            bool useNumbers,
            bool useSpecial,
            int passwordSize)
        {
            // variables para generar la clave
            char[] _password = new char[passwordSize];
            string charSet = ""; // Initialise to blank
            Random _random = new Random();

            // Build up the character set to choose from
            if (useLowercase) charSet += LOWER_CASE;
            if (useUppercase) charSet += UPPER_CAES;
            if (useNumbers) charSet += NUMBERS;
            if (useSpecial) charSet += SPECIALS;

            // primer y ultimo caracter con letras
            string caracterInicial = LOWER_CASE + UPPER_CAES;
            _password[0] = caracterInicial[_random.Next(caracterInicial.Length - 1)];
            _password[passwordSize - 1] = caracterInicial[_random.Next(caracterInicial.Length - 1)];

            // seleccion random del resto de caracteres
            for (int counter = 1; counter < passwordSize - 1; counter++)
                _password[counter] = charSet[_random.Next(charSet.Length - 1)];

            return String.Join(null, _password);
        }
    }
}