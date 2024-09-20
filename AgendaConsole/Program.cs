using AgendaConsole.Models;
using AgendaConsole.Services;

namespace AgendaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Agenda> lista = new List<Agenda>();
            
            AgendaVisual.Menu();

            Console.Write("Selecione a opção desejada: ");
            int item = int.Parse(Console.ReadLine());
            
            do
            {
                switch (item)
                {
                    case 1:
                        AgendaService.AdicionaCompromisso(lista);
                        break;
                    case 2:
                        AgendaService.ConsultaAgenda(lista);
                        break;
                    case 3:
                        AgendaService.ConsultaAgendaPorData(lista);
                        break;
                    case 4:
                        AgendaService.AtualizaCompromisso(lista);
                        break;
                    case 5:
                        AgendaService.DeletaCompromisso(lista);
                        break;
                    case 8:
                        AgendaService.SalvarCompromisso(lista);
                        break;
                    case 9:
                        AgendaService.ConsultaCompromisso();
                        break;
                }
                Console.Clear();
                AgendaVisual.Menu();
                Console.Write("Selecione a opção desejada: ");
                item = int.Parse(Console.ReadLine());

            } while (item != 0);

            Console.WriteLine("Obrigado por usar nosso sistema!");

            Console.WriteLine("COMPROMISSOS MARCADOS: ");
            AgendaService.ConsultaCompromisso();
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}