using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExercicioConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            string caminho;                 //Declaração de Variáveis
            List<string> usuarios;

            
            Console.WriteLine("Insira o caminho do arquivo");       //Leitura de parâmetros
            caminho = Console.ReadLine();

            Console.WriteLine("\nUsuários");
            Console.WriteLine("--------------------------------------");    //Processamento do arquivo e listagem de usuários
            usuarios = new List<string>();

            try
            {
                StreamReader ficheiro = new StreamReader(caminho);
                while (ficheiro.EndOfStream == false)
                {
                    usuarios.Add(ficheiro.ReadLine());
                }
                ficheiro.Dispose();

            }catch (Exception) {}
            

            foreach (var item in usuarios)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\nRequisitando dados da API...");
            Console.WriteLine("------------------------------------------");        //Requisição da api e registro de logs

            foreach (var item in usuarios)
            {
                Console.WriteLine("Usuário: " + item);
                Console.WriteLine("URL: https://api.bitbucket.org/2.0/users/" + item);

                Console.Write("Resultado da requisição: ");
                HttpClient cliente = new HttpClient { BaseAddress = new Uri("https://api.bitbucket.org/2.0/users/") };
                var response = await cliente.GetAsync(item);
                var content = await response.Content.ReadAsStringAsync();
                Console.Write(content+"\n\n");
                
                StreamWriter ficheiroLog = new StreamWriter(Application.StartupPath+"\\logs.txt", true, Encoding.Default);
                ficheiroLog.WriteLine(content);
                ficheiroLog.Dispose();
            }
            //Console.ReadKey();
        }
    }
}
