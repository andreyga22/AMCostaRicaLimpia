using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class BLManejadorEncripcion
    {



        public static string Encrypt(string plainText)
        {
            String textoEncriptado = "";
            char[] array = plainText.ToCharArray();

            foreach (char c in array) {
                textoEncriptado += convertirCharP1(c) + "/";
            }
            textoEncriptado = textoEncriptado.TrimEnd('/');
            String[] arrayConvertido = textoEncriptado.Split('/');

            List<String> cadena;


            foreach (String frase in arrayConvertido) {
                char[] caracteres = frase.ToCharArray();
                cadena = new List<string>();
                foreach (char c in caracteres) {
                   
                    try {
                        int val = Int16.Parse(c.ToString());
                        int valConvertido = operarNumeroEncripcion(val);
                        cadena.Add(valConvertido.ToString());
                }
                catch (Exception e) {
                        String letra = c.ToString();
                        String mayus = letra.ToUpper();
                        String minus = letra.ToLower();

                        if (letra.Equals(mayus))
                            cadena.Add(minus);
                        else
                            cadena.Add(mayus);
                    }
                }
                textoEncriptado= "";
                foreach (String v in cadena) 
                    textoEncriptado += v;
                textoEncriptado += "/";

            }
            textoEncriptado = textoEncriptado.TrimEnd('/');
            return textoEncriptado;
        }

        private static int operarNumeroEncripcion(int num) {
            int n = 0;
            n = 9 - num;
            return n;
        }

        public static string Decrypt(string cipherText)
        {
            String textoDesencriptado = "";

            String[] arrayConvertido = cipherText.Split('/');

            List<String> cadena;


            foreach (String frase in arrayConvertido)
            {
                char[] caracteres = frase.ToCharArray();
                cadena = new List<string>();
                foreach (char c in caracteres)
                {

                    try
                    {
                        int val = Int16.Parse(c.ToString());
                        int valConvertido = operarNumeroEncripcion(val);
                        cadena.Add(valConvertido.ToString());
                    }
                    catch (Exception e)
                    {
                        String letra = c.ToString();
                        String mayus = letra.ToUpper();
                        String minus = letra.ToLower();

                        if (letra.Equals(mayus))
                            cadena.Add(minus);
                        else
                            cadena.Add(mayus);

                    }
                }
                foreach (String v in cadena)
                    textoDesencriptado += v;
                textoDesencriptado += "/";
                
            }

            textoDesencriptado = textoDesencriptado.TrimEnd('/');
            String[] array = textoDesencriptado.Split('/');
            String textoDescifrado = "";

            foreach (String c in array)
            {
                textoDescifrado += convertirEncripcion(c);
            }

            return textoDescifrado;
        }

        private static String convertirCharP1(char c)
        {
            String val = "-";

            switch (c) {

                case '1':
                    val = "ZA71";
                    break;
                case '2':
                    val = "p0h7";
                    break;
                case '3':
                    val = "E93g";
                    break;
                case '4':
                    val = "bF23";
                    break;
                case '5':
                    val = "5H8f";
                    break;
                case '6':
                    val = "h4sS";
                    break;
                case '7':
                    val = "97HJ";
                    break;
                case '8':
                    val = "F5hj";
                    break;
                case '9':
                    val = "MT57";
                    break;
                case '0':
                    val = "226F";
                    break;
            }
            return val;
        }

            private static char convertirEncripcion(String c)
            {
                char val = '-';

                switch (c)
                {

                    case "ZA71":
                        val = '1';
                        break;
                    case "p0h7":
                        val = '2';
                        break;
                    case "E93g":
                        val = '3';
                        break;
                    case "": 
                        val = '4';
                        break;
                    case "bF23":
                        val = '5';
                        break;
                    case "5H8f":
                        val = '6';
                        break;
                    case "h4sS":
                        val = '7';
                        break;
                    case "97HJ":
                        val = '8';
                        break;
                    case "MT57":
                        val = '9';
                        break;
                    case "226F":
                        val = '0';
                        break;

                }
                return val;
            }
    


    }
}
